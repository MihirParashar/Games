using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    GameObject pauseMenu;
    GameObject tutorialText;

    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        tutorialText = GameObject.FindGameObjectWithTag("Tutorial");

        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                //The game is paused and we pressed escape
                Resume();
            } else
            {
                //The game is not paused and we pressed escape
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        if (tutorialText != null)
        {
            tutorialText.SetActive(false);
        }
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseMenu.SetActive(false);
        if (tutorialText != null)
        {
            tutorialText.SetActive(true);
        }
    }
}
