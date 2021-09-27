using System;
using ARWrapper;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace Engenious.Core.Managers
{
    public interface IARManager
    {
        Camera Camera { get; }

        IARController Controller { get; }
        bool IsInitialized { get; }

        void Initialize();
        void Suspend();

        void StartScanPlane();
        void StopScanPlane();

        void Update();

        event Action<Pose> OnPlaneFound;
        event Action OnTrackingFound;
        event Action OnTrackingLost;
    }

    public class ARManager : IARManager
    {
        /// <summary>
        /// 
        /// </summary>
        private IARController _arController;
        public IARController Controller
        {
            get
            {
                if (_arController==null)
                    ComposeController();
                return _arController;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Camera Camera { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private ARConfig _config;

        /// <summary>
        /// 
        /// </summary>
        public event Action<Pose> OnPlaneFound;
        public event Action OnTrackingFound;
        public event Action OnTrackingLost;

        /// <summary>
        /// 
        /// </summary>
        private bool _firstTimeFound;


        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            if (!IsInitialized)
            {
                if (_arController != null)
                    return;

                ComposeController();

                IsInitialized = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Suspend()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartScanPlane()
        {
            Controller.OnCameraToPlaneRaycasted += PlaneFound;
            Controller.IsCamRaycasting = true;
            Controller.StartTracking();
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopScanPlane()
        {
            if (Controller == null) 
                return;
            Controller.OnPlaneRaycasted -= PlaneFound;
            Controller.IsCamRaycasting = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pose"></param>
        private void PlaneFound(Pose pose)
        {
            OnPlaneFound?.Invoke(pose);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ComposeController()
        {
            var arGO = new GameObject("ARController", new Type[] { typeof(ARFoundationController) });
            _arController = arGO.GetComponent<ARFoundationController>();

            var sessionOrigin =
                GameObject.Instantiate(_config.SessionOrigin).GetComponent<ARSessionOrigin>();

            sessionOrigin.transform.SetParent(arGO.transform);
            _arController.Init(sessionOrigin);

            Camera = sessionOrigin.GetComponentInChildren<Camera>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {

        }
    }
}

