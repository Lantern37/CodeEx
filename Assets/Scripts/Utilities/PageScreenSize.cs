using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class PageScreenSize : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }
}