using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Potion")]
public class Potion : ScriptableObject
{

    public enum PotionTypes
    {
        jumpBoost,
        speed
    }

    public PotionTypes potionType;

    public float effectTime;
    public float effectMultiplier;

}
