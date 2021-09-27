using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engenious.Core.Managers.Cart.Order;
using Engenious.Core.Managers.Cart.Order.OrderDetails;
using Newtonsoft.Json;
using UnityEngine;

namespace Engenious.Core.Managers
{
    public interface INetworkCartRequests
    {
        Task<List<ProductResponse>> GetProducts(Action<float> progress = null);
        Task<OrderResponse> PostOrder(OrderRequest order, Action<float> progress = null);

        Task<List<ProductResponse>> GetFilterdProducts(string search, int[] type, int[] brand, int[] strain,
            Action<float> progress = null);

        Task<string> ConfirmOrder(int id, Action<float> progress = null);
        
        Task<List<UserOrder>> GetUserOrders(int id, Action<float> progress = null);
        
        Task<List<UserOrderDetailsResponse>> GetUserOrdersDetails(int orderId, Action<float> progress = null);
    }

    public class NetworkCartRequests : INetworkCartRequests
    {
        private NetworkManager _manager;
        
        public NetworkCartRequests(){}
        public NetworkCartRequests(NetworkManager manager)
        {
            _manager = manager;
        }

        public async Task<List<ProductResponse>> GetProducts(Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.GetProduct;
            var request = await _manager.Request<List<ProductResponse>>(requestString, 
                NetworkManager.RequestTypes.Get, 
                string.Empty, // null?
                new Dictionary<string, string>
                {
                    //если требуется можно подправить хедеры, остальное оставить как есть
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"Accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                }, false, false, progress);
            return request;
        }
        
        public async Task<OrderResponse> PostOrder(OrderRequest order, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.PostOrder;
            var body = JsonConvert.SerializeObject(order);
            Debug.Log("PostOrder = " + body);
            var request = await _manager.Request<OrderResponse>(requestString, 
                NetworkManager.RequestTypes.Post, 
                body, 
                new Dictionary<string, string>
                {
                    //если требуется можно подправить хедеры, остальное оставить как есть
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"Accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                }, false, false, progress);
            return request;
        }
        
        public async Task<List<ProductResponse>> GetFilterdProducts(string search, int[]type, int[]brand, int[]strain ,Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.GetProduct+"?";

            if(!search.Equals(string.Empty))
                requestString += "search=" + search;

            if(type !=null && type.Length > 0)
                for (int i = 0; i < type.Length; i++)
                {
                    requestString += "&type=" + type[i];
                }

            if(brand !=null && brand.Length > 0)
                for (int i = 0; i < brand.Length; i++)
                {
                    requestString += "&brand=" + brand[i];
                }
            
            if(strain !=null && strain.Length > 0)
                for (int i = 0; i < strain.Length; i++)
                {
                    requestString += "&strain=" + strain[i];
                }

            var request = await _manager.Request<List<ProductResponse>>(requestString, 
                NetworkManager.RequestTypes.Get, 
                string.Empty, // null?
                new Dictionary<string, string>
                {
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"Accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                }, false, false, progress);
            return request;
        }

        public async Task<string> ConfirmOrder(int id, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.PostOrder + "/"+id + _manager.Config.ConfirmOrder;
            var body = "{}";
            Debug.Log("ConfirmOrder = " + body);
            var request = await _manager.Request<string>(requestString, 
                NetworkManager.RequestTypes.Put, 
                body, 
                new Dictionary<string, string>
                {
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"Accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                }, false, false, progress);
            return request;
        }

        public async Task<List<UserOrder>> GetUserOrders(int id, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.GetUserOrders + "/"+id;
            var body = "{}";
            Debug.Log("ConfirmOrder = " + body);
            var request = await _manager.Request<List<UserOrder>>(requestString, 
                NetworkManager.RequestTypes.Get, 
                body, 
                new Dictionary<string, string>
                {
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"Accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                }, false, false, progress);
            return request;
        }

        public async Task<List<UserOrderDetailsResponse>> GetUserOrdersDetails(int orderId, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.GetUserOrderDetails + "/"+orderId;
            var body = "{}";
            Debug.Log("ConfirmOrder = " + body);
            var request = await _manager.Request<List<UserOrderDetailsResponse>>(requestString, 
                NetworkManager.RequestTypes.Get, 
                body, 
                new Dictionary<string, string>
                {
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"Accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                }, false, false, progress);
            return request;
        }
    }
}