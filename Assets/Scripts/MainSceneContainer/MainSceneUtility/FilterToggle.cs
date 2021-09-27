using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Engenious.Core
{
    public class FilterToggle : MonoBehaviour, IPointerClickHandler
    {
        public int Index;
        
        private Image _image;
        private TextMeshProUGUI _text;

        private Color _selectedTextColor = Color.white;
        private Color _unSelectedTextColor = Color.black;

        private Color _selectedImageColor = new Color(0.235f, 0.4313f, 0.396f, 1f);
        private Color _unSelectedImageColor = Color.white;

        public event Action<FilterToggle> On;
        public event Action<FilterToggle> Off;

        private bool _isOn;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            
            _selectedTextColor = Color.white;
            _unSelectedTextColor = Color.black;
            _selectedImageColor = new Color(0.235f, 0.4313f, 0.396f, 1f);
            _unSelectedImageColor = Color.white;
        }
        
        public void ResetToggle()
        {
            _isOn = false;
            _image.color = _unSelectedImageColor;
            _text.color = _unSelectedTextColor;
        }

        public void OnClick()
        {
            if (_isOn)
            {
                _image.color = _unSelectedImageColor;
                _text.color = _unSelectedTextColor;
                Off?.Invoke(this);
            }
            else
            {
                _image.color = _selectedImageColor;
                _text.color = _selectedTextColor;
                On?.Invoke(this);
            }
            
            _isOn = !_isOn;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }
    }
}