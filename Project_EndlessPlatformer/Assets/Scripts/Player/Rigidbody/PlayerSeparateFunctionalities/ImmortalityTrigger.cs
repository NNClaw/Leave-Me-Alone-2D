using System.Collections;
using UnityEngine;

public class ImmortalityTrigger : MonoBehaviour, IImmortality
{
    [SerializeField] float immortalityTime = 2f;
    [SerializeField] float flickeringSpriteFrequency = .1f;

    private ICharacterManager _playerMain;

    private bool _isImmortal = false;
    private float _immortalTimer;

    public bool IsImmortal { get { return _isImmortal; } set { _isImmortal = value; } }
    public float ImmortalityStartTime { get { return immortalityTime; } }
    public float ImmortalityTimer { get { return _immortalTimer; } set { _immortalTimer = value; } }

    private void OnEnable()
    {
        _playerMain = GetComponent<ICharacterManager>();
    }

    public IEnumerator ProcessImmortality()
    {
        while (_isImmortal && _playerMain != null)
        {
            _playerMain.GetSpriteRenderer().enabled = false;
            yield return new WaitForSeconds(flickeringSpriteFrequency);
            _playerMain.GetSpriteRenderer().enabled = true;
            yield return new WaitForSeconds(flickeringSpriteFrequency);
        }
    }

    public void ResetImmortality(bool resetTimer)
    {
        if (resetTimer)
        {
            _immortalTimer -= Time.deltaTime;

            if (_immortalTimer <= 0)
            {
                _isImmortal = false;
                _immortalTimer = immortalityTime;
            }
        }
    }
}
