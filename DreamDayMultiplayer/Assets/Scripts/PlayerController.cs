using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private Transform armTransform;
    [SerializeField] private float mouseSens;

    private Rigidbody rb;
    #endregion

    void Start()
    {
        //Initializing our Rigidbody component.
        rb = GetComponent<Rigidbody>();

        //Setting our cursor to lock to the center of the screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //The escape key toggles whether our cursor is locked or not
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        } else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Setting values to move based on our current position and our current player input
        float xMove1 = transform.forward.x * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float xMove2 = transform.forward.z * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        float zMove1 = transform.right.x * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMove2 = transform.right.z * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        //Applying the movement.
        rb.MovePosition(new Vector3(transform.position.x + xMove1 + zMove1, transform.position.y, transform.position.z + xMove2 + zMove2));

        //Setting our rotation based on the current player input.
        float xRot = Input.GetAxis("Mouse X") * mouseSens;
        float yRot = Input.GetAxis("Mouse Y") * mouseSens;

        //Clamping the y-rotation so the player can't look too far down or up.
        yRot = Mathf.Clamp(yRot, -90f, 60f);

        //Applying the x-rotation to the player.
        transform.Rotate(new Vector3(0, xRot, 0));

        Debug.Log(yRot);

        //Applying the y-rotation to the player's arm
        armTransform.localEulerAngles = new Vector3(armTransform.localEulerAngles.x + yRot, 0f, 0f); 

        
    }
}
