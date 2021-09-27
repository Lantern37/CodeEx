using Engenious.Core.Managers;
using Engenious.Core.WindowsController;
using UnityEngine;
using Zenject;

namespace Engenious.Core
{
    public class DefautlSceneMonoInstaller : MonoInstaller
    {
        [SerializeField] private WindowsManager _windowsManager; 
        [SerializeField] private SceneStatesManager _sceneStatesManager; 

        public override void InstallBindings()
        {
            if (_windowsManager == null)
            {
               Container.BindInterfacesAndSelfTo<WindowsManager>().FromComponentsInHierarchy().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<WindowsManager>().FromInstance(_windowsManager).AsSingle();
            }
            
            if (_sceneStatesManager == null)
            {
                 Container.BindInterfacesAndSelfTo<SceneStatesManager>().FromComponentsInHierarchy().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<SceneStatesManager>().FromInstance(_sceneStatesManager).AsSingle();
            }
        }
    }
}