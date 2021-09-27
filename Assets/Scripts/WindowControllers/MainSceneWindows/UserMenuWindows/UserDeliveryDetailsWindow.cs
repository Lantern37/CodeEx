using Assets.Scripts.MainSceneContainer.ViewModels;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Assets.Scripts.MainWindows
{
    public class UserDeliveryDetailsWindow : WindowController
    {
        [SerializeField] private InputField _name;

        [SerializeField] private AutoCompleteComboBox _townDropdown;

        [SerializeField] private Button _update;
        [SerializeField] private Button _back;
        [SerializeField] private Button _clear;

        public string Name => _name.text;

        public AutoCompleteComboBox TownDropdown => _townDropdown;

        private UserDeliveryDetailsVM _deliveryVM; 
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);

            UserDeliveryDetailsVM vm = _params[0] as UserDeliveryDetailsVM;

            if (vm != null)
            {
                _deliveryVM = vm;
                _deliveryVM.SetWindow(this);
            }
            
            if (_deliveryVM.UserData != null && _deliveryVM.UserData.UserData != null)
            {
                _townDropdown.SetText(_deliveryVM.UserData.UserData.FormattedAddress);
                _name.text = _deliveryVM.UserData.UserData.Name;
            }
            
            OnInputChange();
            
            Subscribe();
        }

        protected override void Closed()
        {
            UnSubscribe();
            base.Closed();
        }

        private void Subscribe()
        {
            _name.onValueChanged.AddListener(OnInputChange);
            _townDropdown.OnSelectionTextChanged.AddListener(OnInputChange);
            
            _back.onClick.AddListener(BackClick);
            _clear.onClick.AddListener(ClearClick);
            _update.onClick.AddListener(UpdateClick);
        }

        private void UnSubscribe()
        {
            _name.onValueChanged.RemoveListener(OnInputChange);
            _townDropdown.OnSelectionTextChanged.RemoveListener(OnInputChange);
            
            _back.onClick.RemoveListener(BackClick);
            _clear.onClick.RemoveListener(ClearClick);
            _update.onClick.RemoveListener(UpdateClick);
        }

        private void ClearClick()
        {
            _name.text = string.Empty;
            _townDropdown.SetText(string.Empty);
        }

        private void UpdateClick()
        {
            _deliveryVM.SaveUserData(_name.text, _deliveryVM.DeliveryAutocompleteHelper.SelectedAddress);
            Close();
        }
        
        private void BackClick()
        {
            Close();
        }
        
        private void OnInputChange(string arg = "")
        {
            bool activateNext = !_name.text.Equals(string.Empty) && !_townDropdown.Text.Equals(string.Empty);

            ActivateNextButton(activateNext);
        }
        
        private void ActivateNextButton(bool active)
        {
            _update.interactable = active;
        }
    }
}