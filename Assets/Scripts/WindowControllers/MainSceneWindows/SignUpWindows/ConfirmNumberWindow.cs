using System;
using Engenious.Core.Managers;
using Engenious.WindowControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class ConfirmNumberWindow : WindowController
    {
        [SerializeField] private TextMeshProUGUI _phoneText;
        [SerializeField] private OutlineInputField _codeInputField;
        [SerializeField] private Button _next;

        public OutlineInputField CodeInputField =>_codeInputField;

        public void SubscribeNext(Action onClick)
        {
            _next.onClick.AddListener(()=>onClick());
        }

        public void SetPhoneNumber(string phone)
        {
            _phoneText.text = phone;
        }

        public void SetNextButtonActive(bool value)
        {
            _next.interactable = value;
        }
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            _codeInputField.Open();
        }

        protected override void Closed()
        {
            base.Closed();
            _phoneText.text = string.Empty;
            _codeInputField.Close();
            UnSubscribe();
        }

        private void UnSubscribe()
        {
            _next.onClick.RemoveAllListeners();
        }
    }
}