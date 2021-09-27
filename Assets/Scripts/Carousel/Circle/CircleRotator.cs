using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Carousel
{
    public class CircleRotator
    {
        public event Action RightLimit;

        public event Action LeftLimit;

        private CircleRotatorConfig _config;

        //private float _smoothRotationDuration = 0.5f;

        private float _lastLimitAngle;

        private Transform _rotationTransform;

        private float _rotationStep;

        private Tween _rotatingTween;

        private bool _canRotating;

        public CircleRotator(CircleRotatorConfig config)
        {
            _config = config;
        }

        public void Init(Transform rotationTransform, float rotationStep)
        {
            _rotationTransform = rotationTransform;
            _rotationStep = rotationStep;

            _lastLimitAngle = _rotationTransform.rotation.eulerAngles.y;

            _canRotating = true;
        }

        /// <summary>
        /// For Swipe.OnSwipe += Rotate 
        /// </summary>
        /// <param name="yAngle"></param>
        public void Rotate(float yAngle)
        {
            if (!_canRotating)
                return;

            //float difference = Mathf.Abs(Mathf.DeltaAngle(yAngle,_rotationTransform.rotation.eulerAngles.y));

            // if (difference > _config.MaxSpeed)
            // {
            //     yAngle = Mathf.Sign(yAngle) * _config.MaxSpeed;
            // }

            float rotAngle =
                Mathf.Repeat(
                    Mathf.Repeat(_rotationTransform.eulerAngles.y, 360) - Mathf.Sign(yAngle) * _config.RotationSpeed *
                    (_rotationStep / 36) * Time.deltaTime, 360);

            Vector3 rotation = _rotationTransform.rotation.eulerAngles;
            rotation.y = rotAngle;

            _rotationTransform.rotation = Quaternion.Euler(rotation);

            //_rotationTransform.Rotate(0, -Mathf.Sign(yAngle) * _config.RotationSpeed * Time.deltaTime, 0, Space.Self);

            //CheckLimits();
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
                    LeftLimit?.Invoke();
                }
                else
                {
                    _lastLimitAngle -= _rotationStep;
                    RightLimit?.Invoke();
                }
            }
        }
    }

    [Serializable]
    public class CircleRotatorConfig
    {
        public float RotationSpeed;
        public float SmoothRotationDuration;
    }
}