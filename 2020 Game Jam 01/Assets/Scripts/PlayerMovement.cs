using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Thanks to Brackeys for the tutorial!


    [SerializeField] private CharacterController2D controller;

    [SerializeField] private float moveSpeed = 40f;

    private float xMove = 0f;
    private bool jumping = false;
    private bool crouching = false;

    private void Update()
    {
        //Switch movement speed based on health.
        switch (PlayerHealth.health) {
            case 5:
                moveSpeed = 40f;
                break;
            case 4:
                moveSpeed = 35f;
                break;
            case 3:
                moveSpeed = 30f;
                break;
            case 2:
                moveSpeed = 25f;
                break;
            case 1:
                moveSpeed = 20f;
                break;
            default:
                moveSpeed = 40f;
                break;
        }

        //Getting our player movement horizontally.
        //The result will be 1 if we press the D key,
        //and -1 if we press the A key.
        xMove = Input.GetAxisRaw("Horizontal") * moveSpeed;


        //If we are pressing the button mapped to the jump input,
        //then set jumping to true.
        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }
        
        //If we are pressing and holding the button mapped to the
        //crouch input, then set crouching to true, otherwise make it false.
        if (Input.GetButton("Crouch"))
        {
            crouching = true;
        } else
        {
            crouching = false;
        }

        
    }

    private void FixedUpdate()
    {
        //Moving our move speed multiplied by the fixed delta time
        //so that our movement speed is the same no matter how many frames
        //per second you are getting.
        controller.Move(xMove * Time.fixedDeltaTime, crouching, jumping);


        //Once we jumped, we don't want it to jump again.
        jumping = false;
    }
}
