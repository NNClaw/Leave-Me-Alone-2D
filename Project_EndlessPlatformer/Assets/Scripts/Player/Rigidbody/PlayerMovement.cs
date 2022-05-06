using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float movementSpeed = 100f;
    Animator playerAnimator;

    private InputManager inputManager;
    private SwipeDetection swipeDetection;

    private bool _isJumping;
    private bool _isCrouching;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        swipeDetection = SwipeDetection.Instance;
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        inputManager.OnJump += OnJump;
        inputManager.OnCrouch += OnCrouch;
        swipeDetection.DirectionUp += OnJump;
        swipeDetection.DirectionDown += OnCrouch;
    }

    private void OnDisable()
    {
        inputManager.OnJump -= OnJump;
        inputManager.OnCrouch -= OnCrouch;
        swipeDetection.DirectionUp -= OnJump;
        swipeDetection.DirectionDown -= OnCrouch;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        playerAnimator.SetFloat("Velocity", movementSpeed);
        playerAnimator.SetBool("IsRunning", true);
        controller.Move(Time.fixedDeltaTime * movementSpeed, _isCrouching, _isJumping);
    }

    private void OnJump()
    {
        _isJumping = true;
        playerAnimator.SetBool("IsJumping", _isJumping);
    }

    public void OnLanding()
    {
        playerAnimator.SetBool("IsJumping", _isJumping);
        _isJumping = false;
    }

    private void OnCrouch(bool isCrouching)
    {
        _isCrouching = isCrouching;
    }
}
