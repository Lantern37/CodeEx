using Zenject;

namespace Assets.Scripts.Test
{
    public class TestSceneMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITestSceneCont>().To<TestSceneCont>().AsSingle();
        }
    }
}