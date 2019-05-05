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
    public float timerForCoinRespawn = 0;
    public float distanceTraveledByLayout = 0;
    private float screenX;
    public int numberOfSectionToHold;







    private void Awake()
    {
        instance = this;
        levelLayoutsCreated = new List<GameObject>();

    }

    private void Start()
    {

    }

    private void Update()
    {
        RemovePastSections();
    }

    public void CheckForCoinRespawn()
    {
       
        if (timerForCoinRespawn > distanceToRespawnCoin)
        {
            timerForCoinRespawn = 0;
            ObjectSpawner.instance.canRespawnCoinsAround = true;
            
        }
        else Debug.Log("It was less than distance");

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
            if (obj.GetComponentInChildren<LevelGenerator>().levelGeneratorID + numberOfSectionToHold < currentActiveLevelGeneratorID)
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

    void AddDistanceByLayout(string Layout)
    {
        if (Layout == "Up")
        {
            distanceTraveledByLayout += 15;
            timerForCoinRespawn += 15;
        }

        if (Layout == "Right")
        {
            distanceTraveledByLayout += 10f;
            timerForCoinRespawn += 10;
        }


        if (Layout == "Left")
        {
            distanceTraveledByLayout += 11f;
            timerForCoinRespawn += 11f;
        }
       

    }

    



    public void LevelAdded(GameObject layout,LevelGenerator generator,string direction)
    {
        AddDistanceByLayout(direction);
        levelLayoutsCreated.Add(layout);
        ObjectSpawner.instance.CheckForSpawningCoinAround(layout, generator);
        ObjectSpawner.instance.CheckForRespawnCoinInMiddle(layout, generator);
        CheckForCoinRespawn();
    }
    //


    private int RandomNumGenerator(int min, int max)
    {
        int random = Random.Range(min, max);
        return random;
    }


}
