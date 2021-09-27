using System;
using Assets.Scripts.MainWindows.Cart;

namespace Assets.Scripts.UserDataScripts
{
    [Serializable]
    public class SavedCartDataItem
    {
        public int Count;
        public float Price;
        public string BrandName;
        public string ProductName;
        public int Id;
        public string ImageLink;

        public void SetData(ProductResponse data)
        {
            Count = 1;
            Price = (float)data.Price;
            BrandName = data.Brand.Name;
            Id = data.Id;
            ImageLink = data.ImageLink;
        }

        public void SetData(CartItemViewData data)
        {
            Count = data.Count;
            Price = (float)data.Price;
            BrandName = data.BrandName;
            ProductName = data.ProductName;
            Id = data.ProductId;
            ImageLink = data.ImageLink;
        }
        
        public void SetImageLink(string imageLink)
        {
            ImageLink = imageLink;
        }
    }
}