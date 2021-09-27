using System;
using System.Collections.Generic;
using Engenious.Core.Managers;
using Engenious.MainScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    //TO DO: make view model for this window
    public class ProductDescriptionWindow : WindowController
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;

        [SerializeField] private TextMeshProUGUI _thc;
        [SerializeField] private TextMeshProUGUI _sort;

        [SerializeField] private Button _addToCart;

        [Space(10)] 
        [SerializeField]private RectTransform _effectParent;
        
        [SerializeField]private ProductEffectView _effectViewPrefab;

        private List<ProductEffectView> _effectViews;

        //TO DO: private ProductDecriptionViewData _data;
        private ProductResponse _data;

        public override bool Top
        {
            get { return false; }
        }

        public event Action<ProductResponse> AddToCart;
        //
        // public void Show()
        // {
        //     gameObject.SetActive(true);
        // } 
        //
        // public void Close()
        // {
        //     _addToCart.onClick.RemoveAllListeners();
        //     _name.text = string.Empty;
        //     _price.text = string.Empty;
        //
        //     _data = null;
        //     
        //     DesroyAllEffects();
        //     
        //     gameObject.SetActive(false);
        // }

        protected override void Show(params object[] _params)
        {
            base.Show(_params);
        }

        protected override void Closed()
        {
            base.Closed();
            _addToCart.onClick.RemoveAllListeners();
            _name.text = string.Empty;
            _price.text = string.Empty;

            _data = null;
            
            DesroyAllEffects();
        }

        public void SetData(ProductResponse data)
        {
            if(data == null)
                return;
            
            if(data.Name.Equals(_name.text))
                return;

            _data = data;
            
            _effectViews = new List<ProductEffectView>();
            _name.text = data.Name;
            _price.text = "Price: "+ data.Price.ToString() + "$";
            _thc.text = data.Thc + "%";
            _sort.text = data.Strain.Name;
            
            _addToCart.onClick.AddListener(SubscribeAddToCartButton);
            
            DesroyAllEffects();
            
            foreach (var effect in data.Effects)
            {
                CreateEffectView(effect.Name);
            }
        }

        private void SubscribeAddToCartButton()
        {
            AddToCart?.Invoke(_data);
            //_addToCart.onClick.RemoveAllListeners();
        }

        private void CreateEffectView(string effect)
        {
            var item = Instantiate(_effectViewPrefab, _effectParent);
            _effectViews.Add(item);
            item.SetType(effect);
        }
        
        private void DestroyEffectView(ProductEffectView view)
        {
            if (_effectViews.Contains(view))
            {
                _effectViews.Remove(view);
                view.Delete();
            }
        }

        private void DesroyAllEffects()
        {
            if (_effectViews != null && _effectViews.Count > 0)
            {
                for (int i = _effectViews.Count-1; i >=0; i--)
                {
                    DestroyEffectView(_effectViews[i]);
                }
            }
        }
    }
}