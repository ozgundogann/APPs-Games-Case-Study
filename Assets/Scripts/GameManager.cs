using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStates currentGameState;

    public event Action<GameStates> OnGameStateChange;
    
    public static GameManager Instance { get; private set; }

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

    public void ChangeGameState(GameStates newState)
    {
        if (currentGameState == newState) return;
        
        OnGameStateChange?.Invoke(newState);

        currentGameState = newState;
    }
}