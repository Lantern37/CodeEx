using ARWrapper;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFoundationController : MonoBehaviour, IARController
{

    ARSession _session;
    ARSessionOrigin _sessionOrigin;

    ARPlaneManager _planeManager;
    ARPointCloudManager _pointCloudManager;
    ARRaycastManager _raycastManager;
    ARFaceManager _arFacemanager;
    ARCameraManager _arcameraManager;
    ARTrackedImageManager _arTrackedImageManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    static List<ARPointCloud> _cloudPoints = new List<ARPointCloud>();

    bool raycastOverUI = false;
    bool _isCamRaycasting = false;

    TrackableType _currentTrackableType = TrackableType.Planes;
    
    bool IARController.RaycastOverUI { get => raycastOverUI; set => raycastOverUI = value; }
    bool IARController.IsCamRaycasting { get => _isCamRaycasting; set => _isCamRaycasting = value; }
    Transform IARController.FirstARPlane { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int PlaneCount
    {
        get
        {
            if(_planeManager != null)
            {
                return _planeManager.trackables.count;
            }
            else
            {
                Debug.LogError("There are no PlaneManager inited");
                return -1;
            }
        }
    }

    public int ImageCount
    {
        get
        {
            if (_arTrackedImageManager != null)
            {
                return _arTrackedImageManager.trackables.count;
            }
            else
            {
                Debug.LogError("There are no ImageManager inited");
                return -1;
            }
        }
    }

    /// <summary>
    /// Event from ARController when user raycast to plane
    /// </summary>
    event Action<Pose> OnPlaneRaycasted;
    /// <summary>
    /// Event from ARController when Camera raycast to plane from center of view
    /// </summary>
    event Action<Pose> OnCameraToPlaneRaycasted;
    /// <summary>
    /// Event from ARController when Camera raycast to plane from center of view
    /// </summary>
    event Action<ARPlanesChangedEventArgs> OnPlaneChanged;

    /// <summary>
    /// Event from ARController when Tracking Image was found
    /// </summary>
    event Action<ARTrackedImagesChangedEventArgs> OnTrackingImageChanged;

    event Action<Pose> IARController.OnPlaneRaycasted
    {
        add
        {
            OnPlaneRaycasted += value;
        }
        remove
        {
            OnPlaneRaycasted -= value;
        }
    }

    event Action<Pose> IARController.OnCameraToPlaneRaycasted
    {
        add
        {
            OnCameraToPlaneRaycasted += value;
        }
        remove
        {
            OnCameraToPlaneRaycasted -= value;
        }
    }

    
    event Action<float?, float?, Color?> OnLightDataChanged = (x, y, z) => { };

    event Action<float?, float?, Color?> IARController.OnLightDataChanged
    {
        add
        {
            OnLightDataChanged += value;
        }

        remove
        {
            OnLightDataChanged -= value;
        }
    }

    event Action<ARPlanesChangedEventArgs> IARController.OnPlaneChanged
    {
        add
        {
            OnPlaneChanged += value;
        }
        remove
        {
            OnPlaneChanged -= value;
        }
    }

    event Action<ARTrackedImagesChangedEventArgs> IARController.OnTrackingImageChanged
    {
        add
        {
            OnTrackingImageChanged += value;
        }
        remove
        {
            OnTrackingImageChanged -= value;
        }
    }

    /// <summary>
    /// The estimated brightness of the physical environment, if available.
    /// </summary>
    float? brightness;

    /// <summary>
    /// The estimated color temperature of the physical environment, if available.
    /// </summary>
    float? colorTemperature;

    /// <summary>
    /// The estimated color correction value of the physical environment, if available.
    /// </summary>
    Color? colorCorrection;


    #region PlaneLogic
    void IARController.InitPlaneTracking()
    {
        LogMessage("InitPlaneTracking");
    }
    void IARController.StopPlaneTracking()
    {
        LogMessage("StopPlanetracking");
    }
    #endregion



    void IARController.StartTracking()
    {
        LogMessage("StartTracking");

        //_session.subsystem.Start();
    }
    void IARController.StopTracking()
    {
        LogMessage("StopTracking");
        _session.subsystem.Stop();
    }
    void IARController.ResetTracking()
    {
        LogMessage("ResetTracking");
        _session.subsystem.Reset();
    }



    void IARController.StartDefaultSession()
    {
        LogMessage("StartDefault session");
        _session.subsystem.Stop();
        if (_arFacemanager.subsystem.running)
        {
            _arFacemanager.subsystem.Stop();
            _arFacemanager.enabled = false;
        }

        if (_arTrackedImageManager.subsystem.running)
        {
            _arTrackedImageManager.subsystem.Stop();
            _arTrackedImageManager.enabled = false;
        }
        _session.subsystem.Start();
    }

    void IARController.StartFaceTrackingSession()
    {
        LogMessage("StartFaceTracking session");
        _session.subsystem.Stop();
        _arFacemanager.enabled = true;

        Debug.Log("_arFacemanager.subsystem = " + (_arFacemanager.subsystem == null));
        _arFacemanager.subsystem.Start();

        _session.subsystem.Start();
    }
    void IARController.StopFaceTrackingSession()
    {
        LogMessage("StopFaceTracking session");
        _arFacemanager.subsystem.Stop();
        _arFacemanager.enabled = false;
    }


    void IARController.StartPointCloudVisualization()
    {
        //_pointCloudManager.subsystem.Start();
        _pointCloudManager.pointCloudsChanged += OnPointCloudUpdated;
        LogMessage("StartPointCloud");
    }


    void IARController.StopPointCloudVisualization()
    {
        //_pointCloudManager.subsystem.Stop();
        //_pointCloudManager.subsystem.Destroy();
        foreach (var point in _cloudPoints)
        {
            Destroy(point.gameObject);
        }
        _pointCloudManager.pointCloudsChanged -= OnPointCloudUpdated;
        _cloudPoints.Clear();
        _cloudPoints.TrimExcess();
        LogMessage("StopPointCloud");

        //// Stop updating point clouds
        //_pointCloudManager.enabled = false;

        //// Disable each existing one
        //foreach (var pointCloud in _pointCloudManager.trackables)
        //{
        //    pointCloud.gameObject.SetActive(false);
        //}
    }


    void IARController.SetCurrentTrackbleType(int type = (int)TrackableType.Planes)
    {
        _currentTrackableType = (TrackableType)type;
        LogMessage("Set current TrackbleType" + _currentTrackableType);
    }


    void IARController.SetLightEstimationState(bool value)
    {//todo
        if (value)
        {
            _arcameraManager.lightEstimationMode = LightEstimationMode.AmbientIntensity;
            _arcameraManager.frameReceived += FrameChanged;
        }
        else
        {
            _arcameraManager.lightEstimationMode = LightEstimationMode.Disabled;
            _arcameraManager.frameReceived -= FrameChanged;
        }

    }

    #region ImageTracking Wrapper
    void IARController.StartImageTracking()
    {
        LogMessage("StartImageTracking session");
        //_session.subsystem.Stop();
        _arTrackedImageManager.enabled = true;

        //_arTrackedImageManager.subsystem must be not null
        //_arTrackedImageManager.subsystem.Start();

        //_session.subsystem.Start();
    }

    void IARController.StopImageTracking()
    {
        LogMessage("StopImageTracking session");
        _arTrackedImageManager.subsystem.Stop();
        _arTrackedImageManager.enabled = false;
    }

    void IARController.ResetImageTracking()
    {
        LogMessage("ResetImageTracking");
        _session.subsystem.Reset();
    }
    #endregion

    /// <summary>
    /// InitMap custom arcontroller that implemet Custom interface. In next releases InitMap method will be changed
    /// </summary>
    /// <param name="sessionOrigin"></param>
    public void Init(ARSessionOrigin sessionOrigin)
    {
        _sessionOrigin = sessionOrigin;
        _session = sessionOrigin.GetComponentInChildren<ARSession>();
        _planeManager = sessionOrigin.GetComponent<ARPlaneManager>();
        _pointCloudManager = sessionOrigin.GetComponent<ARPointCloudManager>();
        _raycastManager = sessionOrigin.GetComponent<ARRaycastManager>();
        _arFacemanager = sessionOrigin.GetComponent<ARFaceManager>();
        _arcameraManager = sessionOrigin.GetComponentsInChildren<ARCameraManager>()[0];

        _arTrackedImageManager = sessionOrigin.GetComponent<ARTrackedImageManager>();

        _pointCloudManager.pointCloudsChanged += OnPointCloudUpdated;

        _planeManager.planesChanged += PlaneChanged;
        _arTrackedImageManager.trackedImagesChanged += TrackingImageChanged;

    }

    private void OnDestroy()
    {
        _planeManager.planesChanged -= PlaneChanged;
        _arTrackedImageManager.trackedImagesChanged -= TrackingImageChanged;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        RaycastTouch();
        RaycastCam();

    }


    private void RaycastTouch()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (_raycastManager.Raycast(touch.position, s_Hits, _currentTrackableType))
        {
            if (!raycastOverUI && IsPointerOverUI())
            {
                LogMessage("Pointer over UI");
                return;
            }
            Pose hitPose = s_Hits[0].pose;

            PlaneRaycast(hitPose);
        }
    }
    private void RaycastCam()
    {
        if (!_isCamRaycasting)
        {
            return;
        }

        if (_raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), s_Hits, _currentTrackableType))
        {
            Pose hitPose = s_Hits[0].pose;
            PlaneRaycastedFromCam(hitPose);
        }
    }

    private void PlaneRaycastedFromCam(Pose pose)
    {
        if (OnCameraToPlaneRaycasted != null)
        {
            OnCameraToPlaneRaycasted.Invoke(pose);
        }
    }

    private void PlaneRaycast(Pose pose)
    {
        LogMessage("Plane raycasted");
        if (OnPlaneRaycasted != null)
        {
            OnPlaneRaycasted.Invoke(pose);
        }

    }

    void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        OnPlaneChanged?.Invoke(args);
    }

    void TrackingImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        OnTrackingImageChanged?.Invoke(args);
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

    void OnPointCloudUpdated(ARPointCloudChangedEventArgs args)
    {
        _cloudPoints.AddRange(args.updated);
    }


    void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            brightness = args.lightEstimation.averageBrightness.Value;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            colorCorrection = args.lightEstimation.colorCorrection.Value;
        }

        OnLightDataChanged?.Invoke(brightness, colorTemperature, colorCorrection);
    }

    private void LogMessage(string message)
    {

        Debug.LogFormat("<color=#00ff00ff>ARFoundationController Log:  " + message + " </color>");
    }


}
