using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    //Syncing this variable to all of the clients so that
    //all of the clients know this player's current health.
    [SyncVar]
    [SerializeField] private int currentHealth;

    private void Awake()
    {
        //Set our defaults when we are first initialized.
        SetDefaults();
    }

    //Function that makes player take damage.
    public void TakeDamage(int damageAmount)
    {
        //Reducing our current health by the specified
        //damage amount.
        currentHealth -= damageAmount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");
    }

    //Function that resets our player's health.
    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }
}
