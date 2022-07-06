using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour, ICollisionHandler
{
    private IImmortality _immortalityDetection;

    private void OnEnable()
    {
        _immortalityDetection = GetComponent<IImmortality>();
    }

    private void Start()
    {
        _immortalityDetection.ImmortalityTimer = _immortalityDetection.ImmortalityStartTime;
    }

    private void Update()
    {
        _immortalityDetection.ResetImmortality(_immortalityDetection.IsImmortal);
    }

    void ICollisionHandler.OnTriggerEnter(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IObstacle obstacle = collision.gameObject.GetComponent<IObstacle>();

        if (obstacle == null)
            _immortalityDetection.IsImmortal = true;

        StartCoroutine(_immortalityDetection.ProcessImmortality());
    }
}