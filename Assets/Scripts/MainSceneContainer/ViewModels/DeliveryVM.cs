using System;
using Assets.Scripts.MainWindows;
using Assets.Scripts.MainWindows.Cart;
using Assets.Scripts.UserDataScripts;
using Assets.Scripts.Utilities;
using Engenious.Core.Managers;
using Engenious.MainScene.Delivery;
using Engenious.MainScene.Models;
using Engenious.MainScene.ViewModels;
using UnityEngine;

namespace Assets.Scripts.MainSceneContainer.ViewModels
{
    public class DeliveryVM : BaseVM<DeliveryWindow>
    {
        public AddressLineObject AddressLineObject { get; private set; }
        public event Action OrderSuccess;

        private OrderResponse _responce;
        
        private INetworkCartRequests _cartRequests;
        private IAddressRequests _addressRequests;

        //private LogInVM _logInModel;
        private IUserDataController _userData;

        private DeliveryAutocompleteHelper _deliveryAutocompleteHelper; 
        
        private CartViewModel _cartVM;
        public DeliveryVM(){}
        
        public DeliveryVM(INetworkCartRequests cartRequests, IAddressRequests addressRequests ,CartViewModel cartModel, IUserDataController userData)
        {
            _cartRequests = cartRequests;
            _addressRequests = addressRequests;
            _cartVM = cartModel;
            _userData = userData;

        }

        public void SetAutocompleteText(string address)
        {
            _deliveryAutocompleteHelper.SetAutocompleteText(address);
        }
        
        public void SetAutocompleteText(Result address)
        {
            _deliveryAutocompleteHelper.SetAutocompleteText(address);
        }
        protected override void SetupWindow()
        {
            base.SetupWindow();
            _deliveryAutocompleteHelper = new DeliveryAutocompleteHelper(_addressRequests , _window.TownDropdown);

            _window.AfterShow += OnShowed;
        }

        private void OnShowed()
        {
            //_window.AfterShow -= OnShowed;

            // if (_userData.UserData != null 
            //     && !_userData.UserData.Address.Equals(string.Empty))
            // {
                SetAutocompleteText(_userData.UserData.GetResultAddress());
                _window.NameInput.text = _userData.UserData.Name;
                //}
        }
        
        public async void OnMakeOrder()
        {
            var userData = _userData.UserData;

            AddressLineObject address = GetAddressLine(_deliveryAutocompleteHelper.SelectedAddress.FormattedAddress);


            OrderRequest order = new OrderRequest()
            {
                Name = _window.Name,
                Phone = userData.Phone,
                City = address.City,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = String.Empty,
                ZipCode = address.ZipCode,
                User = userData.Id,
                Sum = _cartVM.GetItemsPrice(),
                DeliverySum = _cartVM.GetDelivery(),
                TotalSum = _cartVM.GetTotalPrice(),
                LatitudeCoordinate = _deliveryAutocompleteHelper.SelectedAddress.Geometry.Location.Lat,
                LongitudeCoordinate = _deliveryAutocompleteHelper.SelectedAddress.Geometry.Location.Lng,
                Details = _cartVM.GetOrderDetailsRequest()
            };

            Debug.LogError("address = "  + (order.LatitudeCoordinate + " " + order.LongitudeCoordinate));

            _responce = await _cartRequests.PostOrder(order);

            OrderSuccess?.Invoke();
        }

        public async void ConfirmOrder(Action success = null)
        {
            await _cartRequests.ConfirmOrder(_responce.Id);
            success?.Invoke();
        }
        
        public DeliveryReviewWindowData GetDeliveryReviewWindowData()
        {
            DeliveryReviewWindowData data = new DeliveryReviewWindowData();

            var userData = _userData.UserData;

            data.Address = _deliveryAutocompleteHelper.SelectedAddress.FormattedAddress;//_window.AddressLine1 + "\n" + _window.AddressLine2;
            data.Name = _window.Name;
            data.Phone = userData.Phone;
            data.DeliveryPrice = _cartVM.GetDelivery().ToString();
            data.ProductPrice = _cartVM.GetItemsPrice().ToString();
            data.TotalPrice = _cartVM.GetTotalPrice().ToString();
            data.TaxSum = _responce.TaxSum.ToString();
            data.CityTaxSum = _responce.CityTaxSum.ToString();
            data.ExciseTaxSum = _responce.ExciseTaxSum.ToString();
            data.SalesTaxSum = _responce.SalesTaxSum.ToString();
            
            return data;
        }

        public string GetFormatedAddresse()
        {
            return _deliveryAutocompleteHelper.SelectedAddress.FormattedAddress;
        }

        public Result GetResultAddress()
        {
            return _deliveryAutocompleteHelper.SelectedAddress;
        }
        
        private AddressLineObject GetAddressLine(string formattedAddress)
        {
            string[] splitedAddress = formattedAddress.Split(',');

            AddressLineObject address = new AddressLineObject()
            {
                AddressLine1 = splitedAddress[0],
                City = splitedAddress[1],
                ZipCode = splitedAddress[2],
                State = splitedAddress[3]
            };

            return address;
        }
    }

    public class AddressLineObject
    {
        public string AddressLine1;
        public string City;
        public string ZipCode;
        public string State;
    }
}