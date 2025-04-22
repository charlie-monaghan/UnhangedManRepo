//Created by: charlie
//Edited by: eddie and Carter
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

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
    
    private Vector3 rollDirection;
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

            if (isTouchingWall && !isGrounded && rigidBody2D.linearVelocity.y < 0)
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
            if (!isWallSliding || (isWallSliding && frameCounter >= 5))
            {
                transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
                //rigidBody2D.linearVelocity = Vector2.Lerp(rigidBody2D.linearVelocity, new Vector2(moveInput * speed * 5, rigidBody2D.linearVelocityY), Time.deltaTime);
                if (moveInput != 0f)
                {
                    PlayerAnimationScript.isMovingX = true;
                }
                else if (moveInput == 0f)
                {
                    PlayerAnimationScript.isMovingX = false;
                }
            }

            // walking SFX
            if (isGrounded && moveInput != 0f && state == State.Moving)
            {
                if (!isPlayWalk)
                {
                    audioSource.clip = walkSFX;
                    audioSource.loop = true;
                    audioSource.Play();
                    isPlayWalk = true;
                }
            }
            else
            {
                if (audioSource.isPlaying && isPlayWalk)
                {
                    audioSource.Stop();
                    isPlayWalk = false;
                }
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
                rollSpeed = 0.5f;
                state = State.Rolling;
                audioSource.PlayOneShot(dashSFX);
                rollDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;
                isInvincible = true;
                anim.SetTrigger("Roll");
                StartCoroutine(CoolDown());
            }
            break;

        case State.Rolling:
            float rollSpeedDropMultiplier = 5f;
            rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;
            float rollSpeedMinimum = 0.1f;
            if (rollSpeed < rollSpeedMinimum)
            {
                state = State.Moving;
                isInvincible = false;
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
                break;
            case State.Rolling:
                transform.position += rollDirection * rollSpeed * speed * 0.2f;
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
        Vector3 wallJumpDirection = new Vector3(axis, 1, 0).normalized;
        rigidBody2D.linearVelocity = new Vector3(wallJumpDirection.x * wallJumpForce * speed * 0.1f, wallJumpDirection.y * wallJumpForce, 0);
        Invoke("ResetWallJump", 0.5f);
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
