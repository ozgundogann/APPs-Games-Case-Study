using System;
using DG.Tweening;
using UnityEngine;

public class LoosingScreen : MonoBehaviour
{
    [SerializeField] private Transform loosingScreen;

    private Tween newTween;
    
    private void OnEnable()
    {
        GameManager.OnGameStateChange += ListenStates;
    }

    private void ListenStates(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.NONE:
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

    private void HandleGameOver()
    {
        loosingScreen.gameObject.SetActive(true);
        loosingScreen.localScale = Vector3.zero;

        newTween?.Kill();
        newTween = loosingScreen.DOScale(Vector3.one, .5f).SetEase(Ease.OutCubic);
    }

    public void HandleStartGame()
    {
        loosingScreen.gameObject.SetActive(false);
    }
}