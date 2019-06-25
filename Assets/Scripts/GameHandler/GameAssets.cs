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


    public GameObject postRestartDataHolder;

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

    public static SoundAudioClip GetAudio(AudioManager.Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip;
            }
        }
        Debug.LogError("Sound" + sound + "wasnt found!");
        return null;
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

    public GameObject GetDesiredPlatform(Surfaces.SurfaceTypes type)
    {
        switch(type)
        {
            case Surfaces.SurfaceTypes.Safe:
                return platformObjectsArray[1].surfaceGameObject;
            case Surfaces.SurfaceTypes.Dangerous:
                return platformObjectsArray[0].surfaceGameObject;
            case Surfaces.SurfaceTypes.Moving:
                return platformObjectsArray[2].surfaceGameObject;
            case Surfaces.SurfaceTypes.DangerousMoving:
                return platformObjectsArray[3].surfaceGameObject;

        }
        return null;
    }

    public PlatformObjects thisPlatformType(GameObject platform)
    {
        
        
            foreach (PlatformObjects platformObject in platformObjectsArray)
            {
            if (platformObject.surfaceGameObject.tag==platform.tag)return platformObject;
            

            }

        

        return null;
        
       
       // return null;
    }


    #region LevelLayouts
   
    public LevelLayouts[] levelLayoutsAArray;
    
     [System.Serializable]
    public class LevelLayouts
    {
        public GameObject levelLayOutPrefab;
        public CurrentDirection direction;
        public LevelType type;
    }
    
    public LevelLayouts GetDesiredLevelLayout( CurrentDirection direction, LevelType type)
    {
       return GetCorrectOrRandomDirectionLayout( direction,type);
        
 
    }

   

    public LevelLayouts GetCorrectOrRandomDirectionLayout( CurrentDirection direction, LevelType type)
    {
       
        
                List<LevelLayouts> possibleLayout = new List<LevelLayouts>();
                foreach (LevelLayouts obj in levelLayoutsAArray)
                    {
                        if (obj.direction == direction && obj.type==type)
                        {
                            possibleLayout.Add(obj);
                        }
                    }
                    int randomNum = Random.Range(0, possibleLayout.Count);
                    return possibleLayout[randomNum];
                
                
          
      
    }


    #endregion

    //Store all level layouts for level generator to create




    #region Items

    public Items[] itemsArray;
    [System.Serializable]
    public class Items
    {
        public GameObject itemPrefab;
        public ItemType type;
    }

    #endregion

    #region LevelTypes

    public LevelTypes[] levelTypesArray;
    [System.Serializable]
    public class LevelTypes
    {
        public Sprite[] sprites;
        public LevelType type;
    }


    public LevelTypes GetCorrectLevelType(LevelType type)
    {
    
        foreach (LevelTypes obj in levelTypesArray)
        {
            if (obj.type == type)
            {
                return obj;
            }
        }
        return null;
  
    }
    #endregion
    

}
