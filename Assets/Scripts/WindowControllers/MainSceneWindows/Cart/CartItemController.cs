using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.MainWindows.Cart
{
    public class CartItemController
    {
        private  CartItem _item;
        public  CartItem Item => _item;

        public event Action<float,CartItemController> PriceChanged;

        public event Action<CartItemController> CountZero;
        public CartItemController(CartItem item)
        {
            _item = item;
            _item.View.SubscribeButtons(Increace, Decreace);
        }

        public void SetData(CartItemViewData data)
        {
            _item.Model.SetData(data);
            _item.View.SetItemInfo(data);
        }

        public void Delete()
        {
            Close();
            Object.DestroyImmediate(_item.gameObject);
        }

        private void Close()
        {
            PriceChanged = null;
            CountZero = null;
            
            //_item.View.ResetView();
        }

        public void Increace()
        {
            _item.Model.IncreaceCount(UpdateCount);
        }

        public void Decreace()
        {
            _item.Model.DecreaceCount(UpdateCount);
        }

        private void UpdateCount(int count)
        {
            _item.View.SetCountText(count);
            _item.View.SetPriceText(_item.Model.GetPrice());

            PriceChanged?.Invoke(_item.Model.GetPrice(), this);
            
            if (_item.Model.Data.Count == 0)
            {
                CountZero?.Invoke(this);
            }
        }

        public override string ToString()
        {
            return "Name " + _item.Model.Data.ProductName + " count " + _item.Model.Data.Count;
        }
    }
}