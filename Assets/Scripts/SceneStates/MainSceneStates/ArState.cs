using Assets.Scripts.MainWindows.Cart;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Engenious.MainScene.SceneStates.MainSceneStates
{
    public class ArState: BasicMainSceneState<DefaultSceneStateParams>
    {
        [SerializeField] private Button _cartButton;

        private CartWindow _cartWindow; 
        public override bool SetActivate(bool value)
        {
            if (base.SetActivate(value))
            {
                if (value)
                {
                    ActivateState();
                }
                else
                {
                    DeActivateState();
                    return false;
                }
            }

            return true;
        }

        private void ActivateState()
        {
            
        }

        private void DeActivateState()
        {
            _cartButton.onClick.RemoveAllListeners();
        }

        private void CartOnClick()
        {
            _cartWindow = StatesManager.WindowsManager.Show<CartWindow>();
            
        }
    }
}