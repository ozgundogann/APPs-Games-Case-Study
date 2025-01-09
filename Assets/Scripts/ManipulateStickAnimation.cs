using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ManipulateStickAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float animationProgress = 0f;
    [SerializeField] private float sensitivity = 2f;
    


    [SerializeField] private PlayerInputs playerInputs;

    [SerializeField] private bool isTouching;

    private Vector2 _initialTouchPos;


    private void Start()
    {
        playerInputs.OnTouchBegin += HandleTouchBegin;
        playerInputs.OnTouchEnd += HandleTouchEnd;
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
        animator.CrossFadeInFixedTime("Armature|Release_Stick", 0.2f, 0);
    }
    

    private void HandleTouchBegin(Vector2 touchPos)
    {
        _initialTouchPos = touchPos;
        isTouching = true;
    }

    private void HandleTouchEnd()
    {
        isTouching = false;
        PlayReleaseAnim(animationProgress);
        PlayerManager.Instance.ChangeStateNode(new RollingStateNode());
    }

    private void OnVirtualUpdate(float value)
    {
        PlayAnimationAtProgress(value);
    }
}