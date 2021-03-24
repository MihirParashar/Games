using UnityEngine;
using UnityEngine.Networking;
using System;
using Random = System.Random;
using TMPro;

public class HostGame : MonoBehaviour
{
    #region Variables
    private const string defaultMatchName = "Untitled Room";
    private string matchName = defaultMatchName;
    private uint matchSize = 5;
    private bool hasPassword = false;
    private string matchPassword = "";

    [SerializeField] private TMP_InputField matchSizeInput;
    [SerializeField] private TextMeshProUGUI generatedPasswordInput;

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
    public void SetHasPassword(bool hasPassword)
    {
        //If we set our has password
        //input to true, then generate a random
        //password.
        if (hasPassword)
        { 
            matchPassword = GenerateRandomPassword();
            generatedPasswordInput.text = "Generated Password: " + matchPassword;
        } else
        {
            generatedPasswordInput.text = "Generated Password: [NONE]";
        }
    }

    public void CreateMatch()
    {
        //Actually create our match.
        networkManager.matchMaker.CreateMatch(matchName, matchSize, matchAdvertise:true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        //Debug.Log(matchPassword);
    }

    //Function that randomly generates a
    //string that we can use as our room
    //password (not my code).
    private string GenerateRandomPassword()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] stringChars = new char[8];
        Random random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        string generatedPassword = new String(stringChars);

        return generatedPassword;
    }


    //Creating a method to get our
    //match password from other scripts.
    public string GetMatchPassword()
    {
        return matchPassword;
    }
}
