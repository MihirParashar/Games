using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Rigidbody2D rb;

    [Header("Rewind")]
    [SerializeField] private TimeBody timeBody;
    [SerializeField] private GameObject rewindUI;

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

        //If we haven't completed all levels yet, remove all levels progressed.
        if (PlayerPrefs.GetInt("HasCompletedLevels", 0) == 0)
        {
            PlayerPrefs.SetInt("LevelOn", 1);
        }

        //Reset the player's health.
        PlayerPrefs.SetInt("Health", 5);

        //Enable the lose text.
        loseMenu.SetActive(true);

        //Disable our rewind arrow and hearts.
        rewindUI.SetActive(false);
        hearts.SetActive(false);
    }

    public void Retry()
    {

        //Load the first level.
        LevelLoader.LoadLevel(2);

        //Set the time scale back to 1 again.
        Time.timeScale = 1.0f;
    }

    public void Win()
    {
        //If our level on is greater than or equal to the scene index - 1, then go
        //to the next level.
        if (PlayerPrefs.GetInt("LevelOn") >= LevelLoader.currentSceneIndex - 1)
        {
            PlayerPrefs.SetInt("LevelOn", LevelLoader.currentSceneIndex);
        }

        //If we have a level after this one, load the next scene.
        if (LevelLoader.sceneCount > LevelLoader.currentSceneIndex + 1)
        {
            LevelLoader.LoadLevel(LevelLoader.currentSceneIndex + 1);
        }

        //If we are on the last level, then set has completed all levels to true.
        if (LevelLoader.sceneCount - 1 == LevelLoader.currentSceneIndex)
        {
            PlayerPrefs.SetInt("HasCompletedLevels", 1);
        }
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
        rewindUI.SetActive(true);



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
        rewindUI.SetActive(false);


    }
}
