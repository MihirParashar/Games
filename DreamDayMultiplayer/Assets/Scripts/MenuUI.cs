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

    [Header("Player Settings")]
    [SerializeField] private TextMeshProUGUI mouseSensitivityText;
    [SerializeField] private Slider mouseSensitivitySlider;

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

        //On start, refresh all of our slider values.
        RefreshSliders();
    }

    private void Update() {
        //Update our mouse sensitivity text.
        mouseSensitivityText.text = PlayerPrefs.GetFloat("MouseSensitivity").ToString("F2");
    }

    //Function that updates all of our slider values to what the
    //corresponding settings are set to.
    public void RefreshSliders() {
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
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
