using System;
using UnityEngine;

namespace Engenious.MainScene.Services
{
    public interface ISoundManager
    {
        void PlayEndShowProduct();
        void PlayStartShowProduct();
        void PlaySelectProduct();
        void PlaySwipeCarousel();
    }

    public class SoundManager : ISoundManager
    {
        private SoundManagerConfig _config;

        private AudioSource _source;

        public SoundManager(SoundManagerConfig config, AudioSource source)
        {
            _config = config;
            _source = source;
        }
        
        public void PlayEndShowProduct()
        {
            SetAudioClip(_config.EndShowProduct);
        }

        public void PlayStartShowProduct()
        {
            SetAudioClip(_config.StartShowProduct);
        }
        
        public void PlaySelectProduct()
        {
            SetAudioClip(_config.SelectProduct);
        }
        
        public void PlaySwipeCarousel()
        {
            SetAudioClip(_config.SwipeCarousel);
        }
        
        private void SetAudioClip(AudioClip clip)
        {
            _source.Stop();
            _source.clip = clip;
            _source.Play();
        }
    }

    [Serializable]
    public class SoundManagerConfig
    {
        public AudioClip EndShowProduct;
        public AudioClip StartShowProduct;
        public AudioClip SelectProduct;
        public AudioClip SwipeCarousel;
    }
}