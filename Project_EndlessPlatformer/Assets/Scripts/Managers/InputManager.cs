using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    private PlayerControls playerControls;
    private Camera mainCam;

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
        playerControls = new PlayerControls();
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Player.PrimaryContact.started += StartTouchPrimary;
        playerControls.Player.PrimaryContact.canceled += EndTouchPrimary;
        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Crouch.performed += Crouch;
        playerControls.Player.Crouch.canceled += Crouch;
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

        var position = playerControls.Player.PrimaryPosition.ReadValue<Vector2>();
        if(position.x == 0 && position.y == 0)
        {
            yield return null;
        }

        OnStartTouch?.Invoke(Utils.ScreenToWorldPoint(playerControls.Player.PrimaryPosition.ReadValue<Vector2>(), mainCam), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        OnEndTouch?.Invoke(Utils.ScreenToWorldPoint(playerControls.Player.PrimaryPosition.ReadValue<Vector2>(), mainCam), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorldPoint(playerControls.Player.PrimaryPosition.ReadValue<Vector2>(), mainCam);
    }
}
