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

    [Header("Velocity Resources")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float decreaseRate = 0.2f;
    
    [Header("Gravity Resources")] 
    [SerializeField] private float defaultGravity = -9.81f;
    [SerializeField] private float gravityReduceRate = 0.2f;

    [Header("RigidBody")]
    [SerializeField] private Rigidbody rb;

    [Header("Flight Resources")] 
    [SerializeField] private float fixedForwardMagnitude = 60f;

    
    private float gravity;
    
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
            velocity.y += gravity * Time.deltaTime;

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
        
        velocity = new Vector3(velocity.x, gravity * gravityReduceRate, 60);
        
        Vector3 targetVelocity = new Vector3(velocity.x, gravity * gravityReduceRate, fixedForwardMagnitude);
        float smoothTime = 1.0f;
        
        newTween?.Kill();
        newTween = DOTween.To(() => velocity, x => velocity = x, targetVelocity, smoothTime);

    }

    public void ExitFlyState()
    {
        isFlying = false;
    }

    public void SetDefaultGravityValue()
    {
        gravity = defaultGravity;
    }
}