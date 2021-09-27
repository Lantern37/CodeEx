using System;
using UnityEngine;

namespace Engenious.Core.Managers
{
    public interface ILoadCartIcon
    {
        void GetSprite(string path, Action<Sprite> success);
    }

    public class LoadCartIcon : ILoadCartIcon
    {
        private IResourcesManager _manager;

        public LoadCartIcon(IResourcesManager manager)
        {
            _manager = manager;
        }
        
        public async  void GetSprite(string path, Action<Sprite> success)
        {
            if (path.Equals(String.Empty))
            {
                return;
            }

            var networkConfig = _manager.Network.Config;
            string fullPath = networkConfig.BaseURL + networkConfig.ProductIcon + path;
            
            Texture2D webTexture = await _manager.GetTextureByUrl(fullPath);

            if (webTexture != null)
            {
                Sprite webSprite = SpriteFromTexture2D (webTexture);
                success?.Invoke(webSprite);
                Debug.Log("Path = " + fullPath);
            }
        }

        private static Sprite SpriteFromTexture2D(Texture2D texture) 
        {
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}