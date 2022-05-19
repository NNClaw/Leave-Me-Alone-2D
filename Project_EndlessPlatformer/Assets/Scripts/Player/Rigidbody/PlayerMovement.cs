using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 100f;

    private PlayerMainManager _playerMain;
    private SwipeDetection _swipeDetection;
    private PlayerAnimationHandler _playerMainAnimation;

    private bool _isJumping;
    private bool _isCrouching;

    private void OnEnable()
    {
        // Components cache and other declarations
        _playerMain = GetComponent<PlayerMainManager>();
        _swipeDetection = SwipeDetection.Instance;

        // Input subscribtions
        _playerMain._inputManager.OnJump += OnJump;
        _playerMain._inputManager.OnCrouch += OnCrouch;
        _swipeDetection.DirectionUp += OnJump;
        _swipeDetection.DirectionDown += OnCrouch;

        // Animation variable setup
        _playerMainAnimation = _playerMain._playerAnimation;

        Debug.Log("PlayerMovement - ON!");
    }

    private void OnDisable()
    {
        _playerMain._inputManager.OnJump -= OnJump;
        _playerMain._inputManager.OnCrouch -= OnCrouch;
        
        _swipeDetection.DirectionUp -= OnJump;
        _swipeDetection.DirectionDown -= OnCrouch;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Setting up animations
        _playerMainAnimation.SetAnimationFloat(_playerMainAnimation._runningVelocityHash, movementSpeed);
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation._isRunningHash, true);

        // Executing player movement
        _playerMain._controller.Move(Time.fixedDeltaTime * movementSpeed, _isCrouching, _isJumping);
    }

    private void OnJump()
    {
        // Manipulating jumping boolean to start the jump
        _isJumping = true;

        // Setting up an animation for jump
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation._isJumpingHash, _isJumping);
    }

    public void OnLanding()
    {
        // Setting up an animation for falling
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation._isJumpingHash, _isJumping);

        // Manipulating jumping boolean to start the jump
        _isJumping = false;
    }

    private void OnCrouch(bool isCrouching)
    {
        // TODO: animation implementation for crouching

        _isCrouching = isCrouching;
    }
}
