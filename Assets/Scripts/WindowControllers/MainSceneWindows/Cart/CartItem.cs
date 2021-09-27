using UnityEngine;

namespace Assets.Scripts.MainWindows.Cart
{
    public class CartItem : MonoBehaviour
    {
        private CartItemController _controller;
        public CartItemController Controller => _controller;

        private CartItemModel _model;
        public CartItemModel Model => _model;

        [SerializeField] private CartItemView _view;
        public CartItemView View => _view;
        
        public void Initialize()
        {
            _controller = new CartItemController(this);
            _model = new CartItemModel();
        }
    }
}