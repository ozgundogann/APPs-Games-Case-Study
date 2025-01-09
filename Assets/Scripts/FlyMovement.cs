using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class FlyMovement : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private float maxRotationAngle = 45f;
    [SerializeField] private float leanAngle;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float leanSpeed = 5f;
    [SerializeField] private float rollSpeed;
    

    [SerializeField] private Transform characterMesh;
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private CharacterPhysics characterPhysics;
    
    private float horizontal;
    private Vector3 targetPos;
    

    private void Update()
    {
        GetMovementInputs();
        RotateFlight();

    }

    private void RotateFlight()
    {
        var rotationAmount = horizontal * rotationSpeed * Time.deltaTime;

        transform.Rotate(0, rotationAmount, 0);

        var leanAngle = horizontal * this.leanAngle;
        Quaternion leanRotation = Quaternion.Euler(0, 0, -leanAngle);

        characterMesh.localRotation = Quaternion.Slerp(
            characterMesh.localRotation, 
            leanRotation, 
            rollSpeed * Time.deltaTime
        );

    }

    private void GetMovementInputs()
    {
        horizontal = dynamicJoystick.Horizontal;
    }

    
}