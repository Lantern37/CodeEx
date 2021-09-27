using System;

namespace Assets.Scripts.MainWindows.Cart
{
    public class CartItemModel
    {
        private CartItemViewData _data;
        public CartItemViewData Data => _data;
        
        public void SetData(CartItemViewData data)
        {
            _data = data;
        }

        public void IncreaceCount(Action<int> callback = null)
        {
            _data.Count++;
            
            callback?.Invoke(_data.Count);
        }
        
        public void DecreaceCount(Action<int> callback = null)
        {
            _data.Count--;
            
            callback?.Invoke(_data.Count);
        }
        
        public float GetPrice()
        {
            return _data.Price * _data.Count;
        }
    }
}