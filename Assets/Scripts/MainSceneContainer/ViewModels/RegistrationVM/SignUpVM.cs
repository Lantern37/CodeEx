using System;
using Assets.Scripts.MainWindows;
using Engenious.MainScene.Models;
using Engenious.MainScene.Services;
using Engenious.MainScene.SignUp;
using UnityEngine;

namespace Engenious.MainScene.ViewModels
{
    public class SignUpVM : BaseVM<SignUpWindow>
    {
        private ISignUpModel _signUpModel;
        private IValidationService _validationService;

        public event Action<SignUpModelUser> SuccessLogIn;
        public event Action<SignUpModelUser> FailLogIn;
        public event Action<string> WrongEmail;


        public SignUpVM(){}
        public SignUpVM(ISignUpModel signUpModel, IValidationService validationService)
        {
            _signUpModel = signUpModel;
            _validationService = validationService;
        }

        public void SubscribeButtons(Action<SignUpModelUser> success)
        {
            SuccessLogIn += success;
        }

        protected override void SetupWindow()
        {
            base.SetupWindow();

            _window.OnClosed += OnClose;
            _window.SubscribeNextStepButton(NextStepButton);
        
            _window.OutlineInput.InputField.onSubmit.AddListener(OnSubmit);

            _window.OutlineInput.InputField.onValueChanged.AddListener(OnEmailChanged);
            _window.PasswordInput.InputField.onValueChanged.AddListener(OnPasswordChanged);

            _signUpModel.SuccessSignUp += Success;
            _signUpModel.FailSignUp += Fail;
            
            _window.SetNextButtonActive(false);
        }

        private void OnClose()
        {
            SuccessLogIn = null;
            
            _signUpModel.UnSubscribeEvents();
        }
        
        private void NextStepButton()
        {
            var email = _window.OutlineInput.InputField.text;
            if (!_validationService.IsEmailValid(email))
            {
                _window.OutlineInput.ShowErrorOutline(true);
                WrongEmail?.Invoke(email);
                return;
            }
            
            SignUpModelUser user = new SignUpModelUser(_window.OutlineInput.InputField.text, 
                                                  _window.PasswordInput.InputField.text);
            
            _signUpModel.SendRegistrationData(user);
        }
        
        private void OnSubmit(string email)
        {
            if (!_validationService.IsEmailValid(email))
            {
                _window.OutlineInput.ShowErrorOutline(true);
                WrongEmail?.Invoke(email);
            }
        }

        private void OnEmailChanged(string email)
        {
            if (_window.OutlineInput.IsError)
            {
                _window.OutlineInput.ShowErrorOutline(false);
            }
            
            ActiveNextButtonCheck();
        }

        private void OnPasswordChanged(string email)
        {
            if (_window.PasswordInput.IsError)
            {
                _window.PasswordInput.ShowErrorOutline(false);
            }
            
            ActiveNextButtonCheck();
        }
        
        private void ActiveNextButtonCheck()
        {
            if (_window.PasswordInput.InputField.text.Equals(String.Empty)
                && _window.OutlineInput.InputField.text.Equals(String.Empty))
            {
                _window.SetNextButtonActive(false);
            }
            else
            {
                _window.SetNextButtonActive(true);
            }
        }
        
        private void Success(SignUpModelUser user)
        {
            Debug.Log("Success");

            SuccessLogIn?.Invoke(user);
        }
        
        private void Fail(SignUpModelUser user)
        {
            _window.OutlineInput.ShowErrorOutline(true);
            _window.PasswordInput.ShowErrorOutline(true);
            
            FailLogIn?.Invoke(user);
        }
    }
}