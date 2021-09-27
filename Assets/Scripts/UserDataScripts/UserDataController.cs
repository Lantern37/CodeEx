using System;
using Engenious.Core.Managers;
using Engenious.MainScene.Models;
using Engenious.MainScene.SceneStates.MainSceneStates;
using Engenious.MainScene.Services;
using Firebase.Messaging;
using UnityEngine;

namespace Assets.Scripts.UserDataScripts
{
    public interface IUserDataController
    {
        event Action<SavedUserData> SuccessDataLoaded;
        event Action FailDataLoad;
        ISavedUserDataHolder UserDataHolder { get; }
        SavedUserData UserData { get; }
        void Initialize();
        void SaveData();
        void SetAddress(string address);
        void DeleteUser();
        void SetNameAndAddress(string name, string address);
        void Init(ILogInModel logInModel, IPushNotificationManager push);
        void SetDeviceToken(string deviceToken);
        void SetNameAndAddress(string name, Result address);
        void SetAddress(Result address);
        void SetFirstTutorial(bool isShowed);
    }

    public class UserDataController : IUserDataController
    {
        private IUserDataLoader _dataLoader;
        public ISavedUserDataHolder UserDataHolder { get; private set; }

        public SavedUserData UserData { get ; private set; }

        public event Action<SavedUserData> SuccessDataLoaded;
        public event Action FailDataLoad;

        private ILogInModel _logInModel;
        private IPushNotificationManager _push;

        public UserDataController (){}

        public UserDataController(ILogInModel logInModel, IPushNotificationManager push)
        {
            _logInModel = logInModel;
            _push = push;
        }

        public void Init(ILogInModel logInModel, IPushNotificationManager push)
        {
            _logInModel = logInModel;
            _push = push;
        }
        
        public void Initialize()
        {
            UserData = new SavedUserData();

            UserDataHolder = new SavedUserDataHolder(UserData);
            _dataLoader = new UserDataLoader(UserData);
            
            _dataLoader.Load(SuccessLoadData, ReadFail);
            
            _logInModel.SuccessLogIn += LogInModelOnSuccessLogIn;
            _logInModel.GetUserIdSuccess += LogInModelOnGetUserIdSuccess;
            
            _push.TokenReceived += PushOnTokenReceived;
        }

        private void PushOnTokenReceived(TokenReceivedEventArgs token)
        {
            SetDeviceToken(token.Token);
        }

        public void SaveData()
        {
            _dataLoader.Write();
        }

        public void SetAddress(string address)
        {
            UserDataHolder.SetAddress(address);
            SaveData();
        }

        public void SetAddress(Result address)
        {
            UserDataHolder.SetAddress(address);
            SaveData();
        }
        
        public void SetNameAndAddress(string name, string address)
        {
            UserDataHolder.SetAddress(address);
            UserDataHolder.SetName(name);

            SaveData();
        }

        public void SetNameAndAddress(string name, Result address)
        {
            //UserDataHolder.SetAddress((string) address);
            UserDataHolder.SetAddress(address);
            UserDataHolder.SetName(name);

            SaveData();
        }
        
        public void SetDeviceToken(string deviceToken)
        {
            UserDataHolder.SetDeviceToken(deviceToken);
            
            SaveData();
        }
        
        public void SetFirstTutorial(bool isShowed)
        {
            UserDataHolder.SetFirstTutorial(isShowed);
            SaveData();
        }
        
        public void DeleteUser()
        {
            UserData.Clear();
            _dataLoader.DeleteFile();
        }
        
        private void LogInModelOnGetUserIdSuccess(GetUserIdResponse user)
        {
            UserDataHolder.GetUserIdSuccess(user);
            SaveData();
        }

        private void LogInModelOnSuccessLogIn(LoginUserResponce user)
        {
            UserDataHolder.SuccessLogIn(user);
            SaveData();
        }

        private void ReadFail()
        {
            FailDataLoad?.Invoke();
        }

        private string _testToken = "cQl0iXcIQAKgEIYBcHEM7B:APA91bF-7IiHdGtAf4N68XS-NSTOkUPpXty9HBAQNiHPRv2LSAzrICHed41kYHmINOBbrkt0zm-kH3326F20zwmzBLHoSAbrjEgiSBWsB87DdKUDy8HFxY7TiFgbWoTaCpYPpJ0q4yKX";
        private void SuccessLoadData(SavedUserData data)
        {
            UserData = data;
            UserDataHolder.SetUserData(data);
// #if UNITY_EDITOR
//             UserDataHolder.SetDeviceToken(_testToken);
// #endif
            //Debug.Log("data = " + UserData);
            
            SuccessDataLoaded?.Invoke(UserData);
        }
    }
}