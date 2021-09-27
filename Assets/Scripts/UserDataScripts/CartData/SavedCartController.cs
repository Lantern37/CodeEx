using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.MainWindows.Cart;
using UnityEngine;

namespace Assets.Scripts.UserDataScripts
{
    public interface ISavedCartController
    {
        SavedCartData CartData { get; }
        event Action<SavedCartData> SuccessDataLoaded;
        event Action FailDataLoad;
        void Initialize();
        // void AddItem(SavedCartDataItem item);
        // void RemoveItem(SavedCartDataItem item);
        // void RemoveItem(CartItemViewData item);
        void ClearCartItems();
        void Save(List<CartItemViewData> cartViewData);
    }

    public class SavedCartController : ISavedCartController
    {
        private ICartDataLoader _cartDataLoader;

        public SavedCartData CartData { get; private set; }

        public event Action<SavedCartData> SuccessDataLoaded;
        public event Action FailDataLoad;
        
        //private List<SavedCartDataItem> _items;
        
        public void Initialize()
        {
            //_items = new List<SavedCartDataItem>();
            
            CartData = new SavedCartData();

            _cartDataLoader = new CartDataLoader(CartData);

            _cartDataLoader.Load(SuccessLoadData, ReadFail);
        }

        // public void AddItem(SavedCartDataItem item)
        // {
        //     _items.Add(item);
        //     
        //     Save();
        // }
        //
        // public void RemoveItem(CartItemViewData item)
        // {
        //     foreach (var dataItem in _items)
        //     {
        //         if (dataItem.Id == item.ProductId)
        //         {
        //             _items.Remove(dataItem);
        //             break;
        //         }
        //     }
        //     
        //     Save();
        // }

        public void RemoveItem(SavedCartDataItem item)
        {
            //_items.Remove(item);
        }
        
        public void ClearCartItems()
        {
            //_items.Clear();
            CartData.ClearItems();
            _cartDataLoader.DeleteFile();
        }

        public void Save(List<CartItemViewData> cartViewData)
        {
            CartData.SetCartItems(cartViewData);

            _cartDataLoader.Write();

            //Debug.Log("Save Cart data = " + CartData.CartItems.Length);
        }

        private void ReadFail()
        {
            //FailDataLoad?.Invoke();
        }

        private void SuccessLoadData(SavedCartData data)
        {
            CartData = data;
            //_items = data.CartItems.ToList();
            //SuccessDataLoaded?.Invoke(UserData);
            // foreach (var card in CartData.CartItems)
            // {
            //     Debug.LogError("Load Cart data = " + card.ProductName);
            // }
        }
    }
}