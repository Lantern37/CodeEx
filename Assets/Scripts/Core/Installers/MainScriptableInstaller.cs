using Engenious.Core.Managers;
using Engenious.MainScene.Configs;
using UnityEngine;
using Zenject;

namespace Engenious.Core
{
    [CreateAssetMenu(fileName = "MainScriptableInstaller", menuName = "Engenious/MainScriptableInstaller")]
    public class MainScriptableInstaller : ScriptableObjectInstaller<MainScriptableInstaller>
    {
        [SerializeField] private MainSceneContainerConfig _mainSceneContainerConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainSceneContainerConfig>().FromInstance(_mainSceneContainerConfig).AsSingle();
        }
    }
}