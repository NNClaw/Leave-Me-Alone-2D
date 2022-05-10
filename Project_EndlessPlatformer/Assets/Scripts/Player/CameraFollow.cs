using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    [SerializeField] Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = transform.position - playerPosition.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PlayerFollow();
    }

    private void PlayerFollow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerPosition.position.x, 0f) + cameraOffset, Time.deltaTime * 3f);
    }
}
