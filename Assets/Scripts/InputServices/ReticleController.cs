using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Scripts.InputServices
{
    /// <summary>
    /// Legacy
    /// </summary>
    public class ReticleController : MonoBehaviour
    {
        [SerializeField] ARRaycastManager m_RaycastManager;
        
        List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        CenterScreenHelper m_CenterScreen;

        [SerializeField] GameObject _reticle;

        public Transform ReticleTransform => _reticle.transform;

        TrackableType m_RaycastMask;

        private bool _isShow;
        
        void Start()
        {
            m_CenterScreen = CenterScreenHelper.Instance;
            m_RaycastMask = TrackableType.PlaneWithinPolygon;
            HideRecticle();
        }

        void Update()
        {
            if (!_isShow)
                return;
            
            if (m_RaycastManager.Raycast(m_CenterScreen.GetCenterScreen(), s_Hits, m_RaycastMask))
            {
                Pose hitPose = s_Hits[0].pose;
                _reticle.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                //_reticle.SetActive(true);
            }
        }

        public void HideRecticle()
        {
            _isShow = false;
            _reticle.SetActive(false);
        }

        public void ShowRecticle()
        {
            _isShow = true;
            _reticle.SetActive(true);
        }
    }
}