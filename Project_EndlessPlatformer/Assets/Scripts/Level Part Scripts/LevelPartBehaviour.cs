using UnityEngine;
using UnityEngine.Pool;

public class LevelPartBehaviour : MonoBehaviour, IObstacle
{
    private IObjectPool<IPoolableObject> _pool;
    private LevelGenerator levelGenerator;

    private void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    public void SetPool(IObjectPool<IPoolableObject> pool)
    {
        _pool = pool;
    }

    private void Update()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (Vector3.Distance(levelGenerator.Player.transform.position, transform.position) > levelGenerator.PlayerDistanceToLevelPart)
        {
            if (_pool != null)
            {
                _pool.Release(this);
            }
        }
    }

    public GameObject GetGameObjectInstance()
    {
        return gameObject;
    }
}

public interface IObstacle : IPoolableObject
{
    
}

public interface IPoolableObject
{
    public GameObject GetGameObjectInstance();
}