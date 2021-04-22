using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioMixer mixer;
    #endregion

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
