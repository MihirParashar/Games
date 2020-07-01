using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    PlayerController controller;
    GameObject levelGen;

    public Animator animator;
    //public Sprite crouchingSprite;
    //public Sprite normalSprite;

    GameObject characterClone;
    Joystick joystick;

    float xMove = 0;

    [HideInInspector] public float moveSpeed = 40f;
    [HideInInspector] public static bool controlsEnabled = true;
    [HideInInspector] public static Vector2 lastPlayerPosition; 

    public float speedMultiplier = 1f;

    
    bool isJumping = false;
    bool isCrouching = false;
    void Start()
    {
        lastPlayerPosition = gameObject.transform.position;

        //Debug.Log(GameObject.FindGameObjectWithTag("Player"));
        characterClone = GameObject.FindGameObjectWithTag("Player");

        controller = gameObject.GetComponent<PlayerController>();
        if (GameObject.FindGameObjectWithTag("Joystick") != null)
        {
            joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        }
        levelGen = GameObject.FindGameObjectWithTag("LevelGen");
    }

    // Update is called once per frame
    void Update()
    {
        //   Debug.Log("From Update Function, characterClone is " + characterClone);   
           

        if (controlsEnabled)
        {
            if (joystick != null)
            {
                xMove = joystick.Horizontal * moveSpeed;
            }
#if UNITY_EDITOR || UNITY_STANDALONE
    xMove = Input.GetAxis("Horizontal") * moveSpeed;
    if (Input.GetButtonDown("Jump"))
    {
                //Save player's position, for reviving.
                if (GetComponent<PlayerController>().m_Grounded)
                {
                    lastPlayerPosition = gameObject.transform.position;
                }
        isJumping = true;
    }

#endif

        


            /*  if (joystick.Vertical < -0.4f) //We are crouching
              {

                  isCrouching = true;

              } else //We aren't crouching
              {

                  isCrouching = false;

              }*/

            /*
             If isCrouching is false, and we are not actually crouching.
             This extra check is needed because, sometimes the isCrouching
             boolean is false even when we are crouching. This happens because
             we will still crouch if there is something above us, but this doesn't
             change the isCrouching variable.
             */
            //Unfortunately, the crouching feature was removed due to bugs. I'll see if I can implement
            //it again in a later update.
        }

       
    }
    void FixedUpdate()
    {

        //Move character
        if (controlsEnabled)
        {
            controller.Move(xMove * speedMultiplier * Time.fixedDeltaTime, isCrouching, isJumping);
            //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.x);
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1f
             || gameObject.GetComponent<Rigidbody2D>().velocity.x < -0.1f)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
            isJumping = false;
        } else
        {
            //So, if controls are not enabled, make sure there are no animations playing.
            animator.SetBool("isWalking", false);
        }
    }

   /* public void Crouch()
    {
        if (!animator.GetBool("isCrouching"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }
        animator.SetBool("isCrouching", true);
        
    }
    
    public void Uncrouch()
    {
        
        animator.SetBool("isCrouching", false);
    }*/
}
