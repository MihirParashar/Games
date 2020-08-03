using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we make a collision with a GameObject that has the
        //player tag, then die.
        if (collision.CompareTag("Player"))
        {

            //Make the player lose one health.
            PlayerHealth.health--;
            StartCoroutine(gameManager.RewindBack(3f));
        }
    }
}
