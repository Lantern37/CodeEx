    using System;
    using Newtonsoft.Json;

    [Serializable]
    public class ProductStrain
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
