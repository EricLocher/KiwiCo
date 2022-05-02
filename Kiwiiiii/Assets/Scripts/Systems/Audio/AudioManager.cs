using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 * How to use
 * To make global sound call
 * AudioManager.instance.Play("name");
 * 
 * To make local sound call
 * GetComponent<AudioManager>().Play("name");
 */


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [HideInInspector]
    public float menuVolume;

    public static AudioManager instance;
    public Slider masterSlider, sfxSlider, musicSlider;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (gameObject.tag == "AudioManager")
                DontDestroyOnLoad(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            sound.audio = gameObject.AddComponent<AudioSource>();
            sound.audio.clip = sound.clip;
            sound.audio.volume = sound.volume;
            sound.audio.loop = sound.loop;
            sound.audio.outputAudioMixerGroup = sound.mixer;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            AudioManager.instance.PauseAllSound();
            AudioManager.instance.Play("Menu Music");
        }
        else
        {
            AudioManager.instance.PauseSound("Menu Music");
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

    public void PauseSound(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.audio.Pause();
            }
        }
    }

    public void SetMasterVolume()
    {
        float soundLevel = masterSlider.value;
        foreach (Sound sound in sounds)
        {
            sound.mixer.audioMixer.SetFloat("Master", soundLevel);
        }
    }

    public void SetSfxVolume()
    {
        float soundLevel = sfxSlider.value;
        foreach (Sound sound in sounds)
        {
            sound.mixer.audioMixer.SetFloat("SFX", soundLevel);
        }
    }

    public void SetMusicVolume()
    {
        float soundLevel = musicSlider.value;
        foreach (Sound sound in sounds)
        {
            sound.mixer.audioMixer.SetFloat("Music", soundLevel);
        }
    }
}