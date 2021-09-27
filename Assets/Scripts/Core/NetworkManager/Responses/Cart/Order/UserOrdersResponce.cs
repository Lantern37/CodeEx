using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Engenious.Core.Managers.Cart.Order
{
    [Serializable]
    public class User
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("email")] public string Email { get; set; }
    }

    [Serializable]

    public class Area
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("partnerCount")]
        public int PartnerCount { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }
    }

    [Serializable]
    public class License
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    [Serializable]
    public class Partner
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("weedMapsLink")]
        public string WeedMapsLink { get; set; }

        [JsonProperty("contactEmail")]
        public string ContactEmail { get; set; }

        [JsonProperty("deliveryEmail")]
        public string DeliveryEmail { get; set; }

        [JsonProperty("exciseTax")]
        public double ExciseTax { get; set; }

        [JsonProperty("salesTax")]
        public double SalesTax { get; set; }

        [JsonProperty("cityTax")]
        public double CityTax { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("areas")]
        public List<Area> Areas { get; set; }

        [JsonProperty("licenses")]
        public List<License> Licenses { get; set; }
    }

    [Serializable]
    public class UserOrder
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("number")] public string Number { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("addressLine1")] public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")] public object AddressLine2 { get; set; }

        [JsonProperty("zipCode")] public string ZipCode { get; set; }

        [JsonProperty("sum")] public double Sum { get; set; }

        [JsonProperty("deliverySum")] public double DeliverySum { get; set; }

        [JsonProperty("totalSum")] public double TotalSum { get; set; }

        [JsonProperty("exciseTaxSum")] public double ExciseTaxSum { get; set; }

        [JsonProperty("salesTaxSum")] public double SalesTaxSum { get; set; }

        [JsonProperty("cityTaxSum")] public double CityTaxSum { get; set; }

        [JsonProperty("taxSum")] public double TaxSum { get; set; }

        [JsonProperty("profitSum")] public double ProfitSum { get; set; }

        [JsonProperty("createdAt")] public DateTime CreatedAt { get; set; }

        [JsonProperty("state")] public int State { get; set; }

        [JsonProperty("comment")] public string Comment { get; set; }

        [JsonProperty("updatedAt")] public DateTime UpdatedAt { get; set; }

        [JsonProperty("latitudeCoordinate")] public double LatitudeCoordinate { get; set; }

        [JsonProperty("longitudeCoordinate")] public double LongitudeCoordinate { get; set; }

        [JsonProperty("area")] public Area Area { get; set; }

        [JsonProperty("partner")] public Partner Partner { get; set; }

        [JsonProperty("userId")] public int UserId { get; set; }

        [JsonProperty("detailCount")] public int DetailCount { get; set; }
    }
}