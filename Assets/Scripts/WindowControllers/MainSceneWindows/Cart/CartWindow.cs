using System;
using Assets.Scripts.Utilities;
using Engenious.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows.Cart
{
    public class CartWindow : WindowController
    {
        [SerializeField] private ClearCartPopup _clearCartPopup;

        [SerializeField] private RectTransform _emptyStateCart;
        [SerializeField] private RectTransform _notEmptyStateCart;

        [SerializeField] private TextMeshProUGUI _totalPrice;
        [SerializeField] private TextMeshProUGUI _deliveryPrice;

        [SerializeField] private Button _next;
        [SerializeField] private Button _back;
        [SerializeField] private Button _clear;

        [SerializeField] private CartItem _cartItemPrefab;
        public CartItem CartItemPrefab => _cartItemPrefab;
        
        [SerializeField] private RectTransform _content;
        public RectTransform Content => _content;

        public void SubscribeButtons(Action onNext, Action onBack)
        {
            if(onNext!=null)
                _next.onClick.AddListener(()=>onNext());
            
            if(onBack !=null)
                _back.onClick.AddListener(()=>onBack());
        }

        public void SetDeliveryText(string text)
        {
            _deliveryPrice.text =  PriceForm.GetFormatedPrice(text);
        }

        public void SetTotalText(string text)
        {
            _totalPrice.text =  PriceForm.GetFormatedPrice(text);
        }

        public void ShowClearCartPopup()
        {
            _clearCartPopup.Show();
        }

        public void SubscribeClearCart(Action onClear)
        {
            _clearCartPopup.SubscribeClearButton(onClear);
        }

        public void SetEmptyCart()
        {
            _notEmptyStateCart.gameObject.SetActive(false);
            _emptyStateCart.gameObject.SetActive(true);
        }

        public void SetNotEmptyCart()
        {
            _notEmptyStateCart.gameObject.SetActive(true);
            _emptyStateCart.gameObject.SetActive(false);
        }

        private void SubscribeClearButtons(Action onClear)
        {
            if(onClear !=null)
                _clear.onClick.AddListener(()=>onClear());
        }

        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            
            SubscribeClearButtons(ShowClearCartPopup);
        }

        protected override void Closed()
        {
            base.Closed();
            
            _next.onClick.RemoveAllListeners();
            _back.onClick.RemoveAllListeners();
            _clear.onClick.RemoveAllListeners();
        }

        private void ResetView()
        {
            _totalPrice.text = string.Empty;
            _deliveryPrice.text = string.Empty;

            _next.onClick.RemoveAllListeners();
        }
    }
}