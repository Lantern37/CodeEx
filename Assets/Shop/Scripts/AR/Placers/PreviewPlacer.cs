using DG.Tweening;
using UnityEngine;

namespace Shop.Behaviours.AR
{
    public class PreviewPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _gfx;
        [Space]
        [SerializeField] private float _moveDuration = 0.4f;
        [SerializeField] private float _fadeDuration = 0.4f;
        [SerializeField] private Vector3 _moveOffset = new Vector3(0f, 0.01f, 0f);
        [SerializeField] private bool _showOnAwake = false;


        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
        
        private Transform _transform;
        public Vector3 Position => _transform.position;

        private void Awake()
        {
            _transform = transform;
            IsActive = _showOnAwake;
        }


        public void Move(Vector3 point)
        {
            _transform.DOMove(point + _moveOffset,
                _moveDuration);

            if (!IsActive)
            {
                Show();
            }
        }

        public void Show()
        {
            IsActive = true;
            _gfx.SetActive(IsActive);
        }
        
        public void Hide()
        {
            _gfx.SetActive(IsActive);
            IsActive = false;
        }
    }
}


