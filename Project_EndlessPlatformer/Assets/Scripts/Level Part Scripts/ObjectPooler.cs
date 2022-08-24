using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    private static int countIndex = 0;
    private ILevelGenerator listOfObjectsToPool;
    private List<IObjectPool<IPoolableObject>> _objectPools;

    private void Awake()
    {
        listOfObjectsToPool = GetComponent<ILevelGenerator>();
    }

    public List<IObjectPool<IPoolableObject>> CreateListOfObjectPools<T>(List<IObjectPool<IPoolableObject>> objectPools, List<T> levelParts)
    {
        objectPools = new List<IObjectPool<IPoolableObject>>();

        foreach (var item in levelParts)
        {
            objectPools.Add(new ObjectPool<IPoolableObject>(CreateLevelPart, OnLevelPartGet, OnLevelPartRelease, maxSize: 3));
        }

        _objectPools = objectPools;

        return objectPools;
    }

    private IObstacle CreateLevelPart()
    {
        var objectToPoolList = listOfObjectsToPool.GetGameObjects();

        // Game object to spawn with set index
        var levelPart = Instantiate(objectToPoolList[countIndex], transform);

        // Set object pool
        levelPart.SetPool(_objectPools[countIndex]);

        // Manipulating index by incrementing it or reassigning it to 0
        countIndex = Utils.IndexManipulator(countIndex, objectToPoolList);

        // Return game object
        return levelPart;
    }

    private void OnLevelPartRelease(IPoolableObject levelPart)
    {
        levelPart.GetGameObjectInstance().SetActive(false);
    }

    private void OnLevelPartGet(IPoolableObject levelPart)
    {
        levelPart.GetGameObjectInstance().SetActive(true);
    }

}
