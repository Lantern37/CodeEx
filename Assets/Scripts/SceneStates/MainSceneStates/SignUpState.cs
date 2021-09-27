using System;
using Assets.Scripts.MainWindows;
using Engenious.Core.Managers;
using Engenious.MainScene.SignUp;
using Engenious.MainScene.ViewModels;
using Firebase.Auth;
using UnityEngine;

namespace Engenious.MainScene.SceneStates.MainSceneStates
{
    public class SignUpState : BasicMainSceneState<DefaultSceneStateParams>
    {
        private SignUpProgressWindow _signUpProgressWindow;
        
        private enum SignUpStages
        {
            SingUp = 0,
            ProvidePhone,
            ConfirnCode,
            VerifyIdentity,
            AccountCreated
        }
        
        public override bool SetActivate(bool value)
        {
            if (base.SetActivate(value))
            {
                if (value)
                {
                    ActivateState();
                }
                else
                {
                    DeActivateState();
                    return false;
                }
            }

            return true;
        }

        private void ActivateState()
        {
            ToSignUp();
        }

        private void ToWelcomeState()
        {
            CloseSignUp();
            StatesManager.WindowsManager.Close<SignUpProgressWindow>();

            StatesManager.DeactivateState<SignUpState>();
            StatesManager.ActivateState<WelcomeState>(new DefaultSceneStateParams());
        }
        
        private void ToSignUp()
        {
            var registrationVMs = StatesManager.MainSceneContainer.MainSceneViewModels.RegistrationVm;
            _signUpProgressWindow = StatesManager.WindowsManager.Show<SignUpProgressWindow>();
            
            _signUpProgressWindow.ClearSubsribers();
            _signUpProgressWindow.SubscribeBack(ToWelcomeState);
            _signUpProgressWindow.SetProgress((int)SignUpStages.SingUp);
            
            var signUpProgressVm = registrationVMs.SignUpProgressVm;
            signUpProgressVm.SetWindow(_signUpProgressWindow);
            
            var signUpWindow = StatesManager.WindowsManager.Show<SignUpWindow>();
            var signUpVm = registrationVMs.SignUpVm;
            signUpVm.SetWindow(signUpWindow);
            signUpVm.SubscribeButtons(OnSuccessRegistration);
        }

        private void CloseSignUp()
        {
            Debug.Log("OnSuccessRegistration");
            _signUpProgressWindow.ClearSubsribers();
            
            StatesManager.WindowsManager.Close<SignUpWindow>();
        }
        private void BackToSignUp()
        {
            _signUpProgressWindow.ClearSubsribers();

            StatesManager.WindowsManager.Close<ProvidePhoneWindow>();
            ToSignUp();
        }
        
        private void OnSuccessRegistration(SignUpModelUser user)
        {
            CloseSignUp();

            var providePhone = StatesManager.WindowsManager.Show<ProvidePhoneWindow>();
            var providePhoneVMs = StatesManager.MainSceneContainer.MainSceneViewModels.RegistrationVm.ProvidePhoneVm;
            providePhoneVMs.SetWindow(providePhone);
            providePhoneVMs.SubscribeButtons(SubscribePhoneSuccess);
            
            _signUpProgressWindow.SubscribeBack(BackToSignUp);
            _signUpProgressWindow.SetProgress((int)SignUpStages.ProvidePhone);
        }

        private void CloseSuccessRegistration()
        {
            _signUpProgressWindow.ClearSubsribers();

            StatesManager.WindowsManager.Close<ProvidePhoneWindow>();
        }

        private void BackToProvidePhone()
        {
            _signUpProgressWindow.ClearSubsribers();

            StatesManager.WindowsManager.Close<ConfirmNumberWindow>();
            
            //ToProvidePhone();
        }
        
        private void SubscribePhoneSuccess(string id, string phoneNumber, ForceResendingToken token)
        {
            CloseSuccessRegistration();
            
            var confirm = StatesManager.WindowsManager.Show<ConfirmNumberWindow>();
            var confirmVM = StatesManager.MainSceneContainer.MainSceneViewModels.RegistrationVm.ConfirmNumberVm;
            confirmVM.SetWindow(confirm);

            PhoneAuthoorizedStruct phone = new PhoneAuthoorizedStruct(phoneNumber, id);
            confirmVM.SubscribeButtons(PhoneModelOnPhoneAuthoorized, phone);
            
            _signUpProgressWindow.SubscribeBack(BackToProvidePhone);
            _signUpProgressWindow.SetProgress((int)SignUpStages.ConfirnCode);
        }

        private void CloseConfirmPhone()
        {
            _signUpProgressWindow.ClearSubsribers();

            StatesManager.WindowsManager.Close<ConfirmNumberWindow>();
            
            //ToPhoneProvider
        }

        private void PhoneModelOnPhoneAuthoorized(PhoneAuthoorizedStruct phone)
        {
            CloseConfirmPhone();
            
            //ToArState();
            ToVerifyPassport();
        }

        private void CloseVerifyPassport()
        {
            StatesManager.WindowsManager.Close<VerifyIdentityWindow>();
            ToWelcomeState();
        }

        private void ToVerifyPassport()
        {
            _signUpProgressWindow.SubscribeBack(CloseVerifyPassport);
            _signUpProgressWindow.SetProgress((int) SignUpStages.VerifyIdentity);

            var verify = StatesManager.WindowsManager.Show<VerifyIdentityWindow>(StatesManager.MainSceneContainer.MainSceneModels.VerifyIdentityModel);
            verify.PressNextButton += VerifyNext;
        }

        private void VerifyNext()
        {
            StatesManager.WindowsManager.Close<VerifyIdentityWindow>();
            
            _signUpProgressWindow.ClearSubsribers();
            _signUpProgressWindow.SubscribeBack(CloseVerifyPassport);
            _signUpProgressWindow.SetProgress((int) SignUpStages.AccountCreated);
            
            var accCreated = StatesManager.WindowsManager.Show<AccountCreatedWindow>();
            accCreated.SubscribeNext(AccCreatedNext);
        }

        private void AccCreatedNext()
        {
            _signUpProgressWindow.ClearSubsribers();
            StatesManager.WindowsManager.Close<AccountCreatedWindow>();

            ToArState();
        }

        private void ToArState()
        {
            StatesManager.WindowsManager.Close<SignUpProgressWindow>();

            StatesManager.DeactivateState<SignUpState>();
            StatesManager.ActivateState<MainPageState>(new DefaultSceneStateParams());
        }
        
        private void DeActivateState()
        {
            //StatesManager.WindowsManager.Close<SignUpWindow>();
            //StatesManager.WindowsManager.Close<SignUpProgressWindow>();
        }
    }
}