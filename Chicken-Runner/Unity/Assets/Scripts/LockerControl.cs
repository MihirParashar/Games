using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockerControl : MonoBehaviour
{
    public Button[] equipButtons;
    public GameObject[] redeemableSkins;
    public GameObject redeemCodeButton;

    /*
     *What I want to do:
     *Disable or enable equip button if bought
     *Set Player Prefs "Skin Selected" to skin in
     * Equip function
     */
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IOS
        redeemCodeButton.SetActive(false);
        for (int i = 0; i < redeemableSkins.Length; i++)
        {
            redeemableSkins[i].SetActive(false);
        }

#endif
    }

    public void EquipItem(int index)
    {
        bool hasGivenError = false;
        Debug.Log("Correct i is: " + index);
        //So we don't display 2 errors at once
        Debug.Log(PlayerPrefs.GetInt("IsItem" + index + "Sold", 0));
        if (PlayerPrefs.GetInt("IsItem" + index + "Sold", 0) == 0)
        {
            hasGivenError = true;
            equipButtons[index].transform.parent.GetChild(1).gameObject.SetActive(true);
            Debug.Log("Buy item " + index + " first!");
        }

        if (!hasGivenError)
        {
            equipButtons[index].transform.parent.GetChild(1).gameObject.SetActive(false);
            PlayerPrefs.SetInt("SkinSelected", index);
        }
    }
}
