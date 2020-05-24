using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedeemSkin : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public GameObject redeemSuccessText;
    public GameObject redeemFailText;

    ShopControl shopControl;
    string codeEntered;
    string codeToRedeem = "langskinget2478​";

    private void Start()
    {
        shopControl = GetComponent<ShopControl>();
    }

    public void redeemSkin(int skinIndex)
    {
        codeEntered = codeText.text;
        if (codeEntered.Equals(codeToRedeem))
        {
            //They can redeem the skin
            PlayerPrefs.SetInt("IsItem" + skinIndex + "Sold", 1);
            StartCoroutine(showRedeemText(true));
        } else
        {
            StartCoroutine(showRedeemText(false));
        }
    }

    IEnumerator showRedeemText(bool isRedeemSuccesful)
    {

        if (isRedeemSuccesful)
        {
            if (redeemFailText.activeInHierarchy)
            {
                redeemFailText.SetActive(false);
            }
            redeemSuccessText.SetActive(true);
            yield return new WaitForSeconds(3f);
            redeemSuccessText.SetActive(false);
        } else
        { 
            if (redeemSuccessText.activeInHierarchy)
            {
                redeemSuccessText.SetActive(false);
            }
            redeemFailText.SetActive(true);
            yield return new WaitForSeconds(3f);
            redeemFailText.SetActive(false);
        }
    }
}

