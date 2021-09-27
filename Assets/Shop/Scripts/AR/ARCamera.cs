using System;
using UnityEngine;

namespace Shop.Behaviours
{
    
    [RequireComponent(typeof(Camera))]
    public class ARCamera : MonoBehaviour
    {
        public event Action<CameraDirections> CameraDirectionChanged;

        [Header("Camera Direction:")] 
        [SerializeField] private float _downCameraDirection = 30f;
        
        public Vector3 Position => transform.position;

        public Quaternion Rotation => transform.rotation;

        public CameraDirections CameraDirection { get; private set; } = CameraDirections.None;
        
        public Camera Camera { get; private set; }

        private void Awake()
        {
            Camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            DetectCameraDirection();
        }

        private void DetectCameraDirection()
        {
            var eulerRotation = Rotation.eulerAngles;

            if (_downCameraDirection > eulerRotation.x
                && CameraDirection != CameraDirections.Forward)
            {
                SetCameraDirectionState(CameraDirections.Forward);
            }
            else if (_downCameraDirection < eulerRotation.x
                     && CameraDirection != CameraDirections.Down)
            {
                SetCameraDirectionState(CameraDirections.Down);
            }
        }

        private void SetCameraDirectionState(CameraDirections cameraDirection)
        {
            CameraDirection = cameraDirection;
            
            Debug.LogWarning(CameraDirection);
            
            OnCameraDirectionChanged(CameraDirection);
        }


        public void SetTargetTexture(RenderTexture renderTexture)
        {
            Camera.targetTexture = renderTexture;
        }

        
        protected virtual void OnCameraDirectionChanged(CameraDirections cameraDirection)
        {
            CameraDirectionChanged?.Invoke(cameraDirection);
        }
    }
    
    
    public enum CameraDirections
    {
        None,
        Forward,
        Down,
    }
}


