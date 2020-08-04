using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Build;
using UnityEngine.Diagnostics;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Rigidbody2D rb;

    [Header("Rewind")]
    [SerializeField] private TimeBody timeBody;
    [SerializeField] private GameObject rewindArrow;

    [Header("Lose")]
    [SerializeField] private GameObject loseMenu;

    [Header("Health")]
    [SerializeField] private GameObject hearts;

    private void Start()
    {
        //Initialize our rigidbody.
        rb = player.GetComponent<Rigidbody2D>();

        //For some reason, there is a very weird glitch that causes the player to die twice,
        //So I'm setting the time scale back to 1 again.
        Time.timeScale = 1.0f;
    }

    public void Die()
    {
        //Disable player colliders so it doesn't die again.
        foreach (Collider2D col in player.GetComponents<Collider2D>())
        {
            col.enabled = false;
        }

        //Set the time scale to 0.
        Time.timeScale = 0.0f;

        Debug.Log("Player Dead");

        //Enable the lose text.
        loseMenu.SetActive(true);

        //Disable our rewind arrow and hearts.
        rewindArrow.SetActive(false);
        hearts.SetActive(false);
    }

    public void Retry()
    {
        LevelLoader.LoadLevel(LevelLoader.currentLevel);

        //Set the time scale back to 1 again.
        Time.timeScale = 1.0f;
    }

    public IEnumerator RewindBack(float time)
    {
        //Start rewinding.
        timeBody.StartRewind();

        //Make our rigidbody kinematic while it's rewinding.
        rb.isKinematic = true;

        //Disabling all colliders during rewinding.
        foreach (Collider2D col in player.GetComponents<Collider2D>())
        {
            col.enabled = false;
        }

        //Disabling the player controller.
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<CharacterController2D>().enabled = false;


        //Enable the rewind arrow.
        rewindArrow.SetActive(true);



        //Wait for the amount of time given.
        yield return new WaitForSeconds(time);



        //Stop rewinding.
        timeBody.StopRewind();

        //Make our rigidbody not kinematic when rewinding stops.
        rb.isKinematic = false;

        //Disabling all colliders during rewinding.
        foreach (Collider2D col in player.GetComponents<Collider2D>())
        {
            col.enabled = true;
        }

        //Enabling the player controller.
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<CharacterController2D>().enabled = true;

        //Disable the rewind arrow.
        rewindArrow.SetActive(false);


    }
}
