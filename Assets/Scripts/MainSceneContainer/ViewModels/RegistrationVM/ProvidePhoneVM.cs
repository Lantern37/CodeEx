using System;
using Assets.Scripts.MainWindows;
using Engenious.MainScene.Models;
using Firebase.Auth;
using UnityEngine;

namespace Engenious.MainScene.ViewModels
{
    public class ProvidePhoneVM : BaseVM<ProvidePhoneWindow>
    {
        private IProvidePhoneModel _phoneModel;

        public event Action<string> FailVerification;
        public event Action<string, string, ForceResendingToken> SuccessVerification;
        
        public ProvidePhoneVM(){}
        public ProvidePhoneVM(IProvidePhoneModel phoneModel)
        {
            _phoneModel = phoneModel;
        }
        
        public void SubscribeButtons(Action<string, string,ForceResendingToken> success)
        {
            SuccessVerification += success;
            
            _phoneModel.FailVerification += PhoneModelOnFailVerification;
            _phoneModel.SuccessVerification += PhoneModelOnSuccessVerification;

            _window.OutlineInput.InputField.onValueChanged.AddListener(OnPhoneChanged);
            _window.SubscribeNext(NextButton);
            _window.OnClosed += UnSubscribe;
            
            _window.SetNextButtonActive(false);
        }

        private void UnSubscribe()
        {
            _phoneModel.UnSubscribe();

            FailVerification = null;
            SuccessVerification = null;
        }

        private void NextButton()
        {
            _phoneModel.LogPhoneNumber(_window.OutlineInput.InputField.text);
        }
        
        private void OnPhoneChanged(string phone)
        {
            if (_window.OutlineInput.IsError)
            {
                _window.OutlineInput.ShowErrorOutline(false);
            }
            
            ActiveNextButtonCheck();
        }
        
        private void ActiveNextButtonCheck()
        {
            if (_window.OutlineInput.InputField.text.Equals(String.Empty))
            {
                _window.SetNextButtonActive(false);
            }
            else
            {
                _window.SetNextButtonActive(true);
            }
        }
        
        private void PhoneModelOnSuccessVerification(string id, string phoneNumber, ForceResendingToken token)
        {
            SuccessVerification?.Invoke(id, phoneNumber ,token);
            
            // #if UNITY_EDITOR
            //
            //
            // #endif
            
            Debug.Log("ID code = " + id + " phone number = " + phoneNumber + " token = " + token.ToString());
        }
        
        private void PhoneModelOnFailVerification(string error)
        {
            _window.OutlineInput.ShowErrorOutline(true);
            FailVerification?.Invoke(error);
        }
    }
}