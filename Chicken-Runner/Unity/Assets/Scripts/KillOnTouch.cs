using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    GameManager gameManager;
    GameObject[] objectsArePlayer;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        objectsArePlayer = GameObject.FindGameObjectsWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameManager.Lose();
        }
    }
}
