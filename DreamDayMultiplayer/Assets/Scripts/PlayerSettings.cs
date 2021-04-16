using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    void Start() {
        //If we have not set a PlayerPref for any of 
        //the player settings yet, then set it to the
        //default.
        if (PlayerPrefs.GetFloat("MouseSensitivity") == 0f) {
            PlayerPrefs.SetFloat("MouseSensitivity", 1f);
        }

        if (PlayerPrefs.GetFloat("Volume") == 0f)
        {
            PlayerPrefs.SetFloat("Volume", 1f);
        }
    }

    //Function that sets the mouse sensitivity
    //to the amount inputted.
    public void SetMouseSensistivity(float newSens) {
        PlayerPrefs.SetFloat("MouseSensitivity", newSens);
    }

    //Function that sets our username to what
    //is specified.
    public void SetUsername(string newUsername) {
        PlayerPrefs.SetString("PlayerUsername", newUsername);
    }

    //Function that sets our volume to what
    //is specified.
    public void SetVolume (float newVolume)
    {
        PlayerPrefs.SetFloat("Volume", newVolume);
        mixer.SetFloat("Volume", Mathf.Log10(newVolume) * 20);
    }   
}
