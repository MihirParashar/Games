using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    #region Variables

    //Creating a static instance of itself, also known as a
    //singleton.
    public static PlayerUI instance;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private Image crosshair;
    [SerializeField] private GameObject pauseMenu;
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
    }

    private void Update()
    {
        //If we are pressing the escape key, then toggle
        //the pause menu.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        //Setting our pause menu's active
        //state to the opposite of what it
        //previously was.
        pauseMenu.SetActive(!pauseMenu.activeSelf);
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
    
    #endregion
}
