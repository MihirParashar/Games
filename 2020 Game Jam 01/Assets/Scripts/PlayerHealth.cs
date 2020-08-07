using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
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

        numOfHearts = 5;

        //Initialize the game manager.
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //If our PlayerPrefs.GetInt("Health", 5) is greater than our number of
        //hearts, then make the PlayerPrefs.GetInt("Health", 5) equal to the number
        //of hearts.
        if (PlayerPrefs.GetInt("Health", 5) > numOfHearts)
        {
            PlayerPrefs.SetInt("Health", numOfHearts);
        }

        //If we have PlayerPrefs.GetInt("Health", 5), set our head sprite to the
        //age sprite list on the PlayerPrefs.GetInt("Health", 5) - 1 index.
        if (PlayerPrefs.GetInt("Health", 5) > 0)
        {
            head.sprite = ageSprites[PlayerPrefs.GetInt("Health", 5) - 1];
        }

        for (int i = 0; i < hearts.Length; i++)
        {

            //If the index is less then our PlayerPrefs.GetInt("Health", 5), then
            //we have at least that much PlayerPrefs.GetInt("Health", 5), so make
            //the heart at that index a full one.
            //Otherwise, make it an empty one.
            if (i < PlayerPrefs.GetInt("Health", 5))
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
        if (PlayerPrefs.GetInt("Health", 5) <= 0 && Time.timeSinceLevelLoad > 1 && Time.timeScale != 0)
        {
            gameManager.Die();
        }
    }
}
