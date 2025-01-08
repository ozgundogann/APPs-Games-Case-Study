using System;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    public FlyMovement flyMovement;
    public RotateMovement rotateMovement;
    public PlayerStates currentPlayerState;
    public GameStates currentGameState;
    public PlayerInputs playerInputs;
    public Animator animator;
    public CharacterPhysics characterPhysics;
    

    private IPlayerStateNode _currentPlayerStateNode;

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
        ChangeStateNode(new RollingStateNode());
    }

    private void HandleTouchBegin()
    {
        ChangeStateNode(new FlyingStateNode());
    }

    private void Update()
    {
        _currentPlayerStateNode?.UpdateState(this);
    }

    private void ChangeStateNode(IPlayerStateNode newState)
    {
        if(_currentPlayerStateNode != null)
            if (_currentPlayerStateNode == newState) return;

        _currentPlayerStateNode?.ExitState(this);
        _currentPlayerStateNode = newState;
        _currentPlayerStateNode.EnterState(this);
    }
}