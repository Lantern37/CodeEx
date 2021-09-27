using UnityEngine;
using Zenject;

namespace Assets.Scripts.Test
{
    public class TestProjectMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITestProjectCont>().To<TestProjectCont>().AsSingle();
        }
    }
}