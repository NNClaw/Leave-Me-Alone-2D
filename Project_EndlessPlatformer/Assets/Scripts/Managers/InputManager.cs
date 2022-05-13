using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Be aware, that this class has an execution order. -50
/// </summary>
public class InputManager : Singleton<InputManager>
{
    private PlayerControls _playerControls;
    private Camera _mainCam;

    #region Events

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public delegate void JumpStart();
    public event JumpStart OnJump;

    public delegate void ProcessCrouch(bool isCrouching);
    public event ProcessCrouch OnCrouch;

    #endregion

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _mainCam = Camera.main;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Start()
    {
        _playerControls.Player.PrimaryContact.started += StartTouchPrimary;
        _playerControls.Player.PrimaryContact.canceled += EndTouchPrimary;
        _playerControls.Player.Jump.performed += Jump;
        _playerControls.Player.Crouch.performed += Crouch;
        _playerControls.Player.Crouch.canceled += Crouch;
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnCrouch?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnCrouch?.Invoke(false);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJump?.Invoke();
        }
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        StartCoroutine(WaitForStartTouchInput(context));
    }

    private IEnumerator WaitForStartTouchInput(InputAction.CallbackContext context)
    {
        yield return new WaitForEndOfFrame();

        var position = _playerControls.Player.PrimaryPosition.ReadValue<Vector2>();
        if(position.x == 0 && position.y == 0)
        {
            yield return null;
        }

        OnStartTouch?.Invoke(Utils.ScreenToWorldPoint(_playerControls.Player.PrimaryPosition.ReadValue<Vector2>(), _mainCam), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        OnEndTouch?.Invoke(Utils.ScreenToWorldPoint(_playerControls.Player.PrimaryPosition.ReadValue<Vector2>(), _mainCam), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorldPoint(_playerControls.Player.PrimaryPosition.ReadValue<Vector2>(), _mainCam);
    }
}
