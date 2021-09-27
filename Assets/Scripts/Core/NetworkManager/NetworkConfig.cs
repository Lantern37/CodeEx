using UnityEngine;

namespace Engenious.Core.Managers
{
    [CreateAssetMenu(fileName = "NetworkConfig", menuName = "Engenious/Core/Configs/NetworkConfig")]
    public class NetworkConfig : ScriptableObject
    {
        public string GuestKey = "6054b54f884f9377e5d141b4b46fcee6";
        public string BaseURL = "http://ec2-3-138-204-158.us-east-2.compute.amazonaws.com:3000";
        public string AuthorizePrefix = "b2c/auth/";
        public float TokenRefreshTime = 20;
        
        public string GetUser = "/user";
        public string PutUser = "/user/register";
        public string PostLogin = "/auth/login";
        public string VerifyPhone = "/verifyPhone";
        public string VerifyPassport = "/verifyPassport";
        public string PutToken = "/token";

        public string GetProduct = "/product";
        public string PostOrder = "/order";
        public string ConfirmOrder = "/confirm";
        public string GetUserOrders = "/order/user";
        public string GetUserOrderDetails ="/orderDetail/order";
        
        public string ProductIcon = "/img/";

        public string GetAddress = "https://maps.googleapis.com/maps/api/place/textsearch/json?";

        public string PostSupport = "/support";
    }
}