using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [HideInInspector]
    public float menuVolume;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            sound.audio = gameObject.AddComponent<AudioSource>();
            sound.audio.clip = sound.clip;
            sound.audio.volume = sound.volume;
            sound.audio.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.audio.Play();
    }

    public void PlayOnce(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.audio.PlayOneShot(s.clip);
    }

    public void PauseAllSound()
    {
        foreach (Sound sound in sounds)
        {
            sound.audio.Pause();
        }
    }
}