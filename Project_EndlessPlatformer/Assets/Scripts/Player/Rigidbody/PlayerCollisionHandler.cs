using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour, ICollisionHandler
{
    [SerializeField] float immortalityTime = 2f;
    [SerializeField] float flickeringSpriteFrequency = .1f;

    private ICharacterManager _playerMain;
    private bool _isImmortal = false;
    private float _immortalTimer;

    private void Start()
    {
        _playerMain = GetComponent<ICharacterManager>();

        _immortalTimer = immortalityTime;

        Debug.Log("PlayerCollisionHandler - ON!");
    }

    private void Update()
    {
        ResetImmortality(_isImmortal);
    }

    private void ResetImmortality(bool resetTimer)
    {
        if (resetTimer)
        {
            _immortalTimer -= Time.deltaTime;

            if(_immortalTimer <= 0)
            {
                _isImmortal = false;
                _immortalTimer = immortalityTime;
            }
        }
    }

    private IEnumerator ProcessImmortality()
    {
        while (_isImmortal)
        {
            _playerMain.GetSpriteRenderer().enabled = false;
            yield return new WaitForSeconds(flickeringSpriteFrequency);
            _playerMain.GetSpriteRenderer().enabled = true;
            yield return new WaitForSeconds(flickeringSpriteFrequency);
        }
    }

    void ICollisionHandler.OnTriggerEnter(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IObstacle obstacle = collision.gameObject.GetComponent<IObstacle>();

        if (obstacle == null)
        {
            _isImmortal = true;
            Debug.Log("Invincibility frames");
        }

        StartCoroutine(ProcessImmortality());
    }
}

public interface ICollisionHandler
{
    public void OnTriggerEnter(Collider2D collision);
}