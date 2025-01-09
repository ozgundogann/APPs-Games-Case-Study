using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ManipulateStickAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float animationProgress = 0f;
    [SerializeField] private float fixedTransitionDuration = 0.2f;
    
    [SerializeField] private float sensitivity = 2f;

    
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float verticalVelocity = 100f;
    [SerializeField] private float horizontalVelocity = 100f;
    
    


    [SerializeField] private PlayerInputs playerInputs;

    [SerializeField] private bool isTouching;

    private Vector2 _initialTouchPos;
    private Coroutine newRoutine;


    private void Start()
    {
        playerInputs.OnTouchBegin += HandleTouchBegin;
        playerInputs.OnTouchEnd += HandleTouchEnd;
    }

    private void OnDisable()
    {
        playerInputs.OnTouchBegin -= HandleTouchBegin;
        playerInputs.OnTouchEnd -= HandleTouchEnd;
    }

    private void Update()
    {
        if (!isTouching) return;
        
        var verticalDelta = (_initialTouchPos.x - playerInputs.CurrentTouchPos.x) / Screen.width * sensitivity;
        animationProgress = Mathf.Clamp01(verticalDelta);

        
        PlayAnimationAtProgress(animationProgress);
    }

    private void PlayAnimationAtProgress(float progress)
    {
        animator.Play("Armature|Bend_Stick", 0, progress);
    }

    private void PlayReleaseAnim(float startProgress)
    {
        animator.CrossFadeInFixedTime("Armature|Release_Stick", fixedTransitionDuration, 0);
        
        if(newRoutine != null)
            StopCoroutine(newRoutine);
        
        newRoutine = StartCoroutine(DelayRelease());
    }

    IEnumerator DelayRelease()
    {
        yield return new WaitForSeconds(fixedTransitionDuration);
        PlayerManager.Instance.ChangeStateNode(new RollingStateNode());
        PlayerManager.Instance.characterPhysics.ThrowPlayerWithVelocity(velocity);
    }
    

    private void HandleTouchBegin(Vector2 touchPos)
    {
        _initialTouchPos = touchPos;
        isTouching = true;
    }

    private void HandleTouchEnd()
    {
        var clampedVerticalDelta = animationProgress;
        velocity = new Vector3(0, clampedVerticalDelta * verticalVelocity,
            clampedVerticalDelta * horizontalVelocity);
        isTouching = false;
        PlayReleaseAnim(animationProgress);
    }
}