using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Build;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Rigidbody2D rb;

    [Header("Rewind")]
    [SerializeField] private TimeBody timeBody;
    [SerializeField] private GameObject rewindArrow;


    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    public static void Die()
    {
        //For now, we will just restart the scene, since we
        //don't have anything to do at death yet.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        //Disable the rewind arrow.
        rewindArrow.SetActive(false);


    }
}
