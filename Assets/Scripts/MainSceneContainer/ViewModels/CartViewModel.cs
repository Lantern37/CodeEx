using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.UserDataScripts;
using Assets.Scripts.Utilities;
using Engenious.Core.Managers;
using Engenious.MainScene.ViewModels;
using UnityEngine;

namespace Assets.Scripts.MainWindows.Cart
{
    public class CartViewModel : BaseVM<CartWindow>
    {
        private ICartModel _model;
        private INetworkCartRequests _cartRequests;
        private ILoadCartIcon _iconLoader;
        private ISavedCartController _savedCartController;
        public CartViewModel(){}

        public List<ProductResponse> Products;
        
        public CartViewModel(ICartModel model, INetworkCartRequests cartRequests, ILoadCartIcon iconLoader,ISavedCartController savedCartController)
        {
            _model = model;
            _cartRequests = cartRequests;
            _iconLoader = iconLoader;
            _savedCartController = savedCartController;
        }

        private void ModelOnCounZero(CartItemController item)
        {
            UpdateWindow();
        }

        /// <summary>
        /// GeTitemsInfo for items in scene
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductResponse>> GetProducts()
        {
            Products = await _cartRequests.GetProducts();
            return Products;
        }

        protected override void SetupWindow()
        {
            base.SetupWindow();
            _model.PriceChanged += ModelOnPriceChanged;
            _model.CounZero += ModelOnCounZero;
            _model.CartEmpty += OnClearCart;
            _model.AddFirstItem += OnAddFirstItem;

            //todo add check load cart items
            _window.SetEmptyCart();

            CartModelConfig config = new CartModelConfig()
            {
                ItemPrefab = _window.CartItemPrefab,
                Parent = _window.Content
            };
            _model.SetConfig(config);

            _window.SubscribeClearCart(ClearCart);

            if (_savedCartController.CartData.CartItems != null)
            {
                foreach (var item in _savedCartController.CartData.CartItems)
                {
                    CreateCartSavedItem(item);
                }
            }
        }

        public override void UpdateWindow()
        {
            base.UpdateWindow();
            _window.SetDeliveryText(GetDelivery().ToString());
            _window.SetTotalText(GetTotalPrice().ToString());
        }

        /// <summary>
        /// For delivery manager request
        /// </summary>
        /// <returns></returns>
        public List<OrderRequestDetail> GetOrderDetailsRequest()
        {
            List<OrderRequestDetail> orderDetails = new List<OrderRequestDetail>();

            if (_model.CartItems != null && _model.CartItems.Count > 0)
            {
                foreach (var item in _model.CartItems)
                {
                    OrderRequestDetail det = new OrderRequestDetail()
                    {
                        Product = item.Item.Model.Data.ProductId,
                        Quantity = item.Item.Model.Data.Count
                    };
                        
                    orderDetails.Add(det);
                }
            }

            return orderDetails;
        }
        
        public List<DeliveryReviewItemData> GetDeliveryProducts()
        {
            List<DeliveryReviewItemData> orderDetails = new List<DeliveryReviewItemData>();

            if (_model.CartItems != null && _model.CartItems.Count > 0)
            {
                foreach (var item in _model.CartItems)
                {
                    var itemData = item.Item.Model.Data;
                    DeliveryReviewItemData det = new DeliveryReviewItemData()
                    {
                        Count = itemData.Count,
                        Name = itemData.ProductName,
                        Price = itemData.Price,
                    };
                        
                    orderDetails.Add(det);
                }
            }

            return orderDetails;
        }
        
        public Task<List<ProductResponse>> GetFilterdRequest(FilterWindowData data)
        {
            return _cartRequests.GetFilterdProducts(data.Search,data.Types.ToArray(), data.Brands.ToArray(), data.Strain.ToArray());
        }
        
        private void ModelOnPriceChanged(float price, CartItemController item)
        {
            UpdateWindow();
        }

        /// <summary>
        /// For ProductDescriptionWindow.AddToCart += () => 
        /// </summary>
        /// <param name="data"></param>
        public void CreateItemResponce(ProductResponse data)
        {
            GetCartItemViewData(data, viewData =>CreateItem(viewData) );

            // SavedCartDataItem savedItem = new SavedCartDataItem();
            // savedItem.SetData(data);
            
            //_savedCartController.AddItem(savedItem);
        }

        public void CreateCartSavedItem(SavedCartDataItem data)
        {
            GetCartSavedItem(data, viewData => CreateItem(viewData));
        }
        
        private void GetCartItemViewData(ProductResponse data, Action<CartItemViewData> returnData = null)
        {
            CartItemViewData viewData = new CartItemViewData();
            viewData.Count = 1;
            viewData.Price = (float)data.Price;
            viewData.BrandName = data.Brand.Name;
            viewData.ProductName = data.Name;
            viewData.ProductId = data.Id;
            viewData.ImageLink = data.ImageLink;
            
            _iconLoader.GetSprite(data.ImageLink, sprite =>
            {
                viewData.ProductSprite = sprite;
                returnData?.Invoke(viewData);
            });
        }
        
        private void GetCartSavedItem(SavedCartDataItem data, Action<CartItemViewData> returnData = null)
        {
            CartItemViewData viewData = new CartItemViewData();
            viewData.Count = data.Count;
            viewData.Price = (float)data.Price;
            viewData.BrandName = data.BrandName;
            viewData.ProductName = data.ProductName;
            viewData.ProductId = data.Id;
            viewData.ImageLink = data.ImageLink;
            
            _iconLoader.GetSprite(data.ImageLink, sprite =>
            {
                viewData.ProductSprite = sprite;
                returnData?.Invoke(viewData);
            });
        }

        public void CreateItem(CartItemViewData data)
        {
            _model.CreateItem(data);
        }

        public void DeleteItem(CartItemViewData data)
        {
            _model.DeleteItem(data);
            
            //_savedCartController.RemoveItem(data);
        }
        
        public void ClearCart()
        {
            _model.ClearCart();
            
            OnClearCart();
            
            _savedCartController.ClearCartItems();
        }

        public float GetItemsPrice()
        {
            float total = 0;

            if (_model.CartItems != null && _model.CartItems.Count > 0)
            {
                // foreach (var item in _model.CartItems)
                // {
                //     total += item.Item.Model.GetPrice();
                // }

                for (int i = 0; i < _model.CartItems.Count; i++)
                {
                    total +=  _model.CartItems[i].Item.Model.GetPrice();
                }
            }

            return total;
        }

        public float GetTotalPrice()
        {
            return GetItemsPrice() + _model.DeliveryPrice;
        }

        public float GetDelivery()
        {
            return _model.DeliveryPrice;
        }

        public void SaveCart()
        {
            var cartItemData = new List<CartItemViewData>();

            foreach (var cartItem in _model.CartItems)
            {
                cartItemData.Add(cartItem.Item.Model.Data);
            }
            
            _savedCartController.Save(cartItemData);
        }

        private void OnAddFirstItem()
        {
            _window.SetNotEmptyCart();
        }
        
        private void OnClearCart()
        {
            _window.SetEmptyCart();
        }
        
        public void DebugLogCartItems()
        {
            foreach (var item in _model.CartItems)
            {
                Debug.Log("Item " + item.ToString());
            }
        }
    }
}