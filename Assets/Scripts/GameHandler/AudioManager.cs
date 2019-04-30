﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager 
{
    
    public enum Sound
    {
        PlayerUnstick,
        PlayerStick,
        Button,
        CollectCoin,
        CollectCoinMain,
        Continue,
        Lose,
        PlayerDie,
        Reborn,
        InGameBGM,

    }

    
    private static Dictionary<Sound, float> soundTimerDictionary;
    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerUnstick] = 0;
        
    }

   

    //Use to play sound from any script (AudioManager.PlaySound(AudioManager.Sound.PlayerJump))'
    //Play a universal audio regardless of object position and audiosource
    public static void PlaySound(Sound sound)
    {
        if(CanPlaySound(sound))
        {
            string soundName = sound.ToString();
            if(GameManager.instance.soundSourcesCreated.Contains(soundName))
            {
                GameObject audioSource = GameManager.instance.audioSourcePlayer.transform.Find(soundName.ToString()).gameObject;
                AudioSource source = audioSource.GetComponent<AudioSource>();
                source.pitch = Random.Range(GetAudioClipMinPitch(sound), GetAudioClipMaxPitch(sound));
                source.volume = GetAudioClipVolume(sound);
                source.PlayOneShot(GetAudioClip(sound));
            }
            
            else if (!GameManager.instance.soundSourcesCreated.Contains(soundName))
            {
                GameManager.instance.soundSourcesCreated.Add(soundName);
                GameObject soundSourceGO = new GameObject(soundName);
                soundSourceGO.transform.SetParent(GameManager.instance.audioSourcePlayer.transform);
                soundSourceGO.AddComponent<AudioSource>();
                AudioSource source = soundSourceGO.GetComponent<AudioSource>();
                source.pitch = Random.Range(GetAudioClipMinPitch(sound), GetAudioClipMaxPitch(sound));
                source.volume= GetAudioClipVolume(sound);
                source.PlayOneShot(GetAudioClip(sound));
            }
            
                  
        }
  
    }
    //pass in gameobject audio source so it play the sound in regard of position and all audio source setting of the particular gameobject.
    public static void Play3DSound(Sound sound,AudioSource selfAudio)
    {
        if (CanPlaySound(sound))
        {
            selfAudio.pitch = Random.Range(GetAudioClipMinPitch(sound), GetAudioClipMaxPitch(sound));
            selfAudio.PlayOneShot(GetAudioClip(sound));
        }

    }


    //Delay for sounds which will be played on Update or simillar functions
    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;

            case Sound.PlayerUnstick:
                if(soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float exampleSoundTimerMax = 0.05f;
                    if (lastTimePlayed + exampleSoundTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
        }
    }

    #region Get Audio From GameAssets

    //Get and match the chosen audioclip from player to game assets
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "wasnt found!");
        return null;
    }
    private static float GetAudioClipMinPitch(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.minPitch;
            }
        }
        Debug.LogError("Sound" + sound + "wasnt found!");
        return 0;
    }
    private static float GetAudioClipMaxPitch(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.maxPitch;
            }
        }
        Debug.LogError("Sound" + sound + "wasnt found!");
        return 0;
    }
    private static float GetAudioClipVolume(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.volume;
            }
        }
        Debug.LogError("Sound" + sound + "wasnt found!");
        return 0;
    }
    #endregion

}
