using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [HideInInspector] public static int currentLevel;

    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
