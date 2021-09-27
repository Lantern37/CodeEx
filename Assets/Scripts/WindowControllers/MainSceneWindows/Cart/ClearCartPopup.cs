using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows.Cart
{
    public class ClearCartPopup : MonoBehaviour
    {
        [SerializeField] private Button _cancel;
        [SerializeField] private Button _clear;

        public void Show()
        {
            gameObject.SetActive(true);
            _clear.onClick.AddListener(CloseByButton);
            _cancel.onClick.AddListener(CloseByButton);
        }

        public void Close()
        {
            _clear.onClick.RemoveListener(CloseByButton);
            _cancel.onClick.RemoveListener(CloseByButton);
            gameObject.SetActive(false);
        }

        public void SubscribeClearButton(Action onCancel)
        {
            _clear.onClick.AddListener(()=> onCancel?.Invoke());
        }

        private void CloseByButton()
        {
            Close();
        }
    }
}