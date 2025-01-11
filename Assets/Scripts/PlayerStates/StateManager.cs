using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class StateManager : MonoBehaviour
{
    [Header("Cameras")] 
    public CinemachineVirtualCamera firstCamera;
    public CinemachineVirtualCamera followingCamera;
    
    public FlyMovement flyMovement;
    public RotateMovement rotateMovement;
    public PlayerStates currentPlayerState;
    public GameStates currentGameState;
    public InputManager inputManager;
    public Animator animator;
    public CharacterMovement characterMovement;
    public Transform characterMesh;
    public Transform topOfStick;
    public StickThrowMechanics stickThrowMechanics;
    

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
    
    private void Start()
    {
        GameManager.OnGameStateChange += ListenGameStateChanges;
        currentGameState = GameManager.Instance.CurrentGameState;
        
        //SİLİNECEK
        HandleInGame();
    }

    private void ListenGameStateChanges(GameStates newState)
    {
        if(currentGameState == newState) return;

        switch (newState)
        {
            case GameStates.STARTGAME:
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
        InputManager.OnTouchBegin += HandleTouchBegin;
        InputManager.OnTouchEnd += HandleOnTouchEnd;
        
        ChangeStateNode(new ChargingState());
    }

    private void HandleGameOver()
    {
        InputManager.OnTouchBegin -= HandleTouchBegin;
        InputManager.OnTouchEnd -= HandleOnTouchEnd;
    }

    private void HandleOnTouchEnd()
    {
        if(currentPlayerState != PlayerStates.CHARGING)
            ChangeStateNode(new RollingStateNode());
    }

    private void HandleTouchBegin(Vector2 touchPos)
    {
        if(currentPlayerState != PlayerStates.CHARGING)
            ChangeStateNode(new FlyingStateNode());
    }

    private void Update()
    {
        _currentPlayerStateNode?.UpdateState(this);
    }

    public void ChangeStateNode(IPlayerStateNode newState)
    {
        if(_currentPlayerStateNode != null)
            if (_currentPlayerStateNode == newState) return;

        _currentPlayerStateNode?.ExitState(this);
        _currentPlayerStateNode = newState;
        _currentPlayerStateNode.EnterState(this);
    }
}