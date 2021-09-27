using Newtonsoft.Json;
using UnityEngine;

namespace Engenious.Core.Managers.Requests
{
   [SerializeField]
    public class PutPassportRequest
    {
        [JsonProperty("passportPhoto")]
        public byte[] PassportPhoto;
        
        [JsonProperty("physicianRecPhoto")]
        public byte[] PhysicianPhoto;
    }
}