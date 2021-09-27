using System;
using Engenious.Core.Managers;
using Engenious.WindowControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Assets.Scripts.MainWindows
{
    public class DeliveryWindow : WindowController
    {
        [SerializeField] private InputField _name;
        public InputField NameInput => _name;

        
        // [SerializeField] private InputField _town;
        //
        // [SerializeField] private InputField _addressLine1;
        // [SerializeField] private InputField _addressLine2;
        // [SerializeField] private InputField _zipCode;

        [SerializeField] private AutoCompleteComboBox _townDropdown;

        [SerializeField] private Button _makeOrder;
        [SerializeField] private Button _back;

        public string Name => _name.text;
        // public string Town => _town.text;
        // public string AddressLine1 => _addressLine1.text;
        // public string AddressLine2 => _addressLine2.text;
        // public string ZipCode => _zipCode.text;

        public AutoCompleteComboBox TownDropdown => _townDropdown;

        public event Action AfterShow;
        
        public void SubscribeButtons(Action makeOrder, Action back = null)
        {
            if(makeOrder != null)
                _makeOrder.onClick.AddListener((() => makeOrder()));
            
            if(back != null)
                _back.onClick.AddListener((() => back()));
        }
        
        public void SubscribeBackButtons( Action back = null)
        {
            if(back != null)
                _back.onClick.AddListener((() => back()));
        }
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            //
            // _name.Open();
            // _town.Open();
            // _addressLine1.Open();
            // _addressLine2.Open();
            // _zipCode.Open();
            //
            ClearInputFields();
            
            AfterShow?.Invoke();
        }

        private void ClearInputFields()
        {
            _name.text = string.Empty;
            // _town.text = string.Empty;
            // _addressLine1.text = string.Empty;
            // _addressLine2.text = string.Empty;
            //_zipCode.text = string.Empty;

            _townDropdown.ResetItems();
        }

        protected override void Closed()
        {
            base.Closed();

            // _name.Close();
            // _town.Close();
            // _addressLine1.Close();
            // _addressLine2.Close();
            // _zipCode.Close();
            //ClearInputFields();

            _back.onClick.RemoveAllListeners();
            _makeOrder.onClick.RemoveAllListeners();
        }
    }
}