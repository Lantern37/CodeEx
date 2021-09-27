using Newtonsoft.Json;

namespace Engenious.Core.Managers
{
    public class SupportResponce
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
    
    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}