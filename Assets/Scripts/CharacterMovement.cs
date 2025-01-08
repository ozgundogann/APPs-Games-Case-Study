using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterPhysics characterPhysics;
    [SerializeField] private Transform meshRoot;
    
    [Header("Animator")] 
    [SerializeField] private Animator animator;

        
    [SerializeField] private float flyingGravityRate;
    
    [SerializeField] private bool isFlying;

    [SerializeField] private float rotateSpeed;

    private Tween newTween;
    
    private static readonly int IsFlying = Animator.StringToHash("isFlying");

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleFlying();
            
            //isFlying must be true AFTER handling Flying
            isFlying = true;
            animator.SetBool(IsFlying, true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isFlying = false;
            animator.SetBool(IsFlying, false);

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
            var eulerAngles = transform.eulerAngles;

            newTween?.Kill();
            newTween = meshRoot.DOLocalRotate(new Vector3(90, eulerAngles.y, eulerAngles.z), 0.2f).SetEase(Ease.Linear);
        }

        
        characterPhysics.gravity *= flyingGravityRate;
    }
    
    

}