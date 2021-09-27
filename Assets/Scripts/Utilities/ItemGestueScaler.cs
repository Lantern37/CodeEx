using System;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Utilities
{
    public class ItemGestueScaler
    {
        private float initialDistance;
        private Vector3 initialScale;

        private Transform _scaledTransform;

        private bool _canScale;

        private float _scaleThreshold = 1.5f;

        private Vector3 _startScale;

        public ItemGestueScaler(Transform scaledTransform)
        {
            _scaledTransform = scaledTransform;
            _startScale = _scaledTransform.localScale;
            
            //Start();
        }

        public void Start()
        {
            _canScale = true;
        }
        
        public void Stop()
        {
            _canScale = false;
        }

        public void ReturnToStartScale(float time = 0.3f, Action callback = null)
        {
            _scaledTransform.DOScale(_startScale, time).OnComplete(()=>callback?.Invoke());
        }
        
        public void Update()
        {
            if(!_canScale)
                return;
            
            if(_scaledTransform == null)
                return;
            
            if (Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0); 
                var touchOne = Input.GetTouch(1);

                // if one of the touches Ended or Canceled do nothing
                if(touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled  
                   || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled) 
                {
                    return;
                }

                // It is enough to check whether one of them began since we
                // already excluded the Ended and Canceled phase in the line before
                if(touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    // track the initial values
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = _scaledTransform.localScale;
                }
                // else now is any other case where touchZero and/or touchOne are in one of the states
                // of Stationary or Moved
                else
                {
                    // otherwise get the current distance
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                    // A little emergency brake ;)
                    if(Mathf.Approximately(initialDistance, 0)) return;

                    // get the scale factor of the current distance relative to the inital one
                    var factor = currentDistance / initialDistance;

                    // apply the scale
                    // instead of a continuous addition rather always base the 
                    // calculation on the initial and current value only

                    if (initialScale.x * factor <= _scaleThreshold)
                    {
                        _scaledTransform.localScale = initialScale * factor;
                    }
                }
            }
        }
    }
}