using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{
    public int moneyAmount;
    public int itemPrice;

    public TextMeshProUGUI moneyAmountText;
    public TextMeshProUGUI itemPriceText;
    public Button buyButton;

    public GameObject loadingScreen;
    public Slider progressSlider;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI loadingText;

    public LevelLoader levelLoader;

    public GameObject LockerPanel;
    public GameObject ShopPanel;
    public int itemLength;

  
    void Update()
    {
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
    }


    void OnApplicationQuit()
    {

        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
    }


    public void ExitShop()
    {
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        levelLoader.LoadLevel(1, loadingScreen, progressSlider, progressText);
    }

    public void Reset()
    {
        //Temporary for testing purposes
        for (int i = 0; i < itemLength; i++)
        {
            PlayerPrefs.SetInt("IsItem" + i.ToString() + "Sold", 0);
        }
        PlayerPrefs.SetInt("MoneyAmount", 0);
    }

    public void EnterLocker()
    {
        ShopPanel.SetActive(false);
        LockerPanel.SetActive(true);
    }

    public void ExitLocker()
    {
        LockerPanel.SetActive(false);
        ShopPanel.SetActive(true);
    }
}
