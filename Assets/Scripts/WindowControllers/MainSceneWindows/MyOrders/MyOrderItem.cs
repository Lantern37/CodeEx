using System;
using System.Globalization;
using Assets.Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows.MyOrders
{
    public class MyOrderItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _status;
        [SerializeField] private TextMeshProUGUI _orderId;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private TextMeshProUGUI _date;

        [SerializeField] private Button _itemButton;

        public event Action<int> ClickEvent;

        private int _id;

        public void SetItem(MyOrderItemViewData viewData, int id, Action<int> onClick = null)
        {
            _id = id;

            _status.text = viewData.Status;
            _orderId.text = "Order " + viewData.OrderId;
            _price.text = PriceForm.GetFormatedPrice(viewData.Price);
            _date.text = viewData.Date.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            _itemButton.onClick.AddListener(Click);

            if (onClick != null)
            {
                ClickEvent += onClick;
            }
        }

        private void Click()
        {
            ClickEvent?.Invoke(_id);
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}