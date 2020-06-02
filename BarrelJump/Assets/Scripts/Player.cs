using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject scoreText;
    public Transform groundCheck;
    public Transform groundCheckBottom;
    public GameObject visual;
    string difficultyLevel;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool onGround;
    float jumpVelocity;

    GameManager gameManager;



    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
       
        jumpVelocity = 7f;

       

        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    //checking if player is in radius of another object so it cannot jump all the time
    void Update()
  
    {

        onGround = Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckRadius, groundCheckRadius), 0, whatIsGround);


        //GameObject groundCheckVisual = Instantiate(visual, groundCheck.position, Quaternion.identity);
        //Destroy(groundCheckVisual, 2f);

        if (Input.GetMouseButtonDown(0) && onGround && gameManager.energySlider.value > 0 ||
            Input.GetButtonDown("Jump") && onGround && gameManager.energySlider.value > 0)
        {
            if (Time.timeScale == 0.0f)
            {
                return;
            }
            //Jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            //Every time the player jumps, the fuel burns
            gameManager.BurnFuel(gameManager.energyBurnAmount);



            difficultyLevel = gameManager.difficultyString;

            if (difficultyLevel == "Easy")
            {
                groundCheckRadius = 4f;
                jumpVelocity = 8;
            }
            else if (difficultyLevel == "Medium")
            {
                groundCheckRadius = 3f;
                jumpVelocity = 8;
            }
            else if (difficultyLevel == "Hard")
            {
                groundCheckRadius = 3f;
                jumpVelocity = 8;
            }
            else if (difficultyLevel == "BOSS")
            {
                groundCheckRadius = 3f;

                jumpVelocity = 7.5f;
            }
        }

    }

}
