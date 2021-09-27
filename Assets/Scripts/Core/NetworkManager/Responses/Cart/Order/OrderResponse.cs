    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [Serializable]
    public class OrderResponse
    {
        [JsonProperty("number")]
        public string Number { get; set; }

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

        [JsonProperty("sum")]
        public double Sum { get; set; }

        [JsonProperty("deliverySum")]
        public double DeliverySum { get; set; }

        [JsonProperty("totalSum")]
        public double TotalSum { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("exciseTaxSum")]
        public double ExciseTaxSum { get; set; }
        
        [JsonProperty("salesTaxSum")]
        public double SalesTaxSum { get; set; }
        
        [JsonProperty("cityTaxSum")]
        public double CityTaxSum { get; set; }
        
        [JsonProperty("taxSum")]
        public double TaxSum { get; set; }
        
        [JsonProperty("details")]
        public List<OrderDetails> Details { get; set; }
    }
