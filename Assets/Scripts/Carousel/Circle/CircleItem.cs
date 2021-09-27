using Assets.Shop.Scripts.Lable;
using DG.Tweening;
using Shop.Core;
using UnityEngine;

namespace Assets.Scripts.Carousel
{
    public class CircleItem : MonoBehaviour, ISelectable
    {
        private Item _item;
        public Item Item => _item;
        
        private bool _isSelected;
        public bool IsSelected => _isSelected;
        
        [SerializeField] private ItemDataWindow _dataWindow;

        private Vector3 _startScale;
        
        public void SetItem(Item item)
        {
            _item = item;
            _item.SetParent(transform);
            _item.EnableItem();
            HideDataWindow();
            _startScale = transform.localScale;

            EnableCircleItem();
        }

        public void SelectedByPoint()
        {
            //Debug.Log("SelectedByPoint " + this.name);
        }

        public void SelectedByScreenRay()
        {
            if(_isSelected)
                return;
            
            ShowDataWindow();

            _isSelected = true;
            
            var newScale = new Vector3(_startScale.x * 1.17f,_startScale.y * 1.17f,_startScale.z * 1.17f);
            
            transform.DOScale(newScale, 0.5f).SetEase(Ease.OutBounce);
        }

        public void UnSelectedByScreenRay()
        {
            if(!_isSelected)
                return;
                
            _isSelected = false;

            HideDataWindow();
            
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        }

        public void Delete()
        {
            //_item.Delete();
            _item.DisableItem();
            Destroy(gameObject);
        }

        public void EnableCircleItem()
        {
            gameObject.SetActive(true);
        }
        
        public void DisableCircleItem()
        {
            _item.DisableItem();
            HideDataWindow();
            gameObject.SetActive(false);
        }
        
        public void ShowDataWindow()
        {
            _dataWindow?.SetData(_item.ProductInfo.ProductData);
        }
        
        public void HideDataWindow()
        {
            _dataWindow?.Hide();
        }
    }
}