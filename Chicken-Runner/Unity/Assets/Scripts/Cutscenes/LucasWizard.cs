 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LucasWizard : MonoBehaviour
{

    public GameObject speechBubbleObj;
    public GameObject chickenInCutscene;
    public Cutscene[] cutscenes;
    GameObject character;
    bool hasCompletedCutscene = false; 
    float cutsceneLength;
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

   /* IEnumerator StartCutscene()
    {
        //Not the best way to detect what cutscene we are on, but it will have to do...
        
            cutsceneLength = 9f;
            GetComponent<Animator>().SetInteger("CutsceneType", 0);
        
        character.GetComponent<SpriteRenderer>().enabled = false;
        character.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(cutsceneLength);
        hasCompletedCutscene = true;
        Time.timeScale = 1.0f;
        character.GetComponent<SpriteRenderer>().enabled = true;
        character.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
    }*/

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


        

 

