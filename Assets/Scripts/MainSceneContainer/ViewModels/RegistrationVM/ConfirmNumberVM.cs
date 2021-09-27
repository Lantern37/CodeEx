using System;
using Assets.Scripts.MainWindows;
using Engenious.MainScene.ViewModels;
using Firebase.Auth;

namespace Engenious.MainScene.ViewModels
{
    public class ConfirmNumberVM : BaseVM<ConfirmNumberWindow>
    {
        public event Action FailVerification;
        public event Action<PhoneAuthoorizedStruct> SuccessVerification;

        private IProvidePhoneModel _phoneModel;
        private PhoneAuthoorizedStruct _phoneStruct;

        public ConfirmNumberVM(IProvidePhoneModel phoneModel)
        {
            _phoneModel = phoneModel;
        }
        
        public void SubscribeButtons(Action<PhoneAuthoorizedStruct> success, PhoneAuthoorizedStruct phoneStruct)
        {
            SuccessVerification += success;
            
            _phoneModel.PhoneAuthoorized += PhoneModelOnPhoneAuthoorized;
            _phoneModel.FailPhoneAuthoorized += FailPhoneModelOnPhoneAuthoorized;

            _window.CodeInputField.InputField.onValueChanged.AddListener(OnCodeChanged);
            _window.SubscribeNext(NextButton);
            _window.OnClosed += UnSubscribe;

            Setup(phoneStruct);
            _window.SetNextButtonActive(false);
        }

        private void Setup(PhoneAuthoorizedStruct phoneStruct)
        {
            _phoneStruct = phoneStruct;
            _window.SetPhoneNumber(_phoneStruct.Phone);
        }
        
        private void UnSubscribe()
        {
            _phoneModel.UnSubscribe();

            FailVerification = null;
            SuccessVerification = null;
        }

        private void PhoneModelOnPhoneAuthoorized(PhoneAuthoorizedStruct phone)
        {
            SuccessVerification?.Invoke(phone);
        }

        private void FailPhoneModelOnPhoneAuthoorized()
        {
            if (!_phoneStruct.Code.Equals(_window.CodeInputField.InputField.text))
            {
                if (!_window.CodeInputField.IsError)
                {
                    _window.CodeInputField.ShowErrorOutline(true);
                }
                FailVerification?.Invoke();
            }
        }
        
        private void NextButton()
        {
            // if (!_phoneStruct.Code.Equals(_window.CodeInputField.InputField.text))
            // {
            //     if (!_window.CodeInputField.IsError)
            //     {
            //         _window.CodeInputField.ShowErrorOutline(true);
            //     }
            //     FailVerification?.Invoke(_window.CodeInputField.InputField.text);
            // }
            // else
            // {
            // }
            _phoneModel.AuthoorizeWithPhone(_phoneStruct.Code, _window.CodeInputField.InputField.text);

        }
        
        private void OnCodeChanged(string phone)
        {
            if (_window.CodeInputField.IsError)
            {
                _window.CodeInputField.ShowErrorOutline(false);
            }
            
            ActiveNextButtonCheck();
        }
        
        private void ActiveNextButtonCheck()
        {
            if (_window.CodeInputField.InputField.text.Equals(String.Empty))
            {
                _window.SetNextButtonActive(false);
            }
            else
            {
                _window.SetNextButtonActive(true);
            }
        }
        
    }
}