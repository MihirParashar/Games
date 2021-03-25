using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    #region Variables
    [SerializeField] private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask shootableLayers;
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    [SerializeField] private Animator animator;

    public Weapon[] playerWeapons;

    private const string playerTag = "Player";
    private int selectedWeapon;
    private float nextTimeToFire = 0f;
    private int selectedWeaponIndex;
    private Weapon currentPlayerWeapon; 
    #endregion


    void Start()
    {
        selectedWeaponIndex = weaponSwitcher.GetSelectedWeaponIndex();
        currentPlayerWeapon = playerWeapons[selectedWeaponIndex];

        if (cam == null)
        {
            //If we have no camera referenced, debug an error
            //and disable this object.
            Debug.LogError("No camera referenced (PlayerShoot)");
            enabled = false;
        }

        //Looping through all of the player weapons to set their
        //ammo to the default max ammo.
        foreach (Weapon playerWeapon in playerWeapons)
        {
            playerWeapon.currentAmmo = playerWeapon.maxAmmo;   
        }
    }

    void Update()
    {
        //If we are in the pause menu, then 
        //we don't want to do anything else
        //in this function (since it involves
        //shooting), so just return.
        if (PlayerUI.pauseMenuActiveState)
        {
            return;
        }
        selectedWeaponIndex = weaponSwitcher.GetSelectedWeaponIndex();
        currentPlayerWeapon = playerWeapons[selectedWeaponIndex];

        //Slowly decreasing our next time to fire.
        nextTimeToFire -= Time.deltaTime;

        //We should only be able to shoot if we are not reloading.
        if (!currentPlayerWeapon.isReloading) {
            //"Fire1" is our left mouse button. So, if we are
            //pressing down the left  button, then run the
            //shoot function.
            if (currentPlayerWeapon.isAutomatic)
            //If the weapon is automatic, then run the shoot 
            //function whenever the Fire1 button is down.
            {
                if (Input.GetButton("Fire1") && nextTimeToFire <= 0f)
                {
                    Shoot();

                    //Reset our firing timer every time we shoot.
                    nextTimeToFire = 1 / currentPlayerWeapon.fireRate;
                }
            } else
            //If the weapon is NOT automatic, then only shoot
            //on the frame we clicked the left mouse button.
            {
                if (Input.GetButtonDown("Fire1") && nextTimeToFire <= 0f)
                { 
                    Shoot();

                    //Reset our firing timer every time we shoot.
                    nextTimeToFire = 1 / currentPlayerWeapon.fireRate;
                }
            }
        }

        //If we have run out of ammo, force us to reload. Or, if we
        //are not currently at our max ammo count, and we press "R",
        //reload.
        if (currentPlayerWeapon.currentAmmo <= 0 && !currentPlayerWeapon.isReloading ||
            currentPlayerWeapon.currentAmmo < currentPlayerWeapon.maxAmmo && !currentPlayerWeapon.isReloading && Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reload());
        }

        //If we are reloading, set it to true in our animator.
        //If it's the opposite, set it to false..
        if (currentPlayerWeapon.isReloading && !animator.GetBool("isReloading")) {
            animator.SetBool("isReloading", true);
        } else if (!currentPlayerWeapon.isReloading && animator.GetBool("isReloading")) {
            animator.SetBool("isReloading", false);
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

        //Reduce our ammo count by 1.
        currentPlayerWeapon.currentAmmo--;

        //Run the comman`d that spawns the muzzle flash effects
        //because we shot.
        CmdOnShoot();

        RaycastHit hit;

        //Checking if a ray that starts at the weapon's firing
        //point in the forward direction and has a range of the
        //specified weapon range hits any objects with any layer
        //from the shootableLayers LayerMask.
        if (Physics.Raycast(firePoint.position, cam.transform.forward, out hit, currentPlayerWeapon.range, shootableLayers))
        {
            //Run the command that spawns the impact effect
            //because we hit something.
            CmdOnHit(hit.point, hit.normal);

            //If the object we hit has the player tag, then run
            //the PlayerShot command.
            if (hit.collider.CompareTag(playerTag))
            {
                CmdPlayerShot(hit.collider.name, currentPlayerWeapon.damage, transform.name);
            }
        }
    }
    
    //Method that waits our current gun's number of seconds to
    //reload, then resets the current ammo to our gun's max ammo.
    IEnumerator Reload () {
            Weapon weaponToReload = currentPlayerWeapon;
            weaponToReload.isReloading = true;

            yield return new WaitForSeconds(weaponToReload.secondsToReload);

            currentPlayerWeapon.currentAmmo = currentPlayerWeapon.maxAmmo;
            weaponToReload.isReloading = false;
    }


    #region Server Commands
    //A command that we can run whenever a player is shot.
    [Command]
    void CmdPlayerShot(string playerID, int damageAmount, string sourcePlayerID)
    { 
        //Getting a reference of the player we hit and making
        //them take the specified amount of damage.
        Player player = GameManager.GetPlayer(playerID);

        player.RpcTakeDamage(damageAmount, sourcePlayerID);
    }

    //Command to run whenever we shoot.
    [Command]
    void CmdOnShoot()
    {
        RpcShootEffects();
    }

    //Command to run whenever an obect is hit.
    [Command]
    void CmdOnHit(Vector3 hitPos, Vector3 hitNormal)
    {
        RpcImpactEffect(hitPos, hitNormal);
    }

    //Command to play the shooting effects.
    [ClientRpc]
    void RpcShootEffects()
    {
        currentPlayerWeapon.muzzleFlash.Play();
    }

    //Command to instantiate the impact effect.
    [ClientRpc]
    void RpcImpactEffect(Vector3 hitPos, Vector3 hitNormal)
    {
        //Instantiating our impact effect at the given hit position
        //and rotation.
        Instantiate(currentPlayerWeapon.impactEffect, hitPos, Quaternion.LookRotation(hitNormal));
    }


    #endregion
}
