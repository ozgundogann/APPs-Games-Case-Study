using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Section platformPrefab; 
    [SerializeField] private int poolSize = 4;
    private Queue<Section> platformPool = new Queue<Section>();

    private void OnEnable()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var platform = Instantiate(platformPrefab, transform);
            platformPool.Enqueue(platform);
        }
    }

    public Section GetPlatform()
    {
        var platform = platformPool.Dequeue();
        platformPool.Enqueue(platform);
        return platform;
    }
}