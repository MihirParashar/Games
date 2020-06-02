using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public LevelLoader levelLoader;
    public GameObject loadingScreen;
    public Slider progressSlider;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI loadingText;
    public GameObject bossDude;
    GameObject music;
    
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0f)
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }

    public void QuitToMenu()
    {
        bossDude.SetActive(false);
        Time.timeScale = 1.00f;
        levelLoader.LoadLevel(0, loadingScreen, progressSlider, progressText);
    }

    public void EnterShop()
    {
        Time.timeScale = 1.00f;
        levelLoader.LoadLevel(2, loadingScreen, progressSlider, progressText);
    }
}
