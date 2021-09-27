using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ARWrapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">AR controller interface like a "IARController"or "IARControllerV3" </typeparam>
    public abstract class ARFactory<T> : MonoBehaviour
    {
        public abstract T GetArController();
    }


    public interface IARController
    {


        /// <summary>
        /// Event from ARController when user raycast to plane
        /// </summary>
        event Action<Pose> OnPlaneRaycasted;
        /// <summary>
        /// Event from ARController when Camera raycast to plane from center of view
        /// </summary>
        event Action<Pose> OnCameraToPlaneRaycasted;

        /// <summary>
        /// Event from ARController when plane changed
        /// </summary>
        event Action<ARPlanesChangedEventArgs> OnPlaneChanged;

        /// <summary>
        /// Event from ARController when image changed
        /// </summary>
        event Action<ARTrackedImagesChangedEventArgs> OnTrackingImageChanged;

        /// <summary>
        /// This Event invoke ONLY when "ligthestimation" enabled!
        /// </summary>
        event Action<float?, float?, Color?> OnLightDataChanged;
        /// <summary>
        /// Is ui should block AR raycast
        /// </summary>
        bool RaycastOverUI { get; set; }

        /// <summary>
        /// Enable raycasting from center of camera
        /// </summary>
        bool IsCamRaycasting { get; set; }

        /// <summary>
        /// Return first tracked plane
        /// </summary>
        Transform FirstARPlane { get; set; }

        void Init(ARSessionOrigin sessionOrigin);

        int PlaneCount { get;}
        int ImageCount { get;}

        void InitPlaneTracking();
        void StopPlaneTracking();


        void StartTracking();
        void StopTracking();
        void ResetTracking();

        void StartFaceTrackingSession();
        void StopFaceTrackingSession();
        void StartDefaultSession();

        void StartPointCloudVisualization();
        void StopPointCloudVisualization();


        #region ImageTracking Wrapper
        void StartImageTracking();
        void StopImageTracking();
        void ResetImageTracking();
        #endregion

        void SetLightEstimationState(bool value);
        /// <summary>
        /// Set current trackble type like a "planes","all" by int to enum
        /// </summary>
        /// <param name="type"></param>
        void SetCurrentTrackbleType(int type);

    }
}
