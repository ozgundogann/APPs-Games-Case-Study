public class ChargingState : IPlayerStateNode
{
    public void EnterState(PlayerManager playerManager)
    {
        playerManager.stickAnimation.enabled = true;
        playerManager.currentPlayerState = PlayerStates.CHARGING;
    }

    public void UpdateState(PlayerManager playerManager)
    {
    }

    public void ExitState(PlayerManager playerManager)
    {
        playerManager.stickAnimation.enabled = false;
        playerManager.characterPhysics.gameObject.transform.SetParent(null);
    }
}