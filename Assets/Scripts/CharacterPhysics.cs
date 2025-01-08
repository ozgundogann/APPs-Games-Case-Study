using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterPhysics : MonoBehaviour
{
    public Vector3 velocity;
    [SerializeField] private Vector3 launchForce;

    [Header("Gravity Resources")]
    [SerializeField] private float defaultGravity = -9.81f;
    public float gravity;
    
    [Header("Bounce Resources")]
    [SerializeField] private float bounciness = 0.8f;
    [SerializeField] private float detectionDistance;

    private void Start()
    {
        SetDefaultGravityValue();
    }

    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;

        Vector3 moveDirection = velocity * Time.deltaTime;
        
        if (CheckCollision(moveDirection, out var hit))
            Bounce(hit);
        
        transform.position += velocity * Time.deltaTime;
    }

    private bool CheckCollision(Vector3 moveDirection, out RaycastHit hit)
    {
        Debug.DrawRay(transform.position, moveDirection.normalized * detectionDistance);
        return Physics.Raycast(transform.position, moveDirection.normalized * detectionDistance, out hit, moveDirection.magnitude);
    }

    private void Bounce(RaycastHit hit)
    {
        transform.position = hit.point;

        velocity = Vector3.Reflect(velocity, hit.normal);

        velocity *= bounciness;
    }
    public void ApplyForce()
    {
        velocity += launchForce;
    }

    public void SetDefaultGravityValue()
    {
        gravity = defaultGravity;
    }
}
