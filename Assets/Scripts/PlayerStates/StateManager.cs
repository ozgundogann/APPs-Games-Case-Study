using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class StateManager : MonoBehaviour
{
    [Header("Camera")] 
    public CinemachineVirtualCamera firstCamera;

    [Header("FlyMovement")] 
    public FlyMovement flyMovement;
    
    [Header("PlayerState")] 
    public PlayerStates currentPlayerState;
    
    [Header("Animator")] 
    public Animator animator;
    
    [Header("CharacterMovement")] 
    public CharacterMovement characterMovement;

    [Header("characterMesh")] 
    public Transform characterMesh;

    [Header("RotateMovement")] 
    public RotateMovement rotateMovement;
    
    private IPlayerStateNode _currentPlayerStateNode;

    public static StateManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

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
        GameManager.OnGameStateChange += ListenGameStateChanges;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= ListenGameStateChanges;
    }

    private void ListenGameStateChanges(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.INGAME:
                HandleInGame();
                break;
            case GameStates.GAMEOVER:
                HandleGameOver();
                break;
            case GameStates.NONE:
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleInGame()
    {
        InputManager.OnTouchBegin += HandleTouchBegin;
        InputManager.OnTouchEnd += HandleOnTouchEnd;
        ChangeStateNode(new ChargingState());
    }

    private void HandleGameOver()
    {
        InputManager.OnTouchBegin -= HandleTouchBegin;
        InputManager.OnTouchEnd -= HandleOnTouchEnd;

        if (currentPlayerState != PlayerStates.ROLLING)
            ChangeStateNode(new RotateStateNode());
    }

    private void HandleOnTouchEnd()
    {
        if (currentPlayerState != PlayerStates.CHARGING)
        {
            ChangeStateNode(new RotateStateNode());
        }
    }

    private void HandleTouchBegin(Vector2 touchPos)
    {
        if (currentPlayerState != PlayerStates.CHARGING)
            ChangeStateNode(new FlyingStateNode());
    }

    private void Update()
    {
        _currentPlayerStateNode?.UpdateState(this);
    }

    public void ChangeStateNode(IPlayerStateNode newState)
    {
        if (newState == null) return;
        if (_currentPlayerStateNode != null)
            if (_currentPlayerStateNode == newState)
                return;

        _currentPlayerStateNode?.ExitState(this);
        _currentPlayerStateNode = newState;
        _currentPlayerStateNode.EnterState(this);
    }
}