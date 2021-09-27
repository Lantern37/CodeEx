    using System;
    using Newtonsoft.Json;

    [Serializable]
    public class ProductType
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
