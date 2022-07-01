using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartBehaviour : MonoBehaviour, IObstacle
{

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
}

public interface IObstacle
{

}