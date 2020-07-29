using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    #region Full Script Summary
    /*
     * WeaponSwitching Script - Created on July 28th, 2020 by Mihir Parashar
     * 
     * SUMMARY START
     * This script allows the player to switch through weapons using
     * the scroll keys, or the number keys.
     * SUMMARY END
     * 
     * Thanks to Brackeys for the tutorial!
     * Link to video here: https://www.youtube.com/watch?v=Dn_BUIVdAPg
     * 
     */
    #endregion

    #region Defining Variables
    [SerializeField] private int selectedWeapon = 0;
    #endregion

    private void Start()
    {
        //Select our weapon right when the game begins
        //so we have the right weapon.
        SwitchWeapon(selectedWeapon);
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        #region Scroll Wheel Logic
        //If we move the scroll wheel down, change the selected
        //weapon to the next one. 
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //If we are at the max selected weapon, reset it back to 0.
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            } else
            {
                selectedWeapon++;
            }
        }

        //If we move the scroll wheel up, change the selected
        //weapon to the previous one. 
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //If we are at the 0th selected weapon, reset it back to the maximum.
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        #endregion

        #region Number Key Logic
        //If we press a number key and we have a weapon at
        //that index, then set the selected weapon to the key minus 1.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selectedWeapon = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6)
        {
            selectedWeapon = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7)
        {
            selectedWeapon = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && transform.childCount >= 8)
        {
            selectedWeapon = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && transform.childCount >= 9)
        {
            selectedWeapon = 8;
        }
        #endregion

        //If our new selected weapon is different then our previously selected weapon, then switch our weapon.
        if (previousSelectedWeapon != selectedWeapon)
        {
            SwitchWeapon(selectedWeapon);
        }
    }

    private void SwitchWeapon(int weaponIndex)
    {
        //A foreach statement doesn't have an index to loop through,
        //so we create it ourselves.
        int i = 0;

        foreach (Transform weapon in transform)
        {
            //If i is our selected weapon index, then set the weapon at that index
            //active.
            if (i == weaponIndex)
            {
                weapon.gameObject.SetActive(true);
            } else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
