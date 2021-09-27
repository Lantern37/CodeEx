using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;


namespace Engenious.Core.Managers
{
    public enum NetworkSpecificType
    {
        NETWORK_TYPE_UNKNOWN,
        NETWORK_TYPE_GPRS,
        NETWORK_TYPE_EDGE,
        NETWORK_TYPE_UMTS,
        NETWORK_TYPE_CDMA,
        NETWORK_TYPE_EVDO_0,
        NETWORK_TYPE_EVDO_A,
        NETWORK_TYPE_1xRTT,
        NETWORK_TYPE_HSDPA,
        NETWORK_TYPE_HSUPA,
        NETWORK_TYPE_HSPA,
        NETWORK_TYPE_IDEN,
        NETWORK_TYPE_EVDO_B,
        NETWORK_TYPE_LTE,
        NETWORK_TYPE_EHRPD,
        NETWORK_TYPE_HSPAP,
        NETWORK_TYPE_GSM,
        NETWORK_TYPE_TD_SCDMA,
        NETWORK_TYPE_IWLAN,
        NETWORK_TYPE_NR
    }

    public enum NetworkType
    {
        Disable,
        Net2G,
        Net3G,
        Net4G,
        Net5G,
        NetWifi,
        NetWireless,
        NetUnknown
    }

    public interface INetworkChecker
    {
        bool IsInitialized { get; }
        Task Initialize();
        NetworkType Type { get; }

        void Update();
        void Invalidate();
    }
}
