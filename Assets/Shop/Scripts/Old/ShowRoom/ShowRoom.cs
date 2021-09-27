using Shop.Core;
using Shop.Core.Utils;
using UnityEngine;

namespace Shop
{
    
    public class ShowRoom : MonoBehaviour
    {
        [SerializeField] private SetLabelsControl _labelControl;
        [SerializeField] private GameObject _circleFX;
        [SerializeField] private GameObject _smokeFX;
        [SerializeField] private GameObject _cure;
        [SerializeField] private GameObject _bloom;
        [SerializeField] private GameObject _plug;
        //Animations States
        private const string SHOW_ROOM_SHOW = "ShowRoom_Show";
   
        private string _currentState;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            ChangeAnimationState(SHOW_ROOM_SHOW);
        }

        void ChangeAnimationState(string newState)
        {
            if (_currentState == newState) return;
      
            _animator.Play(newState);
            _currentState = newState;
        }

        public void BoxSelected(string lable)
        {
            Debug.Log("BoxSelected");

            _labelControl.BoxSelected(lable);
            ShopManager.Instance.ItemInteracted();
        }

        public void ActiveFX(bool active)
        {
            // _smokeFX.SetActive(active);
            _circleFX.SetActive(active);
        }
        
        public async void ShowCurrentLableItems(LableType lableType)
        {
            Debug.Log("ShowCurrentLableItems " + lableType);

            Transform labelParent = _cure.transform;
            if (lableType == LableType.Bloom)
            {
                _bloom.SetActive(true);
                labelParent = _bloom.transform;
            }
            else if (lableType == LableType.Cure)
            {
                _cure.SetActive(true);
                // labelParent = _cure.transform;
            }

            foreach (Transform item in labelParent)
            {
                item.GetComponent<Item>().Show();
                AudioSystem.Instance.PlayOneShot(AudioClips.Pop);

                await UniTaskUtils.Delay(0.05f);
            }
        }

        // public void InteractionWithItem(string lable)
        // {
        //     _labelControl.BoxSelected(lable);
        // }
    }
}
