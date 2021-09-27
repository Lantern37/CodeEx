using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Test
{
    public class TestNativeGallary : MonoBehaviour
    {
        [SerializeField] private RawImage _image;
        [SerializeField] private Button _loadImage;

        private void Awake()
        {
            _loadImage.onClick.AddListener((LoadImage));
        }

        private void OnDestroy()
        {
            _loadImage.onClick.RemoveAllListeners();
        }
        
        private void LoadImage()
        {
            PickImage(Int32.MaxValue, texture2D => _image.texture = texture2D);
        }
        
        public void PickImage(int maxSize, Action<Texture2D> successCallback = null, Action failCallback = null)
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
                    
                    successCallback?.Invoke(texture);
                }
            }, "Select a PNG image", "image/png");
        }
    }
}