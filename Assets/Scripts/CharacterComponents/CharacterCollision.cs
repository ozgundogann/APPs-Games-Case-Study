using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    
    [SerializeField] private CharacterMovement characterMovement;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out var platform))
        {
            platform.PlatformResponsibility(characterMovement);
            return;
        }
        
        //If player didn't touch to platform.
        ApplyBounce(other);
        
        GameManager.Instance.TriggerGameOver();
    }

    private void ApplyBounce(Collision other)
    {
        ContactPoint contact = other.contacts[0];
        Vector3 bounceDirection = Vector3.Reflect(characterMovement.Velocity, contact.normal).normalized;
        
        characterMovement.ApplyBounce(bounceDirection);
    }
}