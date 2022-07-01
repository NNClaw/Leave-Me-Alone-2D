using UnityEngine;

public class PlayerMovement : MonoBehaviour, ICharacterMovement
{
    [SerializeField] private float movementSpeed = 100f;

    private ICharacterManager _playerMain;
    private IAnimationHandler _playerMainAnimation;
    private SwipeDetection _swipeDetection;

    private bool _isJumping;
    private bool _isCrouching;

    private void OnEnable()
    {
        // Components cache and other declarations
        _playerMain = GetComponent<ICharacterManager>();
        _swipeDetection = SwipeDetection.Instance;

        // Input subscribtions
        _playerMain.GetInputManager().OnJump += OnJump;
        _playerMain.GetInputManager().OnCrouch += OnCrouch;
        _swipeDetection.DirectionUp += OnJump;
        _swipeDetection.DirectionDown += OnCrouch;

        // Animation variable setup
        _playerMainAnimation = GetComponent<IAnimationHandler>();

        Debug.Log("PlayerMovement - ON!");
    }

    private void OnDisable()
    {
        // Input unsubscribtions
        _playerMain.GetInputManager().OnJump -= OnJump;
        _playerMain.GetInputManager().OnCrouch -= OnCrouch;   
        _swipeDetection.DirectionUp -= OnJump;
        _swipeDetection.DirectionDown -= OnCrouch;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    public void MoveCharacter()
    {
        // Setting up animations
        _playerMainAnimation.SetAnimationFloat(_playerMainAnimation.GetRunningVelocityHash(), movementSpeed);
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation.GetRunningHash(), true);

        // Executing player movement
        _playerMain.GetCharacterController2D().Move(Time.fixedDeltaTime * movementSpeed, _isCrouching, _isJumping);
    }

    private void OnJump()
    {
        // Manipulating jumping boolean to start the jump
        _isJumping = true;

        // Setting up an animation for jump
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation.GetJumpingHash(), _isJumping);
    }

    public void OnLanding()
    {
        // Setting up an animation for falling
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation.GetJumpingHash(), _isJumping);

        // Manipulating jumping boolean to start the jump
        _isJumping = false;
    }

    private void OnCrouch(bool isCrouching)
    {
        // TODO: animation implementation for crouching

        _isCrouching = isCrouching;
    }
}

internal interface ICharacterMovement
{
    public void MoveCharacter();
}