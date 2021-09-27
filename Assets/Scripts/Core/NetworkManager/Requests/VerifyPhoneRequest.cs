using System;
using Newtonsoft.Json;

namespace Engenious.Core.Managers.Requests
{
    [Serializable]
    public class VerifyPhoneRequest
    {
        [JsonProperty("phone")] 
        public string Phone;// { get; set; }
    }
}