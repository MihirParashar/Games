using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    public Potion potionStats;

    private PlayerController controller;
    private PlayerMovement movement;

    private bool hasTouchedPotion;
    private bool hasJumpBoostPotionEffect = false;
    private bool hasSpeedBoostPotionEffect = false;

    void Start()
    {
        //Initialize player controller and movement
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public IEnumerator PotionEffect()
    {

        if (!hasJumpBoostPotionEffect)
        {
            if (potionStats.potionType == Potion.PotionTypes.jumpBoost)
            {
                PlayerPrefs.SetInt("HasJumpBoostPotionEffect", 1);

                if (PlayerPrefs.GetInt("HasJumpBoostPotionEffect", 0) == 0)
                {
                    controller.m_JumpForce *= potionStats.effectMultiplier;
                }
                yield return new WaitForSeconds(potionStats.effectTime);

                if (PlayerPrefs.GetInt("HasJumpBoostPotionEffect", 0) == 0)
                {
                    controller.m_JumpForce /= potionStats.effectMultiplier;
                }

                PlayerPrefs.SetInt("HasJumpBoostPotionEffect", 0);

            }
            else if (potionStats.potionType == Potion.PotionTypes.speed)
            {
                if (PlayerPrefs.GetInt("HasSpeedBoostPotionEffect", 0) == 0)
                {
                    movement.moveSpeed *= potionStats.effectMultiplier;
                    PlayerPrefs.SetInt("HasSpeedBoostPotionEffect", 1);
                }
                    yield return new WaitForSeconds(potionStats.effectTime);
                    PlayerPrefs.SetInt("HasSpeedBoostPotionEffect", 0);
                    movement.moveSpeed /= potionStats.effectMultiplier;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //So it seems to disappear.
        if (!hasTouchedPotion)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;

            StartCoroutine(PotionEffect());
        }

        //So it only runs once.
        hasTouchedPotion = true;


    }
}
