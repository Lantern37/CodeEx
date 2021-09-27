using DG.Tweening;
using UnityEngine;

public class TestInputRotation : MonoBehaviour
{
    private TestInputManager m_InputManager;
    private float m_RotationSpeed = 0.3f;
    private bool m_IsTouch;
 
    private void Awake()
    {
        m_InputManager = FindObjectOfType<TestInputManager>();
    }

    private void OnEnable()
    {
        m_InputManager.onDeltaStartEvent += OnTouchStart;
        m_InputManager.onDeltaEndEvent += OnTouchEnd;
    }

    private void OnDisable()
    {
        m_InputManager.onDeltaStartEvent -= OnTouchStart;
        m_InputManager.onDeltaEndEvent -= OnTouchEnd;
    }

    private void Update()
    {
        if (!m_IsTouch ) return;
        var delta =  m_InputManager.GetDelta();
        if (delta == Vector2.zero) return;
        var rotationEuler = transform.rotation.eulerAngles;
        rotationEuler.x += delta.y * m_RotationSpeed;
        rotationEuler.y += delta.x * m_RotationSpeed;
        // Debug.Log("Got DELTA " + delta);

        transform.DOLocalRotate(rotationEuler, 0.1f);
    }
    
    void  OnTouchStart()
    {
        m_IsTouch = true;        
    }
    
    void  OnTouchEnd()
    {
        m_IsTouch = false;
    }
}
