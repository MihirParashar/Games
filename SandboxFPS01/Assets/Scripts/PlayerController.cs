using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Initializing Variables

    [SerializeField] private float mouseSens;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;

    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float xMove = Input.GetAxis("Vertical") * Time.deltaTime;
        float yMove = Input.GetAxis("Horizontal") * Time.deltaTime;

        float xRot = Input.GetAxis("Mouse X") * Time.deltaTime;
        float yRot = Mathf.Clamp(Input.GetAxis("Mouse Y") * Time.deltaTime, -90, 90);

        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, xRot * mouseSens, 0)));

        rb.MovePosition(transform.position + (transform.forward * xMove * moveSpeed) + (transform.right * yMove * moveSpeed));
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(transform.up * jumpForce);
        }

    }
}
