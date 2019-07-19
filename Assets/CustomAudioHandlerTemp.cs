using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudioHandlerTemp : MonoBehaviour
{
    public static CustomAudioHandlerTemp instance;
    System.Array AudioSoundType;
    private AudioManagerTemp.Sound[] soundEffectList;
    private AudioManagerTemp.Sound[] bgmList;
    public List<float> prevVolumes;
    bool isMuteOrUnmuting = false;


    // public static float soundEffectVolume;
    //public static float bgmVolume;

    private void Awake()
    {

        soundEffectList = new AudioManagerTemp.Sound[] { AudioManagerTemp.Sound.LogoSound };

        bgmList = new AudioManagerTemp.Sound[] { AudioManagerTemp.Sound.LogoSound };

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        AudioSoundType = System.Enum.GetValues(typeof(AudioManagerTemp.Sound));

    }

    private void Start()
    {
        prevVolumes = new List<float>();
        CapturePrevAudioVolumes();
    }

    public void Update()
    {
        SetAllAudioVolumeOnUpdate();
        // SetCustomAudio();
    }

    public void Mute()
    {
        Debug.Log("muted");
        for (int i = 0; i < GameAssets.i.soundAudioClipArray.Length; i++)
        {
            GameAssets.i.soundAudioClipArray[i].volume = 0;

        }
    }

    public void Unmute()
    {
        Debug.Log("Unmuted");
        for (int i = 0; i < GameAssets.i.soundAudioClipArray.Length; i++)
        {
            GameAssets.i.soundAudioClipArray[i].volume = prevVolumes[i];
            Debug.Log("Changed");
        }
    }



    void CapturePrevAudioVolumes()
    {
        for (int i = 0; i < GameAssets.i.soundAudioClipArray.Length; i++)
        {
            prevVolumes.Add(GameAssets.i.soundAudioClipArray[i].volume);
        }
    }

    void SetAllAudioVolumeOnUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (GameManager.instance.soundSourcesCreated.Contains(transform.GetChild(i).name))
            {
                transform.GetChild(i).GetComponent<AudioSource>().volume = AudioManagerTemp.GetAudioClipVolume(returnCorrectSound(transform.GetChild(i).name));
            }
        }
    }

    AudioManagerTemp.Sound returnCorrectSound(string childName)
    {
        foreach (AudioManagerTemp.Sound sound in AudioSoundType)
        {
            if (childName == sound.ToString()) return sound;
        }
        return AudioManagerTemp.Sound.LogoSound;
    }
}
