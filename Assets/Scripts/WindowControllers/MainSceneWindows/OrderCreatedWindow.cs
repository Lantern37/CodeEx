using System;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class OrderCreatedWindow : WindowController
    {
        [SerializeField] private Button _okButton;

        [SerializeField] private Toggle _saveAddress;

        private event Action OnClick;
        public void SubscribeIsOnSaveAddress(Action onClick)
        {
            OnClick += onClick;
        }
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            _okButton.onClick.AddListener(() => Manager.Close<OrderCreatedWindow>());
            _saveAddress.isOn = false;
        }

        protected override void Closed()
        {
            base.Closed();
            _okButton.onClick.RemoveAllListeners();
            
            if (_saveAddress.isOn)
                OnClick?.Invoke();

            OnClick = null;
        }
    }
}