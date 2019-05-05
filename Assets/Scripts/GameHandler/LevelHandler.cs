﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CurrentDirection {UP,RIGHT,LEFT}
public class LevelHandler : MonoBehaviour
{
 [SerializeField] 
    public List<GameObject> levelLayoutsCreated;
    public static LevelHandler instance;
    public MixingCameraController cameraController;
    [Header("Number of layouts to be created in advance. should be a even number")]
    public int numberOfMapToGenerate;
    [Header("which layout from the last to collide with to create more maps")]
    public int whenToGenerateMoreMaps;
    public CurrentDirection currentDirection;
    public GameObject finalLayout;
    public float currentActiveLevelGeneratorID = 0;
    public float distanceToRespawnCoin=5;
    [HideInInspector]public float timerForCoinRespawn = 0;
    public int numberOfSectionToHold;
    private float screenX;
    [Header("Distance from wall which player will lose when collide with")]
    public float distanceFromWall;

    public float distanceTraveledByLayout = 0;
    


    private void Awake()
    {
        instance = this;
        levelLayoutsCreated = new List<GameObject>();
        
    }

    private void Start()
    {
       
        //
    }

    private void Update()
    {
       RemovePastSections();
      // LoseIfPlayerMoveOutOfScreen();
        
         
    }

    public void CheckForCoinRespawn()
    {
        timerForCoinRespawn += LevelHandler.instance.distanceTraveledByLayout;
        if (timerForCoinRespawn > distanceToRespawnCoin)
        {
            timerForCoinRespawn = 0;
            ObjectSpawner.instance.canRespawnCoinsAround = true;
        }
    }
    void LoseIfPlayerMoveOutOfScreen()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float minimumX = min.x + distanceFromWall;
        float maximumX = max.x - distanceFromWall;

        if (GameManager.instance.player.transform.position.x< minimumX || GameManager.instance.player.transform.position.x > maximumX)
        {
            // Die and Second Chance Menu pop out
            UIManager.Instance.CallSecondChanceMenu();
        }
    
    }

    void RemovePastSections()
    {
        foreach(GameObject obj in levelLayoutsCreated)
        {
            if(obj.GetComponentInChildren<LevelGenerator>().levelGeneratorID + numberOfSectionToHold < currentActiveLevelGeneratorID)
            {
                obj.SetActive(false);
            }
        }
    }

    public void GetLastGeneratedLevel(LevelGenerator level, CurrentDirection direction)
    {
        finalLayout = level.transform.parent.gameObject;
         currentActiveLevelGeneratorID = level.levelGeneratorID;
        currentDirection = direction;
   
    }

    public void AddDistanceByLayout(string Layout)
    {
        if(Layout=="Up")
        {
            distanceTraveledByLayout += 15;
            
        }
        if (Layout == "Right")
        {
            distanceTraveledByLayout += 11f;
            
        }
        if (Layout == "Left")
        {
            distanceTraveledByLayout += 11f;
            
        }
        


    }

    //


    private int RandomNumGenerator(int min, int max)
    {

        int random = Random.Range(min, max);
        return random;
    }


}
