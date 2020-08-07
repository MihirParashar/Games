using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;


    private void Start()
    {

        for (int i = 0; i < levelButtons.Length; i++)
        {
            //Make the button interactable depending on whether or not we are at that level.
            Debug.Log(PlayerPrefs.GetInt("HasCompletedLevels", 0));
            if (PlayerPrefs.GetInt("HasCompletedLevels", 0) == 0)
            {
                levelButtons[i].interactable = (PlayerPrefs.GetInt("LevelOn", 1)) == i + 1;
            } else
            {
                //If we completed all levels, allow us to select all buttons.
                levelButtons[i].interactable = true;
            }
        }
    }
}
