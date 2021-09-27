    using System;
    using Newtonsoft.Json;

    [Serializable]
    public class ProductBrand
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageLink")]
        public string ImageLink { get; set; }

        [JsonProperty("productCount")]
        public int ProductCount { get; set; }
    }
