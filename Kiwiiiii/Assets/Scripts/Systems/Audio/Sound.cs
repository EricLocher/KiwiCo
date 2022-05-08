using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public AudioMixerGroup mixer;

    public float maxDistance = 20f;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource audio;

    public bool loop;
}