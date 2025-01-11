using UnityEngine;

public class ChargingState : IPlayerStateNode
{
    public void EnterState(StateManager stateManager)
    {
        InitializeChargingState(stateManager);
        stateManager.characterMesh.rotation = Quaternion.Euler(Vector3.zero);
        stateManager.firstCamera.Priority = 20;
    }

    public void UpdateState(StateManager stateManager)
    {
    }

    public void ExitState(StateManager stateManager)
    {
        stateManager.firstCamera.Priority = 0;

        DetachCharacterFromStick(stateManager);
        
        CameraController.Instance.SetDefaultBlend();
    }

    private static void InitializeChargingState(StateManager stateManager)
    {
        stateManager.currentPlayerState = PlayerStates.CHARGING;
    }

    

    private static void DetachCharacterFromStick(StateManager stateManager)
    {
        stateManager.characterMovement.gameObject.transform.SetParent(null);
        stateManager.characterMovement.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}