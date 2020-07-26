using System.Collections;
using System.Collections.Generic;
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

    private float xMove;
    private float zMove;

    private Vector3 movement;
    #endregion

    #region Gravity
    private Vector3 downwardsVelocity;
    private const float gravity = -9.81f;
    #endregion


    #endregion

    private void Update()
    {
        #region Ground Check
        //Physics.CheckSphere returns a boolean on whether or not another object with the layer mask given
        //collides with a sphere with the position given, and the radius given.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isGrounded && downwardsVelocity.y < 0)
        {
            downwardsVelocity.y = -2f;
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
            downwardsVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        #endregion

        #region Gravity
        //Adding the speed of gravity (-9.81 meters) to our downwards velocity.
        downwardsVelocity.y += gravity * Time.deltaTime;

        //Applying the downwards velocity.
        controller.Move(downwardsVelocity * Time.deltaTime);
        #endregion

    }

}
