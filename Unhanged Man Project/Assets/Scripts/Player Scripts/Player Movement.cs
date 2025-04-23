//Created by: charlie
//Edited by: eddie and Carter
using System.Collections;
//using UnityEditor.Rendering;
using UnityEngine;
//using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float coolDownTime;
    [SerializeField] private bool isInvincible;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isGroundedForAnimator; //has a larger radius to update animation quicker
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private bool canWallJump;
    [SerializeField] private float wallSlideSpeed; //0.5f
    [SerializeField] private float wallJumpForce; //2.0f
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip rollClip;
    private AudioSource audioSource;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float groundCheckForAnimatorRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    // player moving audios
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip walkSFX;
    [SerializeField] private AudioClip dashSFX;
    private bool isPlayWalk;
      

    [Header("Wall Detection")]
    public Transform wallCheck;
    public float wallCheckRadius = 0.2f;

    private bool isRight = true;
    private Rigidbody2D rigidBody2D;
    private enum State
    {
        Moving,
        Rolling
    }
    private State state;

    private bool isCoolingDown = false;
    
    private Vector2 rollDirection;
    private float moveInput;

    private int frameCounter;

    Animator anim;

    void Start()
    {
        isInvincible = false;
        state = State.Moving;
        isGrounded = false;
        isGroundedForAnimator = false;
        rigidBody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // audio init
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGroundedForAnimator = Physics2D.OverlapCircle(groundCheck.position, groundCheckForAnimatorRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, groundLayer);
        if (isInvincible)
        {
            rigidBody2D.excludeLayers = LayerMask.GetMask("Enemies");
        }
        else
        {
            rigidBody2D.excludeLayers = LayerMask.GetMask("Nothing");
        }
        switch (state) {
        case State.Moving:
            //Grounded check for animator
            if (isGroundedForAnimator)
            {
                anim.SetBool("Grounded", true);
            }
            else
            {
                anim.SetBool("Grounded", false);
            }

           
            

                //fliping sprite
                if (!PlayerAttack.isAttacking && !isWallSliding) //Can't flip while attacking or wall sliding
            {
                if (moveInput > 0 && !isRight)
                {
                    Vector3 currentScale = transform.localScale; //fetching current scale
                    currentScale.x *= -1; //inversing current scale
                    transform.localScale = currentScale; //reassigning current scale
                    isRight = !isRight; //toggle isRight
                }
                else if (moveInput < 0 && isRight)
                {
                    Vector3 currentScale = transform.localScale;
                    currentScale.x *= -1;
                    transform.localScale = currentScale;
                    isRight = !isRight;
                }
            }
    
            //jump button
            //&& Mathf.Abs(rigidBody2D.linearVelocity.y) < 0.001f
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    anim.SetTrigger("Jump");
                    // play jump sound
                    audioSource.PlayOneShot(jumpSFX);
                    rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    audioSource.PlayOneShot(jumpClip);
                    canWallJump = true;
                    Invoke("ResetWallJump", 0.1f);
                }
                else if (isWallSliding)
                {
                    anim.SetTrigger("Jump");
                    // play jump sound
                    audioSource.PlayOneShot(jumpSFX);
                    WallJump();
                }
            }

            //roll button
            //Add a check for on the ground, right click
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCoolingDown /*&& isGrounded*/)
            {
                //rigidBody2D.AddForce(new Vector2(rollSpeed * moveInput, 0), ForceMode2D.Impulse);
                audioSource.PlayOneShot(rollClip);
                rollSpeed = 30f;
                state = State.Rolling;
                rollDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
                isInvincible = true;
                anim.SetTrigger("Roll");
                StartCoroutine(CoolDown());
            }
            break;
        }   
    }
    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0f && isWallSliding)
        {
            frameCounter++;
        }
        else
            frameCounter = 0;

        switch (state)
        {
            case State.Moving:
                if (isTouchingWall && !isGrounded && rigidBody2D.linearVelocity.y <= 0)
                {
                    isWallSliding = true;
                    anim.SetBool("IsWallSliding", true);
                    anim.SetBool("Grounded", false);
                    PlayerAnimationScript.isMovingX = false;
                    rigidBody2D.linearVelocity = new Vector2(0, rigidBody2D.linearVelocity.y * wallSlideSpeed);
                }
                else
                {
                    isWallSliding = false;
                    anim.SetBool("IsWallSliding", false);
                }
                //movement
                moveInput = Input.GetAxisRaw("Horizontal");
                if (!canWallJump && (!isWallSliding || (isWallSliding && frameCounter >= 3 && ((moveInput > 0 && !isRight) || (moveInput < 0 && isRight)))))
                {
                    if (isGrounded)
                    {
                        rigidBody2D.linearVelocity = new Vector2(moveInput * speed, rigidBody2D.linearVelocityY);
                        //transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
                        //rigidBody2D.linearVelocity = Vector2.Lerp(rigidBody2D.linearVelocity, new Vector2(moveInput * speed * 5, rigidBody2D.linearVelocityY), Time.deltaTime);
                    }
                    else
                    {
                        Vector2 desiredVelocity = new Vector2(moveInput * speed, rigidBody2D.linearVelocityY);
                        rigidBody2D.linearVelocity = (rigidBody2D.linearVelocity * 7 + desiredVelocity) / 8f;
                    }

                    if (moveInput != 0f)
                    {
                        PlayerAnimationScript.isMovingX = true;
                    }
                    else if (moveInput == 0f)
                    {
                        PlayerAnimationScript.isMovingX = false;
                    }
                }
                break;
            case State.Rolling:
                rigidBody2D.linearVelocity = rollDirection * rollSpeed;
                float rollSpeedDropMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;
                float rollSpeedMinimum = 6f;
                if (rollSpeed < rollSpeedMinimum)
                {
                    state = State.Moving;
                    isInvincible = false;
                    rigidBody2D.linearVelocity = Vector3.zero;
                }
                break;
        }
    }
    private void WallJump()
    {
        audioSource.PlayOneShot(jumpClip);
        canWallJump = true;
        
        float axis;
        if (isRight)
        {
            axis = -1;
        }
        else
        {
            axis = 1;
        }
        Vector2 wallJumpDirection = new Vector2(axis, 1).normalized;
        rigidBody2D.linearVelocity = new Vector2(wallJumpDirection.x * wallJumpForce * speed * 0.15f, wallJumpDirection.y * wallJumpForce);
        Invoke("ResetWallJump", 0.05f);
    }
    private void ResetWallJump()
    {
        canWallJump = false;
    }
    private IEnumerator CoolDown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(coolDownTime);
        isCoolingDown = false;
    }
    private void OnDrawGizmos()
    {
        // Draw ground check radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // Draw wall check radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
    }
    public bool IsInvincible()
    {
        return isInvincible;
    }
}
