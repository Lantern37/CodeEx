using System;
using Newtonsoft.Json;

namespace Engenious.Core.Managers.Cart.Order
{
    [Serializable]
    public class OrderResponceProduct
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vendorCode")]
        public string VendorCode { get; set; }

        [JsonProperty("thc")]
        public double Thc { get; set; }

        [JsonProperty("labTestLink")]
        public string LabTestLink { get; set; }

        [JsonProperty("imageLink")]
        public string ImageLink { get; set; }

        [JsonProperty("modelLowQualityLink")]
        public string ModelLowQualityLink { get; set; }

        [JsonProperty("modelHighQualityLink")]
        public string ModelHighQualityLink { get; set; }

        [JsonProperty("quantityInStock")]
        public int QuantityInStock { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("cbd")]
        public double Cbd { get; set; }

        [JsonProperty("totalCannabinoids")]
        public double TotalCannabinoids { get; set; }
    }
}