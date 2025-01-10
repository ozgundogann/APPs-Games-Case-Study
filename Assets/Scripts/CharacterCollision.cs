using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private RotateMovement rotateMovement;
    
    [SerializeField] private Rigidbody rb;

    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out var platform))
        {
            platform.PlatformResponsibility(characterMovement);
            return;
        }
        
        Debug.Log("GAME OVER");
        GameManager.Instance.TriggerGameOver();
        rb.useGravity = true;
    }
}