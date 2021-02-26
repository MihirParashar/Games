using UnityEngine;
using System.Collections.Generic;
using System;

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

    private Camera sceneCamera;


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
    }

    //Creating a public method to enable or disable our scene camera.
    public void SetSceneCameraActive (bool active)
    {
       sceneCamera.gameObject.SetActive(active);
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
