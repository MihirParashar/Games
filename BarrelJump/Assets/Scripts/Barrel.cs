using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameManager gameManager;
    public Sprite coinBarrelSprite;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float coinBarrelRandom = Random.Range(0f, 1f);
        if (coinBarrelRandom < 0.2)
        {

            gameObject.GetComponent<SpriteRenderer>().sprite = coinBarrelSprite;
            gameObject.name = "Barrel - Gold";
            PlayerPrefs.SetInt("MoneyAmount", PlayerPrefs.GetInt("MoneyAmount") + 1);
        }

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameManager.Die();
            
        }
    }

    

   
}
