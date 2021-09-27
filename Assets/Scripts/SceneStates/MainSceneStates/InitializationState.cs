using Assets.Scripts.MainWindows;
using Engenious.Core.Managers;
using UnityEngine;

namespace Engenious.MainScene.SceneStates.MainSceneStates
{
    public class InitializationState : BasicMainSceneState<DefaultSceneStateParams>
    {
        private LogoWindow _logoWindow;

        [SerializeField] private bool _startWelcomeAnyway;

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
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            StatesManager.WindowsManager.Init();
            
            CoreInitialize();
            Debug.Log("token = " + StatesManager.CoreApi.NetworkManager.AccessTokenHolder.CurrentToken);

            //StatesManager.CoreApi.NetworkManager.ConnectionStateChange += OnConnectionChange;
            //StatesManager.CoreApi.NetworkManager.OnConnect += () => Connect();

        }

        private void OnCloseWindow()
        {
            ToWelcomeState();
        }

        private void ToWelcomeState()
        {
            if (StatesManager.CoreApi.NetworkManager.UserIdHolder.IsUserIdEmpty() || _startWelcomeAnyway)
            {
                StatesManager.ActivateState<WelcomeState>(new DefaultSceneStateParams());
                StatesManager.DeactivateState<InitializationState>();
            }
            else
            {
                StatesManager.MainSceneContainer.MainSceneModels.LogInModel.SendLoginDataId(
                    int.Parse(StatesManager.CoreApi.NetworkManager.UserIdHolder.UserId), responce =>
                    {
                        StatesManager.ActivateState<MainPageState>(new DefaultSceneStateParams());
                        StatesManager.DeactivateState<InitializationState>();

                    });
            }
        }

        private void DeActivateState()
        {
            
        }
        
        private async void CoreInitialize()
        {
            await StatesManager.CoreApi.Initialize();
            await StatesManager.MainSceneContainer.Initialize();
            
            _logoWindow = StatesManager.WindowsManager.Show<LogoWindow>();
            _logoWindow.OnClosed += OnCloseWindow;
            

            //StatesManager.WindowsManager.Close<LogoWindow>();
            //ToWelcomeState();
        }
        
        private void Connect()
        {
            ToWelcomeState();
        }
        
        private void OnConnectionChange(NetworkType connectionType)
        {
            if (connectionType == NetworkType.Disable)
            {
                //Show poput or something
            }
            else
            {
                //Close popup
            }
        }
    }
}