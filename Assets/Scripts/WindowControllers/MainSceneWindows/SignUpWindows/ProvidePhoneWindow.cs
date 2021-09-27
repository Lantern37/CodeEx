using System;
using Engenious.Core.Managers;
using Engenious.WindowControllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class ProvidePhoneWindow : WindowController
    {
        [SerializeField] private OutlineInputField _outlineInput;

        [SerializeField] private Button _nextStep;

        public OutlineInputField OutlineInput => _outlineInput;
        
        public void SetNextButtonActive(bool value)
        {
            _nextStep.interactable = value;
        }
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            _outlineInput.Open();
        }

        protected override void Closed()
        {
            base.Closed();
            _outlineInput.Close();
            UnSubscribeButtons();
        }
        
        public void SubscribeNext(Action onClick)
        {
            _nextStep.onClick.AddListener(()=>onClick());
        }

        private void UnSubscribeButtons()
        {
            _nextStep.onClick.RemoveAllListeners();
        }
    }
}