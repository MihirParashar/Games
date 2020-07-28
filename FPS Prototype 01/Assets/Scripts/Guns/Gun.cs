﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun", order = 1)]
public class Gun : ScriptableObject
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1f;
    public float impactForce = 30f;

    public bool isAutomatic = true;
}