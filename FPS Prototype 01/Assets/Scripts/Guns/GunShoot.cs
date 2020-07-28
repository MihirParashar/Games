using UnityEngine;

public class GunShoot : MonoBehaviour
{
    #region Full Script Summary
    /*
     * Gun Script - Created on July 26th, 2020 by Mihir Parashar
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
     * 
     */
    #endregion

    #region Defining Variables
    [SerializeField] private Camera fpsCam;

    #region Gun Stats
    [Header("Gun Stats")]

    [SerializeField] private Gun gun;

    private float nextTimeToFire = 0f;
    #endregion

    #region Effects
    [Header("Effects")]

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    #endregion

    #endregion

    private void Update()
    {
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
    }

    private void Shoot()
    {
        //Play the muzzle flash particle effect.
        muzzleFlash.Play();

        RaycastHit hit;

        //Physics raycast returns a boolean on whether or not our ray hit something in that layer mask
        //with the given details, such as the range, the position of the ray, and the direction.
        bool hasShotObject = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gun.range);

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
    }
}
