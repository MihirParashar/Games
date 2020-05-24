using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip caveCutsceneSound;
    public AudioClip normalSound;

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
        if (SceneManager.GetActiveScene().buildIndex - 2 >= 7)
        {
            GetComponent<AudioSource>().clip = caveCutsceneSound;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().clip = normalSound;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }

}

