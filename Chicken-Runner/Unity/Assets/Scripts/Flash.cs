using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public float flashTimeDisappear = 5f;
    public float flashTimeAppear = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoopFlash(flashTimeDisappear, flashTimeAppear));
    }

    IEnumerator LoopFlash(float disappearTime, float appearTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(disappearTime);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(appearTime);

            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
