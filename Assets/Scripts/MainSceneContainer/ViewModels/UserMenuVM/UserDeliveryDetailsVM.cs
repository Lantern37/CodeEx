using System;
using Assets.Scripts.MainWindows;
using Assets.Scripts.UserDataScripts;
using Assets.Scripts.Utilities;
using Engenious.Core.Managers;
using Engenious.MainScene.ViewModels;

namespace Assets.Scripts.MainSceneContainer.ViewModels
{
    public class UserDeliveryDetailsVM : BaseVM<UserDeliveryDetailsWindow>
    {
        private IUserDataController _userData;
        public IUserDataController UserData => _userData;

        private DeliveryAutocompleteHelper _deliveryAutocompleteHelper;
        public DeliveryAutocompleteHelper DeliveryAutocompleteHelper => _deliveryAutocompleteHelper;
        
        private IAddressRequests _addressRequests;

        public UserDeliveryDetailsVM(IUserDataController userData, IAddressRequests addressRequests)
        {
            _userData = userData;
            _addressRequests = addressRequests;
        }
        
        protected override void SetupWindow()
        {
            base.SetupWindow();
            _deliveryAutocompleteHelper = new DeliveryAutocompleteHelper(_addressRequests , _window.TownDropdown);
        }
        
        public string GetFormattedAddress()
        {
            return _deliveryAutocompleteHelper.SelectedAddress.FormattedAddress;
        }
        
        public void SetAutocompleteText(string address)
        {
            _deliveryAutocompleteHelper.SetAutocompleteText(address);
        }

        public void SaveUserData(string name, string address)
        {
            _userData.SetNameAndAddress(name, address);
        }
        
        public void SaveUserData(string name, Result address)
        {
            //_userData.SetNameAndAddress(name, (string) address);
            _userData.SetNameAndAddress(name, address);
        }
        
        private AddressLineObject GetAddressLine(string formattedAddress, Action<AddressLineObject> success = null, Action fail = null)
        {
            string[] splitedAddress = formattedAddress.Split(',');

            if (splitedAddress.Length < 4)
            {
                fail?.Invoke();
                return null;
            }
            
            AddressLineObject address = new AddressLineObject()
            {
                AddressLine1 = splitedAddress[0],
                City = splitedAddress[1],
                ZipCode = splitedAddress[2],
                State = splitedAddress[3]
            };

            success?.Invoke(address);
            return address;
        }
        
        
    }
}