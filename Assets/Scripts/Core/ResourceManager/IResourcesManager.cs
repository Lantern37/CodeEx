using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Engenious.Core.Managers
{
    public interface IResourcesManager
    {
        // Resources
        INetworkManager Network { get; }
        ILoadCartIcon LoadCartIcon { get; }
        Task<Texture2D> GetTextureByUrl(string url, Action<float> progress = null);
        Task<AudioClip> GetAudioClipByUrl(string url, Action<float> progress = null);
        void Initialize();
    }

    public class ResourceManagerNoCached : IResourcesManager
    {
        [Inject]
        private INetworkManager _network;

        public INetworkManager Network
        {
            get => _network;
        }

        public ILoadCartIcon LoadCartIcon { get; private set; }

        public void Initialize()
        {
            LoadCartIcon = new LoadCartIcon(this);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
#if !UNITY_IOS
        public async Task<Texture2D> GetTextureByUrl(string url, Action<float> progress = null)
        {
            Debug.Log("Get texture by url ...");

            using (var request = UnityWebRequestTexture.GetTexture(url, true))
            {
                using (var asyncOperation = await _network.GetResponse(request, progress))
                {
                    if (_network.IsSuccessfull(asyncOperation))
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
#else
        private int _iterration = 0;
        public async Task<Texture2D> GetTextureByUrl(string url, Action<float> progress = null)
        {
            Debug.Log("Get texture by url ...");

            using (var request = UnityWebRequestTexture.GetTexture(url, true))
            {
                using (var asyncOperation = await _network.GetResponse(request, progress))
                {
                    if (_network.IsSuccessfull(asyncOperation))
                    {
                        var texture = ((DownloadHandlerTexture)asyncOperation.downloadHandler).texture;
                        // if (texture.width == 8 && texture.height == 8)
                        // {
                        //     _iterration++;
                        //     if (_iterration < 100)
                        //         return await GetTextureByUrl(url, progress);
                        //     _iterration = 0;
                        // }

                        return texture;
                    }
                    else
                    {
                        throw new NetworkRequestException(asyncOperation.error,
                            $"Get texture by url error.\n URL: {url}");
                    }
                }
            }
        }
#endif


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<AudioClip> GetAudioClipByUrl(string url, Action<float> progress = null)
        {
            Debug.Log("Get audio clip by url ...");

            AudioType type = AudioType.WAV;
            if (url.Contains(".mp3"))
                type = AudioType.MPEG;
            else if (url.Contains(".ogg"))
                type = AudioType.OGGVORBIS;

            using (var request = UnityWebRequestMultimedia.GetAudioClip(url, type))
            {
                using (var asyncOperation = await _network.GetResponse(request, progress))
                {
                    if (_network.IsSuccessfull(asyncOperation))
                    {
                        return ((DownloadHandlerAudioClip) asyncOperation.downloadHandler).audioClip;
                    }
                    else
                    {
                        throw new NetworkRequestException(asyncOperation.error,
                            $"Get sound by url error.\n URL: {url}");
                    }
                }
            }
        }
        
        public Sprite SpriteFromTexture2D(Texture2D texture) 
        {
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}

