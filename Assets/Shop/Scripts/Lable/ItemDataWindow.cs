using Assets.Scripts.Utilities;
using TMPro;
using UnityEngine;

namespace Assets.Shop.Scripts.Lable
{
    public class ItemDataWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;

        [SerializeField] private TextMeshProUGUI _thc;
        [SerializeField] private TextMeshProUGUI _sort;

        public void Show()
        {
            gameObject?.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject?.SetActive(false);
        }
        
        public void SetData(ProductResponse data)
        {
            if(data == null)
                return;
            
            Show();
            
            _name.text = data.Name;
            _price.text = "Price: "+ PriceForm.GetFormatedPrice(data.Price.ToString());
            _thc.text = data.Thc + "% THC";
            _sort.text = data.Strain.Name;
        }
    }
}