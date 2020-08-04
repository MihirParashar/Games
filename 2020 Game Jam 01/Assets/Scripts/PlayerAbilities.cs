using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public enum AbilityTypes { 
        
        CaneDoubleJump,
        PhoneObstacleDisappear,
        BeerSpeedBoost,
        SkateboardSpeedBoost,
        LollipopSpeedBoost

    };

    public static AbilityTypes abilityType = AbilityTypes.CaneDoubleJump;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(abilityType);
        }
    }

    public static void ChangeAbility(AbilityTypes ability)
    {
        abilityType = ability;
    }
}
