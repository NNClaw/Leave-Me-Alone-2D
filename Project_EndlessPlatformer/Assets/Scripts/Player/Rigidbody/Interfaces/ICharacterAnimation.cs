public interface ICharacterAnimation : IAnimationSetter
{
    public int GetJumpingHash();
    public int GetRunningHash();
    public int GetRunningVelocityHash();
}

public interface IAnimationSetter
{
    public void SetAnimationBool(int hashID, bool animationCondition);
    public void SetAnimationFloat(int hashID, float animationFloat);
}