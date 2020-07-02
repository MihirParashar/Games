using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopControl : MonoBehaviour
{

    public Button[] buyButtons;

    public GameObject julyHolidaySkinBuy;
    public GameObject julyHolidaySkinEquip;

    public TextMeshProUGUI moneyAmountText;

    public LevelLoader levelLoader;

    /*
     * What I will do:
     * 
     * 
     * 
     * 
     * Create function: buy(cost, index)
     * Make MoneyAmount PlayerPref itself - cost.    
     * 
     * 
     *
     *On Update, set text to moneyAmount
     * 
     */

    private void Start()
    {
        //Temporary
        //PlayerPrefs.SetInt("MoneyAmount", 100);
        //The default skin should always be available.
        PlayerPrefs.SetInt("IsItem0Sold", 1);

        #region Seasonal Items
        if (julyHolidaySkinBuy != null)
        {
            if (RemoteConfig.isJulyHoliday)
            {
                julyHolidaySkinBuy.SetActive(true);
                julyHolidaySkinEquip.SetActive(true);
            }
            else
            {
                julyHolidaySkinBuy.SetActive(false);
                julyHolidaySkinEquip.SetActive(false);
            }
        }
        #endregion

    }
    private void Update()
    {
        moneyAmountText.text = "COINS: " + PlayerPrefs.GetInt("MoneyAmount", 0).ToString();
    }
    //Mashing cost and index into one integer, because Unity does not support multiple
    //parameters for onClick event.
    public void BuySkin(int costIndex)
    {
        float costIndexSplit = costIndex / 1000f;

        //Logs in case cost index split does not work.
        //Debug.Log("costIndexSplit: " + costIndexSplit);
        //Debug.Log("costIndexSplit[0]: " + costIndexSplit.ToString("").Split('.')[0]);
        //Debug.Log("costIndexSplit[1]: " + costIndexSplit.ToString("0.000").Split('.')[1]);
        
        int cost = int.Parse(costIndexSplit.ToString("").Split('.')[0]);
        int index = int.Parse(costIndexSplit.ToString("0.000").Split('.')[1]);
        bool hasGivenError = false;

            //So we don't display 2 errors at once
            if (PlayerPrefs.GetInt("IsItem" + index + "Sold") == 1 && !hasGivenError)
            {
                Debug.Log("Already bought item!");
                hasGivenError = true;
                buyButtons[index].transform.parent.GetChild(3).gameObject.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("MoneyAmount", 0) < cost && !hasGivenError)
            {
                hasGivenError = true;
                buyButtons[index].transform.parent.GetChild(2).gameObject.SetActive(true);
            }
            
       

        if (!hasGivenError)
        {
            Debug.Log("Item bought!");
            PlayerPrefs.SetInt("IsItem" + index + "Sold", 1);
            PlayerPrefs.SetInt("SkinSelected", index);
            PlayerPrefs.SetInt("MoneyAmount", PlayerPrefs.GetInt("MoneyAmount", 0) - cost);
        }
    }



    //This function is temporary, for testing during develeopment builds
    public void Reset()
    {
        PlayerPrefs.SetInt("numOfTimesAdWatched", 0);
        for (int i = 0; i < buyButtons.Length; i++)
        {
            if (buyButtons[i] != null)
            {
                buyButtons[i].gameObject.SetActive(true);
            }
        }


        //The lines of code below are as dangerous as they sound.
        //DO NOT RUN THIS FUNCTION UNLESS YOU KNOW WHAT YOU ARE DOING!
        PlayerPrefs.SetInt("MoneyAmount", 0);
        PlayerPrefs.SetInt("SkinSelected", 0);
        for (int i = 0; i < buyButtons.Length; i++)
        {
            PlayerPrefs.SetInt("IsItem" + i + "Sold", 0);
        }

        PlayerPrefs.SetInt("IsItem0Sold", 1);

    }
}
