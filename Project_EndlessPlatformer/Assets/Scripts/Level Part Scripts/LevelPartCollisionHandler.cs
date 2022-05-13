using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void ProcessCollision(GameObject colliderGameObject)
    {
        if (colliderGameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Debug.Log("Obstacle hit!");
        }
    }
}
