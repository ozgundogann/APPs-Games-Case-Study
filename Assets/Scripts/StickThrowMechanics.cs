using System.Collections;
using Unity.Collections;
using UnityEngine;

public class StickThrowMechanics : MonoBehaviour
{
    [Header("Animation Resources")]
    [SerializeField] private Animator animator;
    [SerializeField] private float animationProgress = 0f;
    [SerializeField] private float fixedTransitionDuration = 0.2f;
    [SerializeField] private float sensitivity = 2f;

    [Header("Throwing Velocity Value")] 
    [SerializeField] private Vector3 throwingVelocity;

    private bool isTouching;
    private Vector3 velocity;
    private Coroutine newRoutine;


    private void OnEnable()
    {
        InputManager.OnTouchBegin += HandleTouchBegin;
        InputManager.OnTouchEnd += HandleTouchEnd;
    }

    private void OnDisable()
    {
        InputManager.OnTouchBegin -= HandleTouchBegin;
        InputManager.OnTouchEnd -= HandleTouchEnd;
    }

    private void Update()
    {
        if (!isTouching) return;
        CalculateTouchMovement();
        PlayAnimationAtProgress(animationProgress);
    }

    private void CalculateTouchMovement()
    {
        var horizontalDelta = (InputManager.TouchStartPos.x - InputManager.CurrentTouchPos.x) / Screen.width *
                            sensitivity;
        animationProgress = Mathf.Clamp01(horizontalDelta);
    }

    private void PlayAnimationAtProgress(float progress)
    {
        animator.Play("Armature|Bend_Stick", 0, progress);
    }
    
    private void HandleTouchBegin(Vector2 touchPos)
    {
        isTouching = true;
    }

    private void HandleTouchEnd()
    {
        velocity = new Vector3(0, animationProgress * throwingVelocity.y,
            animationProgress * throwingVelocity.z);
        isTouching = false;
        PlayReleaseAnim();
    }
    
    private void PlayReleaseAnim()
    {
        animator.CrossFadeInFixedTime("Armature|Release_Stick", fixedTransitionDuration, 0);

        if (newRoutine != null)
            StopCoroutine(newRoutine);

        newRoutine = StartCoroutine(DelayRelease());
    }

    private IEnumerator DelayRelease()
    {
        yield return new WaitForSeconds(fixedTransitionDuration);
        StateManager.Instance.ChangeStateNode(new RollingStateNode());
        StateManager.Instance.characterMovement.ThrowPlayerFromStick(velocity);
    }
}