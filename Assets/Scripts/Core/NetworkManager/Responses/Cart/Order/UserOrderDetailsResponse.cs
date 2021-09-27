using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Engenious.Core.Managers.Cart.Order.OrderDetails
{
    [Serializable]
    public class UserOrderDetailsResponse
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("quantity")] public int Quantity { get; set; }

        [JsonProperty("createdAt")] public DateTime CreatedAt { get; set; }

        [JsonProperty("product")] public Product Product { get; set; }

        [JsonProperty("orderId")] public int OrderId { get; set; }
    }

    public class Brand
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("imageLink")] public string ImageLink { get; set; }

        [JsonProperty("productCount")] public int ProductCount { get; set; }
    }

    public class Type
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }

    public class Strain
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }

    public class Effect
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }

    public class Product
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("vendorCode")] public string VendorCode { get; set; }

        [JsonProperty("thc")] public int Thc { get; set; }

        [JsonProperty("labTestLink")] public string LabTestLink { get; set; }

        [JsonProperty("imageLink")] public string ImageLink { get; set; }

        [JsonProperty("modelLowQualityLink")] public string ModelLowQualityLink { get; set; }

        [JsonProperty("modelHighQualityLink")] public string ModelHighQualityLink { get; set; }

        [JsonProperty("quantityInStock")] public int QuantityInStock { get; set; }

        [JsonProperty("visible")] public bool Visible { get; set; }

        [JsonProperty("price")] public double Price { get; set; }

        [JsonProperty("wholesalePrice")] public double WholesalePrice { get; set; }

        [JsonProperty("cbd")] public int Cbd { get; set; }

        [JsonProperty("gramWeight")] public string GramWeight { get; set; }

        [JsonProperty("ounceWeight")] public string OunceWeight { get; set; }

        [JsonProperty("totalCannabinoids")] public int TotalCannabinoids { get; set; }

        [JsonProperty("brand")] public Brand Brand { get; set; }

        [JsonProperty("type")] public Type Type { get; set; }

        [JsonProperty("strain")] public Strain Strain { get; set; }

        [JsonProperty("effects")] public List<Effect> Effects { get; set; }
    }
}