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
    [Header("NEXT LAYOUT")]
    public LevelLayouts[] levelLayoutsBArray;
    [Header("NEXT LAYOUT")]
    public LevelLayouts[] levelLayoutsCArray;
    [System.Serializable]
    public class LevelLayouts
    {
        public GameObject levelLayOutPrefab;
        public CurrentDirection direction;
    }
    
    public LevelLayouts GetDesiredLevelLayout( CurrentDirection direction)
    {
       return GetCorrectOrRandomDirectionLayout( direction);
        
 
    }

   

    public LevelLayouts GetCorrectOrRandomDirectionLayout( CurrentDirection direction)
    {
       
        
                List<LevelLayouts> possibleLayout = new List<LevelLayouts>();
                foreach (LevelLayouts obj in levelLayoutsAArray)
                    {
                        if (obj.direction == direction)
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
}
