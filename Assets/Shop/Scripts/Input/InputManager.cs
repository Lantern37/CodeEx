using System;
using System.Linq;
using DG.Tweening;
using MySelectable;
// using Shop.Core;
using UnityEngine;
using UnityEngine.InputSystem;


public interface IUseInput
{
   public void InitInput(InputManager input);
}

public class InputManager : MonoBehaviour
{
   private InputActionsControls m_InputControls;
   private Camera m_Camera;
   private bool m_IsObjectSelected;
   private void Awake()
   {
      m_InputControls = new InputActionsControls();
      m_Camera = Camera.main;
   }
   
   private void OnEnable()
   {
      m_InputControls.Enable();
      
      m_InputControls.User.TouchInput.started +=  ctx => OnStartTouch(ctx);
      m_InputControls.User.TouchInput.canceled +=  ctx => OnEndTouch(ctx);
      m_InputControls.User.Tap.started += ctx => OnTap(ctx);
   }
   private void OnDisable()
   {
      m_InputControls.User.Tap.started -= OnTap;
      m_InputControls.User.TouchInput.started -=  OnStartTouch;
      m_InputControls.User.TouchInput.canceled -=   OnEndTouch;

      m_InputControls.Disable();
   }

   private void Start()
   {
      
      // m_InputControls.User.TouchInput.performed += ctx => OnPointer(ctx);
   }

   // private void OnPointer(InputAction.CallbackContext ctx)
   // {
   //    Debug.Log("OnPointer ");
   //
   //    var mousePos = ctx.ReadValue<Vector2>();
   //
   //    // var mousePos = (Vector2) ctx.ReadValueAsObject();
   //    Ray ray = m_Camera.ScreenPointToRay(mousePos);
   //    RaycastHit hit;
   //    if (Physics.Raycast(ray, out hit))
   //    {
   //       var item = hit.collider.gameObject.GetComponent<Selectable>();
   //       if (item != null)
   //       {
   //          OnSelectedEvent(item);
   //          
   //          //Debug rotation
   //          var rot = item.transform.rotation;
   //          rot.eulerAngles += new Vector3(0, 90, 0);
   //          item.transform.DORotate(rot.eulerAngles, 1);
   //       }
   //    }
   // }

   private void OnStartTouch(InputAction.CallbackContext ctx)
   {
      // Debug.Log("OnStartTouch ");

      var touchPos = ctx.ReadValue<Vector2>();
      OnStartTouchEvent(touchPos, (float)ctx.time);

      // Ray ray = m_Camera.ScreenPointToRay(touchPos);
      // RaycastHit hit;
      // if (Physics.Raycast(ray, out hit))
      // {
      //    var item = hit.collider.gameObject.GetComponent<Item>();
      //    if (item != null)
      //    {
      //       OnSelectedEvent(item);
      //       m_IsObjectSelected = true;
      //    }
      //    else
      //    {
      //       var objectSelected = hit.collider.gameObject;
      //       var rot = objectSelected.transform.rotation;
      //       rot.eulerAngles += new Vector3(0, 90, 0);
      //       objectSelected.transform.DORotate(rot.eulerAngles, 1);
      //    }
      // }
      // else
      // {
      //    OnSwipeEvent(touchPos, (float)ctx.time);
      // }
   }

   void OnEndTouch(InputAction.CallbackContext ctx)
   {
      var touchPos = ctx.ReadValue<Vector2>();
      // Debug.Log("??????  End Touch Position " + touchPos);
      OnEndTouchEvent(touchPos, (float)ctx.time);

   }
   private void OnTap(InputAction.CallbackContext ctx)
   {
      OnTapEvent();
   }

   #region Events for send

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
      m_OnSelectedEvent?.Invoke(item);
   }
   
   //Event Tap
   private event Action m_OnTapEvent;
   public event Action onTapEvent
   {
      add
      {
         if (m_OnTapEvent == null || !m_OnTapEvent.GetInvocationList().Contains(value))
         {
            m_OnTapEvent += value;
         }
      }

      remove
      {
         if (m_OnTapEvent.GetInvocationList().Contains(value))
         {
            m_OnTapEvent -= value;
         }
      }

   }
   
   private void OnTapEvent()
   {
      m_OnTapEvent?.Invoke();
      
   }

   //Event Swipe
   private  event Action<Vector2, float> m_OnSwipeEvent;
   public event Action<Vector2, float>  onSwipeEvent
   {
      add
      {
         if (m_OnSwipeEvent == null || !m_OnSwipeEvent.GetInvocationList().Contains(value))
         {
            m_OnSwipeEvent += value;
         }
      }

      remove
      {
         if (m_OnSwipeEvent.GetInvocationList().Contains(value))
         {
            m_OnSwipeEvent -= value;
         }
      }
   }
   private void OnSwipeEvent(Vector2 pos, float time)
   {
      m_OnSwipeEvent?.Invoke(pos, time);
      
   }
   
   
   
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
   private void OnStartTouchEvent(Vector2 pos, float time)
   {
      m_OnStartTouchEvent?.Invoke(pos, time);
      // Debug.Log("OnStartTouch  m_OnStartTouchEvent");

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
   private void OnEndTouchEvent(Vector2 pos, float time)
   {
      m_OnEndTouchEvent?.Invoke(pos, time);
      
   }

   #endregion
  
}
