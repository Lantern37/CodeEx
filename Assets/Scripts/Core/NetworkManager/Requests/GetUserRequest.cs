using Newtonsoft.Json;

namespace Engenious.Core.Managers.Requests
{
    [System.Serializable]
    public class GetUserRequest
    {
        [JsonProperty("email")]
        public string Email;// { get; set; }
    }
}