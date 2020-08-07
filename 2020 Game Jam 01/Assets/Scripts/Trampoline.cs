using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float upForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Apply upwards force.
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, upForce/Time.deltaTime));
        }
    }
}
