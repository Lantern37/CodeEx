using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class SupportPopup : WindowController
    {
        [SerializeField] private Button _closeButton;
        protected override void Showed()
        {
            base.Showed();
            _closeButton.onClick.AddListener(CloseButton);
        }

        protected override void Closed()
        {
            _closeButton.onClick.RemoveListener(CloseButton);
            base.Closed();
        }

        private void CloseButton()
        {
            Manager.Close<SupportPopup>();
        }
    }
}