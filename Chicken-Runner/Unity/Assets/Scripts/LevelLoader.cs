using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    TextMeshProUGUI progressText;
    Slider progressSlider;

    void Start()
    {
        if (loadingScreen == null)
        {
            loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen");
        }
        progressSlider = loadingScreen.transform.GetChild(1).GetComponent<Slider>();
        progressText = loadingScreen.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();

    }

    public void NextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevelAsync(sceneIndex));
    }

    public IEnumerator LoadLevelAsync(int sceneIndex)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        Debug.Log("loadingScreen obj is: " + loadingScreen);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressSlider.value = progress;
            progressText.text = Mathf.RoundToInt(progress * 100f) + "%";

            yield return null;
        }
    }

    public void Quit()
    {
        //With this extra code, quit now works in every type of build.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}
