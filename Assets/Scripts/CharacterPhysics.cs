using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterPhysics : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;

    [Header("Gravity Resources")]
    [SerializeField] private float defaultGravity = -9.81f;
    public float gravity;
    
    [Header("Bounce Resources")]
    [SerializeField] private float bounciness = 0.8f;
    [SerializeField] private float detectionDistance;

    [SerializeField] private float checkCoolDown = 1f;
    private bool allowedToCheck = true;

    private Coroutine newRoutine;

    public event Action OnPlatformCollisionEnter; 

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    private void Start()
    {
        SetDefaultGravityValue();
    }

    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;

        Vector3 moveDirection = velocity * Time.deltaTime;
        
        if (CheckCollision(moveDirection, out var hit))
        {
            if (hit.collider.CompareTag("BoostPlatform"))
            {
                HandlePlatformBoost(hit);
            }
            else
            {
                Bounce(hit);
            }
        }
        
        transform.position += velocity * Time.deltaTime;
    }

    private void HandlePlatformBoost(RaycastHit hit)
    {
        if (!allowedToCheck) return;
        
        allowedToCheck = false;

        //Gets PlatformBoost Script and use its polymorph function.
        var platform = hit.collider.GetComponent<PlatformBoost>();
        platform.PlatformResponsibility(this);
        
        OnPlatformCollisionEnter?.Invoke();
        
        if (newRoutine != null)
        {
            StopCoroutine(newRoutine);
        }
                    
        newRoutine = StartCoroutine(nameof(CheckProcessCooldown));
    }

    IEnumerator CheckProcessCooldown()
    {
        yield return new WaitForSeconds(checkCoolDown);
        allowedToCheck = true;
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
    

    public void SetDefaultGravityValue()
    {
        gravity = defaultGravity;
    }

    
    public void DecreaseVelocity()
    {
        // Mevcut velocity'yi al
        Vector3 currentVelocity = velocity;
    
        // Hedef velocity'yi sıfırlamak
        Vector3 targetVelocity = 0.5f * currentVelocity;

        // DOTween ile smooth geçiş
        DOTween.To(() => currentVelocity, 
            v => velocity = v, 
            targetVelocity, 
            0.2f);
    }

}
