//Created by: charlie
//Edited by:
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float coolDownTime;
    [SerializeField] private bool isInvincible;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private bool canWallJump;
    [SerializeField] private float wallSlideSpeed; //0.5f
    [SerializeField] private float wallJumpForce; //2.0f

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Wall Detection")]
    public Transform wallCheck;
    public float wallCheckRadius = 0.2f;
    public LayerMask wallLayer;

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

    void Start()
    {
        isInvincible = false;
        state = State.Moving;
        isGrounded = false;
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
        switch (state) {
        case State.Moving:
            if (isTouchingWall && !isGrounded && rigidBody2D.linearVelocity.y < 0)
            {
                isWallSliding = true;
                rigidBody2D.linearVelocity = new Vector2(0, rigidBody2D.linearVelocity.y * wallSlideSpeed);
            }
            else
            {
                isWallSliding = false;
            }
            //movement
            moveInput = Input.GetAxisRaw("Horizontal");
            if (!isWallSliding && !canWallJump)
            {
                transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
            }
            

            //fliping sprite
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
    
            //jump button
            //&& Mathf.Abs(rigidBody2D.linearVelocity.y) < 0.001f
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    if (isGrounded)
                    {
                        rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    }
                    else if (isWallSliding)
                    {
                        WallJump();
                    }
                
            }
            //Add a check for on the ground, right click
            if (Input.GetKeyDown(KeyCode.F) && !isCoolingDown)
            {
                //rigidBody2D.AddForce(new Vector2(rollSpeed * moveInput, 0), ForceMode2D.Impulse);
                rollSpeed = 0.5f;
                state = State.Rolling;
                rollDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;
                isInvincible = true;
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
        switch (state)
        {
            case State.Moving:
                break;
            case State.Rolling:
                transform.position += rollDirection * rollSpeed;
                break;
        }
    }
    private void WallJump()
    {
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
        rigidBody2D.linearVelocity = new Vector3(wallJumpDirection.x * wallJumpForce, wallJumpDirection.y * wallJumpForce, 0);
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
