using System;
using UnityEngine;
using UnityEngine.XR;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterPhysics characterPhysics;
    [SerializeField] private Transform meshRoot;
    
    [SerializeField] private float flyingGravityRate;
    
    [SerializeField] private bool isFlying;

    [SerializeField] private float rotateSpeed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleFlying();
            isFlying = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isFlying = false;
            characterPhysics.SetDefaultGravityValue();
        }

        if (!isFlying)
        {
            meshRoot.RotateAround(meshRoot.position, Vector3.right, 1f * Time.deltaTime * rotateSpeed);
        }
    }

    private void HandleFlying()
    {
        if (!isFlying)
        {
            characterPhysics.velocity = new Vector3(characterPhysics.velocity.x, characterPhysics.velocity.y * 0.1f, characterPhysics.velocity.z);
        }
        
        characterPhysics.gravity *= flyingGravityRate;
    }

}