using System;
using System.Collections;
using UnityEngine;

public class SectionPositioner : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private Transform platformTransform;
    
    [SerializeField] private Transform player; 
    [SerializeField] private int initialPlatforms = 5;

    private float platformLength;
    private Vector3 nextSpawnPosition;
    private Coroutine newRoutine;
    private void Start()
    {
        platformLength = platformTransform.localScale.z;
        
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }

        newRoutine = StartCoroutine(RepositionPlatform());
    }

    private void OnDestroy()
    {
        StopCoroutine(newRoutine);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnPlatform();
        }
    }

    private IEnumerator RepositionPlatform()
    {
        if (player.position.z > nextSpawnPosition.z - (platformLength * initialPlatforms / 2))
        {
            SpawnPlatform();
        }

        yield return null;
    }

    private void SpawnPlatform()
    {
        var platform = poolManager.GetPlatform();
        platform.transform.position = nextSpawnPosition;
        nextSpawnPosition += Vector3.forward * platformLength; 
    }
}