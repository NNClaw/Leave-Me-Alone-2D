using UnityEngine;

public interface ICharacterManager
{
    public GameObject GetGameObject();
    public Rigidbody2D GetRigidbody();
    public SpriteRenderer GetSpriteRenderer();
    public Animator GetAnimator();
    public CharacterController2D GetCharacterController2D();
}