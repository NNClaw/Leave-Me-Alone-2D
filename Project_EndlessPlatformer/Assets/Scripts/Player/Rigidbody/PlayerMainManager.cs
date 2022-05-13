using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Be aware, that this class has an execution order. -20
/// </summary>
public class PlayerMainManager : MonoBehaviour
{
    internal Rigidbody2D _playerRigidbody;
    internal CharacterController2D _controller;
    internal PlayerMovement _playerMovement;
    internal PlayerCollisionHandler _playerCollision;
    internal PlayerAnimationHandler _playerAnimation;
    internal InputManager _inputManager;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _inputManager = InputManager.Instance;
        _controller = GetComponent<CharacterController2D>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _playerAnimation = GetComponentInChildren<PlayerAnimationHandler>();
        Debug.Log("PlayerMainManager - ON!");
    }
}
