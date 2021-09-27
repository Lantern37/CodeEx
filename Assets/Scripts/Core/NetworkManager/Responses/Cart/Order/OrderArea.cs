    using System;
    using Newtonsoft.Json;

    [Serializable]
    public class OrderArea
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("topLeftX")]
        public double TopLeftX { get; set; }

        [JsonProperty("topLeftY")]
        public double TopLeftY { get; set; }

        [JsonProperty("bottomRightX")]
        public double BottomRightX { get; set; }

        [JsonProperty("bottomRightY")]
        public double BottomRightY { get; set; }
    }
