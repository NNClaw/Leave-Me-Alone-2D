using UnityEngine;

/// <summary>
/// Be aware, that this class has an execution order. -20
/// </summary>
public class PlayerMainManager : MonoBehaviour, ICharacterManager
{
    internal Rigidbody2D c_playerRigidbody;
    internal Animator c_playerAnimator;
    internal SpriteRenderer c_playerSprite;

    internal ICharacterController2D i_playerController;
    internal ICharacterMovement i_playerMovement;
    internal ICharacterAnimation i_playerAnimation;

    private void Awake()
    {
        c_playerRigidbody = GetComponent<Rigidbody2D>();
        c_playerAnimator = GetComponentInChildren<Animator>();
        c_playerSprite = GetComponentInChildren<SpriteRenderer>();

        i_playerController = GetComponent<ICharacterController2D>();
        i_playerMovement = GetComponent<ICharacterMovement>();
        i_playerAnimation = GetComponent<ICharacterAnimation>();
    }

    public Rigidbody2D GetRigidbody()
    {
        return c_playerRigidbody;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return c_playerSprite;
    }

    public Animator GetAnimator()
    {
        return c_playerAnimator;
    }

    public CharacterController2D GetCharacterController2D()
    {
        return (CharacterController2D)i_playerController;
    }
}