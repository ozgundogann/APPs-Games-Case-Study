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
        meshRoot.RotateAround(meshRoot.position, transform.right, Time.deltaTime * rotateSpeed);
    }
}