using UnityEngine;

public class ChargingState : IPlayerStateNode
{
    public void EnterState(PlayerManager playerManager)
    {
        playerManager.stickAnimation.enabled = true;
        playerManager.currentPlayerState = PlayerStates.CHARGING;

        playerManager.firstCamera.Priority = 20;
    }

    public void UpdateState(PlayerManager playerManager)
    {
    }

    public void ExitState(PlayerManager playerManager)
    {
        playerManager.stickAnimation.enabled = false;
        playerManager.firstCamera.Priority = 0;

        playerManager.characterPhysics.gameObject.transform.SetParent(null);
        playerManager.characterPhysics.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}