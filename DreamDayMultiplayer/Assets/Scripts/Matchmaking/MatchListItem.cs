using UnityEngine;
using UnityEngine.Networking.Match;
using TMPro;

public class MatchListItem : MonoBehaviour
{
    #region Variables
    private MatchInfoSnapshot matchInfo;
    [SerializeField] private TextMeshProUGUI matchNameText;

    //Creating a delegate to run whenever
    //we join a match.
    public delegate void JoinMatchDelegate(MatchInfoSnapshot match);
    private JoinMatchDelegate onJoinMatch;
    #endregion

    //Creating a method to run on our JoinGame
    //class whenever we want to update our info
    //text based on the match info given.
    public void Setup(MatchInfoSnapshot _matchInfo, JoinMatchDelegate joinMatchCallback)
    {
        matchInfo = _matchInfo;

        //Setting our callback function that runs when
        //we join a match to the callback function
        //that was inputted.
        onJoinMatch = joinMatchCallback;

        //Formatting our text that shows us the match
        //name and the current amount of players out
        //of the max amount of players.
        matchNameText.text = matchInfo.name + " (" + matchInfo.currentSize + "/" + matchInfo.maxSize + ")";
    }

    //Function to run when we click the join button
    //in our GUI. It invokes (runs) the function(s)
    //set for our onJoinMatch callback.
    public void JoinGame()
    {
        onJoinMatch.Invoke(matchInfo);
    }
}
