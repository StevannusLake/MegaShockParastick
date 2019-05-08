using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum CurrentDirection {UP,RIGHT,LEFT}
public enum LevelDifficulty {A,B,C}
public class LevelHandler : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> levelLayoutsCreated;
    public static LevelHandler instance;
    public GameObject finalLayout;
    public GameObject layoutPlayerIsIn;
    public MixingCameraController cameraController;
    [Header("Number of layouts to be created in advance. should be a even number")]
    public int numberOfMapToGenerate;
    [Header("which layout from the last to collide with to create more maps")]
    public int whenToGenerateMoreMaps;
    [Header("Distance from wall which player will lose when collide with")]
    public float distanceFromWall;


    public CurrentDirection currentDirection;
    public LevelDifficulty levelDifficulty;


    public float currentActiveLevelGeneratorID = 0;
    public float distanceToRespawnCoin = 5;
    public float distanceToRespawnOpal;
    [HideInInspector] public float timerForCoinRespawn = 0;
    [HideInInspector] public float timerForOpalRespawn = 0;
    public float distanceTraveledByLayout = 0;
    private float screenX;
    public int numberOfSectionToHold;







    private void Awake()
    {
        instance = this;
        levelLayoutsCreated = new List<GameObject>();
        levelDifficulty = LevelDifficulty.A;

    }

    private void Start()
    {

    }

    private void Update()
    {
        RemovePastSections();
        CheckForDifficulty();
    }

    void CheckForDifficulty()
    {
        if (distanceTraveledByLayout > 100) SetLevelDifficulty(LevelDifficulty.B);
        if (distanceTraveledByLayout > 500) SetLevelDifficulty(LevelDifficulty.C);
    }

    public void SetLevelDifficulty(LevelDifficulty difficulty)
    {
        levelDifficulty = difficulty;
    }
    public void CheckForCoinRespawn()
    {
       
        if (timerForCoinRespawn > distanceToRespawnCoin)
        {
            timerForCoinRespawn = 0;
            ObjectSpawner.instance.canRespawnCoinsAround = true;
            
        }
       

    }

    public void CheckForOpalRespawn()
    {
        if (timerForOpalRespawn > distanceToRespawnOpal)
        {
            timerForOpalRespawn = 0;
            ObjectSpawner.instance.canRespawnOpalMiddle = true;
            Debug.Log("OpalRespawned");
        }

    }
    void LoseIfPlayerMoveOutOfScreen()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float minimumX = min.x + distanceFromWall;
        float maximumX = max.x - distanceFromWall;

        if (GameManager.instance.player.transform.position.x < minimumX || GameManager.instance.player.transform.position.x > maximumX)
        {
            // Die and Second Chance Menu pop out
            UIManager.Instance.CallSecondChanceMenu();
        }

    }

    void RemovePastSections()
    {
        foreach (GameObject obj in levelLayoutsCreated)
        {
            if(obj)
            {
                if (obj.GetComponentInChildren<LevelGenerator>().levelGeneratorID + numberOfSectionToHold < currentActiveLevelGeneratorID)
                {
                    //obj.SetActive(false);
                    Destroy(obj);
                }
            }
            
        }
    }

    public void GetLastGeneratedLevel(LevelGenerator level, CurrentDirection direction)
    {
        finalLayout = level.transform.parent.gameObject;
        currentActiveLevelGeneratorID = level.levelGeneratorID;
        currentDirection = direction;

    }

    void AddDistanceByLayout(string Layout)
    {
        if (Layout == "Up")
        {
            distanceTraveledByLayout += 15;
            timerForOpalRespawn += 15;
            timerForCoinRespawn += 15;
        }

        if (Layout == "Right")
        {
            distanceTraveledByLayout += 10f;
            timerForOpalRespawn += 10f;
            timerForCoinRespawn += 10;
        }


        if (Layout == "Left")
        {
            distanceTraveledByLayout += 11f;
            timerForOpalRespawn += 11;
            timerForCoinRespawn += 11f;
        }
       

    }

    



    public void LevelAdded(GameObject layout,LevelGenerator generator,string direction)
    {
        AddDistanceByLayout(direction);
        levelLayoutsCreated.Add(layout);
        CheckForCoinRespawn();
        CheckForOpalRespawn();
        ObjectSpawner.instance.CheckForSpawningOpalInMiddle(layout, generator);
        ObjectSpawner.instance.CheckForRespawnCoinInMiddle(layout, generator);    
        ObjectSpawner.instance.CheckForSpawningCoinAround(layout, generator);
        
        
       
    }
    //


    private int RandomNumGenerator(int min, int max)
    {
        int random = Random.Range(min, max);
        return random;
    }


}
