
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    GameObject character;
    GameObject levelGen;

    bool hasActiavtedPlatform;

    public Sprite leftSprite;
    public Sprite rightSprite;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        levelGen = GameObject.FindGameObjectWithTag("LevelGen");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasActiavtedPlatform)
        {
            StartCoroutine(ChangePlatformSprite());
            animator.SetTrigger("OnPlatform");
            hasActiavtedPlatform = true;
        }
        
        if (collision.tag == "Player")
        {
            character.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            character.transform.SetParent(levelGen.transform);
        }
    }

    IEnumerator ChangePlatformSprite()
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().sprite = rightSprite;
            yield return new WaitForSeconds(4f);
            GetComponent<SpriteRenderer>().sprite = leftSprite;
            yield return new WaitForSeconds(4f);
        }
    }
}
 