using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LevelPartBehaviour : MonoBehaviour, IObstacle
{
    private IObjectPool<LevelPartBehaviour> _pool;
    private LevelGenerator levelGenerator;

    private void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    public void SetPool(IObjectPool<LevelPartBehaviour> pool) => _pool = pool;

    private void Update()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if(Vector3.Distance(levelGenerator.Player.transform.position, gameObject.transform.position) > LevelGenerator.PlayerDistanceToLevelPart)
        {
            if (_pool != null)
            {
                _pool.Release(this);
            }
        }
    }
}

public interface IObstacle
{

}