using System;
using Assets.Scripts.MainSceneContainer.ViewModels;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class SupportWindow : WindowController
    {
        [SerializeField] private InputField _title;
        [SerializeField] private InputField _message;

        [SerializeField] private Button _next;
        [SerializeField] private Button _close;

        private SupportVM _supportVm;
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);

            var supportVM = _params[0] as SupportVM;

            if (supportVM != null)
            {
                _supportVm = supportVM;
                _supportVm.SetWindow(this);
            }

            OnInputChange();
            
            Subscribe();
        }

        protected override void Closed()
        {
            base.Closed();
            UnSubscribe();
        }

        private void Subscribe()
        {
            _title.onValueChanged.AddListener(OnInputChange);
            _message.onValueChanged.AddListener(OnInputChange);
            _close.onClick.AddListener(CloseButton);
            _next.onClick.AddListener(NextButton);
        }

        private void UnSubscribe()
        {
            _title.onValueChanged.RemoveListener(OnInputChange);
            _message.onValueChanged.RemoveListener(OnInputChange);
            _close.onClick.RemoveListener(CloseButton);
            _next.onClick.RemoveListener(NextButton);
        }

        private void NextButton()
        {
            _supportVm.SendMessage(_title.text, _message.text, Success, Fail);
        }

        private void Fail()
        {
            
        }

        private void Success(SupportResponce response)
        {
            Close();
            Manager.Show<SupportPopup>();
        }

        private void CloseButton()
        {
            Close();
        }

        private void ActivateNextButton(bool active)
        {
            _next.interactable = active;
        }

        private void OnInputChange(string arg = "")
        {
            bool activateNext = !_title.text.Equals(string.Empty) && !_message.text.Equals(string.Empty);

            ActivateNextButton(activateNext);
        }
    }
}