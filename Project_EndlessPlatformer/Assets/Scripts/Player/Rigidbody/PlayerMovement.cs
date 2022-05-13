using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 100f;

    private PlayerMainManager _playerMain;
    private SwipeDetection _swipeDetection;

    private bool _isJumping;
    private bool _isCrouching;

    private void Start()
    {
        _playerMain = GetComponent<PlayerMainManager>();
        _swipeDetection = SwipeDetection.Instance;
        
        _playerMain._inputManager.OnJump += OnJump;
        _playerMain._inputManager.OnCrouch += OnCrouch;
        _swipeDetection.DirectionUp += OnJump;
        _swipeDetection.DirectionDown += OnCrouch;
        Debug.Log("PlayerMovement - ON!");
    }

    private void OnEnable()
    {
        // the event subscribtions should be here, but it won't work if I put them here
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
        _playerMain._playerAnimation._playerAnimator.SetFloat("Velocity", movementSpeed);
        _playerMain._playerAnimation._playerAnimator.SetBool("IsRunning", true);
        _playerMain._controller.Move(Time.fixedDeltaTime * movementSpeed, _isCrouching, _isJumping);
    }

    private void OnJump()
    {
        _isJumping = true;
        _playerMain._playerAnimation._playerAnimator.SetBool("IsJumping", _isJumping);
    }

    public void OnLanding()
    {
        _isJumping = false;
        _playerMain._playerAnimation._playerAnimator.SetBool("IsJumping", _isJumping);
    }

    private void OnCrouch(bool isCrouching)
    {
        _isCrouching = isCrouching;
    }
}
