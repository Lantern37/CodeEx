    using System;
    using Newtonsoft.Json;

    [Serializable]
    public class OrderPartner
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("weedMapsLink")]
        public string WeedMapsLink { get; set; }

        [JsonProperty("contactEmail")]
        public string ContactEmail { get; set; }

        [JsonProperty("deliveryEmail")]
        public string DeliveryEmail { get; set; }
    }
