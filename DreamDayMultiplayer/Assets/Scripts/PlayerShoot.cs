using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    #region Variables
    [SerializeField] private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private LayerMask shootableLayers;

    public Weapon playerWeapon;

    private const string playerTag = "Player";
    private float nextTimeToFire = 0f;
    #endregion


    void Start()
    {
        if (cam == null)
        {
            //We have no camera referenced, debug an error
            //and disable this object.
            Debug.LogError("No camera referenced (PlayerShoot)");
            enabled = false;
        }
    }

    void Update()
    {
        //Slowly decreasing our next time to fire.
        nextTimeToFire -= Time.deltaTime;

        //"Fire1" is our left mouse button. So, if we are
        //pressing down the left  button, then run the
        //shoot function.
        if (playerWeapon.isAutomatic)
        //If the weapon is automatic, then run the shoot 
        //function whenever the Fire1 button is down.
        {
            if (Input.GetButton("Fire1") && nextTimeToFire <= 0f)
            {
                Shoot();

                //Reset our firing timer every time we shoot.
                nextTimeToFire = 1 / playerWeapon.fireRate;
            }
        } else
        //If the weapon is NOT automatic, then only shoot
        //on the frame we clicked the left mouse button.
        {
            if (Input.GetButtonDown("Fire1") && nextTimeToFire <= 0f)
            { 
                Shoot();

                //Reset our firing timer every time we shoot.
                nextTimeToFire = 1 / playerWeapon.fireRate;
            }
        }
    }

    //The client attribute will make the function automatically
    //return if the player is not active.
    [Client]
    void Shoot()
    {
        //We only want to run the commands in this function if
        //we are the local player. If not, just return.
        if (!isLocalPlayer)
        {
            return;
        }

        CmdOnShoot();

        RaycastHit hit;

        //Checking if a ray that starts at the weapon's firing
        //point in the forward direction and has a range of the
        //specified weapon range hits any objects with any layer
        //from the shootableLayers LayerMask.
        if (Physics.Raycast(firePoint.position, cam.transform.forward, out hit, playerWeapon.range, shootableLayers))
        {
            //If the object we hit has the player tag, then run
            //the PlayerShot command.
            if (hit.collider.CompareTag(playerTag))
            {
                CmdPlayerShot(hit.collider.name, playerWeapon.damage);
            }
        }
    }

    #region Server Commands
    //A command that we can run whenever a player is shot.
    [Command]
    void CmdPlayerShot(string playerID, int damageAmount)
    { 
        //Getting a reference of the player we hit and making
        //them take the specified amount of damage.
        Player player = GameManager.GetPlayer(playerID);

        player.RpcTakeDamage(damageAmount);
    }

    //Command to run whenever we shoot.
    [Command]
    void CmdOnShoot()
    {
        RpcShootEffects();
    }

    //Command to play the shooting effects.
    [ClientRpc]
    void RpcShootEffects()
    {
        muzzleFlash.Play();
    }

    #endregion
}
