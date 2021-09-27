using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private InputManager m_InputManager;
    [SerializeField] private float m_MinimumDistance = .2f;
    [SerializeField] private float m_MaxTime = 1f;
    [Range(0,1)]
    [SerializeField] private float m_DirectionThreshold = .9f;

    private Vector2 m_StartPosition;
    private float m_StartTime;
    private Vector2 m_EndPosition;
    private float m_EndTime;

    private void OnEnable()
    {
        m_InputManager.onStartTouchEvent += SwipeStart;
        m_InputManager.onStartTouchEvent += SwipeEnd;
    }
    
    private void OnDisable()
    {
        m_InputManager.onStartTouchEvent -= SwipeStart;
        m_InputManager.onStartTouchEvent -= SwipeEnd;
    }
    
    private void SwipeStart(Vector2 pos, float time)
    {
        m_StartPosition = pos;
        m_StartTime = time;
    }

    private void SwipeEnd(Vector2 pos, float time)
    {
        m_StartPosition = pos;
        m_StartTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector2.Distance(m_StartPosition, m_EndPosition)  >= m_MinimumDistance &&
            (m_EndTime - m_StartTime <= m_MaxTime))
        {
            Debug.Log(" SWIPE " + m_StartPosition + "  " + m_EndPosition);
            Debug.DrawLine(m_StartPosition, m_EndPosition, Color.cyan,55f);
            Vector2 direction = m_EndPosition - m_StartPosition;
            Vector2 normalisedDirection = direction.normalized;
            SwipeDirection(normalisedDirection);
        }
    }


    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > m_DirectionThreshold)
        {
            Debug.Log("Swipe Up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > m_DirectionThreshold)
        {
            Debug.Log("Swipe Down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > m_DirectionThreshold)
        {
            Debug.Log("Swipe left");
        }
        else if (Vector2.Dot(Vector2.right, direction) > m_DirectionThreshold)
        {
            Debug.Log("Swipe rIGHT");
        }
    }
}
