using UnityEngine;

public class ChargingState : IPlayerStateNode
{
    public void EnterState(StateManager stateManager)
    {
        InitializeChargingState(stateManager);

        AttachCharacterToStick(stateManager);

        stateManager.firstCamera.Priority = 20;
    }

    public void UpdateState(StateManager stateManager)
    {
    }

    public void ExitState(StateManager stateManager)
    {
        stateManager.stickThrowMechanics.enabled = false;
        stateManager.firstCamera.Priority = 0;

        DetachCharacterFromStick(stateManager);
    }

    private static void InitializeChargingState(StateManager stateManager)
    {
        stateManager.stickThrowMechanics.enabled = true;
        stateManager.currentPlayerState = PlayerStates.CHARGING;
    }

    private static void AttachCharacterToStick(StateManager stateManager)
    {
        GameObject gameObject;
        (gameObject = stateManager.characterMovement.gameObject).transform.SetParent(stateManager.topOfStick);
        gameObject.transform.localPosition = Vector3.zero;
    }

    private static void DetachCharacterFromStick(StateManager stateManager)
    {
        stateManager.characterMovement.gameObject.transform.SetParent(null);
        stateManager.characterMovement.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}