using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Transform matchListParent;
    [SerializeField] private GameObject matchListItemPrefab;

    //A list of all of our match list item GameObjects.
    private List<GameObject> matchList = new List<GameObject>();

    private NetworkManager networkManager;
    #endregion


    private void Start() { 
        //Caching our variable for more efficiency.
        networkManager = NetworkManager.singleton;

        //If we haven't started a matchmaker yet,
        //then start it manually.
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshMatchList();
    }

    public void RefreshMatchList()
    {
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);

        //Setting our status text to let the player
        //know that we are loading the matches.
        statusText.text = "LOADING...";

    }

    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchListResults)
    {
        //We have finished loading our matches,
        //so we can empty the status text.
        statusText.text = "";

        //If we couldn't find any match list, then
        //tell the player we could not find it.
        if (matchList == null)
        {
            statusText.text = "Could not find match list. Is your internet connection stable?";
            return;
        }

        ClearMatchList();

        //Creating an instance of the match list prefab for every 
        //match list item we found.
        foreach (MatchInfoSnapshot match in matchListResults)
        {
            GameObject matchListItemGO = Instantiate(matchListItemPrefab);
            matchListItemGO.transform.SetParent(matchListParent);

            //Adding our GameObject to the list of matches we have.
            matchList.Add(matchListItemGO);

            MatchListItem matchListItem = matchListItemGO.GetComponent<MatchListItem>();

            //If we have the MatchListItem component (which we
            //should) on our GameObject, then run the setup
            //function on it, and input the match for that
            //match list item.
            matchListItem.Setup(match, JoinMatch);
        }

        //If we have NO matches, then tell the
        //player that we couldn't find any.
        if (matchList.Count == 0)
        {
            statusText.text = "No matches yet. Try hosting one.";
            return;
        }
    }

    private void ClearMatchList()
    {
        //Looping through all of the match list
        //items to destroy them.
        foreach (GameObject matchListItem in matchList)
        {
            Destroy(matchListItem);
        }

        matchList.Clear();
    }

    //The function that runs when our 
    //onJoinMatch callback is invoked.
    public void JoinMatch(MatchInfoSnapshot matchInfo)
    {
        //Joining the match with the properties of the match
        //provided.
        networkManager.matchMaker.JoinMatch(matchInfo.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
    }
}
