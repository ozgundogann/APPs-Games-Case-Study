using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Section platformPrefab; 
    [SerializeField] private int poolSize = 4;
    [SerializeField] private SectionPositioner sectionPositioner;
    
    private Queue<Section> platformPool = new Queue<Section>();

    
    private void OnEnable()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var platform = Instantiate(platformPrefab, transform);
            platformPool.Enqueue(platform);
        }

        GameManager.OnGameStateChange += ListenStateChanges;
    }

    private void ListenStateChanges(GameStates newState)
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
        foreach (var section in platformPool)
        {
            section.enabled = true;
        }
        sectionPositioner.enabled = true;
    }

    private void HandleGameOver()
    {
        foreach (var section in platformPool)
        {
            section.enabled = false;
        }
        sectionPositioner.enabled = false;
    }

    public Section GetPlatform()
    {
        var platform = platformPool.Dequeue();
        platformPool.Enqueue(platform);
        return platform;
    }
}