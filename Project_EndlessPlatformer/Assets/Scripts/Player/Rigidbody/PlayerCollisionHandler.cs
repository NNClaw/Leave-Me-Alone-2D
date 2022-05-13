using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private PlayerMainManager playerMain;

    private void Start()
    {
        playerMain = GetComponent<PlayerMainManager>();
        Debug.Log("PlayerCollisionHandler - ON!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            Debug.Log("Invincibility frames!");
    }
}
