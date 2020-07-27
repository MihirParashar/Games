using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
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

    //I will consider creating a scriptable object for this.
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    #endregion

    #endregion

    private void Update()
    {
        //If we are holding down fire button, shoot.
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;

        //Physics raycast returns a boolean on whether or not our ray hit something in that layer mask
        //with the given details, such as the range, the position of the ray, and the direction.
        bool hasShotObject = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);

        //If we shot an object, then check if the object is a target.
        if (hasShotObject)
        {
            Target target = hit.transform.GetComponent<Target>();

            //If the object does have the target component, then make it take damage.
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
