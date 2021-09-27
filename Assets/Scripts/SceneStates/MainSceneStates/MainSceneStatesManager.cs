using Engenious.Core.Managers;
using Zenject;

namespace Engenious.MainScene.SceneStates.MainSceneStates
{
    public class MainSceneStatesManager : DefaultSceneStatesManager
    {
        
        [field: Inject]
        public IMainSceneContainer MainSceneContainer { get; }
        protected override void ChildInitialize()
        {
            base.ChildInitialize();
            ActivateState<InitializationState>(new DefaultSceneStateParams());
        }

        protected override void Update()
        {
            base.Update();
            
            MainSceneContainer.Update();
        }
    }
}