using UnityEngine;
using TMPro;

public class ScoreboardItem : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerKillsText;
    [SerializeField] private TextMeshProUGUI playerDeathsText;

    private Player assignedPlayer;
    #endregion

    //Function that updates this scoreboard item's text
    //to the data of the player given.
    public void AssignPlayerData(Player _assignedPlayer)
    {
        assignedPlayer = _assignedPlayer;
    }

    private void Update()
    {
        //Update our scoreboard text to our player's
        //stats every frame.
        playerNameText.text = assignedPlayer.GetUsername();
        playerKillsText.text = assignedPlayer.GetKillCount().ToString();
        playerDeathsText.text = assignedPlayer.GetDeathCount().ToString();
    }
}
