using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool isSwiping = false;
    private float swipeThreshold = 50f;
    private bool isTouchHold;

    public static event Action<Vector2> OnTouchBegin;
    public static event Action OnTouchEnd;

    public static Vector2 CurrentTouchPos;
    public static Vector2 TouchStartPos;
    public static Vector2 TouchEndPos;

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        
        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                HandleTouchBegan(touch);
                break;
            case TouchPhase.Ended:
                HandleTouchEnded(touch);
                break;
            case TouchPhase.Moved:
                HandleTouchMoved(touch);
                break;
            case TouchPhase.Stationary:
            case TouchPhase.Canceled:
            default:
                break;
        }
    }

    private void HandleTouchMoved(Touch touch)
    {
        CurrentTouchPos = touch.position;
    }

    private void HandleTouchEnded(Touch touch)
    {
        TouchEndPos = touch.position;
        OnTouchEnd?.Invoke();
        isTouchHold = false;
    }

    private void HandleTouchBegan(Touch touch)
    {
        TouchStartPos = touch.position;
        CurrentTouchPos = touch.position;

        isTouchHold = true;
        OnTouchBegin?.Invoke(touch.position);
    }

}