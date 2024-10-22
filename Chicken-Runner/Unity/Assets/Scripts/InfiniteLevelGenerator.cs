﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevelGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct LevelStagePlatform
    {
        public Stage stage;
        public Transform[] platformTypes;
        //In seonds
        public int numOfPlatformsToStartStage;
    }

    [SerializeField] private LevelStagePlatform[] levelStagePlatforms; 

    [SerializeField] private Transform levelStart;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform tileParent;

    private const float playerDistSpawnLevelPart = 100f;
    private const float playerDistRemoveLevelPart = 250f;

    private Vector3 lastEndPosition;
    private Vector3 lastStartPosition;

    public enum Stage { Begin, Cave, Temple }
    private Stage stageOn = Stage.Begin;

    private List<Transform> instantiatedPlatforms = new List<Transform>();

    int numOfPlatformsSpawned = 0;

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
        for (int i = 0; i < levelStagePlatforms.Length; i++)
        {
        if (numOfPlatformsSpawned > levelStagePlatforms[i].numOfPlatformsToStartStage)
        {
            stageOn = levelStagePlatforms[i].stage;
        }

        }
        for (int i = 0; i < instantiatedPlatforms.Count; i++)
        {
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

        numOfPlatformsSpawned++;
    }

    private Transform SpawnLevelPart(Vector3 pos, bool isRight)
    {
        Transform randomType;

        //If we have completed all stages, then spawn completely random ones.
        if (numOfPlatformsSpawned > levelStagePlatforms[(int)Stage.Temple].numOfPlatformsToStartStage + 7)
        {
            int randStageIndex = Random.Range(0, levelStagePlatforms.Length);
            Debug.Log("Random Stage Index: " + randStageIndex);
            int randTypeIndex = Random.Range(0, levelStagePlatforms[randStageIndex].platformTypes.Length);
            Debug.Log("Random Type Index: " + randTypeIndex);
            randomType = levelStagePlatforms[randStageIndex].platformTypes[randTypeIndex];
        }
        else
        {
            randomType = levelStagePlatforms[(int)stageOn].platformTypes[Random.Range(0, levelStagePlatforms[(int)stageOn].platformTypes.Length)];
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

