using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Engenious.Core.Managers.Requests;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Engenious.Core.Managers
{
    public interface INetworkManager
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        NetworkConfig Config { get; set; }

        [Inject] 
        INetworkChecker Checker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        NetworkType ConnectionType { get; }

        AccessTokenHolder AccessTokenHolder { get; }

        UserIdHolder UserIdHolder { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// 
        /// </summary>
        event Action<NetworkType> ConnectionStateChange;

        /// <summary>
        /// 
        /// </summary>
        event Action OnConnect;

        /// <summary>
        /// 
        /// </summary>
        event Action OnDisconnect;

        INetworkUserRequests NetworkUserRequests { get; }
        
        INetworkCartRequests NetworkCartRequests { get; }
        IAddressRequests AddressRequests { get; }
        ISupportNetwork SupportNetwork { get; }
        /// <summary>
        /// Services
        /// </summary>
        Task Initialize();

        /// <summary>
        /// 
        /// </summary>
        void Update();

        /// <summary>
        /// 
        /// </summary>
        void ClearConnectManager();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
         UniTask<UnityWebRequest> GetResponse(UnityWebRequest req, Action<float> progress = null);
        
        // Sing in
        Task GuestSingIn(Action<float> progress = null);
        Task<GetUserResponce> GetUser(GetUserRequest user, Action<float> progress=null);
        Task<LoginUserResponce> LoginUser(LoginUserRequest user, Action<float> progress=null);

        Task<string> PutUser(PutUserRequest user, Action<float> progress=null);
        bool IsSuccessfull(UnityWebRequest request);
    }

    /// <summary>
    /// 
    /// </summary>
    public class NetworkManager : INetworkManager 
    {
        public enum RequestTypes
        {
            Get, Post, Put, Delete 
        }

        private string m_tempToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InVuc3VpZ29AZ21haWwuY29tIiwidXNlcklkIjozOSwicm9sZSI6eyJpZCI6NCwiY29kZSI6IjIiLCJuYW1lIjoidXNlciJ9LCJpYXQiOjE2MTg5MTc2MjQsImV4cCI6MTYyMTMzNjgyNH0.zBuWR5vKMTEwuN0-NZEIuUWl_A3OdAO39w1ro0oEEmo";
            
        [Inject] 
        public NetworkConfig Config { get; set; }

        [Inject]
        public INetworkChecker Checker { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public NetworkType ConnectionType => Checker.Type;

        public AccessTokenHolder AccessTokenHolder { get; private set; }
        public UserIdHolder UserIdHolder { get; private set;}

        public INetworkUserRequests NetworkUserRequests { get; private set; }
        public INetworkCartRequests NetworkCartRequests { get; private set; }

        public IAddressRequests AddressRequests { get; private set; }
        public ISupportNetwork SupportNetwork { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public event Action<NetworkType> ConnectionStateChange;

        /// <summary>
        /// 
        /// </summary>
        public event Action OnConnect;

        /// <summary>
        /// 
        /// </summary>
        public event Action OnDisconnect;

        /// <summary>
        /// 
        /// </summary>
        private bool _connectionStateCalled;

        /// <summary>
        /// 
        /// </summary>
        private NetworkType _initialConnection;
        
        public async Task Initialize()
        {
            IsInitialized = true;
            _initialConnection = NetworkType.NetUnknown;

            //await Checker.Initialize();

            NetworkUserRequests = new NetworkUserRequests(this);

            NetworkCartRequests = new NetworkCartRequests(this);

            AddressRequests = new AddressRequests(this);

            SupportNetwork = new SupportNetwork(this);
            
            AccessTokenHolder = new AccessTokenHolder();

            UserIdHolder = new UserIdHolder();

            //ChechState();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ChechState()
        {
            if (Checker.Type == NetworkType.Disable)
            {
                if (_initialConnection != NetworkType.Disable)
                {
                    _initialConnection = NetworkType.Disable;

                    ConnectionStateChange?.Invoke(Checker.Type);
                    OnDisconnect?.Invoke();
                    OnDisconnect = null;
                }
            }
            else
            {
                if (_initialConnection == NetworkType.Disable || _initialConnection == NetworkType.NetUnknown)
                {
                    _initialConnection = Checker.Type;
                    ConnectionStateChange?.Invoke(Checker.Type);
                    OnConnect?.Invoke();
                    OnConnect = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            //Checker?.Update();
            //ChechState();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearConnectManager()
        {
            OnConnect = null;
            OnDisconnect = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task GuestSingIn(Action<float> progress = null)
        {
            //if (singInResult == null)
            {
                if (Checker.Type == NetworkType.Disable)
                {
                    throw new NetworkRequestException("Internal error", "No internet available ...");
                }

                var nonce = ((int) ((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds)).ToString();
                
                // Test device
                // var deviceId = "3a743fc0f670270df9a247c22374fb8cb640dc5e";
                
                var deviceId = SystemInfo.deviceUniqueIdentifier;
                var hmac = NetworkHelper.GenerateHMAC(Config.GuestKey, deviceId, nonce);
                var requestString = Config.BaseURL + Config.AuthorizePrefix +
                                    $"guest?device_id={deviceId}&nonce={nonce}&sign={hmac}";
                //singInResult = await Request<SelfResponse>(requestString, RequestTypes.Post, "", null, true,false, progress); 
            }
        }

        /// <summary>
        /// Get user from server by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User data</returns>
        public async Task<GetUserResponce> GetUser(GetUserRequest user, Action<float> progress = null)
        {
            m_tempToken = LocalSettings.CurrentToken;
            var requestString = Config.BaseURL + Config.GetUser;
            // var body = JsonConvert.SerializeObject(user);
            var body = "";
            var request = await Request<GetUserResponce>(requestString, 
                RequestTypes.Get, body, 
                new Dictionary<string, string>
            {
                {"accept", "application/json"},
                {"Authorization", "Bearer " + m_tempToken},  // Added by Alex
            }, false, false, progress);
            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<string> PutUser(PutUserRequest user, Action<float> progress = null)
        {
            var requestString = Config.BaseURL + Config.PutUser;
            var body = JsonConvert.SerializeObject(user);
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(body);
            string utf555 = Encoding.UTF8.GetString(utf8Bytes);
            Debug.Log("body = "+ body);
            var request = await Request<string>(requestString, RequestTypes.Post, body, new Dictionary<string, string>
            {
                {"Content-Type", "application/json"},
                //{"Content-type", "charset=utf-8"}
            }, false, false, progress);
            
            Debug.Log("PutUser request done ");

            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<LoginUserResponce> LoginUser(LoginUserRequest user, Action<float> progress=null)
        {
            var requestString = Config.BaseURL + Config.PostLogin;
            var body = JsonConvert.SerializeObject(user);
            Debug.Log("Body Login " + body);
            
            //var bodyString = "{\"email\":\"myTest@eng.ua\",\"password\":\"test\"}";
            Debug.Log("body login= "+ body.ToString());
            var request = await Request<LoginUserResponce>(requestString, 
                RequestTypes.Post, 
                body, 
                new Dictionary<string, string>
            {
                {"Content-Type", "application/json; charset=utf-8"},
                {"Accept", "application/json"}
            }, false, false, progress);
            
            return request;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private async void RefreshToken()
        {
            // if (Checker.Type == NetworkType.Disable)
            // {
            //     throw new NetworkRequestException("Internal error", "No internet available ...");
            // }
            //
            // if (singInResult == null)
            // {
            //     throw new NetworkRequestException("Internal", "No connection found ...");
            // }
            //
            // var requestString = Config.BaseURL + $"b2c/auth/refresh";
            // var body = new Dictionary<string, string>
            // {
            //     {"refresh_token", singInResult.RefreshToken}
            // };
            //
            // singInResult = await Request<SelfResponse>(requestString, RequestTypes.Post,
            //     JsonConvert.SerializeObject(body), new Dictionary<string, string>
            //     {
            //         {"accept", "application/json"},
            //         {"Content-Type", "application/json"}
            //     }, true, true, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestString"></param>
        /// <param name="type"></param>
        /// <param name="body"></param>
        /// <param name="headers"></param>
        /// <param name="singIn"></param>
        /// <param name="tokenRefreshed"></param>
        /// <returns></returns>
        public async Task<T> Request<T>(string requestString, 
            RequestTypes type, 
            List<IMultipartFormSection> body,
            Dictionary<string, string> headers, 
            bool singIn, 
            bool tokenRefreshed, 
            Action<float> progress) where T : class
        {
            // if (singInResult == null && !singIn)
            // {
            //     throw new NetworkRequestException("Internal", "No connection found ...");
            // }

            UnityWebRequest request = null;
           
            // switch (type)
            // {
            //     case RequestTypes.Post:
            //         request = UnityWebRequest.Post(requestString, body);
            //         break;
            // }

            try
            {
                return await Request<T>(request, headers, singIn, tokenRefreshed, progress);
            }
            catch (NetworkRequestException exception)
            {
                if (exception.Code == "401")
                {
                    return await Request<T>(requestString, type, body, headers, singIn, true, progress);
                }

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="requestString"></param>
        public async Task<T> Request<T>(string requestString, RequestTypes type, string body,
            Dictionary<string, string> headers, bool singIn, bool tokenRefreshed, 
            Action<float> progress) where T : class
        {
            // if (singInResult == null && !singIn)
            // {
            //     throw new NetworkRequestException("Internal", "No connection found ...");
            // }

            UnityWebRequest request = null;

            switch (type)
            {
                case RequestTypes.Get:
                    request = UnityWebRequest.Get(requestString);
                    break;
                case RequestTypes.Post:
                    request = UnityWebRequest.Post(requestString, "POST");
                    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(body);
                    request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
                    request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                    //Debug.Log("body login= "+ body.ToString());
                    break;
                case RequestTypes.Put:
                    request = UnityWebRequest.Put(requestString, body);
                    break;
                case RequestTypes.Delete:
                    request = UnityWebRequest.Delete(requestString);
                    break;
            }
            
            try
            {
                return await Request<T>(request, headers, singIn, tokenRefreshed, progress);
            }
            catch (NetworkRequestException exception)
            {
                return null;
                
                // if (exception.Code == "401")
                // {
                    
                    //
                    // return await Request<T>(requestString, 
                    //     type, 
                    //     body, 
                    //     headers, 
                    //     singIn, 
                    //     true, 
                //     //     progress);
                // }
                //
                // throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        /// <param name="singIn"></param>
        /// <param name="tokenRefreshed"></param>
        /// <returns></returns>
        public async Task<T> Request<T>(UnityWebRequest request, Dictionary<string, string> headers,
            bool singIn, bool tokenRefreshed, Action<float> progress) where T : class
        {
            if (headers != null)
            {
                foreach (var header in headers)
                    request.SetRequestHeader(header.Key, header.Value);
            }
            
            Debug.Log("Req = " + request.url);
            request.chunkedTransfer=false;
            
            
            using (var response = await GetResponse(request))
            {
                if (IsSuccessfull(response))
                {
                    Debug.LogFormat("<color=#ff6815ff> Request complete: " + typeof(T).Name + " </color>");
                    Debug.Log(" response.downloadHandler.text  " + response.downloadHandler.text);

                    //TO DO:
                    if (typeof(T) == typeof(String))
                    {
                        return null;
                    }
                    
                    var jsonGet = JsonConvert.DeserializeObject<T>(response.downloadHandler.text) as T;
                    Debug.Log("jsonGet = " + jsonGet);

                    return jsonGet;
                }
                else
                {
                    if (response.error.Contains("401"))
                    {
                        if (!tokenRefreshed)
                            RefreshToken();

                        throw new NetworkRequestException("401", response.url);
                    }

                    throw new NetworkRequestException(response.error, response.downloadHandler.text);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async UniTask<UnityWebRequest> GetResponse(UnityWebRequest req, Action<float> progress = null)
        {
            Debug.Log("Request sent " );
            var  result = req.SendWebRequest();
            Debug.Log("Request wait till done " + result.ToString() );

            await UniTask.WaitUntil(() =>
            {
                if (!req.isDone)
                {
                    //progress?.Invoke(req.method == "POST" ? req.uploadProgress : req.downloadProgress);
                    return false;
                }

                return true;
            });
            return req;
        }


        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsSuccessfull(UnityWebRequest request)
        {
            if (request.isHttpError || request.isNetworkError || !string.IsNullOrEmpty(request.error))
                return false;
            return true;
        }
    }
}