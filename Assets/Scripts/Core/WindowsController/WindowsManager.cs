using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Engenious.Core.Managers;
using Zenject;
#if UNITY_EDITOR

#endif

namespace Engenious.Core.WindowsController
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(CanvasScaler))]
    public class WindowsManager : MonoBehaviour
    {
        /// <summary>
        /// Upper view
        /// </summary>
        public IWindowController Upper
        {
            get
            {
                if (_modelsList.Count == 0)
                    return null;

                IWindowController upper = null;

                _modelsList.ForEach(v =>
                {
                    if (upper == null || v.Layer > upper.Layer)
                        upper = v;
                });

                return upper;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private WindowsManagerConfig _config;

        /// <summary>
        ///   
        /// </summary>
        private List<IWindowController> _prefabList = new List<IWindowController>();

        /// <summary>
        /// 
        /// </summary>
        private List<IWindowController> _modelsList = new List<IWindowController>();

        /// <summary>
        /// 
        /// </summary>
        private List<IWindowController> _uniquesList = new List<IWindowController>();

        /// <summary>
        /// 
        /// </summary>
        public int Count => _modelsList.Count;

        /// <summary>
        /// 
        /// </summary>
        public RectTransform FadePanel;

        /// <summary>
        /// 
        /// </summary>
        public bool FadeScreen;

        /// <summary>
        /// 
        /// </summary>
        public Color FadeColor = Color.black;

        /// <summary>
        /// 
        /// </summary>
        public bool OutsideTap;

        /// <summary>
        /// 
        /// </summary>
        public event Action OnAllClosed;

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            _uniquesList = gameObject.GetComponentsInChildren<IWindowController>(true).ToList();
            if (_config != null)
            {
                _prefabList = _config?.Uiprefabs;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Show<T>(params object[] _params) where T : MonoBehaviour, IWindowController
        {
            var controller = _uniquesList.FirstOrDefault(v => v as T != null);
            if (controller == null)
            {
                var view = gameObject.GetComponentInChildren<T>(true);
                // if (view==null)
                // {
                //     controller = Instantiate(view.gameObject, transform, false).GetComponent<T>();
                // }
                // else
                // {
                //     var viewPrefab = _config?.Uiprefabs?.OfType<T>().FirstOrDefault(x => x != null);
                //     if (viewPrefab == null)
                //     {
                //         Debug.Log("View not found: " + typeof(T).Name);
                //         return null;
                //     }
                //
                //     controller = Instantiate(viewPrefab.gameObject, transform, false).GetComponent<T>();
                // }
                
                //
                if (view != null)
                {
                    controller = view;
                }
                else
                {
                    var viewPrefab = _prefabList.OfType<T>().FirstOrDefault(x => x != null);
                    
                    if (viewPrefab == null)
                    { 
                        viewPrefab = _config?.Uiprefabs?.OfType<T>().FirstOrDefault(x => x != null);
                    }
                    
                    view = Instantiate(viewPrefab.gameObject, transform, false).GetComponent<T>();
                    
                    if (!_uniquesList.Contains(view))
                    {
                        _uniquesList.Add(view);
                    }
                    
                    controller = view;
                }
            }

            return ShowInternal<T>(controller, _params);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetWindow<T>() where T : MonoBehaviour, IWindowController
        {
            var controller = _uniquesList.FirstOrDefault(v => v as T != null);

            return (T)controller;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="_params"></param>
        /// <returns></returns>
        private T ShowInternal<T>(IWindowController model, params object[] _params) where T : MonoBehaviour, IWindowController
        {
            if (model == null)
            {
                Debug.Log("View not found: " + typeof(T).Name);
                return null;
            }

            model.Manager = this;
            model.OverrideSorting = true;
            model.gameObject.transform.SetParent(transform);
            model.gameObject.transform.localScale = Vector3.one;

            var upper = Upper;
            if (upper != null)
            {
                if (model.Top)
                    model.Layer = Upper.Layer + 1;
                else
                    model.Layer = Upper.Layer - 1;

                if (model.LockInteractable)
                {
                    Upper.gameObject.GetComponent<CanvasGroup>().interactable = false;
                    Upper.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }
            else
            {
                if (FadeScreen && FadePanel != null && !FadePanel.gameObject.activeInHierarchy)
                {
                    var canvas = FadePanel.GetComponent<Canvas>();
                    canvas.sortingOrder = 1;
                    FadePanel.gameObject.SetActive(true);
                }

                model.Layer = 2;
            }

            _modelsList.Add(model);

            model.Manager = this;
            model.Show(_params);

            return (T)model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Close<T>() where T:IWindowController {
            var view = gameObject.GetComponentInChildren<T>();

            if(null == view) 
            {
                Debug.Log("View not found: " + typeof(T).Name);
                throw new ArrayTypeMismatchException();
            }

            view.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsShowed<T> () where T : IWindowController {
            var view = gameObject.GetComponentInChildren<T>();

            if(null==view) {
                Debug.Log("View not found: " + typeof(T).Name);
             //   throw new ArrayTypeMismatchException();
                return false;
            }

            return view.IsShowed ? true : false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void RemoveView(IWindowController model)
        {
            _modelsList.Remove(model);

            if(model.Unique) 
            {
                model.gameObject.SetActive(false);
                if (!_uniquesList.Contains(model))
                    _uniquesList.Add(model);
            } else {
                Destroy(model.gameObject);
            }
           

            var upper = Upper;
            if (upper != null)
            {
                if (model.LockInteractable)
                {
                    Upper.gameObject.GetComponent<CanvasGroup>().interactable = true;
                    Upper.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                }               
            }
            else
            {
                if (FadePanel != null && FadeScreen)
                    FadePanel.gameObject.SetActive(false);
                OnAllClosed?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (!OutsideTap || _modelsList.Count == 0)
                return;

            var upper = Upper;
            if (upper != null && upper.OutsideTap && upper.IsShowed)
            {
                var pos = Vector2.zero;
                var input = false;

                if (Input.touchCount > 0)
                {
                    pos = Input.touches[0].position;
                    input = true;
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        pos = Input.mousePosition;
                        input = true;
                    }
                }

                if (!input)
                    return;

                var pointerData = new PointerEventData(EventSystem.current)
                {
                    pointerId = -1,
                    position = pos
                };

                var results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                if (results.Count > 0)
                {
                    if (results[0].gameObject.layer == LayerMask.NameToLayer("UI"))
                        return;
                    upper.DoOutsideTap();
                }
                else
                {
                    upper.DoOutsideTap();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Factory : PlaceholderFactory<GameObject, IWindowController>
        {
        }

#if UNITY_EDITOR

        [MenuItem("Engenious/GameManagement/View Manager")]
        static void Create()
        {
            if (LayerMask.GetMask("UI") < 0)
                throw new UnassignedReferenceException("Layer UI must be assigned in Layer Manager.");

            var vmo = new GameObject("ViewManager");
            var vm = vmo.AddComponent<WindowsManager>();
            vmo.layer = LayerMask.NameToLayer("UI");

            var fadePanel = new GameObject("FadePanel");
            fadePanel.transform.SetParent(vmo.transform);
            var fpImage = fadePanel.AddComponent<Image>();
            fpImage.color = Color.black;
            var fmCanvas = fadePanel.AddComponent<Canvas>();
            fmCanvas.overrideSorting = true;
            fmCanvas.sortingOrder = 1;
            fadePanel.SetActive(false);
            vm.FadePanel = fadePanel.GetComponent<RectTransform>();

            var evs = GameObject.FindObjectOfType(typeof(EventSystem));
            if (evs == null)
            {
                var esObject = new GameObject("EventSystem");
                esObject.AddComponent<EventSystem>();
               // esObject.transform.SetParent(vmo.transform);
            }

           
        }

#endif
    }
}