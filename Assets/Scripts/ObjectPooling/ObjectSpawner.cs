using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cylinderPlatformPrefab;
    [SerializeField] private GameObject cubePlatformPrefab;
    

    [SerializeField] private List<Transform> cylinderList;
    [SerializeField] private List<Transform> cubeList;

    [SerializeField] private Section section;
    
    [SerializeField] private LayerMask layerMask;


    [SerializeField] private int maxCylinderCount = 10;
    [SerializeField] private int maxCubeCount = 10;

    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 maxPosition;

    [SerializeField] private float objectRadius = 1f;

    private void OnEnable()
    {
        SpawnObjects();
        HandleRepositioning();
        section.OnLocationChange += HandleRepositioning;
    }

    private void OnDisable()
    {
        section.OnLocationChange -= HandleRepositioning; 
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < maxCylinderCount; i++)
        {
            GameObject newObject = Instantiate(cylinderPlatformPrefab, transform);
            cylinderList.Add(newObject.transform);
        }

        for (int i = 0; i < maxCubeCount; i++)
        {
            GameObject newObject = Instantiate(cubePlatformPrefab, transform);
            cubeList.Add(newObject.transform);
        }
    }

    public void HandleRepositioning()
    {
        int spawnedCount = 0;
        int currentCylinderAmount = 0;
        int currentCubeAmount = 0;
        int maxAttempt = 10;
        int attempts = 0;

        while (spawnedCount < maxCylinderCount + maxCubeCount && attempts < maxAttempt)
        {
            attempts++;

            Vector3 randomPosition = GetRandomPosition();

            if (Physics.CheckSphere(randomPosition, objectRadius, layerMask)) continue;

            if (currentCylinderAmount < maxCylinderCount)
            {
                RepositionCylinder(currentCylinderAmount, randomPosition);
                currentCylinderAmount++;
            }
            else
            {
                RepositionCube(currentCubeAmount, randomPosition);
                currentCubeAmount++;
            }

            spawnedCount++;
            attempts = 0;
        }
    }

    private void RepositionCube(int currentCubeAmount, Vector3 randomPosition)
    {
        Transform newObject = cubeList[currentCubeAmount];
        newObject.transform.localPosition = randomPosition;
    }

    private void RepositionCylinder(int currentCylinderAmount, Vector3 randomPosition)
    {
        Transform newObject = cylinderList[currentCylinderAmount];
        newObject.transform.localPosition = randomPosition;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            0,
            Random.Range(minPosition.z, maxPosition.z)
        );
        return randomPosition;
    }
}