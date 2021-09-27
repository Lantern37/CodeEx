using System;
using Engenious.Core.Managers;

namespace Assets.Scripts.UserDataScripts
{
    public interface ISavedUserDataHolder
    {
        SavedUserData UserData { get; }
        event Action<SavedUserData> UserDataChanged;
        void SetUserData(SavedUserData userData);
        void SetPhone(string phone);
        void SetId(int id);
        void SetAddress(string address);
        void SuccessLogIn(LoginUserResponce user);
        void GetUserIdSuccess(GetUserIdResponse user);
        void SetName(string name);
        void SetDeviceToken(string deviceToken);
        void SetAddress(Result address);
        void SetFirstTutorial(bool isShowed);
    }

    public class SavedUserDataHolder : ISavedUserDataHolder
    {
        private SavedUserData _userData;

        public SavedUserData UserData
        {
            get
            {
                return _userData;
            }
            set
            {
                _userData = value;
            }
        }

        public event Action<SavedUserData> UserDataChanged; 
     
        public SavedUserDataHolder(){}

        public SavedUserDataHolder(SavedUserData userData)
        {
            _userData = userData;
        }
        
        public void SetUserData(SavedUserData userData)
        {
            _userData = userData;
            
            UserDataChanged?.Invoke(UserData);
        }

        public void SetPhone(string phone)
        {
            _userData.Phone = phone;
            UserDataChanged?.Invoke(UserData);
        }
        
        public void SetId(int id)
        {
            _userData.Id = id;
            UserDataChanged?.Invoke(UserData);
        }
        public void SetAddress(string address)
        {
            //_userData.Address = address;
            UserDataChanged?.Invoke(UserData);
        }
        
        public void SetAddress(Result address)
        {
            _userData.SetAddress(address);
            UserDataChanged?.Invoke(UserData);
        }
        
        public void SetName(string name)
        {
            _userData.Name = name;
            UserDataChanged?.Invoke(UserData);
        }
        
        public void SuccessLogIn(LoginUserResponce user)
        {
            _userData.Id = user.Id;
            _userData.Phone = user.Phone;
            UserDataChanged?.Invoke(_userData);
        }
        
        public void GetUserIdSuccess(GetUserIdResponse user)
        {
            _userData.Id = user.Id;
            _userData.Phone = user.Phone;
            UserDataChanged?.Invoke(_userData);
        }
        
        public void SetDeviceToken(string deviceToken)
        {
            UnityEngine.Debug.LogError("SetDeviceToken: " + deviceToken);

            _userData.DeviceToken = deviceToken;
            UserDataChanged?.Invoke(_userData);
        }

        public void SetFirstTutorial(bool isShowed)
        {
            _userData.TutorWasShowed = isShowed;
            UserDataChanged?.Invoke(_userData);
        }
    }
}