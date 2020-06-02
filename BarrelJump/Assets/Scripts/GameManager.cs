using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject barrel;
    int score = 0;
    public GameObject scoreText;
    float minVelocity;
    float maxVelocity;
    float velocity;
    GameObject barrelGO;
    public GameObject deathText;
    public GameObject player;
    public GameObject lowEnergyText;
    public Sprite deathSprite;
    public Sprite normalSprite;
    public Slider energySlider;
    public GameObject PauseButton;
    public Animator GameOverAnimator;
    public GameObject characterSprite;
    public Sprite coinBarrelSprite;
    public TextMeshProUGUI moneyAmountText;
    public TextMeshProUGUI difficultyText;
    public Material darkSkyboxMat;
    public Material lightSkyboxMat;
    public int itemLength;
    public float barrelSpawnRate = 2f;
     
    int highScore = 0;
    public GameObject highScoreText;
    public Image sliderFill;
    [HideInInspector]
    public float energyBurnAmount;
    [HideInInspector]
    public float energyReplenAmount;
    GameObject previousSprite;
    public GameObject bossDude;
    public GameObject[] playerSprites;
    int difficultyInt;
    [HideInInspector]
    public string difficultyString;



    void Start()
    {

        difficultyInt = PlayerPrefs.GetInt("DifficultyInt");
        if (difficultyInt == 3)
        {
            difficultyString = "Easy";
        }  else if (difficultyInt == 2)
        {
            difficultyString = "Medium";
        }
        else if (difficultyInt == 1)
        {
            difficultyString = "Hard";
        }
        else if (difficultyInt == 0)
        {
            difficultyString = "BOSS";
        }
            difficultyText.text = "Difficulty: " + difficultyString;

        if (!PlayerPrefs.HasKey("DifficultyInt"))
        {
            PlayerPrefs.SetInt("DifficultyInt", 3);
        }

        //if difficultyInt = 3, difficulty is easy, difficultyInt = 2 is medium, etc.
        if (difficultyInt == 3)
        {
            RenderSettings.skybox = lightSkyboxMat;
            bossDude.SetActive(false);
            minVelocity = 3f;
            maxVelocity = 10f;
        }
        else if (difficultyInt == 2)
        {
            RenderSettings.skybox = lightSkyboxMat;
            bossDude.SetActive(false);
            minVelocity = 3f;
            maxVelocity = 12f;
        }
        else if (difficultyInt == 1)
        {
            RenderSettings.skybox = lightSkyboxMat;
            bossDude.SetActive(false);
            minVelocity = 1f;
            maxVelocity = 15f;
        } else if (difficultyInt == 0)
        {
            RenderSettings.skybox = darkSkyboxMat;
            bossDude.SetActive(true);
            minVelocity = 7f;
            maxVelocity = 15f;

        }

        for (int i = 0; i < itemLength; i++)
        {
            if (playerSprites[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder != PlayerPrefs.GetInt("ItemSelected"))
            {
                continue;
            } else if (playerSprites[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder == PlayerPrefs.GetInt("ItemSelected"))
            {
                if (previousSprite != null)
                {
                    if (previousSprite.name != "Character")
                    {
                        playerSprites[i] = previousSprite;
                    }
                    previousSprite.SetActive(true);
                    characterSprite.SetActive(false);
                } else
                { 
                    characterSprite.SetActive(false);
                }
                
                playerSprites[i].gameObject.SetActive(true);
                previousSprite = playerSprites[i];
               
                break;
            }
        }
        if (!PlayerPrefs.HasKey("ItemSelected"))
        {   
            characterSprite.SetActive(true);
        }

             
        scoreText.GetComponent<TextMeshProUGUI>().text = "SCORE: 0";
        StartCoroutine(SpawnBarrel());
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.GetComponent<TextMeshProUGUI>().text = "HIGH: " + highScore.ToString();
    }

    void Update()
    {
        if (energySlider.value <= 7)
        {
            sliderFill.color = Color.red;
            lowEnergyText.SetActive(true);
        } else
        {
            sliderFill.color = Color.green;
            lowEnergyText.SetActive(false);
        }
        if (Time.timeScale == 0.0f)
        {
            return;
        }
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.GetComponent<TextMeshProUGUI>().text = "HIGH: " + highScore.ToString();
        }
        barrelGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(-velocity * Time.deltaTime * 50, 0f));

    }

    public void BurnFuel(float burnAmount)
    {   
            energySlider.value -= burnAmount;
    }

    public void AddFuel(float replenAmount)
    {
        energySlider.value += replenAmount;
    }

    IEnumerator SpawnBarrel()
    {
        while (true)
        {
            if (difficultyInt == 0)
            {
                barrelSpawnRate = Random.Range(0.75f, 1.5f);
            }
            velocity = Random.Range(minVelocity, maxVelocity);
            barrelGO = Instantiate(barrel, new Vector2(7f, 1f), Quaternion.identity);
            barrelGO.GetComponent<Barrel>().gameManager = gameObject.GetComponent<GameManager>();
            score += 10;
            scoreText.GetComponent<TextMeshProUGUI>().text = "SCORE: " + score;
            AddFuel(energyReplenAmount);

            yield return new WaitForSeconds(barrelSpawnRate);
            Destroy(barrelGO, 5f);

        }
    }

    public void Die()
    { 

        player.GetComponent<SpriteRenderer>().sprite = deathSprite;
        energySlider.gameObject.SetActive(false);
        PauseButton.SetActive(false);
        lowEnergyText.SetActive(false);

        deathText.SetActive(true);

        Time.timeScale = 0.0f;
            
    }

    public void Respawn()
    {
        deathText.SetActive(false);
        energySlider.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.GetComponent<SpriteRenderer>().sprite = normalSprite;
        Time.timeScale = 1.0f;
    }


}
    



