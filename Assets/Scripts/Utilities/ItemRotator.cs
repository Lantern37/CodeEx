using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class ItemRotator
    {
        private ItemRotatorConfig _config;
        
        private Transform _rotationTransform;

        private Quaternion _startRotation;

        public event Action EndReturnRotation; 
        
        private float _rotationStep = 90;

        private Tween _rotatingTween;

        private float _lastLimitAngle;

        private bool _canRotating;
        
        public ItemRotator(ItemRotatorConfig config)
        {
            _config = config;
        }

        public void SetConfig(ItemRotatorConfig config)
        {
            _config = config;
        }
        
        public void SetTransform(Transform transform)
        {
            _canRotating = true;
            _rotationTransform = transform;
            _startRotation = _rotationTransform.rotation;
        }

        public void RotateTransform(Vector2 rotation)
        {
            if(_rotationTransform==null)
                return;
            
            float rotX = Mathf.Sign(rotation.x) * _config.RotationSpeed *Mathf.Deg2Rad *50* Time.deltaTime;
            float rotY = Mathf.Sign(rotation.y) * _config.RotationSpeed *Mathf.Deg2Rad* 10*Time.deltaTime;

            //need ClampRotation

            _rotationTransform.Rotate(Vector3.up, rotX);

            if (Mathf.Abs(Mathf.DeltaAngle(_startRotation.eulerAngles.y, Mathf.Repeat(_rotationTransform.rotation.eulerAngles.y, 360))) <
                _config.RotationThreshold)
            {
                _rotationTransform.Rotate(Vector3.right, rotY);
            }
        }

        public void ReturnToStartRotation(Action callback = null)
        {
            _rotationTransform.DORotate(_startRotation.eulerAngles, _config.TimeToStartRotation)
                .OnComplete((() =>
                {
                    EndReturnRotation?.Invoke();
                    callback?.Invoke();
                }));
        }

        public void ToRightLimit(int step = 1)
        {
            if (!_canRotating)
                return;
            _rotatingTween?.Kill();

            //_canRotating = false;

            Vector3 smoothRot = new Vector3();

            smoothRot.y = _lastLimitAngle + step * _rotationStep;
            smoothRot.y = Mathf.Repeat(smoothRot.y, 360);

            _rotatingTween = _rotationTransform
                .DORotate(smoothRot, step * _config.SmoothRotationDuration, RotateMode.Fast)
                .OnComplete((() => _canRotating = true));
            ;
        }

        public void ToLeftLimit(int step = 1)
        {

            if (!_canRotating)
                return;
            _rotatingTween?.Kill();

            //_canRotating = false;

            Vector3 smoothRot = new Vector3();

            smoothRot.y = _lastLimitAngle - step * _rotationStep;

            smoothRot.y = Mathf.Repeat(smoothRot.y, 360);

            _rotatingTween = _rotationTransform
                .DORotate(smoothRot, step * _config.SmoothRotationDuration, RotateMode.Fast)
                .OnComplete((() => _canRotating = true));
        }

        public void StartRotation()
        {
            _canRotating = true;
            _rotatingTween?.Kill();
        }

        public void StopRotation()
        {
            _canRotating = false;
            _rotatingTween?.Kill();
            SmoothNearestLimit();
        }

        public void Update()
        {
            if (_rotationTransform != null)
            {
                CheckLimits();
            }
        }

        public void SmoothNearestLimit()
        {
            Vector3 smoothRot = new Vector3();
            float yRotation = _rotationTransform.rotation.eulerAngles.y;

            float differencePlus = Mathf.Abs(Mathf.DeltaAngle(_lastLimitAngle + _rotationStep, yRotation));
            float differenceMinus = Mathf.Abs(Mathf.DeltaAngle(_lastLimitAngle - _rotationStep, yRotation));
            float differenceCurrent = Mathf.Abs(Mathf.DeltaAngle(_lastLimitAngle, yRotation));

            if (Mathf.Min(differencePlus, differenceMinus, differenceCurrent) == differenceMinus)
            {
                smoothRot.y = _lastLimitAngle - _rotationStep;
            }
            else if (Mathf.Min(differencePlus, differenceMinus, differenceCurrent) == differencePlus)
            {
                smoothRot.y = _lastLimitAngle + _rotationStep;
            }
            else if (Mathf.Min(differencePlus, differenceMinus, differenceCurrent) == differenceCurrent)
            {
                smoothRot.y = _lastLimitAngle;
            }

            smoothRot.y = Mathf.Repeat(smoothRot.y, 360);

            _rotatingTween = _rotationTransform.DORotate(smoothRot, _config.SmoothRotationDuration, RotateMode.Fast);
        }

        private void CheckLimits()
        {
            float yRotation = _rotationTransform.rotation.eulerAngles.y;

            float difference = Mathf.DeltaAngle(_lastLimitAngle, yRotation); //Rotation - _lastLimitAngle;
            difference = (float)Math.Round((double)difference, 2);

            if (Mathf.Abs(difference) >= _rotationStep)
            {
                //_lastLimitAngle = yRotation;

                if (Mathf.Sign(difference) > 0)
                {
                    _lastLimitAngle += _rotationStep;
                    //LeftLimit?.Invoke();
                }
                else
                {
                    _lastLimitAngle -= _rotationStep;
                    //RightLimit?.Invoke();
                }
            }
        }
    }

    [Serializable]
    public class ItemRotatorConfig
    {
        public float RotationSpeed = 70;
        
        public float RotationThreshold = 15;

        public float TimeToStartRotation = 1;
        
        public float SmoothRotationDuration;

        public int MaxRotationStep;
    }
}