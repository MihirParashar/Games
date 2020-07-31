using UnityEngine;
using System.Collections;
using TMPro;

public class GunShoot : MonoBehaviour
{
    #region Full Script Summary
    /*
     * GunShoot Script - Created on July 26th, 2020 by Mihir Parashar
     * 
     * SUMMARY START
     * This script controls gun shooting. To shoot, it shoots out a raycast
     * with the specified start position, direction, and range. If it hits a
     * GameObject with the target script on it, the GameObject will take the amount
     * of damage specified in the script.
     * SUMMARY END
     * 
     * Thanks to Brackeys for the tutorial!
     * Link to video here: https://www.youtube.com/watch?v=THnivyG0Mvo
     * Link to video 2 here: https://www.youtube.com/watch?v=kAx5g9V5bcM
     * 
     */
    #endregion

    #region Defining Variables
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Animator animator;

    #region Gun Stats
    [Header("Gun Stats")]

    [SerializeField] private Gun gun;

    private float nextTimeToFire = 0.25f;
    private int currentAmmo;
    private bool isReloading = false;
    #endregion

    #region Effects And UI
    [Header("Effects And UI")]

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;

    [SerializeField] private TextMeshProUGUI ammoText;
    #endregion

    #endregion

    private void Start()
    {
        //Setting our ammo to our max ammo at the start.
        currentAmmo = gun.maxAmmo;

    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }

    private void Update()
    {
        //If we are reloading, don't do anything else in the rest of the function.
        if (isReloading)
        {
            return;
        }

        //If we have 0 or less ammo left, we have to reload.
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        #region Detecting Input For Shooting and Reloading
        //If our gun is automatic, allow the user to press and hold the mouse to rapid fire the gun.
        //Otherwise, make the player have to click every time.
        if (gun.isAutomatic)
        {
            //If we are holding down fire button and we have waited enough time to shoot again, shoot.
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                //Add time to the reciprocal of the fire rate, to find the next time to fire.
                nextTimeToFire = Time.time + (1f / gun.fireRate);

                Shoot();
            }
        } else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                //Add time to the reciprocal of the fire rate, to find the next time to fire.
                nextTimeToFire = Time.time + (1f / gun.fireRate);

                Shoot();
            }
        }

        //If we press "R", we are not already reloading, and we have less ammo then our max ammo, then reload.
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < gun.maxAmmo)
        {
            StartCoroutine(Reload());
        }

        #endregion

        //Setting our ammo text to the current ammo out of
        //the gun's max ammo.
        ammoText.text = currentAmmo + " / " + gun.maxAmmo;
    }


    private void Shoot()
    {
        //Play the muzzle flash particle effect.
        muzzleFlash.Play();

        //Subtracting 1 from our current ammo, because every time we shoot,
        //we should have one less ammo.
        currentAmmo--;

        RaycastHit hit;

        //Physics raycast returns a boolean on whether or not our ray hit something in that layer mask
        //with the given details, such as the range, the position of the ray, and the direction.
        bool hasShotObject = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gun.range);

        #region Making Target Take Damage, And Impact Effect

        //If we shot an object, then check if the object is a target.
        if (hasShotObject)
        {
            Target target = hit.transform.GetComponent<Target>();

            //If the object has the target script, then make it take damage.
            if (target != null)
            {
                target.TakeDamage(gun.damage);
            }

            //If the object has a rigidbody, then add negative force to it, to knock back the object,
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * gun.impactForce);
            }

            //Instantiate our impact effect at the hit point, and rotated outwards of the object.
            GameObject impactEffectGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            //Destroying our impact effect GameObject after 1.5 seconds.
            Destroy(impactEffectGO, 1.5f);
        }
        #endregion
    }
    private IEnumerator Reload()
    {
        //Set is reloading true, and set the animator parameter is reloading true, as well.
        isReloading = true;
        animator.SetBool("isReloading", true);

        //Waiting for the reload time in seconds minus 0.25 seconds, because of transition time.
        yield return new WaitForSeconds(gun.reloadTime - .25f);

        //Set the animator parameter is reloading false.
        animator.SetBool("isReloading", false);

        //Waiting the extra 0.25 seconds for transition time.
        yield return new WaitForSeconds(.25f);

        //Setting our current ammo back to our max ammo because we reloaded.
        currentAmmo = gun.maxAmmo;

        //Set is reloading false.
        isReloading = false;
    }
}
