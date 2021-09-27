using System;
using Newtonsoft.Json;

namespace Engenious.Core.Managers.Requests
{
    [Serializable]
    public class LoginUserRequest
    {
        [JsonProperty("email")]
        public string Email;
        
        [JsonProperty("password")]
        public string Password;
    }
}