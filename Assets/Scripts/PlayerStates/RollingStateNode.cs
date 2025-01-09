public class RollingStateNode : IPlayerStateNode
{
    public void EnterState(PlayerManager playerManager)
    {
        playerManager.currentPlayerState = PlayerStates.ROLLING;
        playerManager.rotateMovement.enabled = true;
        playerManager.characterPhysics.enabled = true;
    }

    public void UpdateState(PlayerManager playerManager)
    {
    }

    public void ExitState(PlayerManager playerManager)
    {
        playerManager.rotateMovement.enabled = false;
    }
}