using UnityEngine;
using UnityEngine.Networking;


// SCRIPT COPIED FROM BRACKEYS

public class PlayerSetup : NetworkBehaviour
{

	[SerializeField] private Behaviour[] componentsToDisable;

	[SerializeField] private const string remoteLayerName = "Remote Player";



	Camera sceneCamera;

	void Start()
	{
		if (!isLocalPlayer)
		{
			//Disable the local player's components since we are not the local player.
			DisableComponents();

			//Set our layer to the remote layer.
			AssignRemoteLayer();
			
		}
		else
		{
			//If we are the local player, then disable the scene camera.
			sceneCamera = Camera.main;
			if (sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive(false);
			}
		}
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
		//If we die/exit, re-enable the scene camera.
		if (sceneCamera != null)
		{
			sceneCamera.gameObject.SetActive(true);
		}

		//Then, unlock the cursor.
		Cursor.lockState = CursorLockMode.None;
	}

}