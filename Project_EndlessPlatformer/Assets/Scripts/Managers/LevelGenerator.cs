using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 40f;

    [SerializeField] private List<Transform> levelParts;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private PlayerMainManager player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("End Point").position;

        int preloadLevelPartCount = 5;

        for (int i = 0; i < preloadLevelPartCount; i++)
        {
            SpawnLevelPart();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(player.gameObject.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform randomLevelPart = levelParts[Random.Range(0, levelParts.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(randomLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("End Point").position;
        
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 position)
    {
        Transform levelPartTransform = Instantiate(levelPart, position, Quaternion.identity, transform);
        return levelPartTransform;
    }
}
