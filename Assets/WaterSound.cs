using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlayWaterSound", 0.2f);
    }
    void PlayWaterSound()
    {
        AudioManager.PlaySoundCustom(AudioManager.Sound.Water, AudioManager.GetAudioClipVolume(AudioManager.Sound.Water), AudioManager.GetAudioClipMaxPitch(AudioManager.Sound.Water), true);
    }
}
