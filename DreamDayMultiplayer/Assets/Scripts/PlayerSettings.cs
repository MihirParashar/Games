using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    void Start() {
        //If we have not set a PlayerPref for the mouse
        //sensitivity yet, then set it to the default.
        if (PlayerPrefs.GetFloat("MouseSensitivity") == null || PlayerPrefs.GetFloat("MouseSensitivity") == 0f) {
            PlayerPrefs.SetFloat("MouseSensitivity", 1f);
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
}
