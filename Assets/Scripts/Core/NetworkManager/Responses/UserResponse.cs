using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Engenious.Core.Managers
{
    [Serializable]
    public class UserResponse
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("username")] public string UserName { get; set; }
        [JsonProperty("fullname")] public string FullName { get; set; }
    }
}
