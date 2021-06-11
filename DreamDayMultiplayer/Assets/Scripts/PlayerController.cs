using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Movement")]  
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float jumpHeight = 30f;

    [Header("Rotation")]
    [SerializeField] private float camRotLimit = 60f;
    [SerializeField] private Transform armTransform;
    [SerializeField] private Rigidbody armRigidbody;
    private float currentYRot;
    private float xRot;
    private float yRot;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundCheckHeight;
    private bool isGrounded;

    private Rigidbody rb;
    private float mouseSens;
    private bool isOnMobileDevice;
    private Joystick joystick;
    
    #endregion

    void Start()
    {
        //Initializing our Rigidbody component.
        rb = GetComponent<Rigidbody>();

        //Caching our variables for efficiency.
        mouseSens = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        joystick = PlayerUI.instance.GetMovementJoystick();

        //Adding our Jump function to the onClick event
        //listener for our Jump button. This means that
        //every time the player presses the jump button,
        //the jump function will be ran.
        PlayerUI.instance.AddJumpButtonListener(Jump);

        //Checking if we are on a mobile/handheld
        //device, and storing it.
        isOnMobileDevice = SystemInfo.deviceType == DeviceType.Handheld;
    }

    void Update()
    {
        #region Cursor Locking

        //We only want to lock our cursor if we are
        //not on mobile.
        if (!isOnMobileDevice)
        {
            //If the pause menu is active, unlock
            //our cursor. Otherwise, lock our cursor.
            if (PlayerUI.pauseMenuActiveState)
            {
                Cursor.lockState = CursorLockMode.None;

                //If we are in the pause menu, then 
                //we don't want to do anything else
                //in this function (since it involves
                //movement), so just return.
                return;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        #endregion

        #region Movement

        //Checking if a sphere with the specified radius with a center of the specified position
        //intersects an object that has a layer from the specified LayerMask. If true, then we 
        //are on the ground so we can jump. If false, then we are in the air so we can't jump.
        isGrounded = Physics.CheckBox(groundCheckTransform.position, new Vector3(0.025f, groundCheckHeight, 0.025f), Quaternion.identity, groundLayers);

        float xMove;
        float zMove;

        //Setting values to move based on our current position and our current player input if
        //we are not on mobile. If we are, set our input based on the joystick.
        if (!isOnMobileDevice)
        {
            xMove = (transform.forward.x * Input.GetAxis("Vertical")) + (transform.right.x * Input.GetAxis("Horizontal"));
            zMove = (transform.forward.z * Input.GetAxis("Vertical")) + (transform.right.z * Input.GetAxis("Horizontal"));
        } else
        {
            xMove = (transform.forward.x * joystick.Vertical) + (transform.right.x * joystick.Horizontal);
            zMove = (transform.forward.z * joystick.Vertical) + (transform.right.z* joystick.Horizontal);
        }


        //Multiplying the input by the move speed
        //and deltaTime.
        xMove *= moveSpeed * Time.deltaTime;
        zMove *= moveSpeed * Time.deltaTime;

        //Applying the movement.
        rb.MovePosition(new Vector3(transform.position.x + xMove, transform.position.y, transform.position.z + zMove));

        //Jumping if we press the space bar.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        #endregion

        #region Rotation
        //Setting our rotation based on the current
        //player input.
        xRot = Input.GetAxis("Mouse X") * mouseSens;
        yRot = Input.GetAxis("Mouse Y") * mouseSens;

        //Clamping the y-rotation so the player can't
        //look too far down or up.
        currentYRot -= yRot;
        yRot = Mathf.Clamp(currentYRot, -camRotLimit, camRotLimit);
        #endregion
    }

    private void FixedUpdate()
    {
        //We don't want to do any of this if we are
        //paused, so just return.
        if (PlayerUI.pauseMenuActiveState) {
            return;
        }

        //If we are on a mobile device, then we should only rotate when
        //we are pressing down. Otherwise, we can apply rotation every
        //frame.
        if (Input.GetButton("Fire1") && isOnMobileDevice)
        {
            //Applying the x-rotation to the player.
            transform.Rotate(0f, xRot, 0f);
            //Applying the y-rotation to the player's arm.
            armTransform.localEulerAngles = new Vector3(-yRot, 0f, 0f);
        }
        else if (!isOnMobileDevice)
        {
            //Applying the x-rotation to the player.
            transform.Rotate(0f, xRot, 0f);
            //Applying the y-rotation to the player's arm.
            armTransform.localEulerAngles = new Vector3(-yRot, 0f, 0f);
        }

    }

    //Function that adds a force to make our player jump.
    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
