using System;
using System.Linq;
using MySelectable;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSelector : MonoBehaviour
{
    private InputActionsControls m_InputControlls;
    private Camera m_Camera;

    private void Awake()
    {
        m_InputControlls = new InputActionsControls();
        m_Camera = Camera.main;
    }

    private void OnEnable()
    {
        m_InputControlls.Enable();
    }

    private void OnDisable()
    {
        m_InputControlls.Disable();
    }

    private void Start()
    {
        m_InputControlls.User.TouchInput.started += ctx => StartTouch(ctx);
        m_InputControlls.User.TouchInput.canceled += ctx => EndTouch(ctx);
        m_InputControlls.User.Tap.started += ctx => OnTap(ctx);
    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        Debug.Log("Tap ON " );
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("StartTouch  " );

            if (context.started)
            {
                var mousePos = (Vector2) context.ReadValueAsObject();
                Ray ray = m_Camera.ScreenPointToRay(mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var item = hit.collider.gameObject.GetComponent<Selectable>();
                    if (item != null)
                    {
                        Debug.Log("Item selected " + item.name);

                        //Some Action
                        OnSelectedEvent(item);
                        
                        // var rot = selectable.transform.rotation;
                        // rot.eulerAngles += new Vector3(0, 90, 0);
                        // selectable.transform.DORotate(rot.eulerAngles, 1);
                    }
                    
                }
            }

    }

    void EndTouch(InputAction.CallbackContext context)
    {
        
    }
    
    
    //Event select
    private event Action<Selectable> m_OnSelectedEvent;
    public event Action<Selectable> onSelectedEvent
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
    private void OnSelectedEvent(Selectable item)
    {
        if (m_OnSelectedEvent != null)
        {
            m_OnSelectedEvent(item);
        }
    }
    
}
