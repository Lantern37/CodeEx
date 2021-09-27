using System;
using Newtonsoft.Json;

namespace Engenious.Core.Managers
{
    [Serializable]
    public class GetUserResponce
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("socialSecurityNumber")] public int SocialSecurityNumber { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
        [JsonProperty("createDate")] public DateTime CreateDate { get; set; }
        [JsonProperty("role")] public Role Role { get; set; }
    }

    [Serializable]
    public class Role
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}