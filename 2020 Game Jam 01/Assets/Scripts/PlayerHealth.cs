using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static int health;
    public static int numOfHearts;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Start()
    {
        health = 5;
        numOfHearts = 5;
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

        //If our health is less than or 0, die.
        if (health <= 0)
        {
            GameManager.Die();
        }
    }
}
