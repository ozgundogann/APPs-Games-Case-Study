using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject looseScreen;
    
    private void OnEnable()
    {
        GameManager.OnGameStateChange += ListenGameStates;
    }

    private void ListenGameStates(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.NONE:
                break;
            case GameStates.INGAME:
                looseScreen.SetActive(false);
                break;
            case GameStates.GAMEOVER:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleGameOver()
    {
        looseScreen.SetActive(true);
    }
}