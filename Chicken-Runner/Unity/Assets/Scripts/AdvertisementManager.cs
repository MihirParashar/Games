using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdvertisementManager : MonoBehaviour
{
    GameManager gameManager;
    
    public GameObject adButton;
    public GameObject adLimitText;
    int numOfTimesAdWatched;
    private const string iosID = "3509019";
    private const string androidID = "3509018";
    private const int adLimit = 5;

    // Start is called before the first frame update

    private void Start()
    {
#if UNITY_STANDALONE
        if (adButton != null)
        {
            adButton.SetActive(false);
            adLimitText.SetActive(false);
        }
#endif
        string gameID = null;
#if UNITY_ANDROID
        gameID = androidID;
#elif UNITY_IOS
        gameID = iosID;
#endif
        if (gameID != null)
        {
            Advertisement.Initialize(gameID, false);
        }
            if (PlayerPrefs.GetInt("numOfTimesAdWatched") >= adLimit)
            {
                if (adButton != null)
                {
                    adButton.SetActive(false);
                    adLimitText.SetActive(true);
                }

            }
            else
            {
                if (adButton != null)
                {
                    adButton.SetActive(true);
                    adLimitText.SetActive(false);
                }
            }

        //Temporary, please remove later!
        //PlayerPrefs.SetInt("numOfTimesAdWatched", 0);

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
#region A lot of "if" statements...
        
            //So that we don't show ads in the shop
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                //Every 10 times we win or lose game
                if (gameManager.numOfTimesFinished % 10 == 0)
                {
                    if (Advertisement.IsReady("rewardedVideo"))
                    {

                        Advertisement.Show("rewardedVideo");
                    }
                } //Every 5 times we lose game
                else if (gameManager.numOfTimesFinished % 5 == 0)
                {
                    if (Advertisement.IsReady("video"))
                    {
                        Advertisement.Show("video");
                    }
                }
            }
            
#endregion
    }

    private void Update()
    {
#if UNITY_STANDALONE
        if (adButton != null)
        {
            adButton.SetActive(false);
            adLimitText.SetActive(false);
        }
#endif
        numOfTimesAdWatched = PlayerPrefs.GetInt("numOfTimesAdWatched");
        

        if (PlayerPrefs.GetInt("numOfTimesAdWatched") >= adLimit) {
            if (DateTime.Now.Subtract(DateTime.Parse(PlayerPrefs.GetString("oldDate"))).TotalHours >= 24)
            {
                PlayerPrefs.SetInt("numOfTimesAdWatched", 0);
                if (adButton != null)
                {
                    adButton.SetActive(true);
                    adLimitText.SetActive(false);
                }

          }
        }
    }

    public void PlayRewardedVideo(int rewardAmount)
    {
        
            if (Advertisement.IsReady("rewardedVideo"))
            {
                Advertisement.Show("rewardedVideo");
            } else
        {
            Debug.Log("IsReady(): " + Advertisement.IsReady("rewardedVideo"));
            Debug.Log("Initialized: " + Advertisement.isInitialized);
        }
        

        //Reward player, this is where I can modify what is given to the player.
        gameManager.GiveCoins(rewardAmount);
        
        PlayerPrefs.SetInt("numOfTimesAdWatched", PlayerPrefs.GetInt("numOfTimesAdWatched") + 1);
        Debug.Log(PlayerPrefs.GetInt("numOfTimesAdWatched"));
        if (PlayerPrefs.GetInt("numOfTimesAdWatched") >= adLimit)
        {
            if (adButton != null)
            {
                adButton.SetActive(false);
                adLimitText.SetActive(true);
                PlayerPrefs.SetString("oldDate", DateTime.Now.ToString());
            }
        }
    }

    //If we just want to play a rewarded video, without giving the coins.
    public static void PlayRewardedVideo()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
    }
}
