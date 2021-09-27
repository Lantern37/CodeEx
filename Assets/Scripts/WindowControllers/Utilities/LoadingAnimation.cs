using UnityEngine;

namespace Engenious.WindowControllers
{
    public class LoadingAnimation : MonoBehaviour
    {
        private RectTransform _rectComponent;
        [SerializeField] private float _rotateSpeed = 200f;

        private void Start()
        {
            _rectComponent = GetComponent<RectTransform>();
        }

        private void Update()
        {
            _rectComponent.Rotate(0f, 0f, _rotateSpeed * Time.deltaTime);
        }
        
        public void StartAnimation()
        {
            gameObject.SetActive(true);    
        }

        public void StopAnimation()
        {
            gameObject.SetActive(false);
        }
    }
}