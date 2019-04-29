using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{


    public static GameAssets i;
    

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
        public AudioManager.Sound sound;
        public AudioClip audioClip;
        [Range(0,1)]  public float minPitch = 1;
        [Range(0,1)] public float maxPitch = 1;
        [Range(0, 1)] public float volume = 1;
    }


    #endregion

    //Store all platform types including their prefabs for level generator to randomize and create each 
    #region PlatformObjects

    public PlatformObjects[] platformObjectsArray;
    [System.Serializable]
    public class PlatformObjects
    {
        public Surfaces.SurfaceTypes types;
        public GameObject surfaceGameObject;
        public float radiusForCoins;
    }
    #endregion

    //Store all level layouts for level generator to create
    #region LevelLayouts

    public LevelLayouts[] levelLayoutsArray;
    [System.Serializable]
    public class LevelLayouts
    {       
        public GameObject levelLayOutPrefab;
        public CurrentDirection direction;
    }
    #endregion



    #region Items
    
    public Items[] itemsArray;
    [System.Serializable]
    public class Items
    {
        public GameObject itemPrefab;
        public ItemType type;
    }

    #endregion
}
