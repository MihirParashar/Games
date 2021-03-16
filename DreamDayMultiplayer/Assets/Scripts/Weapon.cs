using UnityEngine;

[System.Serializable]
public class Weapon
{
    [Header("Stats")]
    public string name;

    public int damage;
    public float range;
    public float fireRate; //How many times a second we can shoot our gun.
    public int maxAmmo;
    public float secondsToReload;
    public bool isAutomatic = false;
    [HideInInspector] public bool isReloading = false;
    [HideInInspector] public int currentAmmo;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;
}
