    using Newtonsoft.Json;

    [System.Serializable]
    public class Coordinates
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
