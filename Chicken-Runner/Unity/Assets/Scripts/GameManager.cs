using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winText;
    public GameObject loseText;
    public TextMeshProUGUI seedsFoundText;
    //public Texture2D nextLevelMap;


    Joystick joystick;
    GameObject character;
    GameObject objective;
    GameObject jumpButton;
    GameObject[] seeds;
    BoxCollider2D enemyCol;
    GameObject nextLevelText;

    static int test;

    [HideInInspector]
    public int numOfTimesFinished;

    //How much money we earn when we complete a level
    const int moneyWinAmount = 10;
    int levelsCompleted;

    LevelGenerator levelGen;
    PlayerController controller;

    void Awake()
    {
        gameObject.tag = "GameController";
        numOfTimesFinished = PlayerPrefs.GetInt("numOfTimesFinished", 0);
    }

    private void Start()
    {
        test++;

        //It's complicated, but I don't want any errors, so only
        //assign it if we find an instance of the find.
        if (GameObject.FindGameObjectWithTag("LevelGen") != null)
        {
            levelGen = GameObject.FindGameObjectWithTag("LevelGen").GetComponent<LevelGenerator>();
        }
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            enemyCol = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BoxCollider2D>();
        }
        objective = GameObject.FindGameObjectWithTag("Finish");
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            character = GameObject.FindGameObjectWithTag("Player");
            controller = character.GetComponent<PlayerController>();
        }
        if (GameObject.FindGameObjectWithTag("Joystick") != null)
        {
            joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        }
        if (GameObject.FindGameObjectWithTag("JumpButton"))
        {
            jumpButton = GameObject.FindGameObjectWithTag("JumpButton");
        }
        seeds = GameObject.FindGameObjectsWithTag("Finish");
#if UNITY_EDITOR || UNITY_STANDALONE
        if (joystick != null)
        {
            joystick.gameObject.SetActive(false);
            jumpButton.gameObject.SetActive(false);
        }
#endif
    }

    void Update()
    {
        if (seedsFoundText != null)
        {
            seedsFoundText.text = "SEEDS FOUND: " + PlayerPrefs.GetInt("NumOfSeedsCaught") + "/" + seeds.Length;
        }
    }


    public IEnumerator Win()
    {
        if (enemyCol != null)
        {
            enemyCol.enabled = false;
        }
        

        winText.SetActive(true);

        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            while (!winText.activeSelf)
            {
                yield return null;
            }
            nextLevelText = GameObject.FindGameObjectWithTag("NextLevelText");
            if (nextLevelText != null) //To prevent errors.
            {
                nextLevelText.SetActive(false);
            }

        }
        joystick.gameObject.SetActive(false);
        jumpButton.SetActive(false);
        Time.timeScale = 0.0f;

        int sceneOn = SceneManager.GetActiveScene().buildIndex - 2;


        if (sceneOn - PlayerPrefs.GetInt("numOfTimesFinishedLevel" + sceneOn) < 1)
        {
            GiveCoins(1);
        }
        else
        {
            GiveCoins(sceneOn - PlayerPrefs.GetInt("numOfTimesFinishedLevel" + sceneOn));
        }

        PlayerPrefs.SetInt("numOfTimesFinishedLevel" + sceneOn, PlayerPrefs.GetInt("numOfTimesFinishedLevel" + sceneOn, 0) + 1);
        PlayerPrefs.SetInt("numOfTimesFinished", numOfTimesFinished + 1);
        

    }


    public void Lose()
    {
        loseText.SetActive(true);
        joystick.gameObject.SetActive(false);
        jumpButton.SetActive(false);
        Time.timeScale = 0.0f;
        PlayerPrefs.SetInt("numOfTimesFinished", numOfTimesFinished + 1);
    }

    public void GiveCoins(int amount)
    {
        PlayerPrefs.SetInt("MoneyAmount", PlayerPrefs.GetInt("MoneyAmount") + amount);
    }
}
