using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun", order = 1)]
public class Gun : ScriptableObject
{
    #region Full Script Summary
    /*
     * Gun ScriptableObject - Created on July 27th, 2020 by Mihir Parashar
     * 
     * SUMMARY START
     * This is a ScriptableObject created for guns. With this ScriptableObject,
     * you can control the damage, range, fire rate, impact force, if it's automatic,
     * as well as max ammunition, and the reload time.
     * SUMMARY END
     */
    #endregion

    [Header("Shooting")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1f;
    public float impactForce = 30f;

    public bool isAutomatic = true;

    [Header("Ammuntion")]
    public int maxAmmo = 30;
    public float reloadTime = 3f;

}
