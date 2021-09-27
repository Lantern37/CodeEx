using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Shop.Core;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastSystem : MonoBehaviour
{
    private Camera m_Camera;
    [SerializeField] private TestInputManager m_InputManager;

    private Vector2 _screenSize => new Vector2(Screen.width, Screen.height);
    private Vector2 _centerOfScreen => _screenSize / 2.0f;
    
    private Vector2 m_RayPosition;

    private ISelectable m_LastSelected = null;
    private bool _isStopRaycasting;

    private void Awake()
    {
        m_Camera = Camera.main;
        m_RayPosition = _centerOfScreen;
    }
    private void Start()
    {
        m_InputManager.TapEvent += RaycastTap;
    }

    private void Update()
    {
        Raycasting();
    }
    
    private void Raycasting()
    {
        if(_isStopRaycasting)
            return;

        
        Ray ray = m_Camera.ScreenPointToRay(m_RayPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20))
        {
           // Debug.Log(" name  " + hit.collider.gameObject.name);

            Debug.DrawLine(ray.origin, hit.point);
            
            var selectable = hit.collider.gameObject.GetComponent<ISelectable>();
            
            if (selectable != null ) //Have selectable
            {
                if ( selectable != m_LastSelected)
                {
                    selectable.SelectedByScreenRay();
                    //Debug.Log("Raycast  with new selectable ");
                    if (m_LastSelected != null )
                    {
                        m_LastSelected.UnSelectedByScreenRay();
                        OnTargetLostEvent?.Invoke(m_LastSelected);
                    }
                    m_LastSelected = selectable;
                    //OnSelectedEvent(selectable);
                    OnTargetEvent?.Invoke(m_LastSelected);
                }
            }
            else //Have No selectable
            {
                if (m_LastSelected != null)
                {
                    m_LastSelected.UnSelectedByScreenRay();
                    OnTargetLostEvent?.Invoke(m_LastSelected);
                    m_LastSelected = null;
                    //Debug.Log("Raycast  with No Selectable   m_LastSelected.UnSelectedByScreenRay ");
                }
            }
        }
        else //Nothing raycasted
        {
            // Debug.Log("Raycast  with Nothing ");

            if (m_LastSelected != null)
            {
                //Debug.Log("Raycast  with Nothing  m_LastSelected != null");
                m_LastSelected.UnSelectedByScreenRay();
                OnTargetLostEvent?.Invoke(m_LastSelected);
                m_LastSelected = null;
            }
        }
    }
    
    public void Init(InputManager inputManager)
    {
        // m_InputManager = inputManager;
    }

    public void StopRaysact()
    {
        _isStopRaycasting = true;
    }
    
    public void StartRaysact()
    {
        _isStopRaycasting = false;
    }
    
    void RaycastTap(Vector2 rayPosition, float time)
    {
        if(_isStopRaycasting || IsPointerOverUI())
            return;
        
        var touchPos = rayPosition;
        Ray ray = m_Camera.ScreenPointToRay(touchPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectable = hit.collider.gameObject.GetComponent<ISelectable>();
            if (selectable != null)
            {
                //selectable.SelectedByPoint();
                OnSelectedEvent(selectable);
                // Debug.Log("Raycast  with selectable " + selectable.GetType());
            }
        }
    }

    public void ResetRaycast()
    {
        _isStopRaycasting = false;
    }
    
    private bool IsPointerOverUI()
    {
        bool isoverUI = false;
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        if (raycastResults.Count > 0)
        {
            foreach (var res in raycastResults)
            {
                if (res.gameObject.GetComponent<UIBehaviour>())
                {
                    isoverUI = true;
                    break;
                }
            }
        }
        else
        {
            isoverUI = false;
        }

        return isoverUI;
    }
    
    //Event select
    public event Action<ISelectable> OnTargetEvent;
    public event Action<ISelectable> OnTargetLostEvent;

    private event Action<ISelectable> m_OnSelectedEvent;
    public event Action<ISelectable> OnTapSelectedEvent
    {
        add
        {
            if (m_OnSelectedEvent == null || !m_OnSelectedEvent.GetInvocationList().Contains(value))
            {
                m_OnSelectedEvent += value;
            }
        }

        remove
        {
            if (m_OnSelectedEvent.GetInvocationList().Contains(value))
            {
                m_OnSelectedEvent -= value;
            }
        }
    }
    private void OnSelectedEvent(ISelectable item)
    {
        m_OnSelectedEvent?.Invoke(item);
        Debug.Log("OnSelectedEvent ");
    }
}
