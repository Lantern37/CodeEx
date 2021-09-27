using System;
using Assets.Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows.Cart
{
    public class CartItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _productText;
        [SerializeField] private TextMeshProUGUI _brandText;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _priceText;

        [SerializeField] private Image _itemImage;

        [SerializeField] private Button _increaceButton;
        [SerializeField] private Button _decreaceButton;

        public void SetItemInfo(CartItemViewData data)
        {
            _productText.text = data.ProductName;
            _brandText.text = data.BrandName;
            _priceText.text = PriceForm.GetFormatedPrice(data.Price.ToString());
            _countText.text = data.Count.ToString();
            _itemImage.sprite = data.ProductSprite;
        }

        public void SubscribeButtons(Action increace, Action decreace)
        {
            if(increace != null)
                _increaceButton.onClick.AddListener((() => increace()));
            if(decreace != null)
                _decreaceButton.onClick.AddListener((() => decreace()));
        }

        public void SetCountText(int count)
        {
            _countText.text = count.ToString();
        }

        public void SetPriceText(float price)
        {
            _priceText.text = PriceForm.GetFormatedPrice(price.ToString());
        }
        
        public void ResetView()
        {
            _increaceButton.onClick.RemoveAllListeners();
            _decreaceButton.onClick.RemoveAllListeners();
            
            _productText.text = string.Empty;
            _brandText.text = string.Empty;
            _priceText.text = string.Empty;
            _countText.text = string.Empty;
        }
    }
}