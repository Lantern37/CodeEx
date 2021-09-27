using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Scripts.InputServices
{
    /// <summary>
    /// Not legacy, but need to remove, because use legacy
    /// </summary>
    public class LegacyArClasses : MonoBehaviour
    {
        public event Action ReadyCreateItems;
        
        [SerializeField] private TestSwipeDetection _swipeDetection;
        public TestSwipeDetection SwipeDetection => _swipeDetection;
        
        [SerializeField] private RaycastSystem _raycastSystem;
        public RaycastSystem RaycastSystem => _raycastSystem;

        [SerializeField] private ProductPresenter _productPresenter;
        public ProductPresenter ProductPresenter=>_productPresenter;

        [SerializeField] private ReticleController _reticleController;
        public ReticleController ReticleController => _reticleController;
        
        [SerializeField] private InputManager _inputManager;
        
        [SerializeField] private ARPlaneManager _planeManager;
        [SerializeField] private ARCameraManager _cameraManager;

        [SerializeField] private ScanWindow _scanWindow;
        
        public void StartAR()
        {
            _scanWindow.Show(ScanWindow.ScanWindowStates.FindPlane);

#if UNITY_EDITOR
            FrameChanged(new ARCameraFrameEventArgs());
#else
            _cameraManager.frameReceived += FrameChanged;
#endif            
        }

        private void FrameChanged(ARCameraFrameEventArgs obj)
        {

#if !UNITY_EDITOR
            if (!(_planeManager.trackables.count > 0))
            {
                return;
            }
            _cameraManager.frameReceived -= FrameChanged;
#endif

            _reticleController.ShowRecticle();

            _scanWindow.Show(ScanWindow.ScanWindowStates.Tap);

            _inputManager.onTapEvent += OnTapEvent;
        }

        private void OnTapEvent()
        {
            _scanWindow.Hide();
            _reticleController.HideRecticle();
   
            _inputManager.onTapEvent -= OnTapEvent;

            ReadyCreateItems?.Invoke();
        }


        /// <summary>
        /// Dont know why. Just Legacy
        /// </summary>
        public void ResetAll()
        {
            _planeManager.subsystem?.Start();

            foreach (var trackable in _planeManager.trackables)
            {
                trackable.gameObject.SetActive(true);
            }

            _productPresenter.ResetPresenter();
            _raycastSystem.ResetRaycast();
            //StartAR();
        }
    }
}