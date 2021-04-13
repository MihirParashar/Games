using UnityEngine;
using TMPro;

public class ScoreboardItem : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerKillsText;
    [SerializeField] private TextMeshProUGUI playerDeathsText;
    #endregion

    //Function that updates this scoreboard item's text
    //to the data of the player given.
    public void UpdateItem(Player player)
    {
        playerNameText.text = player.GetUsername();
        playerKillsText.text = player.GetKillCount().ToString();
        playerDeathsText.text = player.GetDeathCount().ToString();
    }

    //Function to get our name text value.
    public string GetNameTextValue()
    {
        return playerNameText.text;
    }
}
