using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    int moneyAmount;
    int[] isItemSold;
    public int[] itemPrice;


    public TextMeshProUGUI moneyAmountText;
    public ShopControl shopControl;
    public TextMeshProUGUI[] itemPriceText;
    public Button[] buyButton;

    // Start is called before the first frame update
    void Start()
    {
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        isItemSold = new int[shopControl.itemLength];

    }





    public void buyItem(int itemIndex)
    {
        moneyAmount -= itemPrice[itemIndex];
        moneyAmountText.text = "COINS: " + moneyAmount.ToString();
        gameObject.GetComponent<LockerItems>().noEquipButtonsEnabled = false;

        PlayerPrefs.SetInt("IsItem" + itemIndex + "Sold", 1);
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);

        itemPriceText[itemIndex].text = "Sold";
        buyButton[itemIndex].gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buyButton != null && moneyAmountText != null)
        {
            moneyAmountText.text = "COINS: " + moneyAmount.ToString();
            for (int i = 0; i < shopControl.itemLength; i++)
            {
                isItemSold[i] = PlayerPrefs.GetInt("IsItem" + i.ToString() + "Sold");
                if (moneyAmount >= itemPrice[i] && isItemSold[i] == 0)
                {
                    buyButton[i].interactable = true;
                }
                else
                {
                    buyButton[i].interactable = false;
                }
            }
            

          
        }
    }

}
