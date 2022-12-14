using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TempAudioManager : MonoBehaviour
{
    public static TempAudioManager instance = null;
    
    public AudioClip dropIngredientClip;
    public AudioClip buttonClip;

    public float MasterVolume = 1f; //d added
    public float MusicVolume = 1f; //d added
    public float EffectsVolume = 1f; //d added

    private float _masterVolume = 1f;
    private float _musicVolume = 1f;
    private float _effectsVolume = 1f;

    [SerializeField] private AudioSource _backgroundMusic;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(AudioClip _audioClip)
    {
        AudioSource _effectSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        _effectSource.clip = _audioClip;
        _effectSource.volume = _effectsVolume;
        _effectSource.Play();
        Destroy(_effectSource, _audioClip.length);
    }

    public void PlayDropIngredient()
    {
        AudioSource _effectSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        _effectSource.clip = dropIngredientClip;
        _effectSource.volume = _effectsVolume;
        _effectSource.Play();
        Destroy(_effectSource, dropIngredientClip.length);
    }
    
    public void PlayButtonClick()
    {
        AudioSource _effectSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        _effectSource.clip = buttonClip;
        _effectSource.volume = _effectsVolume;
        _effectSource.Play();
        Destroy(_effectSource, buttonClip.length);
    }

    public void PlaySoundEffectCustomVolume(AudioClip _audioClip, float _volumeMultiplier)
    {
        AudioSource _effectSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        _effectSource.clip = _audioClip;
        _effectSource.volume = _effectsVolume * _volumeMultiplier;
        _effectSource.Play();
        Destroy(_effectSource, _audioClip.length);
    }

    public void SetVolumes(float _master, float _music, float _effects)
    {
        _masterVolume = _master;
        _musicVolume = _music * _masterVolume;
        _effectsVolume = _effects * _masterVolume;
        _backgroundMusic.volume = _musicVolume;
    }
}
