using System.Threading.Tasks;
using Engenious.MainScene.ViewModels;

namespace Engenious.MainScene.Services
{
    public interface IMainSceneServices
    {
        IFirebaseServices FirebaseServices { get; }
        IValidationService ValidationService { get; }
        INativeGallaryWrapper NativeGallaryWrapper { get; }
        IPushNotificationManager PushNotificationManager { get; }
        ISoundManager SoundManager { get; }
        bool IsInited { get; }

        Task Initialize();
    }

    public class MainSceneServices : IMainSceneServices
    {
        public IFirebaseServices FirebaseServices { get;  private set;}
        public IValidationService ValidationService { get; private set; }
        public INativeGallaryWrapper NativeGallaryWrapper { get; private set;}
        public IPushNotificationManager PushNotificationManager { get; private set; }
        public ISoundManager SoundManager { get; private set; }
        private IMainSceneContainer _mainSceneContainer;
        public bool IsInited { get; private set; }


        public MainSceneServices(){}
        public MainSceneServices(IMainSceneContainer mainSceneContainer)
        {
            _mainSceneContainer = mainSceneContainer;
        }

        public async Task Initialize()
        {
            if(IsInited)
                return;

            CreateRefarences();
            await Initreferences();
            
            IsInited = true;
        }

        private void CreateRefarences()
        {
            ValidationService = new ValidationService();
            FirebaseServices = new FirebaseServices();
            NativeGallaryWrapper = new NativeGallaryWrapper();
            PushNotificationManager = new PushNotificationManager();
            SoundManager = new SoundManager(_mainSceneContainer.Config.SoundManagerConfig,
                _mainSceneContainer.MainSceneObjects.AudioSource);
        }

        private async Task Initreferences()
        {
            await FirebaseServices.Initialization();
            PushNotificationManager.Start();
        }
    }
}