using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerNameTag : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI nameTagText;

    private Player player;
    #endregion

    void Start() {
        //Initializing our player variable
        //to our current player.
        player = GetComponent<Player>();

        //Set our name tag text value to
        //our player's username.
        StartCoroutine(WaitToSetNameTag());
    }

    //Function that waits until our player has
	//a username, then sets the name tag.
    IEnumerator WaitToSetNameTag() { 
        while (string.IsNullOrEmpty(player.GetUsername())) {
			yield return null;
		}

        nameTagText.text = player.GetUsername();
    }
}
