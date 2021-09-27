using DG.Tweening;
using UnityEngine;

public class SwipePathPosition : MonoBehaviour
{
    [SerializeField] private PathBehaviour m_PathBehaviour;
    [SerializeField] private float m_LongSwipeSpeed = 0.01f;
    [SerializeField] private float m_ShortSwipeDuration = 1f;
    
    private TestInputManager m_InputManager;
    private TestSwipeDetection m_SwipeDetection;
    private Vector2 m_Delta = Vector2.zero;
    private bool m_IsLongSwipe;
    private bool m_IsShortSwipe;
    // private float m_ShortSwipePower = 50;

    private void Awake()
    {
        m_SwipeDetection = FindObjectOfType<TestSwipeDetection>();
        m_InputManager = FindObjectOfType<TestInputManager>();
    }

    private void OnEnable()
    {
        m_SwipeDetection.ShortSwipeEvent += SwipeShort;

        m_InputManager.onDeltaStartEvent += OnTouchStart;
        m_InputManager.onDeltaEndEvent += OnTouchEnd;
    }

    private void OnDisable()
    {
        m_SwipeDetection.ShortSwipeEvent -= SwipeShort;

        m_InputManager.onDeltaStartEvent -= OnTouchStart;
        m_InputManager.onDeltaEndEvent -= OnTouchEnd;
    }

    private void Update()
    {
        if (!m_IsLongSwipe || m_IsShortSwipe) return;
        
        var delta =  m_InputManager.GetDelta();
        
        if (delta != m_Delta || delta != Vector2.zero)
        {
            m_Delta = delta * -1;
            // Debug.Log("Swipe Delta " + delta);
            m_PathBehaviour.MoveHeadLongSwipe( m_Delta.x * m_LongSwipeSpeed * Time.deltaTime);

        }
    }

    void  OnTouchStart()
    {
        //Wait if short swipe max time in TestSwipeDetection
        m_IsShortSwipe = false;
        m_PathBehaviour.KillAllTweners();

        DOVirtual.DelayedCall(0.42f, () =>
        {
            if (!m_IsShortSwipe)
            {
                m_IsLongSwipe = true;
                m_PathBehaviour.LongSwipe = true;
            }
           
        });
    }
    
    void  OnTouchEnd()
    {
        if (m_IsLongSwipe)
        {
            m_IsLongSwipe = false;
            m_PathBehaviour.EndTouch();
        }
    }
    
    private void SwipeShort(SwipeSide swipeSide, float power)
    {
        if (m_IsShortSwipe) return;
        m_IsShortSwipe = true;
        Debug.Log("Swipe Short power " + power);
        // m_PathBehaviour.CanSwipe = true;

        if (swipeSide == SwipeSide.Left || swipeSide == SwipeSide.Right)
        {
            // m_PathBehaviour.MoveShortSwipe(swipeSide, power, m_ShortSwipeDuration);
            m_PathBehaviour.ShortSwipeToNextMagnet(swipeSide, power);
            DOVirtual.DelayedCall(m_ShortSwipeDuration, () => m_IsShortSwipe = false);
        }
        else 
        {
            Debug.Log("Swipe Short wrong direction ");
            m_IsShortSwipe = false;
        }

        
    }
    
}
