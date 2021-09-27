using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Shop;
using Shop.Core.Utils;
using UnityEngine;

namespace Shop
{
    
    public class SetLabelsControl : MonoBehaviour
    {
        [SerializeField] private ShowItem[] _showBoxSet;
        
        private const string SET_SHOW = "show_lables";
        private const string SET_HIDE = "hide_lables";
        private string _currentState;
        private string _currentLabel;
        private Animator _animator;

        private bool _selectionInProcess = false;
        private string _labelA = "A";
        private string _labelB = "B";
        private string _labelC = "C";
        private string _labelD = "D";
        private string _labelE = "E";
        private string _labelF = "F";
        private string _labelG = "G";
    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    
        public async void BoxSelected(string lable)
        {
            if (_currentLabel == lable 
                || _selectionInProcess
                )
            {
                return;
            }
            Debug.Log("Lable selected///////    " + lable);
            
            _selectionInProcess = true;
            for (int i = 0; i < _showBoxSet.Length; i++)
            {
                var itemToShow = _showBoxSet[i];
                if (itemToShow.gameObject.activeSelf)
                {
                    if (itemToShow.CurrentAnimatorStateState == AnimatorState.SHOW)
                    {
                        itemToShow.HideAnimation();
                        await UniTaskUtils.Delay(3);
                    }

                    HideAnimation();
                    await UniTaskUtils.Delay(0.5f);
                    itemToShow.gameObject.SetActive(false);
                }
                
                // var showItemLabel = itemToShow.lable;
                // if (showItemLabel == lable)
                // {
                //     Debug.Log("Lable show  " + showItemLabel);
                //
                //     itemToShow.gameObject.SetActive(true);
                //     ShowAnimation();
                //     await UniTaskUtils.Delay(1);
                //     itemToShow.ShowAnimation();
                //     return;
                // }
            }
            
            for (int i = 0; i < _showBoxSet.Length; i++)
            {
                var itemToShow = _showBoxSet[i];
                // if (itemToShow.gameObject.activeSelf)
                // {
                //     if (itemToShow.CurrentAnimatorStateState == AnimatorState.SHOW)
                //     {
                //         itemToShow.HideAnimation();
                //         await UniTaskUtils.Delay(1);
                //     }
                //
                //     HideAnimation();
                //     await UniTaskUtils.Delay(1);
                //     itemToShow.gameObject.SetActive(false);
                // }
                
                var showItemLabel = itemToShow.lable;
                if (showItemLabel == lable)
                {
                    Debug.Log("Lable show  " + showItemLabel);

                    itemToShow.gameObject.SetActive(true);
                    ShowAnimation();
                    await UniTaskUtils.Delay(0.5f);
                    itemToShow.ShowAnimation();
                    AudioSystem.Instance.PlayOneShot(AudioClips.PowerUp);

                    _selectionInProcess = false;
                    return;
                }
            }

        }
        
        public void ShowAnimation()
        {
            ChangeAnimationState(SET_SHOW);
        }
        
        public void HideAnimation()
        {
            ChangeAnimationState(SET_HIDE);
        }
        
        void ChangeAnimationState(string newState)
        {
            if (_currentState == newState) return;
      
            _animator.Play(newState);
            _currentState = newState;
        }
    
    }

}