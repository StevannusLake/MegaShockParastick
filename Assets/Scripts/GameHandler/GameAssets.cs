using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{  
    
    private static GameAssets _instance;
    public static GameAssets i
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
    }




    //Store all audio clips for audiomanager to use . Can be used for Effects, Prefabs and other stuff
    #region AudioClips

    public SoundAudioClip[] soundAudioClipArray;
    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioManager.Sound sound;
        public AudioClip audioClip;
        [Range(0,1)]  public float minPitch;
        [Range(0,1)] public float maxPitch;
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
}
