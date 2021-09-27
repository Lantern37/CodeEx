
namespace Engenious.MainScene.ViewModels
{
    public interface IRegistrationVM
    {
        AccountCreatedVM AccountCreatedVm { get; }
        ConfirmNumberVM ConfirmNumberVm { get; }
        ProvidePhoneVM ProvidePhoneVm { get; }
        SignUpVM SignUpVm { get; }
        SignUpProgressVM SignUpProgressVm { get; }
        VerifyIdentityVM VerifyIdentityVm { get; }
        
        bool IsInited { get; }
        void Initialize();
    }

    public class RegistrationVM : IRegistrationVM
    {
        public AccountCreatedVM AccountCreatedVm { get; private set; }
        public ConfirmNumberVM ConfirmNumberVm { get; private set;}
        public ProvidePhoneVM ProvidePhoneVm { get; private set;}
        public SignUpVM SignUpVm { get; private set;}
        public SignUpProgressVM SignUpProgressVm { get; private set;}
        public VerifyIdentityVM VerifyIdentityVm { get; private set;}

        public bool IsInited { get; private set; }

        private IMainSceneContainer _mainSceneContainer;

        public RegistrationVM(){}
        public RegistrationVM(IMainSceneContainer mainSceneContainer)
        {
            _mainSceneContainer = mainSceneContainer;
        }

        public void Initialize()
        {
            if(IsInited)
                return;

            CreateReferences();
            InitVMs();
            
            IsInited = true;
        }

        private void CreateReferences()
        {
            AccountCreatedVm = new AccountCreatedVM();
            SignUpProgressVm = new SignUpProgressVM();
            VerifyIdentityVm = new VerifyIdentityVM();
            
            ConfirmNumberVm = new ConfirmNumberVM(_mainSceneContainer.MainSceneModels.ProvidePhoneModel);
            
            SignUpVm = new SignUpVM(_mainSceneContainer.MainSceneModels.SignUpModel,
                _mainSceneContainer.MainSceneServices.ValidationService);
            
            ProvidePhoneVm = new ProvidePhoneVM(_mainSceneContainer.MainSceneModels.ProvidePhoneModel);
        }

        private void InitVMs()
        {
            AccountCreatedVm.Initialize();
            ConfirmNumberVm.Initialize();
            ProvidePhoneVm.Initialize();
            SignUpVm.Initialize();
            SignUpProgressVm.Initialize();
            VerifyIdentityVm.Initialize();
        }
    }
}