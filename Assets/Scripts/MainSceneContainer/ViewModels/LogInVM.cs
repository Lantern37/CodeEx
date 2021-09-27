using System;
using Assets.Scripts.MainWindows;
using Engenious.Core.Managers;
using Engenious.MainScene.Models;
using Engenious.MainScene.Services;
using Engenious.WindowControllers;

namespace Engenious.MainScene.ViewModels
{
    public class LogInVM : BaseVM<LogInWindow>
    {
        public event Action<string> WrongEmail;
        public event Action<LoginUserResponce> SuccessLogIn;
        public event Action<LogInModelUser> FailLogIn;
        
        private ILogInModel _logInModel;
        private IValidationService _validationService;
        
        public LogInVM(){}
        public LogInVM(ILogInModel logInModel, IValidationService validationService)
        {
            _logInModel = logInModel;
            _validationService = validationService;
        }

        public void SubscribeButtons(Action<LoginUserResponce> success, Action back)
        {
            SuccessLogIn += success;
            _window.SubscribeBackButton(back);
        }

        // public void SubscribeSuccessLogIn(Action onClick)
        // {
        //     SuccessLogIn += onClick;
        // }
        //
        // public void SubscribeBackButton(Action onClick)
        // {
        //     _window.SubscribeBackButton(onClick);
        // }

        protected override void SetupWindow()
        {
            base.SetupWindow();

            _window.OnClosed += OnClose;
            _window.SubscribeEnterARbutton(OnEnterArButton);
        
            _window.OutlineInput.InputField.onSubmit.AddListener(OnSubmit);

            _window.OutlineInput.InputField.onValueChanged.AddListener(OnEmailChanged);
            _window.PasswordInput.InputField.onValueChanged.AddListener(OnPasswordChanged);

            _logInModel.SuccessLogIn += Success;
            _logInModel.FailLogIn += Fail;
            
            _window.SetNextButtonActive(false);
        }

        private void OnClose()
        {
            SuccessLogIn = null;
            
            _logInModel.UnSubscribeEvents();
        }
        
        private void OnEnterArButton()
        {
            var email = _window.OutlineInput.InputField.text;
            if (!_validationService.IsEmailValid(email))
            {
                _window.OutlineInput.ShowErrorOutline(true);
                WrongEmail?.Invoke(email);
                return;
            }
        
            LogInModelUser user = new LogInModelUser(_window.OutlineInput.InputField.text, 
                                                  _window.PasswordInput.InputField.text);
            
            _logInModel.SendLoginData(user);
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
        
        private void Success(LoginUserResponce user)
        {
            SuccessLogIn?.Invoke(user);
        }
        
        private void Fail(LogInModelUser user)
        {
            _window.OutlineInput.ShowErrorOutline(true);
            _window.PasswordInput.ShowErrorOutline(true);
            
            FailLogIn?.Invoke(user);
        }
    }
}