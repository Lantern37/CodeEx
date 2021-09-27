using System;
using Engenious.Core.Managers;
using Engenious.MainScene.SignUp;
using Engenious.MainScene.ViewModels;
using Engenious.WindowControllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainWindows
{
    public class VerifyIdentityWindow : WindowController
    {
        [SerializeField] private UploadButton _passportPhoto;
        [SerializeField] private UploadButton _physicianPhoto;

        [SerializeField] private LoadingBack _loadingBack;

        [SerializeField] private Button _next;

        [SerializeField] private RawImage _image;
        // public Button PassportPhoto => _passportPhoto;
        // public Button PhysicianPhoto => _physicianPhoto;
        // public Button Next => _next;

        private IVerifyIdentityModel _vm;

        public event Action PressNextButton;
        
        protected override void Show(params object[] _params)
        {
            base.Show(_params);

            SetNextButtonInteractable(false);

            _vm = (IVerifyIdentityModel) _params[0];

            _vm.StartPutPassport += StartPutPassport;
            _vm.StartPutPhysician += StartPutPhysician;
            
            _vm.SuccessPutPassport += SuccessPutPassport;
            _vm.SuccessPutPhysician += SuccessPutPhysician;
            
            _next.onClick.AddListener((() =>NextButton()));

            _passportPhoto.UploadImageState();
            _physicianPhoto.UploadImageState();

            _passportPhoto.AddListener((() => _vm.PutPassportPhoto()));
            _physicianPhoto.AddListener((() => _vm.PutPhysicianPhoto()));
            
            _loadingBack.StopAnimation();
        }

        private void StartPutPassport()
        {
            _passportPhoto.ProcessUploadImageState();
        }

        private void StartPutPhysician()
        {
            _physicianPhoto.ProcessUploadImageState();
        }

        private void SuccessPutPassport(Texture2D text)
        {
            //_image.texture = text;
            _passportPhoto.EndUploadImageState(text);
            SetNextButtonInteractable(true);
        }

        private void SuccessPutPhysician(Texture2D obj)
        {
            _physicianPhoto.EndUploadImageState(obj);
        }

        protected override void Closed()
        {
            UnSubscribeButtons();
            base.Closed();
        }

        public void SetPassportButtonInteractable(bool active)
        {
            _passportPhoto.Interactable = active;
        }
        
        public void SetPhysicianButtonInteractable(bool active)
        {
            _physicianPhoto.Interactable = active;
        }

        public void SetNextButtonInteractable(bool active)
        {
            _next.interactable = active;
        }

        private void NextButton()
        {
            _loadingBack.StartAnimation();
            _next.interactable = false;
            _vm.PutData(Send);
            //_vm.SendData += Send;
        }

        private void Send()
        {
            //_vm.SendData -= Send;
            _next.interactable = true;
            _loadingBack.StopAnimation();
            PressNextButton?.Invoke();
        }
        
        private void UnSubscribeButtons()
        {
            PressNextButton = null;
            _vm.SuccessPutPassport -= SuccessPutPassport;
            _passportPhoto.RemoveAllListeners();
            _physicianPhoto.RemoveAllListeners();
            _next.onClick.RemoveAllListeners();
        }
    }
}