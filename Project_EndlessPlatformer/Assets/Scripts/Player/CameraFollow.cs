using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    [SerializeField] float yOffset = 4f;

    // Update is called once per frame
    void Update()
    {
        PlayerFollow();
    }

    void PlayerFollow()
    {
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y + yOffset);
    }
}
