using Assets.Scripts.MainWindows;
using Engenious.Core.Managers;
using Engenious.MainScene.Models;
using UnityEngine;

namespace Engenious.MainScene.SceneStates.MainSceneStates
{
    public class LogInState : BasicMainSceneState<DefaultSceneStateParams>
    {
        private LogInWindow _loginWindow;
        
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
            _loginWindow = StatesManager.WindowsManager.Show<LogInWindow>();

            var logInVm = StatesManager.MainSceneContainer.MainSceneViewModels.LogInVm;
            
            logInVm.SetWindow(_loginWindow);
            logInVm.SubscribeButtons(OnSuccessLogIn, OnBack);
        }
        
        private void DeActivateState()
        {
            StatesManager.WindowsManager.Close<LogInWindow>();
        }

        private void OnBack()
        {
            StatesManager.DeactivateState<LogInState>();
            StatesManager.ActivateState<WelcomeState>(new DefaultSceneStateParams());
        }

        private void OnSuccessLogIn(LoginUserResponce user)
        {
            StatesManager.DeactivateState<LogInState>();
            StatesManager.ActivateState<MainPageState>(new DefaultSceneStateParams());
        }
    }
}