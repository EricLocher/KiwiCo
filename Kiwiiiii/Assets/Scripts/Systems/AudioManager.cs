using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource, sfxSource;
    public Slider sfx, music;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            sfx.value = sfxSource.volume;
            music.value = musicSource.volume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void SetSFXVolume()
    {
        sfxSource.volume = sfx.value;
    }

    public void SetMusicVolume()
    {
        musicSource.volume = music.value;
    }
}

/* Usage ex.
*[SerializeField] AudioClip sound;
*[SerializeField] AudioClip music;
*
*AudioManager.instance.PlaySFX(sound);
*AudioManager.instance.PlayMusic(music);
 */