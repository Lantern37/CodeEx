using System;
using Engenious.Core.Managers;
using Engenious.WindowControllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class SignUpWindow : WindowController
    {
        [SerializeField] private OutlineInputField _outlineInput;
        [SerializeField] private PasswordInputField _passwordInput;

        public OutlineInputField OutlineInput => _outlineInput;
        public PasswordInputField PasswordInput => _passwordInput;
        
        [SerializeField] private Button _nextStepButton;
        
        public void SetNextButtonActive(bool value)
        {
            _nextStepButton.interactable = value;
        }
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            _outlineInput.Open();
            _passwordInput.Open();
        }

        protected override void Closed()
        {
            base.Closed();
            _outlineInput.Close();
            _passwordInput.Close();
            UnSubscribeButtons();
        }

        public void SubscribeNextStepButton(Action onClick)
        {
            _nextStepButton.onClick.AddListener(()=>onClick());
        }

        private void UnSubscribeButtons()
        {
            _nextStepButton.onClick.RemoveAllListeners();
        }
    }
}