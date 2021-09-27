using System;
using Newtonsoft.Json;

namespace Engenious.Core.Managers
{
    [Serializable]
    public class LoginUserResponce
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}