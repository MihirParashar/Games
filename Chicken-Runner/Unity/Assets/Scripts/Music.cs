using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip normalSound;
    public AudioClip caveCutsceneSound;
    public AudioClip templeMusicSound;

    private int sceneOn = 0;

    private void Awake()
    {   

        DontDestroyOnLoad(this);
        int numOfMusicInstances = GameObject.FindGameObjectsWithTag("Music").Length;

        if (numOfMusicInstances > 1)
        {
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        sceneOn = SceneManager.GetActiveScene().buildIndex - 2;

        if (sceneOn >= 13)
        {
            GetComponent<AudioSource>().clip = templeMusicSound;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }

        } else if (sceneOn >= 7)
        {
            GetComponent<AudioSource>().clip = caveCutsceneSound;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }

        } else
        {
            GetComponent<AudioSource>().clip = normalSound;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }

}

