using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.MainWindows.Cart
{
    public interface ICartModel
    {
        int DeliveryPrice { get; }
        event Action<float, CartItemController> PriceChanged;
        event Action<CartItemController> CounZero;
        event Action CartEmpty;
        event Action AddFirstItem;
        List<CartItemController> CartItems { get; }
        void SetConfig(CartModelConfig config);
        void CreateItem(CartItemViewData data);
        void DeleteItem(CartItemViewData data);
        void ClearCart();
    }

    public class CartModel : ICartModel
    {
        public event Action<float, CartItemController> PriceChanged;

        public event Action<CartItemController> CounZero;
        
        private int _deliveryPrice = 10;
        public int DeliveryPrice => _deliveryPrice;
        
        private List<CartItemController> _cartItems;
        public List<CartItemController> CartItems => _cartItems;
        
        public event Action CartEmpty;
        public event Action AddFirstItem;

        private CartModelConfig _config;
        
        public void SetConfig(CartModelConfig config)
        {
            _cartItems = new List<CartItemController>();
            _config = config;
        }

        public void CreateItem(CartItemViewData data)
        {
            
            if (_cartItems != null && _cartItems.Count > 0)
            {
                //CartItemController checkItem = null;

                int index = -1;
                foreach (var item in _cartItems)
                {
                    
                    if (item.Item.Model.Data.ProductName == data.ProductName)
                    {
                        //checkItem = item;
                        index = _cartItems.IndexOf(item);
                    }
                }
                
                if (index == -1)
                {
                    AddNewItem(data);
                }
                else
                {
                    _cartItems[index].Increace();
                }
            }
            else
            {
                AddNewItem(data);
            }
            
            //Debug.LogError("PRODUCT = " + data.ProductName);
        }

        private void AddNewItem(CartItemViewData cartItemViewData)
        {
            CartItem item = Object.Instantiate(_config.ItemPrefab, _config.Parent);
            
            item.Initialize();
            item.Controller.SetData(cartItemViewData);
            item.Controller.PriceChanged += OnPriceChanged;
            item.Controller.CountZero += ControllerOnCountZero;
            _cartItems.Add(item.Controller);

            if (_cartItems.Count == 1)
            {
                AddFirstItem?.Invoke();
            }
        }

        private void ControllerOnCountZero(CartItemController item)
        {
            CounZero?.Invoke(item);
            DeleteItem(item);
        }

        // public List<OrderRequestDetail> GetDetails()
        // {
        //     List<OrderRequestDetail> details = new List<OrderRequestDetail>();
        //
        //     if (_cartItems != null && _cartItems.Count > 0)
        //     {
        //         foreach (var item in _cartItems)
        //         {
        //             OrderRequestDetail det = new OrderRequestDetail()
        //             {
        //                 Product = 
        //             }
        //             details.Add(item.);
        //         }    
        //     }
        // }
        
        public void DeleteItem(CartItemViewData data)
        {
            var item = _cartItems.Find(x => x.Item.Model.Data.ProductName == data.ProductName);

            if (item == null)
                return;
         
            item.Delete();
            _cartItems.Remove(item);
        }

        public void DeleteItem(CartItemController itemController)
        {
            _cartItems.Remove(itemController);
            itemController.Delete();
            
            if (_cartItems.Count == 0)
            {
                CartEmpty?.Invoke();
            }
        }
        
        public void ClearCart()
        {
            
            for (int i =_cartItems.Count-1; i >= 0; i--)
            {
                DeleteItem(_cartItems[i]);
            }
            //
            // foreach (var item in _cartItems)
            // {
            //     Debug.Log("CARTITEM = " + item.Item.Model.Data.ProductName);
            //     DeleteItem(item.Item.Model.Data);init
            // }
        }

        private void OnPriceChanged(float price, CartItemController item)
        {
            PriceChanged?.Invoke(price, item);
        }
    }

    public class CartModelConfig
    {
        public CartItem ItemPrefab;
        public RectTransform Parent;
    }
}