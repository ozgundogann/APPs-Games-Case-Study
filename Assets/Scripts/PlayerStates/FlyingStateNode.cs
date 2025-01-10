using DG.Tweening;
using UnityEngine;

public class FlyingStateNode : IPlayerStateNode
{
    private static readonly int IsFlying = Animator.StringToHash("isFlying");

    public void EnterState(StateManager stateManager)
    {
        InitializeFlyingState(stateManager);
        ApplyJumpLandingEffects(stateManager);
    }

    public void UpdateState(StateManager stateManager)
    {
    }
    
    public void ExitState(StateManager stateManager)
    {
        ResetFlyingState(stateManager);
    }
    
    private static void InitializeFlyingState(StateManager stateManager)
    {
        stateManager.currentPlayerState = PlayerStates.FLYING;
        stateManager.flyMovement.enabled = true;
        stateManager.animator.SetBool(IsFlying, true);
    }
    private static void ApplyJumpLandingEffects(StateManager stateManager)
    {
        //stateManager.characterMovement.DecreaseVelocity();
        stateManager.characterMovement.ProcessFlyState();
        stateManager.characterMesh.DOLocalRotate(new Vector3(90, 0, 0), 0.1f);
    }
    private static void ResetFlyingState(StateManager stateManager)
    {
        stateManager.flyMovement.enabled = false;
        stateManager.animator.SetBool(IsFlying, false);
        stateManager.characterMovement.SetDefaultGravityValue();
        stateManager.characterMovement.ExitFlyState();
    }
}