using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cineMachineBrain;
    [SerializeField] private CinemachineVirtualCamera followingCamera;
    
    [SerializeField] private float defaultBlendTime = 0.5f;

    [SerializeField] private Transform followingTransform;
    
    public static CameraController Instance { get; private set; }

    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
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
                HandleInGame();
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
        followingCamera.Follow = followingTransform;
        followingCamera.LookAt = followingTransform;
    }

    private void HandleGameOver()
    {
        followingCamera.Follow = null;
        followingCamera.LookAt = null;
        SetCameraBlendCut();
    }

    public void SetCameraBlendCut()
    {
        cineMachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
    }

    public void SetDefaultBlend()
    {
        cineMachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
    }
}