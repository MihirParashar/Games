using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;  

public class GameManager : NetworkBehaviour
{
    [Header("Scoreboard")]
    [SerializeField] public GameObject scoreboardItemPrefab;
    [SerializeField] public Transform scoreboardItemParent;

    [Header("Match Settings")]
    public MatchSettings matchSettings;

    //Creating a static instance of itself, also known as a
    //singleton.
    public static GameManager instance;

    //Creating a dictionary to store the list of player IDs and
    //their player components.
    public static Dictionary<string, Player> players = new Dictionary<string, Player>();

    //Creating a list that stores all of the kills for a 
    //scoreboard. 
    private static List<KillInfo> scoreboardKills = new List<KillInfo>();

    //Syncing the time left in the match to all players.
    [SyncVar] private float timeLeftInMatch;

    private Camera sceneCamera;
    private NetworkManager networkManager;
    private bool hasRoundEnded = false;

    public static void AddKill(KillInfo killInfo) {
        //Find the player that killed the other player from the
        //kill info provided, and add to that player's kill count.
        killInfo.playerKiller.AddToKillCount();

        //Adding this KillInfo to the scoreboard of kills.
        scoreboardKills.Add(killInfo);
    }

    private void Start()
    {
        //Caching our networkManager instance for efficiency.
        networkManager = NetworkManager.singleton;

        //Assigning our scene camera.
        sceneCamera = Camera.main;

        if (instance == null)
        {
            instance = this;
        } else
        {
            //If the instance is already assigned, then that means
            //there is a duplicate GameManager, so log an error.
            Debug.LogError("GameManager: More than one GameManager in the scene!");
        }

        //Starting our round timer to the amount specified in our
        //match settings.
        timeLeftInMatch = matchSettings.roundTimeSeconds;
    }

    private void Update() {
        //If we are the server, we can directly call
        //the client RPC function. If not, we first
        //have to call the command that gets sent
        //to the server.
        if (isServer) {
            RpcReduceTimeLeft();
        } else {
            CmdReduceTimeLeft();
        }

        //If the round has not ended yet, then
        //update our timer to how much time is
        //left in minutes and seconds.
        if (!hasRoundEnded) {
            string minutesLeft = Mathf.Floor(timeLeftInMatch / 60f).ToString("00");
            string secondsLeft = Mathf.RoundToInt(timeLeftInMatch % 60f).ToString("00");
            PlayerUI.instance.SetRoundTimerText(minutesLeft + ":" + secondsLeft);
        }
    }

    //Function that is sent from the client to the server to
    //reduce the time left in the match.
    [Command]
    private void CmdReduceTimeLeft() {
        RpcReduceTimeLeft();
    }

    //Function that is sent from the server to all clients to
    //reduce the time left in the match.
    [ClientRpc]
    private void RpcReduceTimeLeft() {

        //If we have more than 0 seconds left in our match,
        //decrease it more. Otherwise, if our round hasn't
        //ended already, then end it.
        if (timeLeftInMatch > 0) {
            timeLeftInMatch -= Time.deltaTime;
        } else if (!hasRoundEnded && timeLeftInMatch <= 0) 
        {
            StartCoroutine(EndRound());
        }
    }

    //Creating a public method to enable or disable our scene camera.
    public void SetSceneCameraActive (bool active)
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(active);
        }
    }

    //Function that ends our round.
    private IEnumerator EndRound() {
    
        hasRoundEnded = true;

        //Set our text to whoever has the most kills.
        PlayerUI.instance.SetRoundTimerText("Winner: " + FindPlayerWithMostKills());

        //Wait until ending the game.
        yield return new WaitForSeconds(5f);
        
        //Get a reference of the current info of our match.
        MatchInfo currentMatchInfo = networkManager.matchInfo;

        //End/destroy the match.
        networkManager.matchMaker.DestroyMatch(currentMatchInfo.networkId, 0, OnDestroyMatch);
        networkManager.StopHost();
        networkManager.StopMatchMaker();
    }

    private void OnDestroyMatch(bool success, string extendedInfo) {
        Debug.Log("Successful: " + success + "... Reason: " + extendedInfo);
    }

    //Method that loops through all of the registered player IDs and
    //returns the player ID that corresponds to the player with the
    //highest kills.
    private string FindPlayerWithMostKills() {

        int highestKillCount = 0;
        string playerIDWithMostKills = "Nobody";

        //Looping through all of the players, and checking if their
        //kill count is higher than the highest kill count so far.
        //If it is, then store that player and their ID.
        foreach (string playerID in players.Keys)
        {
            if (GetPlayer(playerID).GetKillCount() > highestKillCount) {
                playerIDWithMostKills = playerID;
            }
        }

        return playerIDWithMostKills;
    }

    //Function that disconnects a player
    //from the match.
    public void DisconnectPlayer()
    {
        MatchInfo currentMatchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(currentMatchInfo.networkId, currentMatchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }



    #region Player Registering
    //A function that adds a player with the specified network ID and 
    //Player component to the dictionary of players.
    public static void RegisterPlayer(string registerPlayerID, Player player)
    {
        //Adding the player to the dictionary, or in other words,
        //registering them.
        players.Add(registerPlayerID, player);

        //Setting the player's name in the editor to their player
        //ID, so we can more easily recognize which player is which.
        player.transform.name = registerPlayerID;
    }

    //A function that does the opposite of what our RegisterPlayer
    //function does: remove the player with the specified ID from 
    //the dictionary of players.
    public static void UnregisterPlayer(string unregisterPlayerID)
    {
        players.Remove(unregisterPlayerID);
    }

    //A method that returns us the player that has the given player
    //ID.
    public static Player GetPlayer(string getPlayerID)
    {
        return players[getPlayerID];
    }
    #endregion
}
