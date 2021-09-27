using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Engenious.Core.Managers
{
    [Serializable]
    public class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
    
    [Serializable]
    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
    }
    
    [Serializable]
    public class Result
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }
    
    [Serializable]
    public class GooglePlacesResponse
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}