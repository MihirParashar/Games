using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    #region Variables
    [SerializeField] private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask shootableLayers;

    public Weapon playerWeapon;
    #endregion


    void Start()
    {
        if (cam == null)
        {
            //We have no camera referenced, debug an error and disable this object.
            Debug.LogError("No camera referenced (PlayerShoot)");
            enabled = false;
        }
    }

    void Update()
    {
        //"Fire1" is our left mouse button. So, if we are pressing down the left mouse
        //button, then run the shoot function.
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, cam.transform.forward, out hit, playerWeapon.range, shootableLayers))
        {
            Debug.Log("Player hit: " + hit.transform.name);
        }
    }
}
