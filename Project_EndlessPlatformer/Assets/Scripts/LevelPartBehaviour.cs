using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartBehaviour : MonoBehaviour
{
    #if !UNITY_EDITOR

    private void OnBecameInvisible()
    {
        Destroy(gameObject);   
    }

    #endif

}
