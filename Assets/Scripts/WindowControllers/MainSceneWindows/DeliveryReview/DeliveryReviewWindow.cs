using System;
using System.Collections.Generic;
using Assets.Scripts.MainWindows.Cart;
using Assets.Scripts.Utilities;
using Engenious.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class DeliveryReviewWindowData
    {
       public string ExciseTaxSum;
       public string SalesTaxSum;
       public string CityTaxSum;
       public string TaxSum;
       public string TotalPrice;
       public string DeliveryPrice;
       public string ProductPrice;
       public string Name;
       public string Address;
       public string Phone;
    }

    public class DeliveryReviewWindow : WindowController
    {
        [SerializeField] private Toggle _priceToggle;
        
        [SerializeField] private TextMeshProUGUI _totalPrice;
        [SerializeField] private TextMeshProUGUI _deliveryPrice;
        [SerializeField] private TextMeshProUGUI _productPrice;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _address;
        [SerializeField] private TextMeshProUGUI _phone;

        [SerializeField] private TextMeshProUGUI _salesTaxSum;
        [SerializeField] private TextMeshProUGUI _cityTaxSum;
        [SerializeField] private TextMeshProUGUI _exciseTaxSum;
        [SerializeField] private TextMeshProUGUI _taxSum;

        [SerializeField] private Button _next;
        [SerializeField] private Button _back;

        [SerializeField] private DeliveryReviewItem _deliveryItemPrefab;
        
        [SerializeField] private RectTransform _content;

        [SerializeField] private List<RectTransform> _hideToggleObjects;

        [SerializeField] private Image _fadingToggle;
        
        private List<DeliveryReviewItem> _items;
        
        public void SubscribeButtons(Action onNext = null, Action onBack = null)
        {
            if (onNext != null)
            {
                _next.gameObject.SetActive(true);
                _next.onClick.AddListener(()=>onNext());
            }
            else
            {
                _next.gameObject.SetActive(false);
            }
            
            if(onBack !=null)
                _back.onClick.AddListener(()=>onBack());
        }

        public void SetCartData(List<DeliveryReviewItemData> cartItems, DeliveryReviewWindowData data)
        {
            _items = new List<DeliveryReviewItem>();

            foreach (var cartItem in cartItems)
            {
                var item = CreateItem(cartItem);
                _items.Add(item);
            }
            
            SetProductsText(data.ProductPrice);
            SetDeliveryText(data.DeliveryPrice);
            SetTotalText(data.TotalPrice);

            _name.text = data.Name;
            _address.text = data.Address;
            _phone.text = data.Phone;
            
            _salesTaxSum.text = PriceForm.GetFormatedPrice(data.SalesTaxSum);
            _cityTaxSum.text = PriceForm.GetFormatedPrice(data.CityTaxSum);
            _taxSum.text = PriceForm.GetFormatedPrice(data.TaxSum);
            _exciseTaxSum.text = PriceForm.GetFormatedPrice(data.ExciseTaxSum);
        }

        public void SetDeliveryText(string text)
        {
            _deliveryPrice.text =  PriceForm.GetFormatedPrice(text);
        }

        public void SetTotalText(string text)
        {
            _totalPrice.text =  PriceForm.GetFormatedPrice(text);
        }

        public void SetProductsText(string text)
        {
            _totalPrice.text =  PriceForm.GetFormatedPrice(text);
        }

        protected override void Show(params object[] _params)
        {
            _priceToggle.isOn = false;
            ToggleChanged(_priceToggle.isOn);
            _priceToggle.onValueChanged.AddListener(ToggleChanged);
            base.Show(_params);
        }

        protected override void Closed()
        {
            ResetView();
            DeleteItems();
            base.Closed();
        }

        private void ToggleChanged(bool value)
        {
            foreach (RectTransform item in _hideToggleObjects)
            {
                item.gameObject.SetActive(value);

                _fadingToggle.enabled = !value;
            }
        }

        private void ResetView()
        {
            _totalPrice.text = string.Empty;
            _deliveryPrice.text = string.Empty;
            _productPrice.text = string.Empty;
            _name.text = string.Empty;
            _address.text = string.Empty;
            _phone.text = string.Empty;
            
            _salesTaxSum.text = string.Empty;
            _cityTaxSum.text = string.Empty;
            _exciseTaxSum.text = string.Empty;
            _taxSum.text = string.Empty;
            
            _back.onClick.RemoveAllListeners();
            _next.onClick.RemoveAllListeners();
            _priceToggle.onValueChanged.RemoveAllListeners();
        }

        private void DeleteItems()
        {
            if (_items != null && _items.Count > 0)
                for (int i = _items.Count - 1; i >= 0; i--)
                {
                    _items[i].DestroyItem();
                }

            _items.Clear();
        }

        private DeliveryReviewItem CreateItem(DeliveryReviewItemData data)
        {
            var item = Instantiate(_deliveryItemPrefab, _content);
            item.SetData(data);
            return item;
        }
    }
}