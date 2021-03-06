using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string name = "Ray Shooter";

    public int damage = 10;
    public float range = 100f;
    public float fireRate = 5f; //How many times a second we can shoot our gun.
    public bool isAutomatic = false;
}
