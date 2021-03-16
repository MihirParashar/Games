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
