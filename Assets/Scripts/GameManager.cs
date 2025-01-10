using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private GameStates currentGameState;


    public static event Action<GameStates> OnGameStateChange;

    public static GameManager Instance { get; private set; }

    public GameStates CurrentGameState => currentGameState;

    
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

    public void TriggerStartGame()
    {
        ChangeGameState(GameStates.STARTGAME);
    }

    public void TriggerInGame()
    {
        ChangeGameState(GameStates.INGAME);
    }

    public void TriggerGameOver()
    {
        ChangeGameState(GameStates.GAMEOVER);
    }

    private void ChangeGameState(GameStates newState)
    {
        if (currentGameState == newState) return;

        OnGameStateChange?.Invoke(newState);

        currentGameState = newState;
    }
}