using System;
using Assets.Scripts.Utilities;
using Assets.Shop.Scripts.Lable;
using DG.Tweening;
using Engenious.Core;
using UnityEngine;

namespace Shop.Core
{
    /// <summary>
    /// This class must be exterminated 
    /// </summary>
    public class Item : MonoBehaviour
    {
        public SelectableProductInfo ProductInfo;


        [SerializeField] private Transform _productTransform;

        public Transform ProductTransform
        {
            get { return _productTransform; }
            set { _productTransform = value; }
        }
        
        public string name;
        public Renderer[] m_Renderers;
        private bool m_IsRenderOn;

        private Transform _parent;
        public Transform Parent => _parent;

        private Animator _anim;
        public Animator Anim
        {
            get => _anim;
            private set => _anim = value;
        }

        private Transform _defaultTransform;

        private ItemGestueScaler _gestueScaler;
        
        #region HardLegacy
        private void Start()
        {
            // transform.localScale = Vector3.zero;
            // Show();
        }
        public void Show()
        {
            transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }

        public void Hide()
        {
            transform.DOScale(Vector3.zero, 1);
        }

        public void SwitchRenderer()
        {
            if (m_IsRenderOn)
            {
                SetRenderer(false);
                m_IsRenderOn = false;
            }
            else
            {
                SetRenderer(true);
                m_IsRenderOn = true;
            }
        }
        

        public void SetRenderer(bool active)
         {
             foreach (var renderer in m_Renderers)
             {
                 renderer.enabled = active;
             }
             m_IsRenderOn = active;

         }
        #endregion

        
        //TO DO: remove this from project
        public void OnSelect()
        {
            if (Anim != null)
            {
                Anim.Play("Show");
                DOVirtual.DelayedCall(12, () => Anim.Play("EndShow"));
            }
        }

        private void Awake()
        {
            Anim = GetComponent<Animator>();

            var collider = GetComponent<Collider>();

            //
            _productTransform = transform;
            //} 

            _gestueScaler = new ItemGestueScaler(transform);
            
            if (collider != null)
                collider.enabled = false;
        }

        private void Update()
        {
            _gestueScaler?.Update();
        }
        
        public void StopScaler()
        {
            _gestueScaler.Stop();
        }
        
        public void StartScaler()
        {
            _gestueScaler.Start();
        }
        
        public void ReturnToStartScale(float time = 0.3f, Action callback = null)
        {
            StopScaler();
            _gestueScaler.ReturnToStartScale(time, () =>
                {
                    StartScaler();
                    callback?.Invoke();
                }
            );
        }
        
        public void SetParent(Transform parent)
        {
            _parent = parent;
            transform.SetParent(parent);
            
            SetParentPositionAndLocation();
        }

        private void SetParentPositionAndLocation()
        {
            transform.SetPositionAndRotation(_parent.position, _parent.rotation);
        }

        public void SetDefaultTransform(Transform defaultTransform)
        {
            _defaultTransform = defaultTransform;
            
            transform.SetParent(_defaultTransform);
            
            transform.SetPositionAndRotation(_defaultTransform.position, _defaultTransform.rotation);
        }

        public void EnableItem()
        {
            gameObject.SetActive(true);
        }

        public void DisableItem()
        {
            transform.SetParent(_defaultTransform);
            transform.SetPositionAndRotation(_defaultTransform.position, _defaultTransform.rotation);

            gameObject.SetActive(false);
        }

        public void Delete()
        {
            Destroy(gameObject);
        }
    }
  
}