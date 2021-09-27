#if UNITY_ANDROID && !UNITY_EDITOR

using UnityEngine;
using System.Threading.Tasks;
using UniRx.Async;
using UnityEngine.Android;

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
            Permission.RequestUserPermission("android.permission.READ_PHONE_STATE");
            Permission.RequestUserPermission("android.permission.ACCESS_NETWORK_STATE");

            await UniTask.WaitUntil(CheckPermission);
            await CheckForConnection();
            IsInitialized = true;

        }

        private bool CheckPermission()
        {
            return Permission.HasUserAuthorizedPermission("android.permission.READ_PHONE_STATE") &&
                   Permission.HasUserAuthorizedPermission("android.permission.ACCESS_NETWORK_STATE");
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

        private NetworkType GetConnectionType()
        {
            var TM = new AndroidJavaObject("android.telephony.TelephonyManager");
            var networkType = (NetworkSpecificType) TM.Call<int>("getNetworkType");
            switch (networkType)
            {
                case NetworkSpecificType.NETWORK_TYPE_GPRS:
                case NetworkSpecificType.NETWORK_TYPE_EDGE:
                case NetworkSpecificType.NETWORK_TYPE_CDMA:
                case NetworkSpecificType.NETWORK_TYPE_1xRTT:
                case NetworkSpecificType.NETWORK_TYPE_IDEN:
                    return NetworkType.Net2G;
                case NetworkSpecificType.NETWORK_TYPE_UMTS:
                case NetworkSpecificType.NETWORK_TYPE_EVDO_0:
                case NetworkSpecificType.NETWORK_TYPE_EVDO_A:
                case NetworkSpecificType.NETWORK_TYPE_HSDPA:
                case NetworkSpecificType.NETWORK_TYPE_HSUPA:
                case NetworkSpecificType.NETWORK_TYPE_HSPA:
                case NetworkSpecificType.NETWORK_TYPE_EVDO_B:
                case NetworkSpecificType.NETWORK_TYPE_EHRPD:
                case NetworkSpecificType.NETWORK_TYPE_HSPAP:
                    return NetworkType.Net3G;
                case NetworkSpecificType.NETWORK_TYPE_LTE:
                    return NetworkType.Net4G;
                case NetworkSpecificType.NETWORK_TYPE_NR:
                    return NetworkType.Net5G;
                default:
                    return NetworkType.Disable;
            }
        }
    }
}

#endif