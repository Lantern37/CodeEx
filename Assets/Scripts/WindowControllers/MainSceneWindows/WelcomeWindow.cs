using System;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class WelcomeWindow : WindowController
    {
        [SerializeField] private Button _logInButton;
        [SerializeField] private Button _signInButton;

        protected override void Closed()
        {
            base.Closed();
            UnSubscribeButtons();
        }

        public void SubscribeButtons(Action logInOnClick, Action signInOnClick)
        {
            if (logInOnClick != null)
            {
                _logInButton.onClick.AddListener(()=>logInOnClick());
            }
            
            if (signInOnClick != null)
            {
                _signInButton.onClick.AddListener(()=>signInOnClick());
            }
            
        }   
        
        public void UnSubscribeButtons()
        {
            _logInButton.onClick.RemoveAllListeners();
            _signInButton.onClick.RemoveAllListeners();
        }
    }
}