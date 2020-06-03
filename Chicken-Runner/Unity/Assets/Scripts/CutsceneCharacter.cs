using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneCharacter : MonoBehaviour
{
    [System.Serializable]
    public struct Cutscene
    {
        public int cutsceneBuildIndex;
        public Sprite[] SpeechBubbles;
    }

    public Cutscene[] cutscenes;

    public GameObject speechBubbleObj;
    public GameObject chickenInCutscene;
    GameObject character;


    bool hasCompletedCutscene = false;

    int cutsceneOn;
    int speechBubbleOn = 0;
    int collisions = 0;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {


        //Debug.Log("Working");
        if (collision.tag == "Player" && !hasCompletedCutscene)
        {
            if (collisions < 1)
            {
                collisions++;
                speechBubbleObj.SetActive(true);
                for (int i = 0; i < cutscenes.Length; i++)
                {
                    if (cutscenes[i].cutsceneBuildIndex == SceneManager.GetActiveScene().buildIndex)
                    {
                        cutsceneOn = i;
                        break;
                    }
                }
                character.GetComponent<SpriteRenderer>().enabled = false;
                character.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                chickenInCutscene.SetActive(true);
                Time.timeScale = 0.0f;
                speechBubbleObj.GetComponent<SpriteRenderer>().sprite = cutscenes[cutsceneOn].SpeechBubbles[speechBubbleOn];
                speechBubbleOn++;
            }
        }
    }

    public void movetoNextCutscene()
    {
        if (speechBubbleOn == cutscenes[cutsceneOn].SpeechBubbles.Length)
        {
            chickenInCutscene.SetActive(false);
            hasCompletedCutscene = true;
            Time.timeScale = 1.0f;
            character.GetComponent<SpriteRenderer>().enabled = true;
            character.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        }
        if (!hasCompletedCutscene)
        {
            speechBubbleObj.GetComponent<SpriteRenderer>().sprite = cutscenes[cutsceneOn].SpeechBubbles[speechBubbleOn];
            speechBubbleOn++;
        }
    }
}






