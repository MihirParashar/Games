using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class LevelLoader : MonoBehaviour
{

    public static int currentSceneIndex;
    public static int sceneCount;

    private void Start()
    {
        //Set the scene index variable to the scene we are on.
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //Set the scene count to the number of scenes.
        sceneCount = SceneManager.sceneCount;
    }

    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
