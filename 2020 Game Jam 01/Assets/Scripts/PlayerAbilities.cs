using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public enum AbilityTypes { 
        
        CaneJumpBoost,
        PhoneObstacleDisappear,
        BeerSpeedBoost,
        SkateboardSpeedBoost,
        LollipopSpeedBoost

    };

    public static AbilityTypes abilityType;
    public static AbilityTypes prevAbilityType;

    //The time until the ability ends.
    private float timeUntilBoostEnds = 5f;

    //If we started the ability yet.
    private bool hasStartedAbility = false;

    //If we used the ability.
    private bool hasUsedAbility = false;

    [Header("Powerup")]

    [SerializeField] private SpriteRenderer powerupIcon;

    [SerializeField] private Sprite canePowerupSprite;
    [SerializeField] private Sprite phonePowerupSprite;
    [SerializeField] private Sprite beerPowerupSprite;
    [SerializeField] private Sprite skateboardPowerupSprite;
    [SerializeField] private Sprite lollipopPowerupSprite;

    private void Start()
    {
        int health = PlayerPrefs.GetInt("Health", 5);
        abilityType = (AbilityTypes)(-health + 5);
        prevAbilityType = abilityType;
    }

    private void Update()
    {           

        //If we haven't started the ability yet, and we press p, start the ability.
        if (Input.GetKeyDown(KeyCode.P) && !hasStartedAbility && Time.timeScale != 0)
        {
            hasStartedAbility = true;
            prevAbilityType = abilityType;

            switch (abilityType) {


                case AbilityTypes.CaneJumpBoost:

                    powerupIcon.sprite = canePowerupSprite;

                    //Give the player jump boost.
                    GetComponent<CharacterController2D>().m_JumpForce *= 1.75f;
                    break;

                case AbilityTypes.PhoneObstacleDisappear:

                    powerupIcon.sprite = phonePowerupSprite;

                    //Remove the obstacles collider if it is a trigger collider.
                    foreach (Obstacle obstacleGO in FindObjectsOfType<Obstacle>())
                    {
                        foreach (Collider2D col in obstacleGO.GetComponents<Collider2D>())
                        {
                            if (col.isTrigger)
                            {
                                col.enabled = false;
                            }
                        }
                    }
                    break;

                case AbilityTypes.BeerSpeedBoost:

                    powerupIcon.sprite = beerPowerupSprite;

                    //Speed up the player.
                    GetComponent<PlayerMovement>().moveSpeed *= 1.75f;
                    break;

                case AbilityTypes.SkateboardSpeedBoost:

                    powerupIcon.sprite = skateboardPowerupSprite;

                    //Speed up the player.
                    GetComponent<PlayerMovement>().moveSpeed *= 1.75f;
                    break;

                case AbilityTypes.LollipopSpeedBoost:

                    powerupIcon.sprite = lollipopPowerupSprite;

                    //Speed up the player.
                    GetComponent<PlayerMovement>().moveSpeed *= 1.75f;
                    break;
            }            
        }

        //If we have started the ability.
        if (timeUntilBoostEnds > 0f && hasStartedAbility)
        {
            timeUntilBoostEnds -= Time.deltaTime;
        }
        if (timeUntilBoostEnds <= 0f && !hasUsedAbility)
        {

            powerupIcon.sprite = null;

            hasUsedAbility = true;

            switch (prevAbilityType)
            {

                case AbilityTypes.CaneJumpBoost:

                    //Remove the jump boost.
                    GetComponent<CharacterController2D>().m_JumpForce /= 1.75f;
                    break;

                case AbilityTypes.PhoneObstacleDisappear:

                    //Add back the obstacles collider if it is a trigger collider.
                    foreach (Obstacle obstacleGO in FindObjectsOfType<Obstacle>())
                    {
                        foreach (Collider2D col in obstacleGO.GetComponents<Collider2D>())
                        {
                            if (col.isTrigger)
                            {
                                col.enabled = true;
                            }
                        }
                    }
                    break;

                case AbilityTypes.BeerSpeedBoost:

                    //Slow down the player.
                    GetComponent<PlayerMovement>().moveSpeed /= 1.75f;
                    break;

                case AbilityTypes.SkateboardSpeedBoost:

                    //Slow down the player.
                    GetComponent<PlayerMovement>().moveSpeed /= 1.75f;
                    break;

                case AbilityTypes.LollipopSpeedBoost:

                    //Slow down the player.
                    GetComponent<PlayerMovement>().moveSpeed /= 1.75f;
                    break;
            }
        }
    }

    public static void ChangeAbility(AbilityTypes ability)
    {
        abilityType = ability;
    }
}
