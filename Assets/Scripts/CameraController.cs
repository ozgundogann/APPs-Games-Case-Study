using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cineMachineBrain;
    [SerializeField] private CinemachineVirtualCamera followingCamera;
    
    [SerializeField] private float defaultBlendTime = 0.5f;

    private Transform followingTransform;
    
    private void OnEnable()
    {
        GameManager.OnGameStateChange += ListenGameStates;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= ListenGameStates;
    }

    private void ListenGameStates(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.NONE:
                break;
            case GameStates.INGAME:
                //HandleInGame();
                break;
            case GameStates.GAMEOVER:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleInGame()
    {
        cineMachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        followingCamera.Follow = followingTransform;
        followingCamera.LookAt = followingTransform;
    }

    private void HandleGameOver()
    {
        followingTransform = followingCamera.Follow;
        followingCamera.Follow = null;
        followingCamera.LookAt = null;
    }
}