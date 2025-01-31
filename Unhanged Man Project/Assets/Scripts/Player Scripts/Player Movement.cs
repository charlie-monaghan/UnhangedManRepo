//Created by: charlie
//Edited by: eddie
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

    void Start()
    {
        isInvincible = false;
        state = State.Moving;
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (state) {
        case State.Moving:
            //movement
            float moveInput = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;

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
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rigidBody2D.linearVelocity.y) < 0.001f) // when you press space and are "grounded"
            {
                rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // applies upwards force to jump
            }

            //roll button
            //Add a check for on the ground, right click
            if (Input.GetMouseButtonDown(1) && !isCoolingDown)
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
    private IEnumerator CoolDown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(coolDownTime);
        isCoolingDown = false;
    }
    public bool IsInvincible()
    {
        return isInvincible;
    }
}
