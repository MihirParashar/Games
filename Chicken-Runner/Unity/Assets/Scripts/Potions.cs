using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    PlayerController controller;
    PlayerMovement movement;
    public enum PotionTypes
    {
        jumpBoost,
        speed
    }

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    //When player enters potion collider, give potion effect of varying type
    //Depending on name of potion
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(PotionEffect(787.5f));

        }
    }

    //Where the actual modification happens
    public IEnumerator PotionEffect(float betterForce)
    {
        
        if (name == "JumpBoostPotion(Clone)")
        {
            controller.m_JumpForce = betterForce;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
            yield return new WaitForSecondsRealtime(30f);
            controller.m_JumpForce /= 1.75f;
            Destroy(gameObject);
        } else if (name == "SpeedPotion(Clone)")
        {
            movement.moveSpeed = betterForce;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(30f);
            movement.moveSpeed /= 1.75f;
            Destroy(gameObject);
        }
    }
}
