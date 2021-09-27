using Engenious.Core.Managers;
using Engenious.MainScene.Configs;
using UnityEngine;
using Zenject;

namespace Engenious.Core
{
    public class CoreScriptableInstaller<T> : ScriptableObjectInstaller<T>
        where T : ScriptableObjectInstaller<T>
    {
        [SerializeField] private ARConfig ARConfig;
        [SerializeField] private CoreConfig CoreConfig;
        [SerializeField] private NetworkConfig NetworkConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ARConfig>().FromInstance(ARConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<CoreConfig>().FromInstance(CoreConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkConfig>().FromInstance(NetworkConfig).AsSingle();
        }
    }
}