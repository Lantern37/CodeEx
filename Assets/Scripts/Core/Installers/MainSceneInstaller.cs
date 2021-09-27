using System;
using Engenious.Core.Managers;
using Engenious.Core.WindowsController;
using Engenious.MainScene;
using UnityEngine;
using Zenject;

namespace Engenious.Core
{
    public class MainSceneInstaller : DefautlSceneMonoInstaller
    {
        [SerializeField] private MainSceneObjects _mainSceneObjects;
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.Bind<IMainSceneContainer>().To<MainSceneContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainSceneObjects>().FromInstance(_mainSceneObjects).AsSingle();
        }
    }
}