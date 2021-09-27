using System;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class SignUpProgressWindow : WindowController
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Slider _progressSlider;
        public Slider ProgressSlider => _progressSlider;
        
        private int _progressSliderMaxValue = 4;
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            ResetWidow();
        }

        protected override void Closed()
        {
            base.Closed();
            ResetWidow();
        }

        private void ResetWidow()
        {
            _progressSlider.value = 0;
            _progressSlider.maxValue = _progressSliderMaxValue;
            ClearSubsribers();
        }
        
        public void SetProgress(int value)
        {
            _progressSlider.value = value;
        }
        
        public void SubscribeBack(Action back)
        {
            if (back != null)
            {
                _backButton.onClick.AddListener(()=>back());
            }
        }

        public void ClearSubsribers()
        {
            _backButton.onClick.RemoveAllListeners();
        }
    }
}