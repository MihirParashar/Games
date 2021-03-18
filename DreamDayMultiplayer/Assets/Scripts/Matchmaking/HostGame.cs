using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{
    #region Variables
    private uint matchSize = 5;
    private string matchName = "default";
    private string matchPassword = "";

    private NetworkManager networkManager;
    #endregion

    private void Start()
    {
        //Caching our variable for more efficiency.
        networkManager = NetworkManager.singleton;

        //If we haven't started a matchmaker yet,
        //then start it manually.
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    //Function that sets our match name to 
    //the specified match name inputed.
    public void SetMatchName(string _matchName)
    {
        matchName = _matchName;
    }

    //Function that sets our match size to 
    //the specified match size inputed.
    public void SetMatchSize(uint _matchSize)
    {
        matchSize = _matchSize;
    }

    //Function that sets our match password to 
    //the specified match password inputed.
    public void SetMatchPassword(string _matchPassword)
    {
        matchPassword = _matchPassword;
    }

    public void Creatematch()
    {
        //If our match name actually exists, then create our match.
        if (matchName != "" && matchName != null)
        {
            Debug.Log("Creating match " + matchName + "with " + matchSize + " max players.");

            networkManager.matchMaker.CreateMatch(matchName, matchSize, matchAdvertise:true, matchPassword, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
    }
}
