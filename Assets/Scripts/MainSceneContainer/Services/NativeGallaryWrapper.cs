using System;
using UnityEngine;

namespace Engenious.MainScene.Services
{
    public interface INativeGallaryWrapper
    {
        void PickImage(int maxSize, Action<Texture2D, string> successCallback = null, Action failCallback = null);
    }

    public class NativeGallaryWrapper : INativeGallaryWrapper
    {
        public void PickImage(int maxSize, Action<Texture2D, string> successCallback = null, Action failCallback = null)
        {
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
            {
                Debug.Log("Image path: " + path);
                if (path != null)
                {
                    // Create Texture from selected image
                    Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                    if (texture == null)
                    {
                        Debug.Log("Couldn't load texture from " + path);
                        failCallback?.Invoke();
                        return;
                    }
                    
                    successCallback?.Invoke(texture, path);
                }
            }, "Select a PNG image", "image/*");
        }
    }
}