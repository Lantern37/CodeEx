using System;
using Engenious.Core.Managers;
using Newtonsoft.Json;

[Serializable]
public class SelfResponse
{
    [JsonProperty("user")] public UserResponse User { get; set; }
    [JsonProperty("access_token")] public string AccessToken { get; set; }
    [JsonProperty("refresh_token")] public string RefreshToken { get; set; }
}
