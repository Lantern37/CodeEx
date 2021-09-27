using Newtonsoft.Json;

namespace Engenious.Core.Managers.Requests
{
    [System.Serializable]
    public class PutTokenRequest
    {
        [JsonProperty("firebaseToken")] 
        public string FirebaseToken;
    }
}