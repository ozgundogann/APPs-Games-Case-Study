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
    [SerializeField] private float gravityReduceRate = 0.2f;
    public float gravity;
    
    [Header("Velocity Resources")] 
    [SerializeField] private float decreaseRate = 0.2f;
    
    [Header("Bounce Resources")]
    [SerializeField] private float bounciness = 0.8f;
    [SerializeField] private float detectionDistance;

    public event Action OnGroundTouch; 
    
    [SerializeField] private float checkCoolDown = 1f;
    private bool allowedToCheck = true;

    private Coroutine newRoutine;
    private Tween newTween;
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
                if (CheckGround(hit))
                {
                    //return;
                }
                Bounce(hit);
            }
        }

        var forwardMovement = transform.forward * (velocity.z * Time.deltaTime);
        var verticalMovement = Vector3.up * (velocity.y * Time.deltaTime);

        transform.position += forwardMovement + verticalMovement;
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

    private bool CheckGround(RaycastHit hit)
    {
        if (!hit.collider.CompareTag("Ground")) return false;
        OnGroundTouch?.Invoke();
        return true;

    }

    public void ThrowPlayerWithVelocity(Vector3 force)
    {
        velocity = force;
    }

    public void ReduceGravity()
    {
        gravity *= gravityReduceRate;
    }
    public void SetDefaultGravityValue()
    {
        gravity = defaultGravity;
    }

    public void DisableGravity()
    {
        gravity = 0;
    }
    
    public void DecreaseVelocity()
    {
        var currentVelocity = velocity;
        var targetVelocity = new Vector3(currentVelocity.x, 
            currentVelocity.y > 0 ? currentVelocity.y * decreaseRate : currentVelocity.y, 
            currentVelocity.z);


        newTween?.Kill();
        newTween = DOTween.To(() => currentVelocity,
                v => velocity = v,
                targetVelocity,
                1f) 
            .SetEase(Ease.OutCubic);
    }

}
