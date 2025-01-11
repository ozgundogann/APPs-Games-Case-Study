using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Transform UIObject;
    
    private bool isSwiping = false;
    private float swipeThreshold = 50f;
    private bool isTouchHold;

    public static event Action<Vector2> OnTouchBegin;
    public static event Action OnTouchEnd;

    public static Vector2 CurrentTouchPos;
    public static Vector2 TouchStartPos;
    public static Vector2 TouchEndPos;

    private bool isInGame = true;
    bool isTouchBeganInvoked = false;

    private void OnEnable()
    {
        GameManager.OnGameStateChange += ListenGameStates;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= ListenGameStates;

    }

    private void ListenGameStates(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.NONE:
                break;
            case GameStates.INGAME:
                isInGame = true;
                break;
            case GameStates.GAMEOVER:
                isInGame = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void Update()
    {
        if (Input.touchCount <= 0 || !isInGame) return;

        
        var touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                HandleTouchBegan(touch);
                isTouchBeganInvoked = true;
                break;
            case TouchPhase.Ended:
                if(!isTouchBeganInvoked) break;
                isTouchBeganInvoked = false;
                
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