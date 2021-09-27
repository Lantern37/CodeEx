using Assets.Scripts.MainWindows;
using Engenious.Core.Managers;
using UnityEngine;

namespace Engenious.MainScene.SceneStates.MainSceneStates
{
    public class WelcomeState : BasicMainSceneState<DefaultSceneStateParams>
    {
        private WelcomeWindow _welcomeWindow;
        
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
            Debug.Log("WelcomeState");

            _welcomeWindow = StatesManager.WindowsManager.Show<WelcomeWindow>();

            var welcomeVM = StatesManager.MainSceneContainer.MainSceneViewModels.WelcomeVm;
            welcomeVM.SetWindow(_welcomeWindow);
            welcomeVM.SubscribeButtons(LogInClick, SignUpClick);
        }
        
        private void DeActivateState()
        {
            StatesManager.WindowsManager.Close<WelcomeWindow>();
        }

        private void LogInClick()
        {
            StatesManager.DeactivateState<WelcomeState>();
            StatesManager.ActivateState<LogInState>(new DefaultSceneStateParams());
        }
        
        private void SignUpClick()
        {
            StatesManager.DeactivateState<WelcomeState>();
            StatesManager.ActivateState<SignUpState>(new DefaultSceneStateParams());
        }
    }
}