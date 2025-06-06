//Created by: Carter
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    int frameCounter = 0;
    Animator anim;
    public static bool isMovingX = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("AttackInput");
        }
        */
        if (isMovingX)
        {
            frameCounter = 0;
            anim.SetBool("IsMovingX", true);
        }
    }
    
    private void FixedUpdate() //This prevents the player from animating as idle whenever they change directions
    {
        if (isMovingX == false)
        {
            frameCounter++;

            if (frameCounter >= 2)
            {
                anim.SetBool("IsMovingX", false);
                frameCounter = 0;
            }
        }
    }
}
