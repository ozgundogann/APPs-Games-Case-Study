public class RollingStateNode : IPlayerStateNode
{
    public void EnterState(CharacterMovements characterMovements)
    {
        characterMovements.currentPlayerState = PlayerStates.ROLLING;
        characterMovements.rotateMovement.enabled = true;
    }

    public void UpdateState(CharacterMovements characterMovements)
    {
    }

    public void ExitState(CharacterMovements characterMovements)
    {
        characterMovements.rotateMovement.enabled = false;
    }
}