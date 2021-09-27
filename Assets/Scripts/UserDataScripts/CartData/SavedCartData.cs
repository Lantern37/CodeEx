using System;
using System.Collections.Generic;
using Assets.Scripts.MainWindows.Cart;

namespace Assets.Scripts.UserDataScripts
{
    [Serializable]
    public class SavedCartData
    {
        public SavedCartDataItem[] CartItems;

        public void SetCartItems(List<SavedCartDataItem> items)
        {
            CartItems = items.ToArray();
        }

        public void SetCartItems(List<CartItemViewData> items)
        {
            List<SavedCartDataItem> tempItems = new List<SavedCartDataItem>();
            
            foreach (var itemViewData in items)
            {
                var cartItem = new SavedCartDataItem();
                cartItem.SetData(itemViewData);
                tempItems.Add(cartItem);
            }

            CartItems = tempItems.ToArray();
        }
        
        public void ClearItems()
        {
            CartItems = null;
        }
    }
}