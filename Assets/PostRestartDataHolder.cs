using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRestartDataHolder : MonoBehaviour
{
    public bool secondLifeUsed = false;
    public static PostRestartDataHolder instance;

    [HideInInspector] public LevelDifficulty savedDifficulty;
    [HideInInspector]public float savedDistanceTraveledByLayout;
    


    

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseSecondLife()
    {
        
        secondLifeUsed = true;
        Object.DontDestroyOnLoad(this.gameObject);

        //
        savedDifficulty = LevelHandler.instance.levelDifficulty;
        savedDistanceTraveledByLayout = LevelHandler.instance.distanceTraveledByLayout;
      
    }
}
