    using System;
    using Newtonsoft.Json;

    [Serializable]
    public class ProductEffect
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
