using DG.Tweening;
using UnityEngine;

public class FlyingStateNode : IPlayerStateNode
{
    private static readonly int IsFlying = Animator.StringToHash("isFlying");

    public void EnterState(CharacterMovements characterMovements)
    {
        characterMovements.currentPlayerState = PlayerStates.FLYING; 
        characterMovements.flyMovement.enabled = true;

        characterMovements.characterPhysics.DecreaseVelocity();
        
        characterMovements.animator.SetBool(IsFlying, true);
    }

    public void UpdateState(CharacterMovements characterMovements)
    {
    }

    public void ExitState(CharacterMovements characterMovements)
    {
        characterMovements.flyMovement.enabled = false;
        characterMovements.animator.SetBool(IsFlying, false);

    }
}