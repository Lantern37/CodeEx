using UnityEngine;

namespace Engenious.WindowControllers
{
    public class LoadingBack : MonoBehaviour
    {
        [SerializeField] private LoadingAnimation _loadingAnimation;
        
        public void StartAnimation()
        {
            _loadingAnimation.StartAnimation();
            gameObject.SetActive(true);    
        }

        public void StopAnimation()
        {
            _loadingAnimation.StopAnimation();
            gameObject.SetActive(false);
        }
    }
}