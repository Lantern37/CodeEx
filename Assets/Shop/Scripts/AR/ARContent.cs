using DG.Tweening;
using Shop.Behaviours;
using Shop.Behaviours.AR;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using Zenject;

public class ARContent : MonoBehaviour
{
    public const float MinDistanceToUser = 0.25f;
    
    [SerializeField] private PreviewPlacer _previewPlacer;
    

    private Transform _transform;

    public Vector3 Position => _transform.position;

    private ARSessionOrigin _sessionOrigin;
    // private ScaleController _scaleController;
    private ARCamera _arCamera;
    
    private Vector3 _previousAppearPosition = Vector3.zero;
    
    public float DistanceToUser => Vector3.Distance(
        _arCamera.transform.position, 
        _transform.position);

    public Vector3 DirectionToUser => _arCamera.transform.position - _transform.position;

    public Vector3 CameraPosition => _arCamera.transform.position;
    public Vector3 PreviewPlacerPosition => _previewPlacer.Position;


    [Inject]
    private void Construct(ARSessionOrigin sessionOrigin,
                           // ScaleController scaleController,
                           ARCamera arCamera)
    {
        _sessionOrigin = sessionOrigin;
        // _scaleController = scaleController;
        _arCamera = arCamera;
    }

    private void Awake()
    {
        _transform = transform;
    }


    public void Appear(Vector3 position, Quaternion rotation)
    {
        if (!_previewPlacer.IsActive)
        {
            _previewPlacer.Show();
        }

        RotateToCamera();
        AppearContent(position);
    }

    private void AppearContent(Vector3 position)
    {
        if (_previousAppearPosition == position)
        {
            return;
        }
                
        _previousAppearPosition = position;
        _sessionOrigin.MakeContentAppearAt(_transform, position, _transform.rotation);
        
        ApplyPreviewScaleByDistanceToUser();
    }

    private void RotateToCamera()
    {
        _transform.DOLookAt(
            _arCamera.Position,
            0f,
            AxisConstraint.Y);
    }

    
    private void ApplyPreviewScaleByDistanceToUser()
    {
        // var previewScale = _scaleController.CalculatePreviewScale(DistanceToUser);
        // _previewPlacer.ApplyScale(previewScale);
    }
    
    public void ApplyContentScaleByDistanceToUser()
    {
        // _scaleController.ApplyContentScale(DistanceToUser);
    }
    
    

    public void Reset()
    {
        // _scaleController.ResetScale();
    }
}
