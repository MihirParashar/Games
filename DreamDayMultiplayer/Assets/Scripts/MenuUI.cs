using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    #region Variables

    //Creating a static instance of itself, also known as a
    //singleton.
    public static MenuUI instance;

    [Header("Matchmaking")]
    [SerializeField] private TMP_InputField matchSizeInput;
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Usernames")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TextMeshProUGUI noUsernameErrorText;

    [Header("Player Settings")]
    [SerializeField] private TextMeshProUGUI mouseSensitivityText;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private Slider volumeSlider;
    

    #endregion

    private void Start() {
        if (instance == null)
        {
            instance = this;
        } else
        {
            //If the instance is already assigned, then that means
            //there is a duplicate MenuUI, so log an error.
            Debug.LogError("MenuUI: More than one MenuUI in the scene!");
        }

        //On start, update all of our values that are controlled
        //by PlayerPrefs.
        UpdateSavedUI();
    }

    private void Update() {
        //Update our mouse sensitivity and volume text.
        mouseSensitivityText.text = PlayerPrefs.GetFloat("MouseSensitivity", 1f).ToString("F2");
        volumeText.text = Mathf.Round(PlayerPrefs.GetFloat("Volume", 1f) * 100).ToString() + "%";
    }

    //Function that updates all of our values controlled
    //by PlayerPrefs to what the corresponding settings
    // are set to.
    public void UpdateSavedUI() {
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerUsername"))) {
            usernameInput.text = PlayerPrefs.GetString("PlayerUsername");
        }
    }

    //Function that checks if our username input field is empty
    //or not. If it is, return true and set the error text.
    //Otherwise, empty the error text and return false.
    public bool CheckIfUsernameEmpty() {
        if (string.IsNullOrEmpty(usernameInput.text)) {
            noUsernameErrorText.text = "PLEASE ENTER A USERNAME.";
            return true;
        } else {
            noUsernameErrorText.text = "";
            return false;
        }
    }

    #region Get/Set Methods For UI
    //Creating get/set methods for all of the UI variables
    //so we can access them from other scripts if we need to.
    public TMP_InputField GetMatchSizeInput() {
        return matchSizeInput;
    }

    public TextMeshProUGUI GetStatusText() {
        return statusText;
    }

    #endregion
}
