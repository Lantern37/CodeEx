using System;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class AccountCreatedWindow : WindowController
    {
        [SerializeField] private Button _next;

        public void SubscribeNext(Action onClick)
        {
            if (onClick != null)
            {
                _next.onClick.AddListener(()=>onClick?.Invoke());
            }
        }
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);
        }

        protected override void Closed()
        {
            base.Closed();
            _next.onClick.RemoveAllListeners();
        }
    }
}