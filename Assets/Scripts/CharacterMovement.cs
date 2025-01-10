using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [Header("Gravity Resources")] 
    [SerializeField] private float defaultGravity = -9.81f;
    [SerializeField] private float gravityReduceRate = 0.2f;
    public float Gravity;

    [Header("Velocity Resources")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float decreaseRate = 0.2f;

    [Header("RigidBody")]
    [SerializeField] private Rigidbody rb;

    private bool isFlying;
    
    private Tween newTween;

    private void OnEnable()
    {
        SetDefaultGravityValue();
    }

    private void Update()
    {
        HandleMovementWithGravity();
    }

    private void HandleMovementWithGravity()
    {
        if(!isFlying)
            velocity.y += Gravity * Time.deltaTime;

        Vector3 forwardMovement = transform.forward * (velocity.z * Time.deltaTime);
        Vector3 verticalMovement = Vector3.up * (velocity.y * Time.deltaTime);

        transform.position += forwardMovement + verticalMovement;
    }
    
    public void PerformBoosterJump(float impulse)
    {
        velocity = new Vector3(velocity.x, impulse, velocity.z);
    }

    public void ThrowPlayerFromStick(Vector3 force)
    {
        velocity = force;
    }

    public void ProcessFlyState()
    {
        isFlying = true;
        velocity = new Vector3(velocity.x, Gravity * gravityReduceRate, 60);
    }

    public void ExitFlyState()
    {
        isFlying = false;
    }

    public void SetDefaultGravityValue()
    {
        Gravity = defaultGravity;
    }

    public void DisableGravity()
    {
        Gravity = 0;
    }

    public void DecreaseVelocity()
    {
        var currentVelocity = velocity;
        var targetVelocity = new Vector3(currentVelocity.x,
            currentVelocity.y > 0 ? currentVelocity.y * decreaseRate : currentVelocity.y,
            60);


        newTween?.Kill();
        newTween = DOTween.To(() => currentVelocity,
                v => velocity = v,
                targetVelocity,
                1f)
            .SetEase(Ease.OutCubic);
    }
}