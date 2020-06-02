using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressSlider;
    public LevelLoader levelLoader;
    public TextMeshProUGUI progressText;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LockerControl") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("LockerControl"));
        }
    } 

    public void PlayGame()
    {
        levelLoader.LoadLevel(1, loadingScreen, progressSlider, progressText);
    }

    public void QuitGame()
    {


#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
    
#endif

        }
}
