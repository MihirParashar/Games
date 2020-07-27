using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Full Script Summary
    /*
     * PlayerMovement Script - Created on July 25th, 2020 by Mihir Parashar
     * 
     * SUMMARY START
     * This script controls player movement.Player movement
     * is implemented in orthodox style - W to move forward,
     * S to move backward, A to move to the left, and S to 
     * move to the right.
     * SUMMARY END
     * 
     * Thanks to Brackeys for the tutorial!
     * Link to video here: https://www.youtube.com/watch?v=_QajrabyTJc
     * 
     */
    #endregion

    #region Defining Variables
    [SerializeField] private CharacterController controller;

    #region Ground Check
    [Header("Ground Check")]

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float groundDist = 0.4f;

    private bool isGrounded;
    #endregion

    #region Movement
    [Header("Movement")]

    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float crouchMultiplier = 0.8f;

    [SerializeField] private Transform GFX;

    private float xMove;
    private float zMove;

    private float normalHeight;
    private float crouchingHeight;

    private Vector3 movement;
    #endregion

    #region Gravity
    private Vector3 playerVelocity;

    //Normal gravity speed, multiplied by 2.5.
    //I found this to work better.
    private const float gravity = -9.81f * 2.5f;
    #endregion

    #endregion

    private void Awake()
    {
        //Defining the normal player height, as well as
        //the crouching height, for our crouch and uncrouch
        //methods to use.
        normalHeight = controller.height;
        crouchingHeight = controller.height * crouchMultiplier;
    }

    private void Update()
    {
        #region Ground Check
        //Physics.CheckSphere returns a boolean on whether or not another object with the layer mask given
        //collides with a sphere with the position given, and the radius given.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        #endregion

        #region Movement
        //Getting our movement from the input axes.
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");

        //Defining our movement to apply in a variable.
        movement = (transform.right * xMove) + (transform.forward * zMove);

        //Applying our movement to the character controller.
        controller.Move(movement * moveSpeed * Time.deltaTime);

        //If the player presses the jump button, and we are on the ground,
        //then jump.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        //If crouch button is pressed, crouch.
        if (Input.GetButton("Crouch"))
        {
            Crouch();
        } else
        {
            Uncrouch();
        }
        #endregion

        #region Gravity
        //Adding the speed of gravity (-9.81 meters) to our downwards velocity.
        playerVelocity.y += gravity * Time.deltaTime;

        //Applying the downwards velocity.
        controller.Move(playerVelocity * Time.deltaTime);
        #endregion

    }

    #region Movement Methods
    private void Jump()
    {
        //Using a formula to calculate force needed to jump, then
        //applying it to our Y velocity.
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void Crouch()
    {
        //Changing height of character controller to
        //give the effect of the player crouching.
        controller.height = crouchingHeight;
    }
    private void Uncrouch()
    {
        //Restoring player's height back to the original.
        controller.height = normalHeight;
    }
    #endregion
}
