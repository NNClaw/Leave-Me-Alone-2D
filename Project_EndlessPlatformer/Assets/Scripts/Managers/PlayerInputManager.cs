using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Be aware, that this class has an execution order. -50
/// </summary>
public class PlayerInputManager : Singleton<PlayerInputManager>, IPlayerInput
{
    private PlayerControls _playerControls;
    private Camera _mainCam;

    private Vector2 v_currentMovementInput, v_currentMovement;
    private bool b_isCurrentMovementPressed;

    #region Events

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    
    public delegate void JumpStart();
    public event JumpStart OnJump;

    public delegate void ProcessCrouch(bool isCrouching);
    public event ProcessCrouch OnCrouch;

    public delegate void ProcessMovement(Vector2 currentMovement, bool isRunning);
    public event ProcessMovement OnMove;

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
        _playerControls.Player.MovePlayer.started += MovePlayer;
        _playerControls.Player.MovePlayer.canceled += MovePlayer;
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

    private void MovePlayer(InputAction.CallbackContext context)
    {
        v_currentMovementInput = context.ReadValue<Vector2>();
        v_currentMovement.x = v_currentMovementInput.x;

        b_isCurrentMovementPressed = v_currentMovementInput.x != 0;

        OnMove?.Invoke(v_currentMovement, b_isCurrentMovementPressed);
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

public interface IPlayerInput
{
    event PlayerInputManager.StartTouch OnStartTouch;
    event PlayerInputManager.EndTouch OnEndTouch;

    event PlayerInputManager.JumpStart OnJump;
    event PlayerInputManager.ProcessCrouch OnCrouch;
    event PlayerInputManager.ProcessMovement OnMove;

    public Vector2 PrimaryPosition();
}