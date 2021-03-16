using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public MatchSettings matchSettings;

    //Creating a static instance of itself, also known as a
    //singleton.
    public static GameManager instance;

    //The string that will be added before the player's network
    //ID in order to create a player ID.
    private const string playerIDPrefix = "Player ";

    //Creating a dictionary to store the list of player IDs and
    //their player components.
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    //Creating a list that stores all of the kills for a 
    //scoreboard. 
    private static List<KillInfo> scoreboardKills = new List<KillInfo>();

    private Camera sceneCamera;

    public static void AddKill(KillInfo killInfo) {
        //Find the player that killed the other player from the
        //kill info provided, and add to that player's kill count.
        killInfo.playerKiller.AddToKillCount();

        //Adding this KillInfo to the scoreboard of kills.
        scoreboardKills.Add(killInfo);
    }

    private void Start()
    {
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

        //Invoking our method to find the player with the most kills
        //every time our round ends.
        InvokeRepeating("FindPlayerWithMostKills", matchSettings.roundTimeSeconds, matchSettings.roundTimeSeconds);
    }

    //Creating a public method to enable or disable our scene camera.
    public void SetSceneCameraActive (bool active)
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(active);
        }
    }

    //A simple function that loops through all of the players
    //and finds the player with the highest amount of kills. 
    private void FindPlayerWithMostKills() {

        int highestKillCount = 0;

        Player playerWithMostKills = null;
        string playerWithMostKillsID = null;

        int i = 0;

        //Looping through all of the players, and checking if their
        //kill count is higher than the highest kill count so far.
        //If it is, then store that player and their ID.
        foreach (Player player in players.Values)
        {
            if (player.GetKillCount() > highestKillCount) {
                highestKillCount = player.GetKillCount();
                playerWithMostKills = player;
                playerWithMostKillsID = players.Keys.ToArray()[i];
            }
            i++;
        }
        //If the highest kill count is more than 1, make the player with
        //that number of kills win. Otherwise, nobody wins.
        if (highestKillCount > 0) {
            //Debug.LogError("Player with most kills: " + playerWithMostKillsID + ", with " + highestKillCount + " kills.");
        } else {
            //Debug.LogError("Nobody has won yet.");
        }
    }

    #region Player Registering
    //A function that adds a player with the specified network ID and 
    //Player component to the dictionary of players.
    public static void RegisterPlayer(string networkID, Player player)
    { 
        //Creating the player ID based on the network ID given and
        //the prefix created in the top of this script.
        string playerID = playerIDPrefix + networkID;

        //Adding the player to the dictionary, or in other words,
        //registering them.
        players.Add(playerID, player);

        //Setting the player's name in the editor to their player
        //ID, so we can more easily recognize which player is which.
        player.transform.name = playerID;
    }

    //A function that does the opposite of what our RegisterPlayer
    //function does: remove the player with the specified ID from 
    //the dictionary of players.
    public static void UnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    //A method that returns us the player that has the given player
    //ID.
    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }
    #endregion
}
