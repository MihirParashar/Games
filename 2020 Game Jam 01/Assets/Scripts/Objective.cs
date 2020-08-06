using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private GameManager gameManager;

    private bool hasWon = false;

    private void Start()
    {
        //Initialie the game manager.
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !hasWon)
        {
            hasWon = true;
            gameManager.Win();
            Debug.Log("Player won");
        }
    }
}
