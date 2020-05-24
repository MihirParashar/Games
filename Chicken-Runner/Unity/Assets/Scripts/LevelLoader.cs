using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    GameObject loadingScreen;
    TextMeshProUGUI progressText;
    Slider progressSlider;

    void Start()
    {
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen");
        if (GameObject.FindGameObjectWithTag("LoadingText") != null)
        {
            progressText = GameObject.FindGameObjectWithTag("LoadingText").GetComponent<TextMeshProUGUI>();
            progressSlider = GameObject.FindGameObjectWithTag("LoadingSlider").GetComponent<Slider>();
            loadingScreen.SetActive(false);
        }

    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Retry()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex));
    }

    public void MainMenu()
    {
        StartCoroutine(LoadLevelAsync(0));
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevelAsync(sceneIndex));
    }

    public IEnumerator LoadLevelAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //Wait until all objects are initialized
        while (loadingScreen == null) { yield return null; }
        while (progressSlider == null) { yield return null; }
        while (progressText == null) { yield return null; }
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
