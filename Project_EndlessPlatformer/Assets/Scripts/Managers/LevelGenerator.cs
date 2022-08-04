using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 40f;
    private const int ENDPOINT_TRANSFORM_ID = 1;

    [SerializeField] private List<LevelPartBehaviour> levelParts;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private PlayerMainManager player;

    private Vector3 lastEndPosition;
    private IObjectPool<LevelPartBehaviour> objectPool;

    public static float PlayerDistanceToLevelPart { get { return PLAYER_DISTANCE_SPAWN_LEVEL_PART; } }
    public PlayerMainManager Player { get { return player; } }

    private void Awake()
    {
        objectPool = new ObjectPool<LevelPartBehaviour>(CreateLevelPart, OnLevelPartGet, OnLevelPartRelease);

        lastEndPosition = levelPart_Start.GetChild(ENDPOINT_TRANSFORM_ID).position;

        int preloadLevelPartCount = 2;

        for (int i = 0; i < preloadLevelPartCount; i++)
        {
            SpawnLevelPart();
        }
    }

    private void OnLevelPartRelease(LevelPartBehaviour levelPart)
    {
        levelPart.gameObject.SetActive(false);
    }

    private void OnLevelPartGet(LevelPartBehaviour levelPart)
    {
        levelPart.gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(player.gameObject.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    // Fix randomness
    private LevelPartBehaviour CreateLevelPart()
    {
        // This Random class is in UnityEngine namespace and not in System
        var randomLevelPart = levelParts[Random.Range(0, levelParts.Count)];
        var levelPart = Instantiate(randomLevelPart);
        levelPart.SetPool(objectPool);

        return levelPart;
    }

    private void SpawnLevelPart()
    {
        var levelPart = objectPool.Get();
        levelPart.transform.position = lastEndPosition;
        lastEndPosition = levelPart.transform.GetChild(ENDPOINT_TRANSFORM_ID).position;
    }
}
