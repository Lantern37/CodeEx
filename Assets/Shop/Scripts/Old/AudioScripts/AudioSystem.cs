using System.Linq;
using Shop.Core.Utils;
using UniRx.Async;
using UnityEngine;


namespace Shop
{
    
    public class AudioSystem : SingletonT<AudioSystem>
    {
        [SerializeField] private AudioSource _2DShotSource;
        [SerializeField] private AudioSource _2DShotSourcePitched;
        [SerializeField] private AudioSource _2DNoRepeatedSource;
        [SerializeField] private AudioSource _musicSource;
    
        [Space] 
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private float _changeMusicDuration = 0.25f;
        
        public Sound[] sounds;
    
        public bool musicOn = true;
        public bool soundsOn = true;


    public Sound GetSound(string soundName)
    {
        return sounds.FirstOrDefault(s => s.name == soundName);
    }

    
    public  void PlayOneShot(string soundName, bool pitched = false)
    {
        Sound sound = GetSound(soundName);
        
        PlayOneShot(pitched 
                ? _2DShotSourcePitched
                : _2DShotSource,
            sound.clip, sound.pitch, sound.volume);
    }
    
    public  void PlayOneShot(string soundName, float pitch, bool pitched = false)
    {
        Sound sound = GetSound(soundName);
        PlayOneShot(pitched 
            ? _2DShotSourcePitched
            : _2DShotSource,
            sound.clip, pitch, sound.volume);
    }
    
    private  void PlayOneShot(AudioSource source, AudioClip clip, float pitch, float volume)
    {
        if (clip == null)
        {
            return;
        }
        
        source.pitch = pitch;
        source.volume = volume;
        source.PlayOneShot(clip);
    }

    public void PlaySound(string soundName)
    {
        if (_2DNoRepeatedSource.isPlaying)
        {
            return;
        }
        
        Sound sound = GetSound(soundName);
        // _2DNoRepeatedSource.PlaySound(sound);
    }
    
    public void ChangeMusic(string soundName)
    {
        StopMusic();
        PlayMusic(soundName);
    }
    
    public void StopMusic()
    {
        _musicSource.Stop();
    }
    
    public void PlayMusic(string soundName) 
    {
        Sound sound = GetSound(soundName);

        _musicSource.clip = sound.clip;
        _musicSource.pitch = sound.pitch;
        _musicSource.volume = sound.volume;
        _musicSource.loop = sound.loop;
        // _musicSource.PlayDelayed(0.1f);
        _musicSource.Play();
    }
    

    public void ToggleSound()
    {
        _2DShotSource.mute = !(soundsOn = !soundsOn);
    }

    public void ToggleMusic()
    {
         _musicSource.mute = !(musicOn = !musicOn);
    }
    
    
    // // __________________ Async __________________
    //
    //
    // public async UniTask ChangeMusicAsync(string soundName)
    // {
    //     Sound sound = GetSound(soundName);
    //
    //     await _musicSource.Mute(_changeMusicDuration);
    //     _musicSource.clip = sound.clip;
    //     await _musicSource.Activate(_changeMusicDuration);
    // }
    //
    //
    // private async UniTask MuteGlobal()
    // {
    //     await UniTask.WhenAll(
    //         _2DShotSource.Mute(_fadeDuration),
    //         _musicSource.Mute(_fadeDuration));
    // }
    //
    // private async UniTask ActivateGlobal()
    // {
    //     await UniTask.WhenAll(
    //         _2DShotSource.Activate(_fadeDuration),
    //         _musicSource.Activate(_fadeDuration));
    // }
    //
    // public UniTask ActivateMusic()
    // {
    //     return _musicSource.Activate(_fadeDuration);
    // }
    //
    // public UniTask MuteMusic()
    // {
    //     return _musicSource.Mute(_fadeDuration);
    // }
    //     
    //     
    // public UniTask ActivateSounds()
    // {
    //     return _2DShotSource.Activate(_fadeDuration);
    // }
    //
    // public UniTask MuteSounds()
    // {
    //     return _2DShotSource.Mute(_fadeDuration);
    // }
}

}