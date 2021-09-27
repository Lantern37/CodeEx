using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Legacy
/// </summary>
public class TestSwipeDetection : MonoBehaviour
{
    [SerializeField] private TestInputManager m_InputManager;
    [SerializeField] private float m_MinimumDistance = .2f;
    [SerializeField] private float m_MaxTime = 0.2f;
    [Range(0,1)]
    [SerializeField] private float m_DirectionThreshold = .9f;

    private Vector2 m_StartPosition;
    private float m_StartTime;
    private Vector2 m_EndPosition;
    private float m_EndTime;
    private Vector2 m_Delta = Vector2.zero;

    public event Action<Vector2, float> SwipeStartEvent;
    public event Action<Vector2, float> SwipeEndEvent;
    public event Action<Vector2> SwipeEvent;
    
    private void OnEnable()
    {
        m_InputManager.onStartTouchEvent += SwipeStart;
        m_InputManager.onEndTouchEvent += SwipeEnd;
    }
    
    private void OnDisable()
    {
        m_InputManager.onStartTouchEvent -= SwipeStart;
        m_InputManager.onEndTouchEvent -= SwipeEnd;
    }
    
    private void Update()
    {
        
        var delta =  m_InputManager.GetDelta();
        
        if ((Time.time - m_StartTime) >  m_MaxTime && (delta != m_Delta || delta != Vector2.zero))
        {
            if(Input.touchCount > 1)
                return;
            SwipeEvent?.Invoke(delta);
        }
    }

    private void SwipeStart(Vector2 pos, float time)
    {
        if(Input.touchCount > 1)
            return;
        
        m_StartPosition = pos;
        m_StartTime = Time.time;
        SwipeStartEvent?.Invoke(pos, time);
    }

    private void SwipeEnd(Vector2 pos, float time)
    {
        if(Input.touchCount > 1)
            return;
        
        m_EndPosition = pos;
        m_EndTime = Time.time;

        DetectShortSwipe();

        if (m_EndTime - m_StartTime <= m_MaxTime)
        {
            return;
        }
        
        SwipeEndEvent?.Invoke(pos, time);
    }

    private void DetectShortSwipe()
    {
        if (Vector2.Distance(m_StartPosition, m_EndPosition)  >= m_MinimumDistance
            && (m_EndTime - m_StartTime <= m_MaxTime))
        {
            // Debug.Log(" SWIPE " + m_StartPosition + "  " + m_EndPosition);
            // Debug.DrawLine(m_StartPosition, m_EndPosition, Color.cyan,5f);
            Vector2 direction = m_EndPosition - m_StartPosition;
            //Debug.Log("direction = " + direction);

            Vector2 direction2D =  new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        SwipeSide side = SwipeSide.Right;
        float power = 0;
        
        Vector2 direction2D =  new Vector2(direction.x, direction.y).normalized;
        
        if (Vector2.Dot(Vector2.up, direction2D) > m_DirectionThreshold)
        {
            side = SwipeSide.Up;
            power = Mathf.Abs(direction.y) / Screen.height;
        }
        else if (Vector2.Dot(Vector2.down, direction2D) > m_DirectionThreshold)
        {
            side = SwipeSide.Down;
            power = Mathf.Abs(direction.y) / Screen.height;
        }
        else if (Vector2.Dot(Vector2.left, direction2D) > m_DirectionThreshold)
        {
            side = SwipeSide.Left;
            power = Mathf.Abs(direction.x) / Screen.width;
        }
        else if (Vector2.Dot(Vector2.right, direction2D) > m_DirectionThreshold)
        {
            side = SwipeSide.Right;
            power = Mathf.Abs(direction.x) / Screen.width;
        }

        if (power > 0)
        {
            OnSwipe(side, power);
        }
    }


    private void OnSwipe(SwipeSide side, float power)
    {
        _shortSwipeEvent?.Invoke(side, power);
    }
    
    #region Events

    //Event Swipe
    private  event Action<SwipeSide, float> _shortSwipeEvent;
    public event Action<SwipeSide, float>  ShortSwipeEvent
    {
        add
        {
            if (_shortSwipeEvent == null || !_shortSwipeEvent.GetInvocationList().Contains(value))
            {
                _shortSwipeEvent += value;
            }
        }

        remove
        {
            if (_shortSwipeEvent.GetInvocationList().Contains(value))
            {
                _shortSwipeEvent -= value;
            }
        }
    }
  
    private  event Action<SwipeSide, float> _longSwipeEvent;
    public event Action<SwipeSide, float>  LongSwipeEvent
    {
        add
        {
            if (_longSwipeEvent == null || !_longSwipeEvent.GetInvocationList().Contains(value))
            {
                _longSwipeEvent += value;
            }
        }

        remove
        {
            if (_longSwipeEvent.GetInvocationList().Contains(value))
            {
                _longSwipeEvent -= value;
            }
        }
    }
    #endregion

}

public enum SwipeSide
{
    Up,
    Down,
    Left,
    Right
}
