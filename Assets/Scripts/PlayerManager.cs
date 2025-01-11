using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private RotateMovement rotateMovement;
    [SerializeField] private FlyMovement flyMovement;
    [SerializeField] private CharacterCollision characterCollision;
    [SerializeField] private Transform topOfStick;
    [SerializeField] private StickThrowMechanics stick;
    
    
    

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
        AttachCharacterToStick();
        characterCollision.enabled = true;
    }

    private void AttachCharacterToStick()
    {
        Transform characterTransform;
        (characterTransform = characterMovement.transform).SetParent(topOfStick);
        characterTransform.localPosition = Vector3.zero;
        characterTransform.rotation = Quaternion.Euler(Vector3.zero);
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
        characterCollision.enabled = false;
    }
}