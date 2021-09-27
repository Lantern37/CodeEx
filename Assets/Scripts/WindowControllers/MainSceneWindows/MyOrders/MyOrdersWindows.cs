using System;
using System.Collections.Generic;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows.MyOrders
{
    public class MyOrdersWindows : WindowController
    {
        public event Action<int> ItemClicked;
        
        [SerializeField] private Button _back;

        [SerializeField] private MyOrderItem _deliveryItemPrefab;
        
        [SerializeField] private RectTransform _content;

        private List<MyOrderItem> _items;

        protected override void Closed()
        {
            base.Closed();
            _back.onClick.RemoveAllListeners();
            ItemClicked = null;
            DeleteItems();
        }

        public void SubscribeEvents(Action<int> itemSelected, Action backClick)
        {
            if (itemSelected != null)
            {
                ItemClicked += itemSelected;
            }

            if (backClick != null)
            {
                _back.onClick.AddListener(()=> backClick());
            }
        }
        
        public void SetCartData(List<MyOrderItemViewData> cartItems)
        {
            _items = new List<MyOrderItem>();

            for (int i = 0; i < cartItems.Count; i++)
            {
                var item = CreateItem(cartItems[i], i);
                item.ClickEvent += OnItemClick;
                _items.Add(item);
            }
        }

        private void OnItemClick(int index)
        {
            ItemClicked?.Invoke(index);
        }
        
        private void DeleteItems()
        {
            if (_items != null && _items.Count > 0)
                for (int i = _items.Count - 1; i >= 0; i--)
                {
                    _items[i].DestroyItem();
                }

            _items.Clear();
        }

        private MyOrderItem CreateItem(MyOrderItemViewData data, int index)
        {
            var item = Instantiate(_deliveryItemPrefab, _content);
            item.SetItem(data, index);
            return item;
        }
    }
}