using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{

    public void LoadLevel(int sceneIndex, GameObject loadingScreen, Slider progressSlider, TextMeshProUGUI progressText)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex, loadingScreen, progressSlider, progressText));
    }

    IEnumerator LoadAsynchronously(int sceneIndex, GameObject loadingScreen, Slider progressSlider, TextMeshProUGUI progressText)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressSlider.value = progress;
            progressText.text = Mathf.Round(progress * 100f) + "%";
            

            yield return null;
        }
    }
}
