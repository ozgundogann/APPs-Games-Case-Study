using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private RotateMovement rotateMovement;
    [SerializeField] private FlyMovement flyMovement;

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
            case GameStates.STARTGAME:
                HandleStartGame();
                break;
            case GameStates.INGAME:
                break;
            case GameStates.GAMEOVER:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleStartGame()
    {
        EnableScripts();
    }

    private void EnableScripts()
    {
        characterMovement.enabled = true;
        rotateMovement.enabled = true;
        flyMovement.enabled = true;
    }

    private void HandleGameOver()
    {
        DisableScripts();
    }

    private void DisableScripts()
    {
        characterMovement.enabled = false;
        rotateMovement.enabled = false;
        flyMovement.enabled = false;
        Debug.Log("Disable");
    }
}