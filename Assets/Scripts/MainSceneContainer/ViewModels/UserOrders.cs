using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.MainWindows;
using Assets.Scripts.MainWindows.MyOrders;
using Assets.Scripts.UserDataScripts;
using Engenious.Core.Managers;
using Engenious.Core.Managers.Cart.Order;
using Engenious.Core.Managers.Cart.Order.OrderDetails;
using Engenious.Core.WindowsController;
using Engenious.MainScene.ViewModels;

namespace Assets.Scripts.MainSceneContainer.ViewModels
{
    public class UserOrders
    {
        private INetworkCartRequests _cartRequests;
        private IUserDataController _userData;

        private WindowsManager _windowsManager;
        
        private MyOrdersWindows _userOrdersWindow;
        private DeliveryReviewWindow _deliveryReviewWindow;

        private List<UserOrder> _orderResponce;

        private Dictionary<int, string> _status = new Dictionary<int, string>();
        
        public UserOrders(INetworkCartRequests cartRequests, IUserDataController userData,
                            WindowsManager windowsManager)
        {
            _cartRequests = cartRequests;
            _userData = userData;
            _windowsManager = windowsManager;

            SetupDictionary();
        }
        
        public void OpenUserOrders()
        {
            GetUsrOrders(() =>
            {
                _userOrdersWindow = _windowsManager.Show<MyOrdersWindows>();
                _userOrdersWindow.SetCartData(GetOrderItems());
                
                _deliveryReviewWindow = _windowsManager.GetWindow<DeliveryReviewWindow>();
                _userOrdersWindow.SubscribeEvents(ItemSelected, BackClick);
            });
        }

        private void BackClick()
        {
            _windowsManager.Close<MyOrdersWindows>();
            _orderResponce = null;
        }

        private void ItemSelected(int index)
        {
            if(_orderResponce == null)
                return;

            var order = GetUserOrder(index);
            
            //Call network
            GetUsrOrdersDetails(order.Id, list =>
            {
                _deliveryReviewWindow = _windowsManager.Show<DeliveryReviewWindow>();
                _deliveryReviewWindow.SetCartData(GetDeliveryProducts(list), GetDeliveryReviewWindowData(order));
                _deliveryReviewWindow.SubscribeButtons(onBack: OnReviewBack);
            });
        }

        private void OnReviewBack()
        {
            _windowsManager.Close<DeliveryReviewWindow>();
        }
        
        private async void GetUsrOrders(Action callback = null)
        {
            _orderResponce = await _cartRequests.GetUserOrders(_userData.UserData.Id);
            if (_orderResponce != null)
            {
                callback?.Invoke();
            }
        }

        private async void GetUsrOrdersDetails(int orderId, Action<List<UserOrderDetailsResponse>> callback = null)
        {
            var responce = await _cartRequests.GetUserOrdersDetails(orderId);
            if (responce != null)
            {
                callback?.Invoke(responce);
            }
        }
        
        private UserOrder GetUserOrder(int index)
        {
            return  _orderResponce[index];
        }
        
        private DeliveryReviewWindowData GetDeliveryReviewWindowData(UserOrder userOrder)
        {
            DeliveryReviewWindowData data = new DeliveryReviewWindowData();

            var userData = _userData.UserData;

            data.Address = userOrder.AddressLine1;
            data.Name = userOrder.Name;
            data.Phone = userData.Phone;
            data.DeliveryPrice = GetDelivery(userOrder).ToString();
            data.ProductPrice = GetItemsPrice(userOrder).ToString();
            data.TotalPrice = GetTotalPrice(userOrder).ToString();
            data.TaxSum = userOrder.TaxSum.ToString();
            data.CityTaxSum = userOrder.CityTaxSum.ToString();
            data.ExciseTaxSum = userOrder.ExciseTaxSum.ToString();
            data.SalesTaxSum = userOrder.SalesTaxSum.ToString();
            
            return data;
        }

        private List<DeliveryReviewItemData> GetDeliveryProducts(List<UserOrderDetailsResponse> order)
        {
            List<DeliveryReviewItemData> orderDetails = new List<DeliveryReviewItemData>();

            if (order != null && order.Count > 0)
            {
                foreach (var item in order)
                {
                    DeliveryReviewItemData det = new DeliveryReviewItemData()
                    {
                        Count = item.Quantity,
                        Name = item.Product.Name,
                        Price = (float)item.Product.Price,
                    };
                        
                    orderDetails.Add(det);
                }
            }

            return orderDetails;
        }

        private List<MyOrderItemViewData> GetOrderItems()
        {
            List<MyOrderItemViewData> orderDetails = new List<MyOrderItemViewData>();

            foreach (var item in _orderResponce)
            {
                MyOrderItemViewData det = new MyOrderItemViewData()
                {
                    Status = GetStatus(item.State),
                    OrderId = item.Id.ToString(),
                    Price = item.TotalSum.ToString(),
                    Date = item.CreatedAt
                };

                orderDetails.Add(det);
            }

            return orderDetails;
        }

        private void SetupDictionary()
        {
            _status = new Dictionary<int, string>()
            {
                {0, "Drafted"},
                {1, "Created"},
                {2, "Assigned"},
                {3, "Rejected"},
                {4, "Accepted"},
                {5, "Packed"},
                {6, "In delivery"},
                {7, "Delivered"},
                {8, "Complete"},
                {9, "Returned"},
                {10, "Cancelled"}
            };
        }

        private string GetStatus(int index)
        {
            return _status[index];
        }
        
        private float GetItemsPrice(List<UserOrderDetailsResponse> order)
        {
            float total = 0;

            if (order != null && order.Count > 0)
            {
                for (int i = 0; i < order.Count; i++)
                {
                    total +=  (float)order[i].Product.Price * order.Count;
                }
            }

            return total;
        }

        private float GetItemsPrice(UserOrder userOrder)
        {
            float total = (float)userOrder.TotalSum - (float)userOrder.DeliverySum;

            return total;
        }
        
        private float GetTotalPrice(UserOrder userOrder)
        {
            return (float)userOrder.TotalSum;
        }

        private float GetDelivery(UserOrder userOrder)
        {
            return (float)userOrder.DeliverySum;
        }

        // private void OnAddFirstItem()
        // {
        //     _window.SetNotEmptyCart();
        // }
    }
}