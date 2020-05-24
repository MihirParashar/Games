using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Button[] levelButtons;
    int levelOn;

    // Start is called before the first frame update
    void Start()
    {
        InitButtons();
    }

    void InitButtons()
    {
        levelOn = PlayerPrefs.GetInt("LevelOn", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {

            //Debug.Log("levelOn is: " + levelOn);
            //Debug.Log("i is: " + i);
            //Debug.Log(levelOn > i);
            if (levelOn > i)
            {
                levelButtons[i].interactable = true;
            }
        }
    }

    public void Reset()
    {
        //Reset for testing purposes
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
       InitButtons();


        PlayerPrefs.DeleteKey("LevelOn");
        
    }
}
