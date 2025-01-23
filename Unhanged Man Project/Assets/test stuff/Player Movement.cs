using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rigidBody2D;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rigidBody2D.linearVelocity.y) < 0.001f) 
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        
    }
}
