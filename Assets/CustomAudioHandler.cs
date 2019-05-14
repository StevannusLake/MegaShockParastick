using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomAudioHandler : MonoBehaviour
{
    public static CustomAudioHandler instance;
    public float coinPitchRestartTime = 2f;
    float coinPickUpTimer = 0;
    bool pickedCoin = false;
    System.Array AudioSoundType;
    public float defultCoinPitch = 0.8f;
    private float coinPitch ;
    public Slider soundEffectSlider;
    private AudioManager.Sound[] soundEffectList;
    
    
    private void Awake()
    {
        
        soundEffectList = new AudioManager.Sound[] { AudioManager.Sound.CollectCoin, AudioManager.Sound.PlayerDie,AudioManager.Sound.InGameBGM };

        coinPitch = defultCoinPitch;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        AudioSoundType = System.Enum.GetValues(typeof(AudioManager.Sound));
        
    }

    public void Update()
    {
        PickedUpCoin();
        SetAllAudioVolumeOnUpdate();
        SetCustomAudio();
    }

  




    void SetCustomAudio()
    {
       
        foreach(GameAssets.SoundAudioClip clips in GameAssets.i.soundAudioClipArray)
        {
            foreach(AudioManager.Sound sound in soundEffectList)
            {
                if (clips.sound == sound) clips.volume = soundEffectSlider.value;
            }
            
        }
        foreach (GameAssets.SoundAudioClip clips in GameAssets.i.soundAudioClipArray)
        {
            foreach (AudioManager.Sound sound in soundEffectList)
            {
                //if (clips.sound == sound) clips.volume = 
            }

        }
    }



    
    

    public void PickUpCoin()
    {
        pickedCoin = true;
        coinPickUpTimer = 0;
    }

    void SetAllAudioVolumeOnUpdate()
    {
        for(int i=0; i<transform.childCount;i++)
        {

            if (GameManager.instance.soundSourcesCreated.Contains(transform.GetChild(i).name))
            {
                transform.GetChild(i).GetComponent<AudioSource>().volume = AudioManager.GetAudioClipVolume(returnCorrectSound(transform.GetChild(i).name));
            }


        }

    }

    AudioManager.Sound returnCorrectSound(string childName)
    {
        foreach(AudioManager.Sound sound in AudioSoundType)
        {
            if (childName == sound.ToString()) return sound;
        }
       return AudioManager.Sound.Continue;
    }
    void PickedUpCoin()
    {
        if(pickedCoin)
        {
            coinPickUpTimer += Time.deltaTime;
            if(coinPickUpTimer> coinPitchRestartTime)
            {
                coinPickUpTimer = 0;
                coinPitch = defultCoinPitch;
                pickedCoin = false;
            }
        }
    }

    public void HandleCoinSound()
    {
        PickUpCoin();
        
        coinPitch += 0.05f;
        coinPitch = Mathf.Clamp(coinPitch, defultCoinPitch, 1f);
    }
    public float CoinPitch()
    {
        return coinPitch;
    }


}
