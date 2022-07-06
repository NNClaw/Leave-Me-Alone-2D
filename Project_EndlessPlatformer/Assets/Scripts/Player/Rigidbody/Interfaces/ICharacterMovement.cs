internal interface ICharacterMovement
{
    public void MoveCharacter();
    public void CharacterJump();
    public void CharacterCrouch(bool isCrouching);
    public void CharacterLand();
}