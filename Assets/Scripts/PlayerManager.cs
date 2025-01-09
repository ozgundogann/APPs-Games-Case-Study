using System;
using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Cameras")] 
    public CinemachineVirtualCamera firstCamera;
    public CinemachineVirtualCamera followingCamera;
    
    public FlyMovement flyMovement;
    public RotateMovement rotateMovement;
    public PlayerStates currentPlayerState;
    public GameStates currentGameState;
    public PlayerInputs playerInputs;
    public Animator animator;
    public CharacterPhysics characterPhysics;
    public Transform characterMesh;
    public ManipulateStickAnimation stickAnimation;
    

    private IPlayerStateNode _currentPlayerStateNode;

    public static PlayerManager Instance { get; private set; }

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
        GameManager.Instance.OnGameStateChange += ListenGameStateChanges;
        currentGameState = GameManager.Instance.CurrentGameState;
        
        //SİLİNECEK
        HandleInGame();
    }

    private void ListenGameStateChanges(GameStates newState)
    {
        if(currentGameState == newState) return;

        switch (newState)
        {
            case GameStates.LOBBY:
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
        playerInputs.OnTouchBegin += HandleTouchBegin;
        playerInputs.OnTouchEnd += HandleOnTouchEnd;
        playerInputs.OnTouchHold += HandleOnTouchHold;
        
        ChangeStateNode(new ChargingState());
    }

    private void HandleOnTouchHold(Vector2 obj)
    {
    }

    private void HandleGameOver()
    {
        playerInputs.OnTouchBegin -= HandleTouchBegin;
        playerInputs.OnTouchEnd -= HandleOnTouchEnd;
        playerInputs.OnTouchHold -= HandleOnTouchHold;

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