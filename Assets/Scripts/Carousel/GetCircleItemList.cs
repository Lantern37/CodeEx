using System;
using System.Collections.Generic;
using Shop.Core;
using UnityEngine;

namespace Assets.Scripts.Carousel
{
    public interface IGetCircleItemList
    {
        List<Item> GetItems();
        Dictionary<Item, ProductResponse> GetDictionaryItems();
    }

    public class GetCircleItemList : IGetCircleItemList
    {
        private List<ProductResponse> _products;
        private List<Item> _itemPrefabs;

        public GetCircleItemList(GetCircleItemListConfig config)
        {
            _itemPrefabs = config.ItemPrefabs;
        }

        public void Initialize(List<ProductResponse> products)
        {
            _products = products;
        }
        
        public List<Item> GetItems()
        {
            List<Item> items = new List<Item>();

            foreach (var product in _products)
            {
                foreach (var item in _itemPrefabs)
                {
                    if (item.name == product.Name)
                    {
                        items.Add(item);
                    }                    
                }
            }
            
            return items;
        }
        
        public Dictionary<Item, ProductResponse> GetDictionaryItems()
        {
            var items = new Dictionary<Item, ProductResponse>();

            foreach (var product in _products)
            {
                foreach (var item in _itemPrefabs)
                {
                    if (item.name == product.Name)
                    {

                        if (!items.ContainsKey(item))
                            items.Add(item, product);
                    }
                }
            }
            
            return items;
        }
    }
    
    [Serializable]
    public class GetCircleItemListConfig
    {
        public List<Item> ItemPrefabs;
    }
}