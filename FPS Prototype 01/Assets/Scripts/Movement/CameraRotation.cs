using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    #region Full Script Summary
    /*
     * CameraRotation Script - Created on July 25th, 2020 by Mihir Parashar
     * 
     * SUMMARY START
     * This script controls the rotation of the camera.
     * If we are moving our mouse on the X axis, we will
     * rotate the entire player. However, if we are moving
     * it on they Y axis, we will rotate only the camera,
     * because we don't want our player to be rotated
     * vertically. We also clamp our Y axis rotation to
     * a minimum of -90 degrees, and a maximum of 90
     * degrees.
     * SUMMARY END
     * 
     * Thanks to Brackeys for the tutorial!
     * Link to video here: https://www.youtube.com/watch?v=_QajrabyTJc
     * 
     */
    #endregion

    #region Defining Variables
    [SerializeField] private Transform playerBody;

    [SerializeField] private float mouseSens = 100f;

    private float xRot;
    #endregion


    private void Start()
    {
        //Locking our cursor in the game window.
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        //Getting our rotation from the input axes.
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        //Subtracting our mouse Y input from our rotation. Normally, we would
        //add the Y input to our rotation, but for some reason, that inverts
        //the rotation.
        xRot -= mouseY;

        //Clamping our Y rotation between -90 degrees and 90 degrees.
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        //Applying our Y rotation to the camera.
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        //Applying our X rotation to the player.
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
