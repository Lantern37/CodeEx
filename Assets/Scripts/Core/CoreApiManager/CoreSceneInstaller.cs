using Engenious.Core.Managers;
using Engenious.Core.WindowsController;
using Zenject;

namespace Engenious.Core
{
    public class CoreSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WindowsManager>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneStatesManager>().FromComponentsInHierarchy().AsSingle();
        }
    }
}