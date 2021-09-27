#if UNITY_IOS && !UNITY_EDITOR

using System.Threading.Tasks;
using UnityEngine;

namespace Engenious.Core.Managers
{
    public class NetworkChecker : INetworkChecker
    {
       private static readonly float CHECK_INTERVAL = 5f;

        public bool IsInitialized { get; set; }

        private bool _isConnectedToInternet = false;
        private bool _isCheckedNow;
        private float _checkTime;

        public NetworkType Type
        {
            get
            {
                switch (Application.internetReachability)
                {
                    case NetworkReachability.NotReachable:
                        return NetworkType.Disable;

                    case NetworkReachability.ReachableViaCarrierDataNetwork:
                        return _isConnectedToInternet ? NetworkType.Net4G : NetworkType.Disable;

                    case NetworkReachability.ReachableViaLocalAreaNetwork:
                        return _isConnectedToInternet ? NetworkType.NetWifi : NetworkType.Disable;
                }

                return NetworkType.Disable;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task Initialize()
        {
            IsInitialized = true;
            await CheckForConnection();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task CheckForConnection()
        {
            var png = new Ping("8.8.8.8");
            await Task.Delay(500);
            _isConnectedToInternet = png.isDone;
        }

        /// <summary>
        /// 
        /// </summary>
        public async void Update()
        {
            if (_isCheckedNow || !IsInitialized)
                return;

            _checkTime -= Time.deltaTime;
            if (_checkTime <= 0)
            {
                _isCheckedNow = true;
                _checkTime = CHECK_INTERVAL;
                await CheckForConnection();
                _isCheckedNow = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Invalidate()
        {
        }
    }
}
#endif