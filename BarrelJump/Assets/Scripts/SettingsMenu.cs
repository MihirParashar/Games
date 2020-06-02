using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    AudioSource music;
    public Slider volumeSlider;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown difficultyDropdown;
    public Toggle musicToggle;
    int difficultyIndex;
    


    int isMusicOnInt;
    bool isMusicOnBool;


    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("VolumeKey"));
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevelKey"), true);
        if (PlayerPrefs.GetInt("MusicToggleKey") == 1)
        {
            isMusicOnBool = true;
        } else
        {
            isMusicOnBool = false;
        }
        music.enabled = isMusicOnBool;
        

        volumeSlider.value = PlayerPrefs.GetFloat("VolumeKey");
        qualityDropdown.value = PlayerPrefs.GetInt("QualityLevelKey");
        difficultyDropdown.value = PlayerPrefs.GetInt("DifficultyInt");
        musicToggle.isOn = isMusicOnBool;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("VolumeKey", volume);
    }

    public void ToggleMusic(bool isMusicOn)
    {
        music.enabled = isMusicOn;
        if (isMusicOn)
        {
            isMusicOnInt = 1;
        } else
        {
            isMusicOnInt = 0;
        }
        PlayerPrefs.SetInt("MusicToggleKey", isMusicOnInt);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("QualityLevelKey", qualityIndex);
    }

    public void SetDifficulty(int _difficultyIndex)
    {
        PlayerPrefs.SetInt("DifficultyInt", _difficultyIndex);
    }

}
