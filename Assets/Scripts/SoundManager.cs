using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    
    public AudioSource soundEffect;
    public AudioSource soundMusic;
    public AudioSource movementSound;

    public SoundType[] Sounds;
    public bool IsMute = false;
    public float Volume = 1.0f;

    private void Start()
    {
        SetVolume(0.5f);
        PlayMusic(global::Sounds.Music);
    }

    public void Mute(bool status)
    {
        IsMute = status;
    }

    public void SetVolume(float volume)
    {
        Volume = volume;
        soundEffect.volume = Volume;
        soundMusic.volume = Volume;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(Sounds sound)
    {
        if (IsMute){return;}

        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Clip not found for the sound type:" + sound);
        }
    }
    public void Play(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Clip not found for the sound type:" + sound);
        }
    }
    private AudioClip getSoundClip(Sounds sound)
    {
        SoundType sound1 =  Array.Find(Sounds, i => i.soundType == sound);
        if (sound1 != null)
        {
            return sound1.soundClip;
        }
        return null;
    }

}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public enum Sounds
{
    ButtonClick,
    Music,
    PlayerMove,
    PlayerJump,
    PlayerDeath,
    EnemyDeath
}
