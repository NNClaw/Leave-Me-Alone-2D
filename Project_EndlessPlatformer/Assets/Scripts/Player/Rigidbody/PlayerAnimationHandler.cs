using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour, ICharacterAnimation
{
    private ICharacterManager _playerMain;

    private int _isJumpingHash;
    private int _isRunningHash;
    private int _runningVelocityHash;

    private void OnEnable()
    {
        _playerMain = GetComponent<ICharacterManager>();

        _runningVelocityHash = Animator.StringToHash("Velocity");
        _isJumpingHash = Animator.StringToHash("IsJumping");
        _isRunningHash = Animator.StringToHash("IsRunning");
    }

    public void SetAnimationFloat(int hashID, float animationFloat)
    {
        _playerMain.GetAnimator().SetFloat(hashID, animationFloat);
    }

    public void SetAnimationBool(int hashID, bool animationCondition)
    {
        _playerMain.GetAnimator().SetBool(hashID, animationCondition);
    }

    public int GetJumpingHash()
    {
        return _isJumpingHash;
    }

    public int GetRunningHash()
    {
        return _isRunningHash;
    }

    public int GetRunningVelocityHash()
    {
        return _runningVelocityHash;
    }
}