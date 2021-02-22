using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Movement")]  
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float jumpHeight = 30f;

    [Header("Rotation")]
    [SerializeField] private float camRotLimit = 60f;
    [SerializeField] private float mouseSens;
    [SerializeField] private Transform armTransform;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundCheckRadius;

    private Rigidbody rb;
    private bool isGrounded;
    #endregion

    void Start()
    {
        //Initializing our Rigidbody component.
        rb = GetComponent<Rigidbody>();

        //Setting our cursor to lock to the center
        //of the screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        #region Locking Cursor
        //The escape key toggles whether our cursor
        //is locked or not.
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        } else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        #endregion

        #region Movement
        
        //Checking if a sphere with the specified radius with a center of the specified position
        //intersects an object that has a layer from the specified LayerMask. If true, then we 
        //are on the ground so we can jump. If false, then we are in the air so we can't jump.
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundLayers);

        //Setting values to move based on our current position and our current player input
        float xMove = (transform.forward.x * Input.GetAxis("Vertical")) + (transform.right.x * Input.GetAxis("Horizontal"));
        float zMove = (transform.forward.z * Input.GetAxis("Vertical")) + (transform.right.z * Input.GetAxis("Horizontal"));
        float yMove = Input.GetAxis("Jump");


        //Multiplying the input by the move speed
        //and deltaTime.
        xMove *= moveSpeed * Time.deltaTime;
        zMove *= moveSpeed * Time.deltaTime;

        //Applying the movement.
        rb.MovePosition(new Vector3(transform.position.x + xMove, transform.position.y, transform.position.z + zMove));

        //If we are pressing the space key and we
        //are on the ground, then jump.
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
        #endregion

        #region Rotation
        //Setting our rotation based on the current
        //player input.
        float xRot = Input.GetAxis("Mouse X") * mouseSens;
        float yRot = Input.GetAxis("Mouse Y") * mouseSens;

        //Clamping the y-rotation so the player can't
        //look too far down or up.
        yRot = Mathf.Clamp(yRot, -camRotLimit, camRotLimit);

        //Applying the x-rotation to the player.
        transform.Rotate(new Vector3(0, xRot, 0));

        //Applying the y-rotation to the player's arm.
        armTransform.localEulerAngles = new Vector3(armTransform.localEulerAngles.x + yRot, 0f, 0f);
        #endregion
    }
}
