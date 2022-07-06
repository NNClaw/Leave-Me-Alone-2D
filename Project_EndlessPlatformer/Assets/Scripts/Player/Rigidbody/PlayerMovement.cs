using UnityEngine;

public class PlayerMovement : MonoBehaviour, ICharacterMovement
{
    [SerializeField] private float movementSpeed = 100f;

    private ICharacterAnimation _playerMainAnimation;
    private ICharacterManager _playerMain;
    private IPlayerInput _playerInput;
    private ISwipeDetection _swipeDetection;

    private bool _isJumping;
    private bool _isCrouching;

    private void OnEnable()
    {
        // Components cache and other declarations
        _playerMain = GetComponent<ICharacterManager>();
        _playerMainAnimation = GetComponent<ICharacterAnimation>();

        _playerInput = PlayerInputManager.Instance;
        _swipeDetection = SwipeDetection.Instance;

        // Input subscribtions
        _playerInput.OnJump += CharacterJump;
        _playerInput.OnCrouch += CharacterCrouch;
        _swipeDetection.DirectionUp += CharacterJump;
        _swipeDetection.DirectionDown += CharacterCrouch;
    }

    private void OnDisable()
    {
        // Input unsubscribtions
        _playerInput.OnJump -= CharacterJump;
        _playerInput.OnCrouch -= CharacterCrouch;   
        _swipeDetection.DirectionUp -= CharacterJump;
        _swipeDetection.DirectionDown -= CharacterCrouch;
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

    public void CharacterJump()
    {
        // Manipulating jumping boolean to start the jump
        _isJumping = true;

        // Setting up an animation for jump
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation.GetJumpingHash(), _isJumping);
    }

    public void CharacterLand()
    {
        // Setting up an animation for jumping + falling
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation.GetJumpingHash(), _isJumping);

        // Manipulating jumping boolean to start the jump
        _isJumping = false;
    }

    public void CharacterCrouch(bool isCrouching)
    {
        // TODO: animation implementation for crouching

        _isCrouching = isCrouching;
    }
}