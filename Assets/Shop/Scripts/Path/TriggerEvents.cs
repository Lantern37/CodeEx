using System;
using Dreamteck.Splines;
using UnityEngine;
using DG.Tweening;

    public class TriggerEvents : MonoBehaviour
    {
        
        private PathBehaviour _splineBehaviour;
        private SplineComputer _spline;

        void Awake()
        {
            _spline = GetComponent<SplineComputer>();
        }

        private void Start()
        {
            foreach (var trigger in _spline.triggerGroups[0].triggers)
            {
                trigger.enabled = false;
            }
        }

        public void Init(PathBehaviour pathBehaviour)
        {
            _splineBehaviour = pathBehaviour;
        }
        
        
        public void TriggerEvent()
        {
            Debug.Log("Trigger Event");
            // _splineBehaviour
        }
      
    }
