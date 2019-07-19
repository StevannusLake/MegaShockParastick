using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetsTemp : MonoBehaviour
{
    public static GameAssetsTemp i;

    void Awake()
    {
        if (i == null) i = this;
        else if (i != this) Destroy(gameObject);
    }

    //Store all audio clips for audiomanager to use . Can be used for Effects, Prefabs and other stuff
    #region AudioClips

    public SoundAudioClip[] soundAudioClipArray;
    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioManagerTemp.Sound sound;
        public AudioClip audioClip;
        [Range(0, 1)] public float minPitch = 1;
        [Range(0, 1)] public float maxPitch = 1;
        [Range(0, 1)] public float volume = 1;
    }


    #endregion
}
