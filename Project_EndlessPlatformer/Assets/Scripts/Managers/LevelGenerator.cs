using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LevelGenerator : MonoBehaviour, ILevelGenerator
{
    // Constant values
    private const float PLAYER_DISTANCE_TO_LAST_LEVEL_PART = 40f;
    private const int ENDPOINT_TRANSFORM_ID = 1;
    private const int PART_PRELOAD_COUNT = 2;

    // Values for visibility in the inspector
    [SerializeField] private List<LevelPartBehaviour> levelParts;
    [SerializeField] private Transform levelPart_Start;
    

    // Values for internal work
    private Vector3 lastEndPosition;
    private List<IObjectPool<IPoolableObject>> objectPools;
    private ICharacterManager player;
    private ObjectPooler objectPooler;

    // Properties
    public float PlayerDistanceToLevelPart { get { return PLAYER_DISTANCE_TO_LAST_LEVEL_PART; } }
    public PlayerMainManager Player { get { return (PlayerMainManager) player; } }

    private void Awake()
    {
        objectPooler = GetComponent<ObjectPooler>();
        player = PlayerMainManager.Instance;

        objectPools = objectPooler.CreateListOfObjectPools(objectPools, levelParts);

        lastEndPosition = levelPart_Start.GetChild(ENDPOINT_TRANSFORM_ID).position;

        for (int i = 0; i < PART_PRELOAD_COUNT; i++)
        {
            SpawnLevelPart();
        }
    }

    public List<LevelPartBehaviour> GetGameObjects()
    {
        if (levelParts != null)
            return levelParts;
        else
            throw new System.NullReferenceException("The list, which is currently returned, doesn't have any items in it");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(player.GetGameObject().transform.position, lastEndPosition) < PLAYER_DISTANCE_TO_LAST_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        // Get a random object pool from object pool list
        var randomObjectPool = objectPools[Random.Range(0, objectPools.Count)];
        var levelPart = randomObjectPool.Get();

        GetLastPositionOfLevelPart(levelPart);
    }

    private void GetLastPositionOfLevelPart(IPoolableObject levelPart)
    {
        // Get the endpoint position of the last levelpart and assign it
        levelPart.GetGameObjectInstance().transform.position = lastEndPosition;
        lastEndPosition = levelPart.GetGameObjectInstance().transform.GetChild(ENDPOINT_TRANSFORM_ID).position;
    }
}

internal interface ILevelGenerator
{
    public List<LevelPartBehaviour> GetGameObjects();
}