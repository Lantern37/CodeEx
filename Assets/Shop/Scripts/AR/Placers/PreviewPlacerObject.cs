using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PreviewPlacerObject : MonoBehaviour
{
    [SerializeField] private float _rotateTime = 0f;

    [Space] 
    [SerializeField] private float _minScale = 0.2f;
    [SerializeField] private float _maxScale = 1f;

    [Range(1f, 10f)] 
    [SerializeField] private float _effectRange = 1f;

    [Space] 
    [SerializeField] private List<Transform> _transformsToScale;
    
    private Camera _camera;
    private Transform _targetTransform;

    private void Awake()
    {
        _camera = Camera.main;
        _targetTransform = _camera.transform;
    }

    private void Update ()
    {
        transform.DOLookAt(
            _targetTransform.position,
            _rotateTime,
            AxisConstraint.Y);

        ScaleByDistanceToCam();
    }

    private void ScaleByDistanceToCam()
    {
        float distance = Vector3.Distance(_camera.transform.position,this.transform.position);

        var scale = Mathf.Lerp(_minScale, _maxScale, distance / _effectRange);

        foreach (var transformToScale in _transformsToScale)
        {
            transformToScale.DOScale(scale, 0f);
        }
    }
}
