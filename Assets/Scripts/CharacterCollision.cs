using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    
    [SerializeField] private CharacterMovement characterMovement;

    [SerializeField] private float bounceForce = 10f;
    
    [SerializeField] private Rigidbody rb;
    
    private bool isGameOver;
    private void OnEnable()
    {
        isGameOver = false;
    }

    private void OnDisable()
    {
        isGameOver = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        //if(isGameOver) return;
        if (other.gameObject.TryGetComponent<Platform>(out var platform))
        {
            platform.PlatformResponsibility(characterMovement);
            return;
        }
        
        
        ContactPoint contact = other.contacts[0];
        Vector3 bounceDirection = Vector3.Reflect(characterMovement.Velocity, contact.normal).normalized;

        rb.useGravity = true;
        if(bounceForce < 0.01) return;
        bounceForce *= 0.5f;
        rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        Debug.DrawRay(transform.position,bounceDirection, Color.red);
        isGameOver = true;
        GameManager.Instance.TriggerGameOver();

    }
}