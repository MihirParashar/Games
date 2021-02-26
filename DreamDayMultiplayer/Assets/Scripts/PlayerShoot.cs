using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    #region Variables
    [SerializeField] private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask shootableLayers;

    public Weapon playerWeapon;

    private const string playerTag = "Player";
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
        //"Fire1" is our left mouse button. So, if we are
        //pressing down the left  button, then run the
        //shoot function.
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot()
    {
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

    //A command that we can run whenever a player is shot.
    [Command]
    void CmdPlayerShot(string playerID, int damageAmount)
    { 
        Player player = GameManager.GetPlayer(playerID);

        player.RpcTakeDamage(damageAmount);
    }
}
