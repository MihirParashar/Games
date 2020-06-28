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
    public TextMeshProUGUI highscoreText;
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

    public static List<PotionItem> potionsInEffect = new List<PotionItem>();

    //How much money we earn when we complete a level
    const int moneyWinAmount = 10;
    float timeUntilPotionEnds;

    float originalJumpAmount;

    int potionIndex;

    LevelGenerator levelGen;
    PlayerController controller;
    PlayerMovement movement;
    private bool potionEffectGiven;

    void Awake()
    {
        gameObject.tag = "GameController";
        numOfTimesFinished = PlayerPrefs.GetInt("numOfTimesFinished", 0);

        controller = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        movement = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        PlayerPrefs.SetInt("NumOfSeedsCaught", 0);
        PlayerPrefs.SetInt("HasJumpBoostPotionEffect", 0);
        PlayerPrefs.SetInt("HasSpeedBoostPotionEffect", 0);


        //Change tutorial based on platform you are on
#if UNITY_STANDALONE
        //If we are on level 1
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameObject.FindGameObjectWithTag("Tutorial").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "USE \"A\" AND \"D\" KEYS TO MOVE. PRESS SPACE TO JUMP.  THE AMOUNT OF COINS YOU GET WHEN YOU FINISH A LEVEL VARIES BASED ON THE LEVEL AND THE NUMBER OF TIMES YOU'VE COMPLETED IT . COLLECT THE BAG OF SEEDS TO PROCEED TO THE NEXT LEVEL.";
        }
#endif
        #region Initializing Variables
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

        #endregion
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
        while (originalJumpAmount == 0f)
        {
            originalJumpAmount = controller.m_JumpForce;
        }

        if (seedsFoundText != null)
        {
            if (SceneManager.GetActiveScene().name != "Infinite")
            {
                seedsFoundText.text = "SEEDS FOUND: " + PlayerPrefs.GetInt("NumOfSeedsCaught") + "/" + seeds.Length;
            } else
            {
                if (PlayerPrefs.GetInt("NumOfSeedsCaught", 0) > PlayerPrefs.GetInt("Highscore", 0))
                {
                    PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("NumOfSeedsCaught"));
                }
                seedsFoundText.text = "SCORE: " + PlayerPrefs.GetInt("NumOfSeedsCaught");
                highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("Highscore", 0);
            }
        }
        if (character != null)
        {
            if (character.GetComponent<Rigidbody2D>().velocity.y < -30 && character.transform.position.y < 0 && SceneManager.GetActiveScene().name == "Infinite")
            {
                Lose();
            }
        }



        #region Potions

        for (int i = 0; i < potionsInEffect.Count; i++)
        {
            if (potionsInEffect[i].potionStats.potionType == Potion.PotionTypes.jumpBoost)
            {
                //So it only runs if potion is in effect and we have not already applied extra force.
                if (originalJumpAmount == controller.m_JumpForce)
                {
                    if (!potionsInEffect[i].hasAppliedEffects)
                    {
                        potionsInEffect[i].hasAppliedEffects = true;
                        timeUntilPotionEnds = potionsInEffect[i].potionStats.effectTime;
                        controller.m_JumpForce *= potionsInEffect[i].potionStats.effectMultiplier;
                        //So we know what index to use when de-applying force.
                        potionIndex = i;
                    }

                } else
                {   
                    if (!potionsInEffect[i].hasAppliedEffects)
                    {
                        potionsInEffect[i].hasAppliedEffects = true;
                        //If potion is added but we have already applied extra force.
                        timeUntilPotionEnds += potionsInEffect[i].potionStats.effectTime;
                    }
                }
            }
        }

        if (timeUntilPotionEnds > 0f)
        {
            timeUntilPotionEnds -= Time.deltaTime;
            if (timeUntilPotionEnds <= 0f)
            {
                //Potion ended.
                controller.m_JumpForce /= potionsInEffect[potionIndex].potionStats.effectMultiplier;
                potionsInEffect.RemoveAt(potionIndex);
            }
        }


        #endregion
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
