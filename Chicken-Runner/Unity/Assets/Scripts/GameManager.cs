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

    [HideInInspector]
    public int numOfTimesFinished;


    //Now we can know if the game ended,
    //and whether we won or lost.
    [HideInInspector]
    public bool hasEndedGame = false;

    [HideInInspector]
    public bool hasLostGame = false;

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
        PlayerPrefs.SetInt("NumOfSeedsCaught", 0);


        //Change tutorial based on platform you are on
#if UNITY_STANDALONE
        //If we are on level 1
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameObject.FindGameObjectWithTag("Tutorial").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "USE \"WASD\" KEYS TO MOVE. PRESS SPACE TO JUMP.  THE AMOUNT OF COINS YOU GET WHEN YOU FINISH A LEVEL VARIES BASED ON THE LEVEL AND THE NUMBER OF TIMES YOU'VE COMPLETED IT . COLLECT THE BAG OF SEEDS TO PROCEED TO THE NEXT LEVEL.";
        }
#endif

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
            if (SceneManager.GetActiveScene().name != "Infinite")
            {
                seedsFoundText.text = "SEEDS FOUND: " + PlayerPrefs.GetInt("NumOfSeedsCaught") + "/" + seeds.Length;
            } else
            {
                seedsFoundText.text = "SCORE: " + PlayerPrefs.GetInt("NumOfSeedsCaught");
            }
        }

        //if (character.transform.position.y <= -20 && SceneManager.GetActiveScene().name == "Infinite")
        //{
        //    Lose();
        //}
    }


    public IEnumerator Win()
    {
        hasEndedGame = true;
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


        if (sceneOn - PlayerPrefs.GetInt("numOfTimesFinishedLevel" + sceneOn, 0) < 1)
        {
            GiveCoins(1);
        }
        else
        {
            GiveCoins(sceneOn - PlayerPrefs.GetInt("numOfTimesFinishedLevel" + sceneOn, 0));
        }
        if (sceneOn == PlayerPrefs.GetInt("LevelOn", 1))
        {
            PlayerPrefs.SetInt("LevelOn", PlayerPrefs.GetInt("LevelOn", 1) + 1);
        }

        Debug.Log("numOfTimesFinishedLevel" + sceneOn + ": " + PlayerPrefs.GetInt("numOfTimesFinishedLevel" + sceneOn, 0));

        PlayerPrefs.SetInt("numOfTimesFinishedLevel" + sceneOn, PlayerPrefs.GetInt("numOfTimesFinishedLevel" + sceneOn, 0) + 1);
        PlayerPrefs.SetInt("numOfTimesFinished", numOfTimesFinished + 1);
    }


    public void Lose()
    {
        hasEndedGame = true;
        hasLostGame = true;
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
