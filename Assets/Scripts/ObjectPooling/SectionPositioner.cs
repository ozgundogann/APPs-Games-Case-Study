using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SectionPositioner : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private Transform platformGround;
    
    [SerializeField] private Transform player; 
    [SerializeField] private int initialPlatforms = 5;

    [SerializeField] private int checkPerSecond = 3;
    
    
    private float platformLength;
    private Vector3 nextSpawnPosition;
    private Coroutine newRoutine;

    private void OnEnable()
    {
        platformLength = platformGround.localScale.z;
        
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    
        newRoutine = StartCoroutine(RepositionPlatform());
    }

    private void OnDisable()
    {
        if(newRoutine != null)
            StopCoroutine(newRoutine);
    }

    private IEnumerator RepositionPlatform()
    {
        while (true)
        {
            if (player.position.z > nextSpawnPosition.z - platformLength * initialPlatforms / 2)
            {
                SpawnPlatform();
            }

            yield return new WaitForSeconds(checkPerSecond);
        }
    }

    private void SpawnPlatform()
    {
        var platform = poolManager.GetPlatform();
        platform.gameObject.transform.position = nextSpawnPosition;
        nextSpawnPosition += Vector3.forward * platformLength; 
        platform.InvokeEvent();
    }
}