using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    GameManager gameManager;
    public AIPath aiPath;
    public AstarPath astarPath;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (astarPath == null)
        {
            astarPath = GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<AstarPath>();
        }
        astarPath.Scan();
    }

    // Update is called once per frame
    void Update()
    {
      if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale =  new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.Lose();
        }
    }
}
