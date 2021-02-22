using UnityEngine;
using UnityEngine.Networking;

//Require the player manager component 
//since we need it to register ourseleves.
[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    #region Variables
    [SerializeField] private Behaviour[] componentsToDisable;

	private const string remoteLayerName = "Remote Player";
	private Camera sceneCamera;
#endregion

	void Start()
	{
		if (!isLocalPlayer)
		{
			//Disable the local player's components since we
			//are not the local player.
			DisableComponents();

			//Set our layer to the remote layer.
			AssignRemoteLayer();
			
		}
		else
		{
			//If we are the local player, then disable the
			//scene camera.
			sceneCamera = Camera.main;
			if (sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive(false);
			}
		}
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

		//Getting our network ID from the
		//NetworkIdentity component.
		string networkID = GetComponent<NetworkIdentity>().netId.ToString();

		//Assigning our player manager 
		//component  to a variable.
		Player player = GetComponent<Player>();

		//Registering this player.
		GameManager.RegisterPlayer(networkID, player);
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

		GameManager.UnregisterPlayer(transform.name);
	}

}