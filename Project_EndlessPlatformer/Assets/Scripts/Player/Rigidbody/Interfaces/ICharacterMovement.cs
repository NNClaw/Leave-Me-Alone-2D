using UnityEngine;

internal interface ICharacterMovement
{
    public void MoveCharacter();

    // Bellow method is for movement testing purposes only
    public void MoveCharacter(Vector2 currentMovement, bool isRunning);
    public void CharacterJump();
    public void CharacterCrouch(bool isCrouching);
    public void CharacterLand();
}