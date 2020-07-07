using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    SpriteRenderer playerSprite;
    public Sprite[] skinSprites;

    

    private void Start()
    {

        //Debug.Log("Skin Selected: " + PlayerPrefs.GetInt("SkinSelected", 999));
        playerSprite = GetComponent<SpriteRenderer>();
        Debug.Log(PlayerPrefs.GetInt("SkinSelected", 0));
        playerSprite.sprite = skinSprites[PlayerPrefs.GetInt("SkinSelected", 0)];
        //Debug.Log("Current sprite is: " + playerSprite.sprite);
        
        
    }

    private void Update()
    {
        playerSprite.sprite = skinSprites[PlayerPrefs.GetInt("SkinSelected", 0)];
    }
}
