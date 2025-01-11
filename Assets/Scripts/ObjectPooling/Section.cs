using System;
using UnityEngine;

public class Section : MonoBehaviour
{
    [SerializeField] private ObjectSpawner objectSpawner;
    

    public event Action OnLocationChange;
    public void InvokeEvent()
    {
        OnLocationChange?.Invoke();
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
        objectSpawner.enabled = true;
    }

    private void HandleGameOver()
    {
        objectSpawner.enabled = false;
    }
}