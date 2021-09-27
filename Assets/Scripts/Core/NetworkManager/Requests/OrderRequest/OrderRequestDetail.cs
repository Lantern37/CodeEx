    using Newtonsoft.Json;
    
    [System.Serializable]
    public class OrderRequestDetail
    {
        [JsonProperty("product")]
        public int Product { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
