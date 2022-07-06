using UnityEngine;

public interface ICollisionHandler
{
    public void OnTriggerEnter(Collider2D collision);
}