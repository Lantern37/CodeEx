using TMPro;
using UnityEngine;

namespace Assets.Scripts.MainWindows
{
    public class ProductEffectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sort;

        public void SetType(string sort)
        {
            _sort.text = sort;
        }

        public void Delete()
        {
            Destroy(gameObject);
        }
    }
}