using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BGMAudioManager : MonoBehaviour
{
    public BGMSound[] sounds;
    public List<float> prevVolumes;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(BGMSound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        CapturePrevAudioVolumes();
    }

    public void Play(string name)
    {
        BGMSound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
            
        s.source.Play();
    }

    public void Stop(string name)
    {
        BGMSound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    void CapturePrevAudioVolumes()
    {
        foreach (BGMSound s in sounds)
        {
            prevVolumes.Add(s.source.volume);
        }
    }

    public void Mute()
    {
        foreach (BGMSound s in sounds)
        {
            s.source.mute = true;
        }
    }

    public void Unmute()
    {
        foreach (BGMSound s in sounds)
        {
            s.source.mute = false;
        }
    }
}
