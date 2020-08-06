using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;


    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            //Make the button interactable depending on whether or not we are at that level
            //or above it.
            levelButtons[i].interactable = (PlayerPrefs.GetInt("levelOn", 1)) >= i + 1;
        }

        //Put an 
    }
}
