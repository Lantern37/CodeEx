using System;
using System.Threading.Tasks;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Utilities
{
    public static class ImageLoader
    {
        public async static void GetSprite(string path, Action<Sprite> success)
        {
            if (path.Equals(String.Empty))
            {
                return;
            }

            Texture2D webTexture = await GetTextureByUrl(path);
            if (webTexture != null)
            {
                Sprite webSprite = SpriteFromTexture2D (webTexture);
                success?.Invoke(webSprite);
            }
          
            Debug.Log("Path = " + path);
        }

        private static Sprite SpriteFromTexture2D(Texture2D texture) 
        {
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        
        public static async Task<Texture2D> GetTextureByUrl(string url, Action<float> progress = null)
        {
            Debug.Log("Get texture by url ...");

            using (var request = UnityWebRequestTexture.GetTexture(url, true))
            {
                using (var asyncOperation = await GetResponse(request, progress))
                {
                    if (IsSuccessfull(asyncOperation))
                    {
                        return ((DownloadHandlerTexture) asyncOperation.downloadHandler).texture;
                    }
                    else
                    {
                        throw new NetworkRequestException(asyncOperation.error,
                            $"Get texture by url error.\n URL: {url}");
                    }
                }
            }
        }
        
        public static async UniTask<UnityWebRequest> GetResponse(UnityWebRequest req, Action<float> progress = null)
        {
            var  result = req.SendWebRequest();

            await UniTask.WaitUntil(() =>
            {
                if (!req.isDone)
                {
                    //progress?.Invoke(req.method == "POST" ? req.uploadProgress : req.downloadProgress);
                    return false;
                }

                return true;
            });
            return req;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsSuccessfull(UnityWebRequest request)
        {
            if (request.isHttpError || request.isNetworkError || !string.IsNullOrEmpty(request.error))
                return false;
            return true;
        }
    }
}