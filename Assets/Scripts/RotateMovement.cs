using System;
using UnityEngine;

public class RotateMovement : MonoBehaviour
{
    [SerializeField] private Transform meshRoot;

    [Header("Additional Variables")] 
    public float rotateSpeed;

    private void Update()
    {
        HandleRotateMovement();
    }

    private void HandleRotateMovement()
    {
        meshRoot.RotateAround(meshRoot.position, Vector3.right, Time.deltaTime * rotateSpeed);
    }
}