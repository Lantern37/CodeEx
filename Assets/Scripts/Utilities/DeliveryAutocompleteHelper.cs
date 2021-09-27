using System;
using System.Collections.Generic;
using DG.Tweening;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Assets.Scripts.Utilities
{
    public class DeliveryAutocompleteHelper
    {
        private IAddressRequests _requests;
        private AutoCompleteComboBox _autoComplete;
        private Tween _requestTween;
        private float _requestDelay = 0.5f;
        private bool _dontNeedRequestFlag;
        private string _lastResult;
        
        public GooglePlacesResponse Responce;

        public Result SelectedAddress;
        
        public DeliveryAutocompleteHelper(IAddressRequests requests, AutoCompleteComboBox autoComplete)
        {
            _requests = requests;
            _autoComplete = autoComplete;
            _autoComplete.OnItemSelected += AutoCompleteOnOnItemSelected;
            SelectedAddress = new Result();
            SubscribeAutocomplete();
        }

        private void AutoCompleteOnOnItemSelected(string obj)
        {
            _dontNeedRequestFlag = true;
        }

        public async void SetAutocompleteText(string address)
        {
            Responce = await _requests.GetAddresses(address);
            SelectedAddress = GetResult(address);
            
            _dontNeedRequestFlag = true;
            _autoComplete.SetText(address);
        }
        
        public void SetAutocompleteText(Result address)
        {
            //Responce = await _requests.GetAddresses(address);
            SelectedAddress = address;
            
            _dontNeedRequestFlag = true;
            _autoComplete.SetText(address.FormattedAddress);
        }
        
        private void SubscribeAutocomplete()
        {
            _autoComplete.OnSelectionChanged.AddListener(SelectionChanged);    
        }

        private void SelectionChanged(string item, bool isValid)
        {
            if (_dontNeedRequestFlag || item.Equals(_lastResult, StringComparison.OrdinalIgnoreCase))
            {
                _dontNeedRequestFlag = false;
                return;
            }
            
            if (item.Length < 3)
            {
                return;
            }

            _lastResult = item;
            
            if (Responce?.Results?.Count > 0)//isValid)
            {
                SelectedAddress = GetResult(item);
            }
            
            TweenRequest(item);
        }

        private void TweenRequest(string item)
        {
            _requestTween?.Kill();

            _requestTween = DOVirtual.DelayedCall(_requestDelay, () => Request(item));
        }

        private async void Request(string item)
        {
            Responce = await _requests.GetAddresses(item);
            
            _autoComplete.SetAvailableOptions(GetItemsList());
            _autoComplete.SetActivatePanel(true);
        }
        
        private List<string> GetItemsList()
        {
            List<string> items = new List<string>();

            foreach (var result in Responce.Results)
            {
                items.Add(result.FormattedAddress);
            }

            return items;
        }

        private Result GetResult(string item)
        {
            return Responce.Results.Find(result => result.FormattedAddress.Equals(item,StringComparison.OrdinalIgnoreCase));
        }
    }
}