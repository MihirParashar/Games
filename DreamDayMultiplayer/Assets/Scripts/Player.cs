using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Behaviour[] disableBehavioursOnDeath;
    [SerializeField] private GameObject[] disableGameObjectsOnDeath;
    private bool[] wasEnabled;

    //Syncing this variable to all of the clients so that
    //all of the clients know this player's current health.
    [SyncVar] private int currentHealth;

    //Creating a private boolean for if we are alive or not,
    //then creating an accessor for it. This also needs to
    //be synced so that all clients know if the player is 
    //alive or not.
    [SyncVar] private bool _alive = true;

    public bool alive { 
        get { return _alive; }
        protected set { _alive = value; }
    }

    private void Update()
    {
        //This should only be allowed to be used for
        //the local player.
        if (!isLocalPlayer)
        {
            return;
        }

        //[TEMPORARY] For testing purposes.
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(99999);
        } 
    }


    public void Setup ()
    {
        //Looping through all of the objects in the 
        //wasEnabled array and setting them to the 
        //enabled state of the objects in the
        //disableOnDeath array.
        wasEnabled = new bool[disableBehavioursOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableBehavioursOnDeath[i].enabled;
        }

        //Set our defaults when we are first initialized.
        SetDefaults();
    }

    //Function that makes player take damage.
    //ClientRpc makes this function called on all clients.
    [ClientRpc]
    public void RpcTakeDamage(int damageAmount)
    {
        //If the player is not alive, then abort the 
        //function.
        if (!alive)
        {
            return;
        }

        //Reducing our current health by the specified
        //damage amount.
        currentHealth -= damageAmount;

        //If our current health is less than 1, then kill the player.
        if (currentHealth < 1)
        {
            Die();
        }
    }

    //Function to be ran when the player dies.
    private void Die()
    {

        alive = false;

        //Looping through all of the behaviours in the list to
        //disable them.
        foreach (Behaviour behaviourToDisable in disableBehavioursOnDeath)
        {
            behaviourToDisable.enabled = false;
        }
        //Looping through all of the GameObjects in the list to
        //disable them.
        foreach (GameObject gameObjectToDisable in disableGameObjectsOnDeath)
        {
            gameObjectToDisable.SetActive(false);
        }

        //Colliders do not count as behaviours, so
        //instead I have to manually reference the
        //collider and disable it.
        Collider col = GetComponent<Collider>();

        if (col != null)
        {
            col.enabled = false;
        }

        //Enabling our scene camera and death text
        //(only if we are the local player).
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
            GameManager.instance.SetDeathTextActive(true);
        }


        //Call respawn function.
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        //Waiting for the specified amount of
        //time before respawning.
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        //Setting our defaults back.
        SetDefaults();

        //Getting a random spawn point from the 
        //NetworkManager, then assigning it to
        //a variable.
        Transform startPoint = NetworkManager.singleton.GetStartPosition();

        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;

    }

    //Function that resets our player's health.
    public void SetDefaults()
    {
        alive = true;

        //Re-enabling the behaviours in a for loop to what 
        //their previous enabled state was.
        for (int i = 0; i < disableBehavioursOnDeath.Length; i++)
        {
            disableBehavioursOnDeath[i].enabled = wasEnabled[i];
        }

        //Re-enabling the objects in a foreach loop to what 
        //their previous enabled state was.
        foreach (GameObject gameObjectToEnable in disableGameObjectsOnDeath)
        {
            gameObjectToEnable.SetActive(true);
        }

        //Colliders do not count as behaviours, so
        //instead I have to manually reference the
        //collider and re-enable it.
        Collider col = GetComponent<Collider>();
        
        if (col != null)
        {
            col.enabled = true;
        }

        //Disabling our scene camera and death text
        //(only if we are the local player).
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(false);
            GameManager.instance.SetDeathTextActive(false);
        }


        //Resetting the player's health to their max 
        //health.
        currentHealth = maxHealth;
    }
}
