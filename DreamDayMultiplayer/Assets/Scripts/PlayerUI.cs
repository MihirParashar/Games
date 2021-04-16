using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    #region Variables

    //Creating a static instance of itself, also known as a
    //singleton.
    public static PlayerUI instance;

    public static bool pauseMenuActiveState = false;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private Image crosshair;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject scoreboard;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthBarText;
    [SerializeField] private TextMeshProUGUI roundTimerText;
    [SerializeField] private TextMeshProUGUI frameCountText;
    #endregion

    private void Start() {
        if (instance == null)
        {
            instance = this;
        } else
        {
            //If the instance is already assigned, then that means
            //there is a duplicate PlayerUI, so log an error.
            Debug.LogError("PlayerUI: More than one PlayerUI in the scene!");
        }

        //The pause menu should be turned off by default (if we
        //haven't already turned it off in the inspector)
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        //If we are pressing the escape key, then toggle
        //the pause menu.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        //If we are pressing and holding the tab key,
        //then enable the scoreboard. Otherwise, 
        //disable it.
        scoreboard.SetActive(Input.GetKey(KeyCode.Tab));
    }

    public void TogglePauseMenu()
    {
        //Setting our pause menu's active
        //state to the opposite of what it
        //previously was.
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        pauseMenuActiveState = pauseMenu.activeSelf;
    }

    #region Set Methods For UI
    //Creating set methods for all of the UI variables
    //so we can access them from other scripts if we need to.
    public void SetKillCountText(string killCount) {
        killCountText.text = killCount;
    }

    public void SetDeathTextActive(bool active) {
        deathText.gameObject.SetActive(active);
    }

    public void SetCrosshairActive(bool active) {
        crosshair.gameObject.SetActive(active);
    }

    public void SetAmmoText(string newText)
    {
        ammoText.text = newText;
    }

    public void SetHealthBarValue(int newValue)
    {
        healthBar.value = newValue;

        //Whenever we change the health bar's value,
        //we should change the text that displays
        //the value as well.
        healthBarText.text = healthBar.value.ToString();
    }

    public void SetRoundTimerText(string newText)
    {
        roundTimerText.text = newText;
    }

    public void SetFrameCountText(string newText)
    {
        frameCountText.text = newText;
    }

    #endregion
}
