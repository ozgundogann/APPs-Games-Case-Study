using System;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;
    private Vector2 _currentTouchPos;

    private bool isSwiping = false;
    private float swipeThreshold = 50f;
    private bool isTouchHold;

    public event Action<Vector2> OnTouchBegin;
    public event Action OnTouchEnd;
    public event Action<Vector2> OnTouchHold;

    public Vector2 CurrentTouchPos => _currentTouchPos;

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        
        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                HandleTouchBegan(touch);
                break;
            case TouchPhase.Moved:
                HandleTouchMoved(touch);
                break;
            case TouchPhase.Ended:
                HandleTouchEnded(touch);
                break;
            case TouchPhase.Stationary:
            case TouchPhase.Canceled:
            default:
                break;
        }
    }

    private void HandleTouchMoved(Touch touch)
    {   
        if (!isSwiping && Vector2.Distance(touch.position, _touchStartPos) > swipeThreshold)
        {
            isSwiping = true;
        }
        else
        {
            _currentTouchPos = touch.position;
            OnTouchHold?.Invoke(_currentTouchPos);
        }
    }

    private void HandleTouchEnded(Touch touch)
    {
        _touchEndPos = touch.position;
        OnTouchEnd?.Invoke();
        isTouchHold = false;
    }

    private void HandleTouchBegan(Touch touch)
    {
        _touchStartPos = touch.position;
        _currentTouchPos = touch.position;

        isTouchHold = true;
        OnTouchBegin?.Invoke(touch.position);
    }

}