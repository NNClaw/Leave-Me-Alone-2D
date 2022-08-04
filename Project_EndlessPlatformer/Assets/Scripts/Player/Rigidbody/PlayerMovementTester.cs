using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTester : MonoBehaviour, ICharacterMovement
{
    [SerializeField] private float movementSpeed = 35f;

    private ICharacterAnimation _playerMainAnimation;
    private ICharacterManager _playerMain;
    private IPlayerInput _playerInput;
    private ISwipeDetection _swipeDetection;

    private Vector2 v_currentMovement;

    private bool _isJumping;
    private bool _isCrouching;
    private bool _isRunning;

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
        _playerInput.OnMove += MoveCharacter;
    }

    private void OnDisable()
    {
        // Input unsubscribtions
        _playerInput.OnJump -= CharacterJump;
        _playerInput.OnCrouch -= CharacterCrouch;
        _swipeDetection.DirectionUp -= CharacterJump;
        _swipeDetection.DirectionDown -= CharacterCrouch;
        _playerInput.OnMove -= MoveCharacter;
    }

    private void FixedUpdate()
    {
        MoveCharacter(v_currentMovement, _isRunning);
    }

    public void MoveCharacter(Vector2 currentMovement, bool isRunning)
    {
        // Setting up variables for movement and animation. P.S. This shite works somehow and I have no idea why, cuz it makes no fcking sense to me. This Vector2 is EMPTY! HOW THE FUCK DOES IT WORK?! OMFG
        v_currentMovement = currentMovement;
        _isRunning = isRunning;

        // Setting up animations
        _playerMainAnimation.SetAnimationFloat(_playerMainAnimation.GetRunningVelocityHash(), movementSpeed);
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation.GetRunningHash(), _isRunning);

        // Moving the character
        _playerMain.GetCharacterController2D().Move(v_currentMovement.x * movementSpeed * Time.fixedDeltaTime, _isCrouching, _isJumping);
    }

    public void MoveCharacter() { }

    public void CharacterCrouch(bool isCrouching)
    {
        _isCrouching = isCrouching;
    }

    public void CharacterJump()
    {
        // Manipulating jumping boolean to start the jump
        if(!_isJumping)
            _isJumping = true;

        // Setting up an animation for jump
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation.GetJumpingHash(), _isJumping);
    }

    public void CharacterLand()
    {
        // Manipulating jumping boolean to end the jump
        if(_isJumping)
            _isJumping = false;

        // Setting up an animation for falling
        _playerMainAnimation.SetAnimationBool(_playerMainAnimation.GetJumpingHash(), _isJumping);
    }
}
