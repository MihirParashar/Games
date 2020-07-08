using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{

    GameManager gameManager;
    ParticleSystem ps;

    List<GameObject> seeds;
    GameObject[] seedsInScene;

    bool isSeedCaught;
    bool allSeedsCaught;
    bool hasWon = false;

    int timesHitPlayer = 0;

    void Start()
    {

        seeds = new List<GameObject>();
        seedsInScene = GameObject.FindGameObjectsWithTag("Finish");

        ps = GetComponent<ParticleSystem>();

        for (int i = 0; i < seedsInScene.Length; i++)
        {
            seeds.Add(seedsInScene[i]);
        }
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }   

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            timesHitPlayer++;
            if (timesHitPlayer < 2)
            {
                ps.Play();
                //Disabling renderer for sprite, in other words, making it invisible
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                isSeedCaught = true;
                PlayerPrefs.SetInt("NumOfSeedsCaught", PlayerPrefs.GetInt("NumOfSeedsCaught", 0) + 1);

                //If on infinite mode, give coins.
                if (SceneManager.GetActiveScene().name == "Infinite")
                {
                    gameManager.GiveCoins(1);
                }
            }
        }
    }

    private void Update()
    {
        //So it only runs once
        if (SceneManager.GetActiveScene().name != "Infinite")
        {
            if (!hasWon)
            {
                allSeedsCaught = true;
                foreach (GameObject seed in seeds)
                {
                    if (seed != null)
                        if (!seed.GetComponent<Objective>().isSeedCaught)
                        {
                            allSeedsCaught = false;
                        }
                }
                if (allSeedsCaught)
                {
                    gameManager.StartCoroutine(gameManager.Win());
                    allSeedsCaught = false;
                    hasWon = true;
                }
            }
        }
        
    }
}
