using UnityEngine;

/// <summary>
/// Be aware, that this class has an execution order. -20
/// </summary>
public class PlayerMainManager : MonoBehaviour, ICharacterManager
{
    internal Rigidbody2D _playerRigidbody;
    internal Animator _playerAnimator;
    internal SpriteRenderer _playerSprite;

    internal ICharacterController2D _playerController;
    internal ICharacterMovement _playerMovement;
    internal ICollisionHandler _playerCollision;
    internal IAnimationHandler _playerAnimation;
    internal IInputManager _inputManager;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();

        _playerController = GetComponent<ICharacterController2D>();
        _playerMovement = GetComponent<ICharacterMovement>();
        _playerCollision = GetComponent<ICollisionHandler>();
        _playerAnimation = GetComponentInChildren<IAnimationHandler>();
        _inputManager = PlayerInputManager.Instance;
        Debug.Log("PlayerMainManager - ON!");
    }

    public Rigidbody2D GetRigidbody()
    {
        return _playerRigidbody;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return _playerSprite;
    }

    public Animator GetAnimator()
    {
        return _playerAnimator;
    }

    public CharacterController2D GetCharacterController2D()
    {
        return (CharacterController2D)_playerController;
    }

    public PlayerInputManager GetInputManager()
    {
        return (PlayerInputManager)_inputManager;
    }
}

public interface ICharacterManager
{
    public Rigidbody2D GetRigidbody();
    public SpriteRenderer GetSpriteRenderer();
    public Animator GetAnimator();
    public CharacterController2D GetCharacterController2D();
    public PlayerInputManager GetInputManager();
}