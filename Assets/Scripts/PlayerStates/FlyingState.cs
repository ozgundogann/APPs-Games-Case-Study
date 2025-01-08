using DG.Tweening;
using UnityEngine;

public class FlyingState : IPlayerState
{
    private Tween newTween;
    private static readonly int IsFlying = Animator.StringToHash("isFlying");


    public void EnterState(CharacterMovement character)
    {
        character.isFlying = true;
        character.GetCharacterPhysics.gravity *= character.flyingGravityRate;

        //y value of the velocity is resetting for a moment.
        character.GetCharacterPhysics.Velocity = new Vector3(
            character.GetCharacterPhysics.Velocity.x,
            character.GetCharacterPhysics.Velocity.y * 0.1f,
            character.GetCharacterPhysics.Velocity.z
        );

        var eulerAngles = character.transform.eulerAngles;

        newTween?.Kill();
        newTween = character.MeshRoot.DOLocalRotate(new Vector3(90, eulerAngles.y, eulerAngles.z), 0.2f)
            .SetEase(Ease.Linear);
        
        character.GetAnimator.SetBool(IsFlying, true);
    }

    public void UpdateState(CharacterMovement character)
    {
    }

    public void ExitState(CharacterMovement character)
    {
    }
}