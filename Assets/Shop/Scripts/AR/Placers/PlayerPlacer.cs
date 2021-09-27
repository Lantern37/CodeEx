using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Zenject;
// using UnityEngine.InputSystem;


namespace Shop.Behaviours.AR
{
    public class PlayerPlacer : MonoBehaviour
    {
        [SerializeField] private SpawnerControll _spawner;
        
        private static Quaternion Rotation180Degree = Quaternion.Euler(0f, 180f, 0f);
        private const TrackableType TrackableTypes = TrackableType.PlaneWithinPolygon;
        private readonly List<ARRaycastHit> _hits = new List<ARRaycastHit>();
        private Vector2 _centerOfScreen;
        private SpawnerControll _instantiatedSpawner;
        
        [Inject] private ARRaycastManager _raycastManager;
        [Inject] private InputManager m_InputManager;
        // [Inject] private ARScaleControls _scaleControls;
        private void Awake()
        {
            _centerOfScreen = new Vector2(Screen.width / 2.0f, 
                                          Screen.height / 2.0f);
        }
        
        
        //Updated
        public bool TryGetHit(out Pose hit)
        {
#if UNITY_EDITOR
            hit = new Pose(new Vector3(0,-0.5f,1.5f), 
                           Quaternion.identity);
            return true;
#endif
            
            var isTouch = _raycastManager.Raycast(_centerOfScreen,
                                                  _hits,
                                                  TrackableTypes);
            
            hit = isTouch
                ? _hits[0].pose
                : Pose.identity;
            return isTouch;
        }

        public bool IsTouch()
        {
#if UNITY_EDITOR
            return Input.GetMouseButtonDown(0);
#endif
            
            return Input.touchCount > 0
                   && Input.GetTouch(0).phase == TouchPhase.Began;
        }

        
        public void SpawnPlayer(Pose hit)
        {
            Debug.Log("Spaner spawned");
            _instantiatedSpawner = Instantiate (_spawner, hit.position, 
                                                        hit.rotation * Rotation180Degree);
        }
    }
}

