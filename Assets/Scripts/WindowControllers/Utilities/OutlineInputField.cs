using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Engenious.WindowControllers
{
    public class OutlineInputField : MonoBehaviour
    {
        [SerializeField] protected InputField _inputField;
        public InputField InputField => _inputField;
        
        [SerializeField] private Outline _errorOutline;

        public bool IsError { get; private set; }
        
        public void SetText(string text)
        {
            _inputField.text = text;
        }

        public virtual void ResetInputField()
        {
            _inputField.text = string.Empty;
            _inputField.onSubmit.RemoveAllListeners();
            _inputField.onValueChanged.RemoveAllListeners();

            ShowErrorOutline(false);
        }
        
        public void ShowErrorOutline(bool value)
        {
            _errorOutline.enabled = value;
            IsError = value;
        }
        
        public virtual void Open()
        {
            ResetInputField();
        }

        public virtual void Close()
        {
            ResetInputField();
        }
    }
}
