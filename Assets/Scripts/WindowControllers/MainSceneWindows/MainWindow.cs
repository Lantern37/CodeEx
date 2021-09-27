using System;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class MainWindow : WindowController
    {
        public override bool Top => false;

        [SerializeField] private Button _cartButton;

        [SerializeField] private Button _resetButton;

        [SerializeField] private Button _filterButton;

        [SerializeField] private Button _userButton;

        [SerializeField] private Button _tutorialButton;
        
        public Button ResetButton =>_resetButton;

        public void SubscribeButtons(Action onclick, Action filterButton, Action userButton, Action tutorialButton)
        {
            if (onclick != null)
            {
                _cartButton.onClick.AddListener(()=>onclick());
            }
            
            if (filterButton != null)
            {
                _filterButton.onClick.AddListener(()=>filterButton());
            }
            
            if (userButton != null)
            {
                _userButton.onClick.AddListener(()=>userButton());
            }

            if (tutorialButton != null)
            {
                _tutorialButton.onClick.AddListener(() => tutorialButton());
            }
        }

        public void ActivateFilterButton()
        {
            _filterButton.gameObject.SetActive(true);
        }
        
        public void DeactivateFilterButton()
        {
            _filterButton.gameObject.SetActive(false);
        }
        
        protected override void Closed()
        {
            base.Closed();
            //UnSubscribe();
        }

        public void UnSubscribe()
        {
            _cartButton.onClick.RemoveAllListeners();
            _resetButton.onClick.RemoveAllListeners();
            _filterButton.onClick.RemoveAllListeners();
            _userButton.onClick.RemoveAllListeners();
            _tutorialButton.onClick.RemoveAllListeners();
        }
    }
}