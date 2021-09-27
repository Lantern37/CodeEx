using Newtonsoft.Json;

namespace Engenious.Core.Managers.Requests
{

    [System.Serializable]
    public class SupportRequest
    {
        [JsonProperty("title")] public string Title;

        [JsonProperty("message")] public string Message;
    }
}