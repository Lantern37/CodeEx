using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TEstFrameRecieved : MonoBehaviour
{
    [SerializeField] private ARCameraManager _cameraManager;

    void Start()
    {
        _cameraManager.frameReceived += CameraManagerOnframeReceived;   
    }

    private void CameraManagerOnframeReceived(ARCameraFrameEventArgs obj)
    {
        Debug.Log("FRAME RECIEVED");
    }

    void Update()
    {
        
    }
}
