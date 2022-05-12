using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartBehaviour : MonoBehaviour
{
    [SerializeField] BoxCollider2D obstacleCollider;

    private void Awake()
    {
        
    }

#if !UNITY_EDITOR

    private void OnBecameInvisible()
    {
        StartCoroutine(DestroyObjectDelay());
    }

    IEnumerator DestroyObjectDelay()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }

#endif

    // fix colliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void ProcessCollision(GameObject colliderGameObject)
    {
        if (colliderGameObject.CompareTag("Player"))
        {
            Debug.Log("Obstacle hit!");
        }
    }

}
