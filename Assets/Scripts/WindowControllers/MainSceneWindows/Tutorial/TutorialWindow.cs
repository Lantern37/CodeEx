using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Assets.Scripts.MainWindows.Tutorial
{
    public class TutorialWindow : WindowController
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _skipButton;

        [SerializeField] private Button _closeButton;

        [SerializeField] private HorizontalScrollSnap _swiper;

        [SerializeField] private int _pageCount = 3;
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);
            _skipButton.onClick.AddListener(()=> Close());
            _closeButton.onClick.AddListener(()=> Close());
            //_nextButton.onClick.AddListener(()=> _swiper.NextScreen());
            _swiper.OnSelectionPageChangedEvent.AddListener(PageChange);
            
            NotLastPage();
        }

        private void PageChange(int page)
        {
            if (page >= _pageCount)
            {
                LastPage();
            }
            else
            {
                NotLastPage();
            }
        }

        protected override void Closed()
        {
            base.Closed();
            _skipButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
            _swiper.GoToScreen(0);
            _swiper.OnSelectionPageChangedEvent.RemoveListener(PageChange);
            //_nextButton.onClick.RemoveAllListeners();
        }

        private void NotLastPage()
        {
            _nextButton.gameObject.SetActive(true);
            _skipButton.gameObject.SetActive(true);
            
            _closeButton.gameObject.SetActive(false);
        }
        
        private void LastPage()
        {
            _nextButton.gameObject.SetActive(false);
            _skipButton.gameObject.SetActive(false);
            
            _closeButton.gameObject.SetActive(true);
        }
    }
}