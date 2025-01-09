using DG.Tweening;
using UnityEngine;

public class FlyingStateNode : IPlayerStateNode
{
    private static readonly int IsFlying = Animator.StringToHash("isFlying");

    public void EnterState(PlayerManager playerManager)
    {
        playerManager.currentPlayerState = PlayerStates.FLYING; 
        playerManager.flyMovement.enabled = true;

        playerManager.characterPhysics.DecreaseVelocity();
        
        playerManager.animator.SetBool(IsFlying, true);
    }

    public void UpdateState(PlayerManager playerManager)
    {
    }

    public void ExitState(PlayerManager playerManager)
    {
        playerManager.flyMovement.enabled = false;
        playerManager.animator.SetBool(IsFlying, false);

    }
}