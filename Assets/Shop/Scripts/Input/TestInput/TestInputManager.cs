using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Legacy
/// </summary>
[DefaultExecutionOrder(-1)]
public class TestInputManager : MonoBehaviour
{
   [SerializeField] private float _tapTimer = 0.5f;
   [SerializeField] private float _tapDistance = 70;
   
   private TestIput m_TestInput;
   private Camera m_MainCamera;
   private bool m_IsHolding;
   private void Awake()
   {
      m_TestInput = new TestIput();
      m_MainCamera = Camera.main;
   }

   private void OnEnable()
   {
      m_TestInput.Enable();
   }

   private void OnDisable()
   {
      m_TestInput.Disable();
   }

   private void Start()
   {
      m_TestInput.Touch.PrimaryContact.started += context => StartTouchPrimary(context);
      m_TestInput.Touch.PrimaryContact.canceled += context => EndTouchPrimary(context);
      // m_TestInput.Rotate.Rotate.performed += context => Rotate(context);
      
   }

   private void StartTouchPrimary(InputAction.CallbackContext context)
   {
      // var touchPosition =
         // ScreenToWorldUtil.ScreenToWorld(m_MainCamera, m_TestInput.Touch.PrimaryPosition.ReadValue<Vector2>());
      m_IsHolding = true;
      var touchPosition = m_TestInput.Touch.PrimaryPosition.ReadValue<Vector2>();

      OnStartTouchEvent(touchPosition, (float) context.time);
   }
   private void EndTouchPrimary(InputAction.CallbackContext context)
   {
         m_IsHolding = false;
      var touchPosition = m_TestInput.Touch.PrimaryPosition.ReadValue<Vector2>();
      OnEndTouchEvent(touchPosition, (float) context.time);
   }

   // private void  Rotate(InputAction.CallbackContext context)
   // {
   //    var delta =  context.ReadValue<Vector2>();
   //    OnRotateEvent(delta);
   // }

   public Vector2 GetDelta()
   {
      if (m_IsHolding)
      {
         return m_TestInput.Rotate.Rotate.ReadValue<Vector2>();
      }
      
      return Vector2.zero;
   }
   
   public Vector2 PrimaryPosition()
   {
      return ScreenToWorldUtil.ScreenToWorld(m_MainCamera, m_TestInput.Touch.PrimaryPosition.ReadValue<Vector2>());
   }

   
   //Invoke Eventd
   private Vector2 _startVectorTime;
   private float _startTapTime;
   private void OnStartTouchEvent(Vector2 pos, float time)
   {
      if (IsPointerOverUI())
         return;
      
      m_OnStartTouchEvent?.Invoke(pos, time);
      m_OnDeltaStartEvent?.Invoke();
      // Debug.Log("OnStartTouch  m_OnStartTouchEvent");
      _startTapTime = time;
      _startVectorTime = pos;
   }

   private void OnEndTouchEvent(Vector2 pos, float time)
   {
      m_OnEndTouchEvent?.Invoke(pos, time);
      m_OnDeltaEndEvent?.Invoke();

      float dist = Vector2.Distance(pos, _startVectorTime);
      
      if (Mathf.Abs(time - _startTapTime) <= _tapTimer && dist <= _tapDistance)
      {
         TapEvent?.Invoke(pos, time);
      }
   }
   
   // private void OnRotateEvent(Vector2 delta)
   // {
   //    m_OnRotateEvent?.Invoke(delta);
   //    
   // }

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
   
   #region Events

   //Event Start touch
   private  event Action<Vector2, float> m_OnStartTouchEvent;
   public event Action<Vector2, float>  onStartTouchEvent
   {
      add
      {
         if (m_OnStartTouchEvent == null || !m_OnStartTouchEvent.GetInvocationList().Contains(value))
         {
            m_OnStartTouchEvent += value;
         }
      }

      remove
      {
         if (m_OnStartTouchEvent.GetInvocationList().Contains(value))
         {
            m_OnStartTouchEvent -= value;
         }
      }
   }
  
   
   //Event End touch
   private  event Action<Vector2, float> m_OnEndTouchEvent;
   public event Action<Vector2, float>  onEndTouchEvent
   {
      add
      {
         if (m_OnEndTouchEvent == null || !m_OnEndTouchEvent.GetInvocationList().Contains(value))
         {
            m_OnEndTouchEvent += value;
         }
      }

      remove
      {
         if (m_OnEndTouchEvent.GetInvocationList().Contains(value))
         {
            m_OnEndTouchEvent -= value;
         }
      }
   }
   
   //Event Rotate
   // private  event Action<Vector2> m_OnRotateEvent;
   // public event Action<Vector2>  onRotateEvent
   // {
   //    add
   //    {
   //       if (m_OnRotateEvent == null || !m_OnRotateEvent.GetInvocationList().Contains(value))
   //       {
   //          m_OnRotateEvent += value;
   //       }
   //    }
   //
   //    remove
   //    {
   //       if (m_OnRotateEvent.GetInvocationList().Contains(value))
   //       {
   //          m_OnRotateEvent -= value;
   //       }
   //    }
   // }
   
   //Event Delta
   private  event Action m_OnDeltaStartEvent;
   public event Action onDeltaStartEvent
   {
      add
      {
         if (m_OnDeltaStartEvent == null || !m_OnDeltaStartEvent.GetInvocationList().Contains(value))
         {
            m_OnDeltaStartEvent += value;
         }
      }

      remove
      {
         if (m_OnDeltaStartEvent.GetInvocationList().Contains(value))
         {
            m_OnDeltaStartEvent -= value;
         }
      }
   }
   
   private  event Action m_OnDeltaEndEvent;
   public event Action onDeltaEndEvent
   {
      add
      {
         if (m_OnDeltaEndEvent == null || !m_OnDeltaEndEvent.GetInvocationList().Contains(value))
         {
            m_OnDeltaEndEvent += value;
         }
      }

      remove
      {
         if (m_OnDeltaEndEvent.GetInvocationList().Contains(value))
         {
            m_OnDeltaEndEvent -= value;
         }
      }
   }

   public event Action<Vector2, float> TapEvent;

   #endregion


}
