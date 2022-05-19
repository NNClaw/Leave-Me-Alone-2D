using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private PlayerMainManager _playerMain;
    internal Animator _playerAnimator;

    internal int _isJumpingHash;
    internal int _isRunningHash;
    internal int _runningVelocityHash;

    private void OnEnable()
    {
        _playerMain = GetComponent<PlayerMainManager>();
        _playerAnimator = GetComponentInChildren<Animator>();

        _runningVelocityHash = Animator.StringToHash("Velocity");
        _isJumpingHash = Animator.StringToHash("IsJumping");
        _isRunningHash = Animator.StringToHash("IsRunning");
        Debug.Log("PlayerAnimationHandler - ON!");
    }

    internal void SetAnimationBool(int hashID, bool animationCondition)
    {
        _playerAnimator.SetBool(hashID, animationCondition);
    }

    internal void SetAnimationFloat(int hashID, float animationFloat)
    {
        _playerAnimator.SetFloat(hashID, animationFloat);
    }
}
