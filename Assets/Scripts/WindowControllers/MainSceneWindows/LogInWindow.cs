using System;
using Engenious.Core.Managers;
using Engenious.WindowControllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class LogInWindow : WindowController
    {
        [SerializeField] private OutlineInputField _outlineInput;
        [SerializeField] private PasswordInputField _passwordInput;

        public OutlineInputField OutlineInput => _outlineInput;
        public PasswordInputField PasswordInput => _passwordInput;
        
        [SerializeField] private Button _enterArButton;
        [SerializeField] private Button _backButton;
        
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

        public void SubscribeEnterARbutton(Action onClick)
        {
            _enterArButton.onClick.AddListener(()=>onClick());
        }

        public void SubscribeBackButton(Action onClick)
        {
            _backButton.onClick.AddListener(()=>onClick());
        }
        
        private void UnSubscribeButtons()
        {
            _enterArButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        public void SetNextButtonActive(bool value)
        {
            _enterArButton.interactable = value;
        }
    }
}