using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float appearTime;
    [SerializeField] private float disappearTime;

    private void Start()
    {
        StartCoroutine(FlashGameObject(appearTime, disappearTime));
    }

    IEnumerator FlashGameObject(float appearTime, float disappearTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(disappearTime);

            foreach (Collider2D col in GetComponents<Collider2D>())
            {
                col.enabled = false;
            }
            GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(appearTime);

            foreach (Collider2D col in GetComponents<Collider2D>())
            {
                col.enabled = true;
            }
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
