using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] float immortalityTime = 2f;

    private PlayerMainManager _playerMain;
    private SpriteRenderer _playerSprite;
    private bool _isImmortal = false;
    private float _immortalTimer;

    private void Start()
    {
        _playerMain = GetComponent<PlayerMainManager>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            _isImmortal = true;
            StartCoroutine(ProcessImmortality());
            Debug.Log("Invincibility frames!");
    }

    private IEnumerator ProcessImmortality()
    {
        while (_isImmortal)
        {
            _playerSprite.enabled = false;
            yield return new WaitForSeconds(.1f);
            _playerSprite.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
