using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

//Require the player manager component 
//since we need it to register ourseleves.
[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    #region Variables
    [SerializeField] private Behaviour[] componentsToDisable;

	private const string remoteLayerName = "Remote Player";
#endregion

	void Start()
	{ 
		if (!isLocalPlayer)
		{
			//Disable the local player's components since
			//we are not the local player.
			DisableComponents();

			//Set our layer to the remote layer.
			AssignRemoteLayer();
			
		}

		//Since we already know that this object must have
		//a player component (we required it in the
		//beginning), we can simply directly reference it
		//without having to make a variable.
		GetComponent<Player>().Setup();
	}

	//Overriding the OnStartClient function from the 
	//NetworkBehaviour script so that we can add our
	//RegisterPlayer function.
    public override void OnStartClient()
    {

		//This basically means add whatever was 
		//previously in this function, because we
		//don't want to overwrite this function,
		//we just want to add to it.
        base.OnStartClient();

		//Assigning our player manager 
		//component  to a variable.
		Player player = GetComponent<Player>();

		//Registering this player.
		StartCoroutine(WaitToRegisterPlayer(player));
    }
	
	//Function hat waits until our player has
	//a username, then registers the player.
	IEnumerator WaitToRegisterPlayer(Player player) {

		while (player.GetUsername() == "" || player.GetUsername() == null) {
			yield return null;
		}

		GameManager.RegisterPlayer(player.GetUsername(), player);
	}

    void AssignRemoteLayer ()
    {
		//Set the GameObject's layer to the remote layer.
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

	void DisableComponents()
    {
		//Looping through each of the components to disable them.
		foreach (Behaviour componentToDisable in componentsToDisable)
        {
			componentToDisable.enabled = false;
        }
	}

	void OnDisable()
	{
		//If we die/exit, do the following:

		//Re-enable our scene camera (if we are the local
		//player).
		if (isLocalPlayer)
		{
			GameManager.instance.SetSceneCameraActive(true);

			//Close the pause menu if it's open.
			if (PlayerUI.pauseMenuActiveState) {
				PlayerUI.instance.TogglePauseMenu();
			}
		}

		//Unlock the cursor.
		Cursor.lockState = CursorLockMode.None;

		//Unregister ourselves from the player list.
		GameManager.UnregisterPlayer(transform.name);
	}
}