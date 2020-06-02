using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockerItems : MonoBehaviour
{
    [HideInInspector]
    public int itemSelected;
    [HideInInspector]
    public bool noEquipButtonsEnabled = true;

    public Button[] equipButtons;
    public ShopControl shopControl;
    int[] buttonInteractable;

    public TextMeshProUGUI moneyAmountText;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    void Start()
    {
       
        buttonInteractable = new int[shopControl.itemLength];
    }

    public void EquipItem(int itemIndex)
    {
        itemSelected = itemIndex;
        PlayerPrefs.SetInt("ItemSelected", itemSelected);
        noEquipButtonsEnabled = false;

    }

    void Update()
    {
        

            for (int i = 0; i < shopControl.itemLength; i++)
        {
            if (equipButtons[i] != null)
            {
                moneyAmountText.text = "COINS: " + shopControl.moneyAmount.ToString();
                if (PlayerPrefs.GetInt("IsItem" + i.ToString() + "Sold") == 1)
                {
                    equipButtons[i].interactable = true;
                }
                else
                {
                    equipButtons[i].interactable = false;
                }

               

                
            }
        }
        for (int j = 0; j < equipButtons.Length; j++)
        {
            if (equipButtons[j] != null)
            {
                if (equipButtons[j].interactable)
                {

                    noEquipButtonsEnabled = false;
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        if (noEquipButtonsEnabled)
        {
            PlayerPrefs.SetInt("ItemSelected", shopControl.itemLength + 1);
        } else
        {
            PlayerPrefs.SetInt("ItemSelected", itemSelected);
        }

    }


   
}
