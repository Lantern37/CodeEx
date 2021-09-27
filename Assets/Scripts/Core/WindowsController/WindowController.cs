using System;
using Engenious.Core.WindowsController;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Engenious.Core.Managers
{
    public class WindowResult {
        public IWindowController Controller;

        public WindowResult(IWindowController controller) {
            Controller = controller;
        }
    }

    public interface IWindowController
    {
        GameObject gameObject { get; }
        WindowsManager Manager { get; set; }

        /// <summary>
        /// If controller is Unique they destroy after closed
        /// </summary>
        bool Unique { get; }

        /// <summary>
        /// Close by outside tap
        /// </summary>
        bool OutsideTap { get; }

        /// <summary>
        /// Lock interactable for down view
        /// </summary>
        bool LockInteractable { get; }

        /// <summary>
        /// Window render upper from other
        /// </summary>
        bool Top { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsShowed { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// 
        /// </summary>
        int Layer { get; set; }

        bool OverrideSorting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        event Func<bool> CloseCondition;

        /// <summary>
        /// 
        /// </summary>
        event UnityAction OnClosed;

        /// <summary>
        /// 
        /// </summary>
        event UnityAction OnShowed;        

        void Show(params object[] _params);
        void Close();
        void DoOutsideTap();
    }

    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [Serializable]
    public abstract class WindowController : MonoBehaviour, IWindowController
    {
        /// <summary>
        /// 
        /// </summary>
        public int Layer
        {
            get => GetComponent<Canvas>().sortingOrder;
            set 
            {
                GetComponent<Canvas>().sortingOrder = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool OverrideSorting
        {
            get => GetComponent<Canvas>().overrideSorting;
            set => GetComponent<Canvas>().overrideSorting = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Unique => true;

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Blocker => false;

        /// <summary>
        /// 
        /// </summary>
        public virtual bool OutsideTap => true;

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Top => true;

        /// <summary>
        /// Lock interactable for down view
        /// </summary>
        public virtual bool LockInteractable => false;

        /// <summary>
        /// Parent view
        /// </summary>
        public IWindowController Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public WindowsManager Manager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsShowed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public UnityEvent OnOutsideTap;

        /// <summary>
        /// 
        /// </summary>
        public event UnityAction OnClosed; 
        /// <summary>
        /// 
        /// </summary>
        public event UnityAction<WindowResult> OnResult;

        /// <summary>
        /// 
        /// </summary>
        public event UnityAction OnShowed;

        /// <summary>
        /// 
        /// </summary>
        public event Func<bool> CloseCondition;

        public void ShowWidow(params object[] _params)
        {
            Show(_params);
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void Show(params object[] _params)
        {
            gameObject.SetActive(true);
         
            if (OutsideTap)
                OnOutsideTap.AddListener(() => (this as IWindowController).Close());

            GetComponent<Canvas>().overrideSorting = true;
            Showed();
            IsClosed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        void IWindowController.Show(params object[] _params)
        {
            Show(_params);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Showed()
        {
            OnShowed?.Invoke();
            OnShowed = null;
            IsShowed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        void IWindowController.DoOutsideTap()
        {   
            OnOutsideTap?.Invoke();
            OnOutsideTap.RemoveAllListeners();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {  
            Closed();
        }

        /// <summary>
        /// Close view
        /// </summary>
        /// <returns></returns>
        void IWindowController.Close()
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Closed( )
        {
            gameObject.SetActive(false);
            OnClosed?.Invoke();

            OnClosed = null;
            OnResult = null;
            OnOutsideTap.RemoveAllListeners();

            Manager.RemoveView(this);

            IsClosed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SendResult(WindowResult result) {
           
            OnResult?.Invoke(result ?? new WindowResult(this));
            
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Update()
        {
            if (CloseCondition != null && CloseCondition())
                (this as IWindowController).Close();
        }        

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnDestroy()
        {
            CloseCondition = null;
            OnClosed = null;
            OnResult = null;
            OnOutsideTap.RemoveAllListeners();
            
        }
    }
}