
using Newtonsoft.Json;

[System.Serializable]
public class PutUserRequest
{
    [JsonProperty("email")] 
    public string Email;// { get; set; }
    
    [JsonProperty("password")] 
    public string Password;//{ get; set; }
    
    [JsonProperty("socialSecurityNumber")] 
    public int SocialSecurityNumber;// { get; set; }
}
