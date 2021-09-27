    using System;
    using Newtonsoft.Json;

    [Serializable]
    public class OrderDetail
    {
        [JsonProperty("product")]
        public OrderProduct Product { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("orderId")]
        public int OrderId { get; set; }
    }
