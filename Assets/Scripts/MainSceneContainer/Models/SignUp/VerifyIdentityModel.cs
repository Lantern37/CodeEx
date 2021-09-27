using System;
using Engenious.Core.Managers;
using Engenious.Core.Managers.Requests;
using Engenious.MainScene.Services;
using UnityEngine;

namespace Engenious.MainScene.SignUp
{
    public interface IVerifyIdentityModel
    {
        event Action StartPutPassport;
        event Action StartPutPhysician;
        event Action FailPutPassport;
        event Action FailPutPhysician;
        event Action<Texture2D> SuccessPutPassport;
        event Action<Texture2D> SuccessPutPhysician;
        event Action SendData;
        void PutPassportPhoto(int maxSize = Int32.MaxValue);
        void PutPhysicianPhoto(int maxSize = Int32.MaxValue);
        void ClearPutPassportRequest();
        void PutData(Action callback = null);
    }

    public class VerifyIdentityModel : IVerifyIdentityModel
    {
        private INativeGallaryWrapper _nativeGallery;
        private INetworkUserRequests _userRequests;
        private UserIdHolder _userIdHolder;
        
        public event Action StartPutPassport;
        public event Action StartPutPhysician;
        
        public event Action FailPutPassport;
        public event Action FailPutPhysician;

        public event Action<Texture2D> SuccessPutPassport;
        public event Action<Texture2D> SuccessPutPhysician;

        public event Action SendData;

        private PutPassportRequest _person;
        private IVerifyIdentityModel _verifyIdentityModelImplementation;

        public VerifyIdentityModel(INativeGallaryWrapper nativeGallery, INetworkUserRequests userRequests, UserIdHolder userIdHolder)
        {
            _nativeGallery = nativeGallery;
            _userRequests = userRequests;
            _userIdHolder = userIdHolder;

            _person = new PutPassportRequest();
        }
        
        public void PutPassportPhoto(int maxSize = 512)
        {
            StartPutPassport?.Invoke();
            _nativeGallery.PickImage(maxSize, SuccessPassportCallback, FailPassportCallback);
        }

        public void PutPhysicianPhoto(int maxSize = 512)
        {
            StartPutPhysician?.Invoke();
            _nativeGallery.PickImage(maxSize, SuccessPhysicianCallback, FailPhysicianCallback);
        }

        public async void PutData(Action callback = null)
        {
            await _userRequests.PutPassportMultipart(_person, int.Parse(_userIdHolder.UserId));
            
            callback?.Invoke();
            SendData?.Invoke();
        }
        
        public void ClearPutPassportRequest()
        {
            _person = new PutPassportRequest();
        }

        private void SuccessPassportCallback(Texture2D texture, string path)
        {
            Texture2D copyTexture = GetReadTextureCopy(texture);

            //_person.PassportPhotoData.SetData(GetTextureCopy(texture), path);
            _person.PassportPhoto = copyTexture.EncodeToPNG();

            SuccessPutPassport?.Invoke(copyTexture);
        }

        private void FailPassportCallback()
        {
            FailPutPassport?.Invoke();
        }

        private void SuccessPhysicianCallback(Texture2D texture, string path)
        {
            Texture2D copyTexture = GetReadTextureCopy(texture);
            //_person.PhysicianPhotoData.SetData(GetTextureCopy(texture), path);
            _person.PhysicianPhoto = copyTexture.EncodeToPNG();
            
            SuccessPutPhysician?.Invoke(copyTexture);
        }

        private void FailPhysicianCallback()
        {
            FailPutPhysician?.Invoke();
        }

        private Texture2D GetTextureCopy (Texture2D source)
        {
            byte[] pix = source.GetRawTextureData();
            Texture2D readableText = new Texture2D(source.width, source.height, source.format, false);
            readableText.LoadRawTextureData(pix);
            readableText.Apply();
            return readableText;
        }
        
        private Texture2D GetReadTextureCopy (Texture2D texture)
        {
            // Create a temporary RenderTexture of the same size as the texture
            RenderTexture tmp = RenderTexture.GetTemporary( 
                texture.width,
                texture.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

// Blit the pixels on texture to the RenderTexture
            Graphics.Blit(texture, tmp);
// Backup the currently set RenderTexture
            RenderTexture previous = RenderTexture.active;
// Set the current RenderTexture to the temporary one we created
            RenderTexture.active = tmp;
// Create a new readable Texture2D to copy the pixels to it
            Texture2D myTexture2D = new Texture2D(texture.width, texture.height);
// Copy the pixels from the RenderTexture to the new Texture
            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.Apply();
// Reset the active RenderTexture
            RenderTexture.active = previous;
// Release the temporary RenderTexture
            RenderTexture.ReleaseTemporary(tmp);

// "myTexture2D" now has the same pixels from "texture" and it's readable.
            return myTexture2D;
        }
    }
}