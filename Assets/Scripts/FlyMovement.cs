using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class FlyMovement : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private float maxRotationAngle = 45f;
    [SerializeField] private float deadZone = 50f;
    [SerializeField] private float moveDirection;
    [SerializeField] private float leanAngle;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float rollSpeed = 5f;

    [SerializeField] private Transform characterMesh;


    [SerializeField] private PlayerInputs inputs;

    private void Update()
    {
        HandleFlight();
    }

    private void HandleFlight()
    {
        var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        var horizontalDistance = inputs.CurrentTouchPos.x - screenCenter.x;

        if (Mathf.Abs(horizontalDistance) < deadZone)
        {
            RotateCharacter(0, 0);

            return;
        }

        var rotationAmount = Mathf.Clamp(horizontalDistance / (Screen.width / 2) * maxRotationAngle,
            -maxRotationAngle, maxRotationAngle);
        var leanAmount = Mathf.Clamp(horizontalDistance / (Screen.width / 2) * leanAngle, -leanAngle, leanAngle);
        
        RotateCharacter(rotationAmount, leanAmount);
        MoveCharacter(horizontalDistance);
    }


    private void MoveCharacter(float horizontalDistance)
    {
        moveDirection = Mathf.Sign(horizontalDistance);
        var targetPos = transform.position + Vector3.right * (moveDirection * flySpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    private void RotateCharacter(float rotationAmount, float leanAmount)
    {
        Quaternion targetRotation = Quaternion.Euler(0, rotationAmount, leanAmount);

        characterMesh.localRotation = Quaternion.Slerp(
            characterMesh.localRotation,
            targetRotation,
            Time.deltaTime * rollSpeed
        );
    }
}