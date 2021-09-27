using DG.Tweening;
using UnityEngine;

namespace Shop
{
   
    public class ShowItem : MonoBehaviour
    {
        [SerializeField] private GameObject _plasmaFX;
        public string lable;

        private const string ITEM_SHOW = "box_opening";
        private const string ITEM_HIDE = "box_closing";
   
        private string _currentState;
        private AnimatorState _currentAnimatorStateState;

        public AnimatorState CurrentAnimatorStateState => _currentAnimatorStateState;

        private Animator _animator;
   
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentAnimatorStateState = AnimatorState.NONE;
        }

        public void ShowAnimation()
        {
            Debug.Log("ShowAnimation" + lable);
            ChangeAnimationState(ITEM_SHOW);
            _currentAnimatorStateState = AnimatorState.SHOW;
        }
        
        public void HideAnimation()
        {
            Debug.Log("HideAnimation" + lable);
            ChangeAnimationState(ITEM_HIDE);
            _currentAnimatorStateState = AnimatorState.HIDE;
            HidePlasma();
            DOVirtual.DelayedCall(1, () => _currentAnimatorStateState = AnimatorState.NONE);
        }

        void ShowPlasma()
        {
            _plasmaFX.SetActive(true);
            _plasmaFX.transform.localScale = Vector3.zero;
            _plasmaFX.transform.DOScale(new Vector3(1, 1, 1), 1);
        }
        
        public void HidePlasma()
        {
            _plasmaFX.transform.DOScale(new Vector3(0, 0, 0), 1);
            DOVirtual.DelayedCall(0.3f, ()=> _plasmaFX.SetActive(true));
        }

        void ChangeAnimationState(string newState)
        {
            Debug.Log("Anim selected" + lable);
            if (_currentState == newState) return;
            _animator.Play(newState);
            _currentState = newState;
        }
   
    }

    public enum AnimatorState
    {
        NONE,
        SHOW,
        HIDE
    }
}
