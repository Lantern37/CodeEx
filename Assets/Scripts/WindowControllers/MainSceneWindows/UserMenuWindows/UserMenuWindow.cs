using System;
using Assets.Scripts.MainSceneContainer.ViewModels;
using Assets.Scripts.UserDataScripts;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class UserMenuWindow : WindowController
    {
        [SerializeField] private Button _close;

        [SerializeField] private Button _orders;
        [SerializeField] private Button _delivery;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _support;
        
        [SerializeField] private Button _logOut;

        private IUserDataController _userData;
        private UserOrders _userOrders;

        private UserIdHolder _userId;
        private SupportVM _supportVM;
        private UserDeliveryDetailsVM _userDeliveryDetails;
        public event Action LogOutEvent;

        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            
            if (_params[0] is IUserDataController)
            {
                _userData = _params[0] as IUserDataController;
            }

            if (_params[1] is UserIdHolder)
            {
                _userId = _params[1] as UserIdHolder;
            }
            
            if (_params[2] is SupportVM)
            {
                _supportVM = _params[2] as SupportVM;
            }
            
            if (_params[3] is UserDeliveryDetailsVM)
            {
                _userDeliveryDetails = _params[3] as UserDeliveryDetailsVM;
            }
           
            if (_params[4] is UserOrders)
            {
                _userOrders = _params[4] as UserOrders;
            }
            
            Manager.Show<MainWindow>();

            _close.onClick.AddListener(CloseWindow);
            
            _delivery.onClick.AddListener(DeliveryWindow);
            _orders.onClick.AddListener(Orders);
            _support.onClick.AddListener(Support);
            
            _logOut.onClick.AddListener(LogOut);
        }

        protected override void Closed()
        {
            base.Closed();

            _close.onClick.RemoveListener(CloseWindow);
            
            _delivery.onClick.RemoveListener(DeliveryWindow);
            _orders.onClick.RemoveListener(Orders);
            _support.onClick.RemoveListener(Support);
            
            _logOut.onClick.RemoveListener(LogOut);
        }

        private void Support()
        {
            Manager.Show<SupportWindow>(_supportVM);
        }

        private void DeliveryWindow()
        {
            //CloseWindow();
            Manager.Show<UserDeliveryDetailsWindow>(_userDeliveryDetails);
        }
        
        private void CloseWindow()
        {
            Manager.Close<UserMenuWindow>();
        }

        private void Orders()
        {
            _userOrders.OpenUserOrders();
        }

        private void LogOut()
        {
            _userData.DeleteUser();
            _userId.ClearUserId();
            CloseWindow();
            LogOutEvent?.Invoke();
        }
    }
}