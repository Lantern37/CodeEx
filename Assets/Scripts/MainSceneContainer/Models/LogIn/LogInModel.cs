using System;
using Assets.Scripts.UserDataScripts;
using Engenious.Core.Managers;
using Engenious.Core.Managers.Requests;
using Engenious.MainScene.Services;
using Firebase.Messaging;
using UnityEngine;

namespace Engenious.MainScene.Models
{
    public interface ILogInModel
    {
        //LoginUserData UserData { get; }
        event Action<LogInModelUser> FailLogIn;
        event Action<LoginUserResponce> SuccessLogIn;
        event Action<GetUserIdResponse> GetUserIdSuccess;

        void SendLoginData(LogInModelUser user, Action<LogInModelUser> success = null);
        void SendLoginDataId(int userId, Action<GetUserIdResponse> success = null);
        void UnSubscribeEvents();
        void Init(INetworkUserRequests network, UserIdHolder userId, IUserDataController userData);
    }

    public class LogInModel : ILogInModel
    {
        //public LoginUserData UserData { get; private set; }
        public event Action<LogInModelUser> FailLogIn;
        public event Action<LoginUserResponce> SuccessLogIn;

        public event Action<GetUserIdResponse> GetUserIdSuccess;

        private INetworkUserRequests _network;

        private UserIdHolder _userId;
        private IUserDataController _userData;

        public LogInModel()
        {
        }

        public LogInModel(INetworkUserRequests network, UserIdHolder userId, IUserDataController userData)
        {
            _network = network;
            _userId = userId;
            _userData = userData;
            //UserData = new LoginUserData();
        }

        public void Init(INetworkUserRequests network, UserIdHolder userId, IUserDataController userData)
        {
            _network = network;
            _userId = userId;
            _userData = userData;
            //UserData = new LoginUserData();
        }
        
        public async void SendLoginData(LogInModelUser user, Action<LogInModelUser> success = null)
        {
            LoginUserRequest request = new LoginUserRequest()
            {
                Email = user.Email,
                Password = user.Password
            };

            LoginUserResponce resp = await _network.LoginUser(request);

            if (resp == null)
            {
                FailLogIn?.Invoke(user);
            }
            else
            {
                if (_userData?.UserData?.DeviceToken != String.Empty)
                {
                    // UserData.Id = resp.Id;
                    // UserData.Phone = resp.Phone;
                    //_push.TokenReceived += PushOnTokenReceived;
                    PutTokenRequest requestToken = new PutTokenRequest()
                    {
                        FirebaseToken = _userData.UserData.DeviceToken
                    };

                    await _network.PutToken(requestToken, resp.Id);
                }

                _userId.UserId = resp.Id.ToString();
                
                SuccessLogIn?.Invoke(resp);
                success?.Invoke(user);
            }
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

        public async void SendLoginDataId(int userId, Action<GetUserIdResponse> success = null)
        {
            GetUserIdResponse resp = await _network.GetUser(userId);

            if (resp == null)
            {
            }
            else
            {
                if (_userData?.UserData?.DeviceToken != String.Empty)
                {
                    PutTokenRequest requestToken = new PutTokenRequest()
                    {
                        FirebaseToken = _userData.UserData.DeviceToken
                    };


                    await _network.PutToken(requestToken, resp.Id);
                }

                //_userId.UserId = resp.Id.ToString();
                //_push.TokenReceived += PushOnTokenReceived;
                //UserData = GetUserDataFromResponse(resp);
                _userId.UserId = resp.Id.ToString();
                GetUserIdSuccess?.Invoke(resp);
                success?.Invoke(resp);
            }
        }

        private LoginUserData GetUserDataFromResponse(GetUserIdResponse response)
        {
            LoginUserData data = new LoginUserData()
            {
                Id = response.Id,
                Phone = response.Phone
            };
            return data;
        }
        
        public void UnSubscribeEvents()
        {
            FailLogIn = null;
            SuccessLogIn = null;
        }
    }

    public class LoginUserData
    {
        public int Id { get; set; }

        public string Phone { get; set; }
    }
}