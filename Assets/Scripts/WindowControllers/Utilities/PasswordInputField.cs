using UnityEngine;
using UnityEngine.UI;

namespace Engenious.WindowControllers
{
    public class PasswordInputField : OutlineInputField
    {
        [SerializeField] private Toggle _checkbox;

        public override void Open()
        {
            base.Open();
            _checkbox.onValueChanged.AddListener((isOn) => SetPasswordField(isOn));
        }

        public override void Close()
        {
            base.Close();
            _checkbox.onValueChanged.RemoveAllListeners();
        }
        
        public override void ResetInputField()
        {
            base.ResetInputField();
            _checkbox.isOn = true;
            SetPasswordField(true);
        }

        public void SetPasswordField(bool isPassword)
        {
            if (isPassword)//(m_Checkbox.isOn)
            {
                _inputField.contentType = InputField.ContentType.Password;
            }
            else
            {
                _inputField.contentType = InputField.ContentType.Standard;
            }

            _inputField.ForceLabelUpdate();
        }
    }
}