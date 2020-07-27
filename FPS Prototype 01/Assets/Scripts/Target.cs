using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    #region Full Script Summary
    /*
     * Target Script - Created on July 26th, 2020 by Mihir Parashar
     * 
     * SUMMARY START
     * This script turns the GameObject it's on into a damageable enemy.
     * It will lose damage when shot at, and when at zero health, it will
     * be destroyed.
     * SUMMARY END
     * 
     * Thanks to Brackeys for the tutorial!
     * Link to video here: https://www.youtube.com/watch?v=THnivyG0Mvo
     * 
     */
    #endregion

    #region Defining Variables
    [SerializeField] private float health = 50f;
    #endregion

    public void TakeDamage(float amount)
    {
        //Subtract the amount of damage taken from our health.
        health -= amount;

        //If health is less than or equal to zero, then die.
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        //For now, the only thing we want to do here is destroy
        //the GameObject.
        Destroy(gameObject);
    }
}
