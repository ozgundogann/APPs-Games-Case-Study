public class RollingStateNode : IPlayerStateNode
{
    public void EnterState(StateManager stateManager)
    {
        stateManager.currentPlayerState = PlayerStates.ROLLING;
        stateManager.rotateMovement.enabled = true;
        stateManager.characterMovement.enabled = true;
    }

    public void UpdateState(StateManager stateManager)
    {
    }

    public void ExitState(StateManager stateManager)
    {
        stateManager.rotateMovement.enabled = false;
    }
}