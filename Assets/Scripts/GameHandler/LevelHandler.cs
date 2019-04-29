using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CurrentDirection {UP,RIGHT,LEFT}
public class LevelHandler : MonoBehaviour
{
 [SerializeField] 
    public List<GameObject> levelLayoutsCreated;
    public static LevelHandler instance;
    public CurrentDirection currentDirection;
    public GameObject finalLayout;
    public float currentActiveLevelGeneratorID = 0;
    public float distanceToRespawnCoin;
    public float timerForCoinRespawn = 0;
    public int numberOfSectionToHold;
    private float screenX;
    [Header("Distance from wall which player will lose when collide with")]
    public float distanceFromWall;


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
       LoseIfPlayerMoveOutOfScreen();
        CheckForCoinRespawn();
         
    }

    void CheckForCoinRespawn()
    {
        timerForCoinRespawn += GameManager.instance.playerDistanceTraveled;
        if (GameManager.instance.playerDistanceTraveled> distanceToRespawnCoin)
        {
            timerForCoinRespawn = 0;
            ObjectSpawner.instance.canRespawnCoins = true;
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
                obj.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void GetLastGeneratedLevel(LevelGenerator level, CurrentDirection direction)
    {
        finalLayout = level.transform.parent.gameObject;
         currentActiveLevelGeneratorID = level.levelGeneratorID;
        currentDirection = direction;
        
    }

   

    
}
