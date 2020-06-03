using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevelGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct LevelStagePlatform
    {
        public Stage stage;
        public Transform[] platformTypes;
    }

    private const float playerDistSpawnLevelPart = 30f;
    private const float playerDistRemoveLevelPart = 200f;

    [SerializeField] private Transform levelStart;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform tileParent;

    [SerializeField] private LevelStagePlatform[] levelStagePlatforms; 

    private Vector3 lastEndPosition;
    private Vector3 lastStartPosition;

    public enum Stage { Begin, Cave, Temple }
    private Stage stageOn = Stage.Begin;

    private List<Transform> instantiatedPlatforms = new List<Transform>();

    private void Awake()
    {
        lastEndPosition = levelStart.Find("EndPosition").position;
        lastStartPosition = levelStart.Find("StartPosition").position;
        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)   
        {
            SpawnLevelPart(true);
            SpawnLevelPart(false);
        }
    }

    private void Update()
    {
        if (Mathf.RoundToInt(Time.timeSinceLevelLoad) >= 10)
        {
            stageOn = Stage.Cave;
        }
        for (int i = 0; i < instantiatedPlatforms.Count; i++)
        {
            //Debug.Log("Player Position: " + player.transform.position);
            //Debug.Log("Platform " + i + " Position:" + instantiatedPlatforms[i].position);
            //If it's out of range
            if (Vector3.Distance(instantiatedPlatforms[i].position, player.transform.position) > playerDistRemoveLevelPart)
            {
                Transform outOfRangePlatform = instantiatedPlatforms[i];
                instantiatedPlatforms.Remove(instantiatedPlatforms[i]);
                Destroy(outOfRangePlatform.gameObject);
            }
        }
        if (Vector3.Distance(player.transform.position, lastEndPosition) < playerDistSpawnLevelPart && player.transform.position.x >= 0)
        {
            SpawnLevelPart(true);
        }
        else if (Vector3.Distance(player.transform.position, lastStartPosition) < playerDistSpawnLevelPart && player.transform.position.x < 0)
        {
            SpawnLevelPart(false);
        }
    }

    private void SpawnLevelPart(bool isRight)
    {
        if (isRight)
        {
            Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition, true);
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        } else
        {
            Transform lastLevelPartTransform = SpawnLevelPart(lastStartPosition, false);
            lastStartPosition = lastLevelPartTransform.Find("EndPosition").position;
        }


    }

    private Transform SpawnLevelPart(Vector3 pos, bool isRight)
    {
        Transform randomType = levelStagePlatforms[0].platformTypes[Random.Range(0, levelStagePlatforms[0].platformTypes.Length)];
        if (stageOn == Stage.Cave)
        {
            randomType = levelStagePlatforms[1].platformTypes[Random.Range(0, levelStagePlatforms[1].platformTypes.Length)];

        }
        Transform levelPartTransform;

        if (isRight)
        {
            levelPartTransform = Instantiate(randomType, new Vector3(pos.x + (randomType.localScale.x / 2), pos.y), Quaternion.identity);
        }
        else
        {
            levelPartTransform = Instantiate(randomType, new Vector3(pos.x - (randomType.localScale.x / 2), pos.y), Quaternion.identity);
            levelPartTransform.localScale = new Vector3(
                -levelPartTransform.localScale.x,
                levelPartTransform.localScale.y
                );
        }
        levelPartTransform.SetParent(tileParent);
        instantiatedPlatforms.Add(levelPartTransform);
        return levelPartTransform;
    }
}

