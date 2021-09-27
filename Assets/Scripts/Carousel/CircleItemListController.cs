using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Carousel
{
    public class CircleItemListController
    {
        private CircleItemListControllerConfig _config;
        public int ElementsCount { get; private set; }
        
        //private List<Item> _itemsPrefabs;

        private Dictionary<Item, ProductResponse> _itemsPrefabs;

        private GameObject _itemDefaultParent;
        
        private List<Item> _leavingItems;
        
        private int _rightIndex;
        private int _leftIndex;

        public CircleItemListController(CircleItemListControllerConfig config)
        {
            _config = config;
        }
        //public void Initialize(List<Item> itemPrefabs)
        public void Initialize(Dictionary<Item, ProductResponse> itemsPrefabs)
        {
            _leavingItems = new List<Item>();
            _itemsPrefabs = itemsPrefabs;
            
            CreateAllItems();
            
            ElementsCount = itemsPrefabs.Count < _config.DesiredItemsCount ? itemsPrefabs.Count : _config.DesiredItemsCount;
            _rightIndex = 0;
            _leftIndex = ElementsCount - 1;
        }

        public List<Item> GetInitItems()
        {
            List<Item> items = new List<Item>();
            
            for (int i = _rightIndex; i <= _leftIndex; i++)
            {
                var item = EnableItem(i);
                items.Add(item);
            }

            return items;
        }
        
        public Item GetNextRightItem()
        {
            _rightIndex--;

            
            if (_rightIndex < 0)
            {
                _rightIndex = _itemsPrefabs.Count-1;
            }
            
            _leftIndex--;
            
            if (_leftIndex < 0)
            {
                _leftIndex = _itemsPrefabs.Count-1;
            }

            // item = CreateItem(_rightIndex);
            Item item = EnableItem(_rightIndex);

            return item;
        }
        
        public Item GetNextLeftItem()
        {
            _rightIndex++;
            if (_rightIndex >= _itemsPrefabs.Count)
            {
                _rightIndex = 0;
            }
            _leftIndex++;
            if (_leftIndex >= _itemsPrefabs.Count)
            {
                _leftIndex = 0;
            }

            //item = CreateItem(_leftIndex);
            Item  item = EnableItem(_leftIndex);

            return item;
        }

        public void DestroyItems()
        {
            // foreach (var item in _leavingItems)
            // {
            //     item.Delete();
            // }
            
            _leavingItems.Clear();

            if (_itemDefaultParent != null)
            {
                Object.Destroy(_itemDefaultParent);    
            }
        }
        
        private void CreateAllItems()
        {
            foreach (var prefab in _itemsPrefabs)
            {
                Item item = prefab.Key;
                item.ProductInfo.SetData(_itemsPrefabs[prefab.Key]);
                // item.SetDefaultTransform(_itemDefaultParent.transform);
                // item.DisableItem();
                _leavingItems.Add(item);
            }
        }
        
        private Item CreateItem(int index)
        {
            var prefab = _itemsPrefabs.ElementAt(index).Key;
            Item item = Object.Instantiate(prefab);
            item.ProductInfo.SetData(_itemsPrefabs[prefab]);

            return item;
        }
        
        private Item EnableItem(int index)
        {
            var item = _leavingItems[index];

            item.EnableItem();
            return item;
        }
    }

    [Serializable]
    public class CircleItemListControllerConfig
    {
        public int DesiredItemsCount;
    }
}