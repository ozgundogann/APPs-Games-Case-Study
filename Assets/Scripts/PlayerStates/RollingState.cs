using UnityEngine;

public class RollingState : IPlayerState
{
    private static readonly int IsFlying = Animator.StringToHash("isFlying");
    
    public void EnterState(CharacterMovement character)
    {
        character.isFlying = false;
        character.GetCharacterPhysics.SetDefaultGravityValue();
        
        character.GetAnimator.SetBool(IsFlying, false);
        Debug.Log("Rolling State");
    }

    public void UpdateState(CharacterMovement character)
    {
        character.MeshRoot.RotateAround(character.MeshRoot.position, Vector3.right, Time.deltaTime * character.rotateSpeed);
    }

    public void ExitState(CharacterMovement character)
    {
    }
}