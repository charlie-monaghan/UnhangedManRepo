//Created by: charlie
//Edited by:
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool isRight = true;
    private Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rigidBody2D.linearVelocity.y) < 0.001f) 
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }


    }
}
