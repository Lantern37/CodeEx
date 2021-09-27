    using System;
    using Engenious.Core.Managers.Cart.Order;
    using Newtonsoft.Json;

    [Serializable]
    public class OrderDetails
    {
        [JsonProperty("product")]
        public OrderResponceProduct Product { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
