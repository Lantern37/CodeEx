using System.Threading.Tasks;
using Assets.Scripts.MainWindows;
using Assets.Scripts.MainWindows.Cart;
using Assets.Scripts.UserDataScripts;
using Engenious.MainScene.Models;
using Engenious.MainScene.SignUp;

namespace Engenious.MainScene
{
    public interface IMainSceneModels
    {
        ILogInModel LogInModel { get; }

        ISignUpModel SignUpModel { get; }
        
        IProvidePhoneModel ProvidePhoneModel { get; }
        ICartModel CartModel { get; }
        
        IVerifyIdentityModel VerifyIdentityModel { get; }
        
        IUserDataController UserDataController { get; }
        
        ISavedCartController SavedCartController { get; }
        bool IsInited { get; }
        
        Task Initialize();
    }

    public class MainSceneModels : IMainSceneModels
    {
        public ILogInModel LogInModel { get; private set; }
        public ISignUpModel SignUpModel { get; private set; }
        public IProvidePhoneModel ProvidePhoneModel { get; private set; }
        public ICartModel CartModel { get; private set; }
        public IVerifyIdentityModel VerifyIdentityModel { get; private set; }
        public IUserDataController UserDataController { get; private set;}
        public ISavedCartController SavedCartController { get; private set;}

        private IMainSceneContainer _mainContainer;

        public MainSceneModels()
        {
            
        }
        
        public MainSceneModels(IMainSceneContainer mainContainer)
        {
            _mainContainer = mainContainer;
        }

        public bool IsInited { get; private set; }

        public async Task Initialize()
        {
            if (!IsInited)
            {
                LogInModel = new LogInModel();
                
                UserDataController = new UserDataController(LogInModel, _mainContainer.MainSceneServices.PushNotificationManager);
                SavedCartController = new SavedCartController();
                LogInModel.Init(_mainContainer.CoreApi.NetworkManager.NetworkUserRequests,
                    _mainContainer.CoreApi.NetworkManager.UserIdHolder,
                    _mainContainer.MainSceneModels.UserDataController);

                SignUpModel = new SignUpModel(_mainContainer.CoreApi.NetworkManager.NetworkUserRequests, LogInModel, 
                    _mainContainer.CoreApi.NetworkManager.UserIdHolder,
                    _mainContainer.MainSceneModels.UserDataController);
                ProvidePhoneModel = new ProvidePhoneModel(_mainContainer.CoreApi.NetworkManager.NetworkUserRequests, 
                    UserDataController);
                
                
                CartModel = new CartModel();

                VerifyIdentityModel = new VerifyIdentityModel(_mainContainer.MainSceneServices.NativeGallaryWrapper,
                                                              _mainContainer.CoreApi.NetworkManager.NetworkUserRequests,
                                                              _mainContainer.CoreApi.NetworkManager.UserIdHolder);
                
                IsInited = true;
                
                ProvidePhoneModel.Initialize();
                UserDataController.Initialize();
                SavedCartController.Initialize();
            }
        }
    }
}