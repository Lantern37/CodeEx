using Assets.Scripts.MainWindows.Cart;
using Assets.Scripts.Utilities;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.MainWindows
{
    public class DeliveryReviewItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;
        //[SerializeField] private TextMeshProUGUI _weight;

        public void SetData(DeliveryReviewItemData data)
        {
            _count.text = data.Count.ToString();
            _name.text = data.Name;
            _price.text = PriceForm.GetFormatedPrice(data.Price.ToString());
            //_weight.text = data.Weight;
        }
        
        public void Clear()
        {
            _count.text = string.Empty;
            _name.text = string.Empty;
            _price.text = string.Empty;
            //_weight.text = string.Empty;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }

    public class DeliveryReviewItemData
    {
        public int Count;
        public string Name;
        public float Price;
        public string Weight;
    }
}