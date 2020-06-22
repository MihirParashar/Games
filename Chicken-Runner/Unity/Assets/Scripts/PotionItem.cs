using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PotionItem : MonoBehaviour
{
    public Potion potionStats;

    private PlayerController controller;
    private PlayerMovement movement;


    public bool hasTouchedPotion = false;
    public bool hasAppliedEffects = false;


    void Start()
    {
        //Initialize player controller and movement
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public IEnumerator PotionEffect()
    {
        yield return null;
    }

    private void Update()
    {
        if (hasTouchedPotion)
        {
            GameManager.potionsInEffect.Add(this);
            hasTouchedPotion = false;
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
        }

        //So it only runs once.
        hasTouchedPotion = true;
    }
}
