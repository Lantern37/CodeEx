using System;
using Newtonsoft.Json;

namespace Engenious.Core.Managers
{
    [Serializable]
    public class GetUserIdResponse
    {
        [JsonProperty("id")] public int Id;

        [JsonProperty("email")] public string Email;

        [JsonProperty("phone")] public string Phone;

        [JsonProperty("passportPhotoLink")] public string PassportPhotoLink;

        [JsonProperty("physicianRecPhotoLink")]
        public string PhysicianRecPhotoLink;

        [JsonProperty("state")] public int State;

        [JsonProperty("createdAt")] public DateTime CreatedAt;

        [JsonProperty("role")] public UserIdRole Role;

        [JsonProperty("roleId")] public int RoleId;
    }
    
    [Serializable]
    public class UserIdRole
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}