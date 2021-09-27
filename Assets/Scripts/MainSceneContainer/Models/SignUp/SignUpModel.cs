using System;
using Assets.Scripts.UserDataScripts;
using Engenious.Core.Managers;
using Engenious.Core.Managers.Requests;
using Engenious.MainScene.Models;
using Engenious.MainScene.Services;
using Firebase.Messaging;
using UnityEngine;

namespace Engenious.MainScene.SignUp
{
    public interface ISignUpModel
    {
        event Action<SignUpModelUser> FailSignUp;
        event Action<SignUpModelUser> SuccessSignUp;
        void SendRegistrationData(SignUpModelUser user);
        void UnSubscribeEvents();
    }

    public class SignUpModel : ISignUpModel
    {
        public event Action<SignUpModelUser> FailSignUp;
        public event Action<SignUpModelUser> SuccessSignUp;
        private ILogInModel _logInModel;
        //private IPushNotificationManager _push;
        private INetworkUserRequests _network;
        private UserIdHolder _userId;
        private IUserDataController _userData;

        public SignUpModel(){}
        
        public SignUpModel(INetworkUserRequests network, ILogInModel logInModel, UserIdHolder userId, IUserDataController userData)
        {
            _logInModel = logInModel;
            _network = network;
            _userId = userId;
            _userData = userData;

            //_push.TokenReceived += PushOnTokenReceived;
        }
        
        public async void SendRegistrationData(SignUpModelUser user) 
        {
            PutUserRequest request = new PutUserRequest()
            {
                Email = user.Email,
                Password = user.Password
            };
            
            var resp = await _network.PutUser(request);
            
            Debug.Log("SendRegistrationData");
            // if (resp == null)
            // {
            //     FailSignUp?.Invoke(user);
            // }
            // else
            // {
            //     SuccessSignUp?.Invoke(user);
            // }

            LogInModelUser logInUser = new LogInModelUser(request.Email, request.Password);
            _logInModel.SendLoginData(logInUser , (x)=>SuccessSignUp?.Invoke(user));
        }
        
        // private async void PushOnTokenReceived(TokenReceivedEventArgs token)
        // {
        //     _push.TokenReceived -= PushOnTokenReceived;
        //
        //     PutTokenRequest request = new PutTokenRequest()
        //     {
        //         FirebaseToken = token.Token
        //     };
        //
        //     await _network.PutToken(request, int.Parse(_userId.UserId));
        // }
        
        public void UnSubscribeEvents()
        {
            FailSignUp = null;
            SuccessSignUp = null;
        }
    }
}