using DG.Tweening;
using Engenious.Core.Managers;
using UnityEngine;

namespace Assets.Scripts.MainWindows
{
    public class LogoWindow : WindowController
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private float _closeDelay = 2f;

        protected override void Show(params object[] _params)
        {
            _canvasGroup.alpha = 0;
            base.Show(_params);
            _canvasGroup.DOFade(1, _fadeDuration).OnComplete(() => Manager.Close<LogoWindow>());
        }

        protected override void Closed()
        {
            _canvasGroup.alpha = 1;
            DOVirtual.DelayedCall(1.5f,() => base.Closed());
            //base.Closed();
        }
    }
}