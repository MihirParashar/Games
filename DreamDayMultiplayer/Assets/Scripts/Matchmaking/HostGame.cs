using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class HostGame : MonoBehaviour
{
    #region Variables
    private const string defaultMatchName = "Untitled Room";
    private string matchName = defaultMatchName;
    private uint matchSize = 5;
    private string matchPassword = "";

    [SerializeField] private TMP_InputField matchSizeInput;

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

    private void Update()
    {
        //Dynamic integers don't work in TextMeshPro
        //for input fields, so we have to do it manually.
        if (matchSizeInput.text != null && matchSizeInput.text != "")
        {
            SetMatchSize((uint)int.Parse(matchSizeInput.text));
        }
    }

    //Function that sets our match name to 
    //the specified match name inputed.
    public void SetMatchName(string _matchName)
    {
        //We only want to change our match name
        //if the field is not empty. If it is,
        //then just set it to the default.
        if (matchName != "") {
            matchName = _matchName;
        } else {
            matchName = defaultMatchName;
        }
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

    public void CreateMatch()
    {
        //Actually create our match.
        networkManager.matchMaker.CreateMatch(matchName, matchSize, matchAdvertise:true, matchPassword, "", "", 0, 0, networkManager.OnMatchCreate);
    }
}
