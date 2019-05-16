using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum CurrentDirection {UP,RIGHT,LEFT}
public enum LevelDifficulty {A,B,C,D,E,F}
public enum LevelType {TYPE1,TYPE2,TYPE3 }
public class LevelHandler : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> levelLayoutsCreated;
    public static LevelHandler instance;
    public GameObject finalLayout;
    public GameObject layoutPlayerIsIn;
    public GameObject firstLayoutPos;
    public MixingCameraController cameraController;
    [Header("Number of layouts to be created in advance. should be a even number")]
    public int numberOfMapToGenerate;
    [Header("which layout from the last to collide with to create more maps")]
    public int whenToGenerateMoreMaps;
    [Header("Distance from wall which player will lose when collide with")]
    public float distanceFromWall;


    public CurrentDirection currentDirection;
    public LevelDifficulty levelDifficulty;
    public LevelType levelType;
    public List<SpriteRenderer> listOfOldSprites;

    public float currentActiveLevelGeneratorID = 0;
    public float distanceToRespawnCoin = 5;
    public float distanceToRespawnOpal;
    [HideInInspector] public float timerForCoinRespawn = 0;
    [HideInInspector] public float timerForOpalRespawn = 0;
    public float distanceTraveledByLayout = 0;
    private float screenX;
    public int numberOfSectionToHold;




    private void OnEnable()
    {
        //ChangeLevelSprites();
        //SetLevelSprite();
    }


    private void Awake()
    {
        
        instance = this;
        levelLayoutsCreated = new List<GameObject>();
        GameObject dataHolder = GameObject.Find("PostRestartDataHolder");
        if (!dataHolder)
        {
            GameObject newDataHolder = Instantiate(GameAssets.i.postRestartDataHolder);
            newDataHolder.name = "PostRestartDataHolder";
        }
        if (PostRestartDataHolder.instance.secondLifeUsed) GetSecondLifeData();
       
        listOfOldSprites = new List<SpriteRenderer>();
        
        CreateFirstLayout();






    }

    void CreateFirstLayout()
    {
       
        GameObject firstLayout=Instantiate(GameAssets.i.GetDesiredLevelLayout(CurrentDirection.UP, levelType).levelLayOutPrefab, new Vector3(firstLayoutPos.transform.position.x, firstLayoutPos.transform.position.y), Quaternion.identity);
        Transform pivotAnchor =firstLayout.transform.Find("PivotAnchor").transform;
        GameObject generator = pivotAnchor.Find("LevelGenerator").transform.gameObject;
        Transform positionBeforeSettingActive= pivotAnchor.GetComponentInChildren<LevelGenerator>().nextLayoutAnchor.transform;
        generator.SetActive(false);
        pivotAnchor.transform.position = firstLayout.transform.position;
        GameObject secondLayout= Instantiate(GameAssets.i.GetDesiredLevelLayout(CurrentDirection.UP, levelType).levelLayOutPrefab, new Vector3(firstLayoutPos.transform.position.x, firstLayoutPos.transform.position.y), Quaternion.identity);
        Transform pivotAnchor2 = secondLayout.transform.Find("PivotAnchor").transform;
        pivotAnchor2.transform.position = positionBeforeSettingActive.position;
        LevelGenerator levelGenerator = secondLayout.GetComponentInChildren<LevelGenerator>();
        levelGenerator.borderCollider.size = new Vector2(levelGenerator.borderCollider.size.x, levelGenerator.borderCollider.size.y + 30f);


        //////////////////
        GameObject backgroundFirstLayer = pivotAnchor.transform.Find("BrownBackground").gameObject;
        for (int i = 0; i < backgroundFirstLayer.transform.childCount; i++)
        {
            backgroundFirstLayer.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder += -10;
            
   
        }

    }
     /*
    void ChangeLevelSprites()
    {
        int levelsCount = GameAssets.i.levelLayoutsAArray.Count();
        
        //Get all old Sprites
        for (int i=0; i<levelsCount;i++)
        {
            Transform pipeParent = GameAssets.i.levelLayoutsAArray[i].levelLayOutPrefab.transform.Find("PivotAnchor").transform.Find("PipeShape").transform;
            Transform backgroundParent= GameAssets.i.levelLayoutsAArray[i].levelLayOutPrefab.transform.Find("PivotAnchor").transform.Find("BrownBackground").transform;
            for (int j=0; j<pipeParent.childCount;j++)
            {
                listOfOldSprites.Add(pipeParent.GetChild(j).GetComponent<SpriteRenderer>());
            }
            for (int j = 0; j < backgroundParent.childCount; j++)
            {
                listOfOldSprites.Add(backgroundParent.GetChild(j).GetComponent<SpriteRenderer>());
            }
        }
        //Replace them with new ones
       

    }

    void SetLevelSprite()
    {
        GameAssets.LevelTypes correctLevelType = GameAssets.i.GetCorrectLevelType(levelType);
        for (int i = 0; i < listOfOldSprites.Count; i++)
        {

            foreach (Sprite sprite in correctLevelType.sprites)
            {
                if (sprite.name == listOfOldSprites[i].sprite.name) listOfOldSprites[i].sprite = sprite;
            }

        }
    }*/

    void GetSecondLifeData()
    {
        levelDifficulty = PostRestartDataHolder.instance.savedDifficulty;
        distanceTraveledByLayout = PostRestartDataHolder.instance.savedDistanceTraveledByLayout;
    }

    private void Start()
    {
        if (PostRestartDataHolder.instance.secondLifeUsed)
        {
            Movement.deadState = 0;
        }
        
        

    }

    private void Update()
    {
        RemovePastSections();
       
    }

    void CheckForDifficulty()
    {
        if (distanceTraveledByLayout > 100) SetLevelDifficulty(LevelDifficulty.B);
        if (distanceTraveledByLayout > 250) SetLevelDifficulty(LevelDifficulty.C);
        if (distanceTraveledByLayout > 500) SetLevelDifficulty(LevelDifficulty.D);
        if (distanceTraveledByLayout > 1000) SetLevelDifficulty(LevelDifficulty.E);
        if (distanceTraveledByLayout > 5000) SetLevelDifficulty(LevelDifficulty.F);
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
       // CheckForDifficulty();


    }
    //


    private int RandomNumGenerator(int min, int max)
    {
        int random = Random.Range(min, max);
        return random;
    }


}
