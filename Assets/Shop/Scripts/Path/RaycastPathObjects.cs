using System;
using System.Linq;
using Shop.Core;
using UnityEngine;

public class RaycastPathObjects : MonoBehaviour
{
    private Camera m_Camera;
    private RaycastHit m_Hit;
    private Ray m_Ray;
    private Vector2 m_RayPosition;
    private ISelectable m_LastSelected = null;

    private Vector2 _screenSize => new Vector2(Screen.width, Screen.height);
    private Vector2 _centerOfScreen => _screenSize / 2.0f;

    private void Awake()
    {
        m_Camera = Camera.main;
        m_RayPosition = _centerOfScreen;
    }

    private void Update()
    {
        Raycasting();
    }

    void Raycasting()
    {
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
                        // ShopManager.Instance.UnselectItem();
                    }
                    m_LastSelected = selectable;
                    Debug.Log("SELECTABLE = "+ (selectable as MonoBehaviour).GetComponentInChildren<Item>().name);
                    OnSelectedEvent(selectable);
                    ShopManager.Instance.SelectedItem(selectable);
                }
            }
            else //Have No selectable
            {
                if (m_LastSelected != null)
                {
                    m_LastSelected.UnSelectedByScreenRay();
                    ShopManager.Instance.UnselectItem();

                    m_LastSelected = null;
                    Debug.Log("Raycast  with No Selectable   m_LastSelected.UnSelectedByScreenRay ");

                }
            }
        }
        else //Nothing raycasted
        {
            // Debug.Log("Raycast  with Nothing ");

            if (m_LastSelected != null)
            {
                Debug.Log("Raycast  with Nothing  m_LastSelected != null");
                m_LastSelected.UnSelectedByScreenRay();
                ShopManager.Instance.UnselectItem();

                m_LastSelected = null;
            }
        }
    }

    
    //Event select
    private event Action<ISelectable> m_OnSelectedEvent;
    public event Action<ISelectable> onSelectedEvent
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
        Debug.Log("OnSelectedEvent " + item.GetType());
    }
    
}
