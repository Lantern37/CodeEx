using Assets.Scripts.MainSceneContainer.ViewModels;
using Assets.Scripts.MainWindows.Cart;

namespace Engenious.MainScene.ViewModels
{
    public interface IMainSceneViewModels
    {
        IRegistrationVM RegistrationVm { get; }
        WelcomeVM WelcomeVm { get; }
        LogInVM LogInVm { get; }
        CartViewModel CartViewModel { get; }
        DeliveryVM DeliveryVM { get; } 
        bool IsInited { get; }
        SupportVM SupportVm { get; }
        UserDeliveryDetailsVM UserDeliveryDetailsVm { get; }

        void Initialize();
    }

    public class MainSceneViewModels : IMainSceneViewModels
    {
        public IRegistrationVM RegistrationVm { get; private set; }
        public WelcomeVM WelcomeVm { get; private set; }
        public LogInVM LogInVm { get; private set; }
        public DeliveryVM DeliveryVM { get; private set;}
        public CartViewModel CartViewModel { get; private set; }
        
        public UserDeliveryDetailsVM UserDeliveryDetailsVm { get; private set; }

        public SupportVM SupportVm { get; private set; }
        public bool IsInited { get; private set; }

        private IMainSceneContainer _mainSceneContainer;

        public MainSceneViewModels(){}
        public MainSceneViewModels(IMainSceneContainer mainSceneContainer)
        {
            _mainSceneContainer = mainSceneContainer;
        }

        public void Initialize()
        {
            if(IsInited)
                return;

            CreateRefarences();
            InitVMs();
            
            IsInited = true;
        }

        private void CreateRefarences()
        {
            RegistrationVm = new RegistrationVM(_mainSceneContainer);

            SupportVm = new SupportVM( _mainSceneContainer.CoreApi.NetworkManager.SupportNetwork);

            UserDeliveryDetailsVm = new UserDeliveryDetailsVM(_mainSceneContainer.MainSceneModels.UserDataController,
                                                    _mainSceneContainer.CoreApi.NetworkManager.AddressRequests);
            
            WelcomeVm = new WelcomeVM();
            LogInVm = new LogInVM(_mainSceneContainer.MainSceneModels.LogInModel, 
                                  _mainSceneContainer.MainSceneServices.ValidationService);

            CartViewModel = new CartViewModel(_mainSceneContainer.MainSceneModels.CartModel,
                                              _mainSceneContainer.CoreApi.NetworkManager.NetworkCartRequests,
                                              _mainSceneContainer.CoreApi.ResourcesManager.LoadCartIcon,
                                              _mainSceneContainer.MainSceneModels.SavedCartController);

            DeliveryVM = new DeliveryVM(_mainSceneContainer.CoreApi.NetworkManager.NetworkCartRequests, 
                                        _mainSceneContainer.CoreApi.NetworkManager.AddressRequests,                        
                                        CartViewModel,
                                        _mainSceneContainer.MainSceneModels.UserDataController);
        }

        private void InitVMs()
        {
            RegistrationVm.Initialize();
            LogInVm.Initialize();
            WelcomeVm.Initialize();
        }
    }
}