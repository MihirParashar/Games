using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameManager gameManager;    

    private void Start()
    {
        //Initialize the game manager.
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we make a collision with a GameObject that has the
        //player tag, and our time scale is not 0, then rewind.
        if (collision.CompareTag("Player") && Time.timeScale != 0)
        {

            //Make the player lose one health.
            PlayerHealth.health--;
            StartCoroutine(gameManager.RewindBack(3f));
        }
    }
}
