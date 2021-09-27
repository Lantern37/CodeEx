    using System.Collections.Generic;
    using Newtonsoft.Json;

    [System.Serializable]
    public class OrderRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("user")]
        public int User { get; set; }

        [JsonProperty("sum")]
        public double Sum { get; set; }

        [JsonProperty("deliverySum")]
        public double DeliverySum { get; set; }

        [JsonProperty("totalSum")]
        public double TotalSum { get; set; }

        [JsonProperty("latitudeCoordinate")]
        public double LatitudeCoordinate { get; set; }

        [JsonProperty("longitudeCoordinate")]
        public double LongitudeCoordinate { get; set; }

        [JsonProperty("details")]
        public List<OrderRequestDetail> Details { get; set; }
    }
    
