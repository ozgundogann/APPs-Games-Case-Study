using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab; 
    [SerializeField] private int poolSize = 4;
    private Queue<GameObject> platformPool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var platform = Instantiate(platformPrefab, transform);
            platform.SetActive(false); 
            platformPool.Enqueue(platform);
        }

        Debug.Log(platformPool.Count);
    }

    public GameObject GetPlatform()
    {
        var platform = platformPool.Dequeue();
        platform.SetActive(true);
        platformPool.Enqueue(platform);
        return platform;
    }
}