using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector] public static bool isPaused = false;

    [SerializeField] private GameObject pauseMenu;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            }
            else
            {
                //The game is not paused and we pressed escape
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;

        //Enable the pause menu.
        pauseMenu.SetActive(true);

        //Freeze the game.
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        isPaused = false;

        //Unfreeze the game.
        Time.timeScale = 1.0f;

        //Disable the pause menu.
        pauseMenu.SetActive(false);
    }
}
