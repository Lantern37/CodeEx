using System.Collections;
using System.Collections.Generic;
using Engenious.Core.Managers;
using UnityEngine;
using Zenject;

namespace  Engenious.Core
{
    public class ProjectCoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IARManager>().To<ARManager>().AsSingle();
            Container.Bind<INetworkManager>().To<NetworkManager>().AsSingle();
            Container.Bind<INetworkChecker>().To<NetworkChecker>().AsSingle();
            Container.Bind<IResourcesManager>().To<ResourceManagerNoCached>().AsSingle();
            Container.Bind<ICoreApiManager>().To<CoreApiManager>().AsSingle();
        }
    }
}

