using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static int health;
    public static int numOfHearts;

    private GameManager gameManager;

    [Header("Hearts")]

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [Header("Age")]

    [SerializeField] private SpriteRenderer head;
    [SerializeField] private Sprite[] ageSprites;


    private void Start()
    {
        health = 5;
        numOfHearts = 5;

        //Initialize the game manager.
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //If our health is greater than our number of
        //hearts, then make the health equal to the number
        //of hearts.
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        //If we have health, set our head sprite to the
        //age sprite list on the health - 1 index.
        if (health > 0)
        {
            head.sprite = ageSprites[health - 1];
        }

        for (int i = 0; i < hearts.Length; i++)
        {

            //If the index is less then our health, then
            //we have at least that much health, so make
            //the heart at that index a full one.
            //Otherwise, make it an empty one.
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }

        //If our health is less than or 0, and our time scale is not 0, then die.
        if (health <= 0 && Time.timeSinceLevelLoad > 1 && Time.timeScale != 0)
        {
            gameManager.Die();
        }
    }
}
