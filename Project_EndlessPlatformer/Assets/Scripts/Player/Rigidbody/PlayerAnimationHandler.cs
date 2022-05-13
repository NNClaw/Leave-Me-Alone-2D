using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private PlayerMainManager _playerMain;
    internal Animator _playerAnimator;

    private void Start()
    {
        _playerMain = GetComponent<PlayerMainManager>();
        _playerAnimator = GetComponentInChildren<Animator>();
        Debug.Log("PlayerAnimationHandler - ON!");
    }
}
