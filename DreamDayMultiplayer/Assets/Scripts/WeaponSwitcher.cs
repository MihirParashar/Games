using System;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponSwitcher : NetworkBehaviour
{
    #region Variables
    //The index of our currently selected weapon.
    [SyncVar] private int selectedWeapon = 0;
    
    //Creating a non-synced version of it
    //so we can edit it and then apply it to
    //the synced version.
    private int _selectedWeapon = 0;
    private int prevSelectedWeapon = 0;
    [SerializeField] private Transform itemHolder;
    #endregion

    void Start() {
        if (isLocalPlayer) {
            //If we are the server, we can call the RPC
            //function directly. If not, then we must
            //run the command which gets sent to the
            //server first.
            if (isServer) {
                RpcWeaponSelect(_selectedWeapon);
            } else {
                CmdSelectWeapon(_selectedWeapon);
            }
        }
    }

    void Update()
    {
        //If we are in the pause menu, then 
        //we don't want to do anything else
        //in this function (since it involves
        //weapon switching), so just return.
        if (PlayerUI.pauseMenuActiveState)
        {
            return;
        }

        prevSelectedWeapon = selectedWeapon;

        //We don't want to do any of this if we are not
        //the local player.
        if (!isLocalPlayer) {
            return;
        }
        
        #region Switching Weapons With Scrollwheel
        //If we are moving our scroll wheel up, then
        //switch our weapon to the next one.
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //If we are at the final weapon and we scroll
            //up again, then wrap it around (in other words
            //set it back to 0).
            if (_selectedWeapon >= itemHolder.childCount - 1)
            {
                _selectedWeapon = 0;
            }
            else
            {
                _selectedWeapon++;
            }
        }

        //If we are moving our scroll wheel down, then
        //switch our weapon to the previous one.
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            //If we are at the first weapon and we scroll
            //down again, then wrap it around (in other words
            //set it back to the final weapon).
            if (_selectedWeapon <= 0)
            {
                _selectedWeapon = itemHolder.childCount - 1;
            }
            else
            {
                _selectedWeapon--;
            }
        }
        #endregion
        
        #region Switching Weapons With Number Keys
        //If we are pressing a number key down (it is 
        //not -1), and we have enough weapons,
        //then switch our selected weapon.
        if (itemHolder.childCount >= GetNumberKeyDown() && GetNumberKeyDown() != -1) {
            _selectedWeapon = GetNumberKeyDown() - 1;
        }
        #endregion

        //We only want to run the weapon select command
        //if we actually changed our weapon.
        if (prevSelectedWeapon != _selectedWeapon)
        {
            if (isServer)
            {
                RpcWeaponSelect(_selectedWeapon);
            }
            else
            {
                CmdSelectWeapon(_selectedWeapon);
            }
        }
    }

    //Overriding our OnStartLocalPlayerMethod to add in
    //our weapon select function.
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isServer)
        {
            RpcWeaponSelect(_selectedWeapon);
        }
        else
        {
            CmdSelectWeapon(_selectedWeapon);
        }
    }

    //Creating a public method that allows us to access
    //our selected weapon index from other scripts.
    public int GetSelectedWeaponIndex()
    {
        //Debug.Log(selectedWeapon);
        return selectedWeapon;
    }

    //A method that returns what number key we are
    //currently pressing down. If we are not presssing
    //down any key, it just returns -1.
    private int GetNumberKeyDown() {
    for (int number = 0; number <= 9; number++) {
        if (Input.GetKeyDown(number.ToString()))
            return number;
    }
 
    return -1;
    }

    #region Server CommandsS
    //Function that equips our currently selected weapon.
    [Command]
    private void CmdSelectWeapon(int _selectedWeapon)
    {
        //Debug.Log("Selecting Weapon");
        RpcWeaponSelect(_selectedWeapon);
    }
    
    [ClientRpc]
    private void RpcWeaponSelect(int _selectedWeapon) {

        //Syncing our selected weapon variable.
        selectedWeapon = _selectedWeapon;

        int i = 0;
        foreach (Transform weapon in itemHolder)
        {
            //If we have found our selected weapon, 
            //enable it. If it's not, disable it.
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            } else
            {
                weapon.gameObject.SetActive(false);
            } 

            i++;
        }

    }
    #endregion
}