using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public int levelGeneratorID = 0;
    public List<GameObject> platformPlacementList;
    public List<GameObject> platformList;
    public GameObject topObject;
    public GameObject bottomObject;
    public  BoxCollider2D borderCollider;
    
    
    private bool DangerAlreadyMade = false;
    public bool test;
    
    public int[] upwardPossibilities;
   
    private void Awake()
    {


        platformList = new List<GameObject>();
        platformPlacementList = new List<GameObject>();
        borderCollider = transform.parent.Find("PipeShape").GetComponent<BoxCollider2D>();
        
    }
    private void Start()
    {
        upwardPossibilities = new int[] {0,1,2};    
        AddChildsToList();
        RespawnPlatforms();
        SortAllPlatfromsBasedOnDistance();
        GetTheToppestAndBottomPlatform();
        RemoveSpriteRenderers();
       if(levelGeneratorID==0) AddSelfToLevelHandler();
       
    }


   

    void SendLastGeneratedLevel(LevelGenerator lastGenerator, CurrentDirection direction)
    {
        LevelHandler.instance.GetLastGeneratedLevel(lastGenerator, direction);
    }

    void AddChildsToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "PlatformChild")
            {
                platformPlacementList.Add(transform.GetChild(i).gameObject);
            }

        }
    }
    void AddSelfToLevelHandler()
    {
        GameManager.instance.levelHandler.levelLayoutsCreated.Add(transform.parent.gameObject);
        
    }
    void RemoveSpriteRenderers()
    {
        foreach (GameObject obj in platformPlacementList)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void RespawnPlatforms()
    {

        int randomDangerousPlatform = Random.Range(1, platformPlacementList.Count);
       
        for (int i = 0; i < platformPlacementList.Count; i++)
        {
            if (i == randomDangerousPlatform)
            {

                Renderer renderer = platformPlacementList[i].GetComponent<Renderer>();
                float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
                float randYOnRenderer = Random.Range(renderer.bounds.min.y, renderer.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.platformObjectsArray[0].surfaceGameObject,
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.platformObjectsArray[0].surfaceGameObject.transform.rotation,
                    transform);

                //Add the platform to platform list
                platformList.Add(Platforms);
            }
            else
            {

                Renderer renderer = platformPlacementList[i].GetComponent<Renderer>();
                float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
                float randYOnRenderer= Random.Range(renderer.bounds.min.y, renderer.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.platformObjectsArray[1].surfaceGameObject,
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.platformObjectsArray[1].surfaceGameObject.transform.rotation,
                    transform);              
                //Add the platform to platform list
                platformList.Add(Platforms);

            }
            //RespawnCoins
            RespawnCoinAround();

        }
        //Respawn Coin in Middle
        RespawnCoinInMiddle();
        

    }
    
    void RespawnCoinAround()
    {
        Randomize:
        int randomNum = RandomNumGenerator(0, platformList.Count);
        if (platformList[randomNum].tag == "Deadly")
        {
            goto Randomize;
        }
        ObjectSpawner.instance.RespawnCoins(platformList[randomNum].transform, GameAssets.i.platformObjectsArray[0].radiusForCoins);
    }

    void RespawnCoinInMiddle()
    {
        RandomizeAgain:
        int randomNumCoinInMiddle = Random.Range(0, platformList.Count - 1);
        if (platformList[randomNumCoinInMiddle].tag == "Deadly" || platformList[randomNumCoinInMiddle + 1].tag == "Deadly")
        {           
            goto RandomizeAgain;
        }
        ObjectSpawner.instance.RespawnCoinsInMiddle(platformList[randomNumCoinInMiddle], platformList[randomNumCoinInMiddle + 1]);
       // Debug.Log(randomNumCoinInMiddle);
    }
    



    private int RandomNumGenerator(int min, int max)
    {

        int random = Random.Range(min, max);
        return random;
    }

    void SortAllPlatfromsBasedOnDistance()
    {
        platformList.Sort(Compare);
    }

    void GetTheToppestAndBottomPlatform()
    {
        topObject = platformList.Last();
        bottomObject = platformList.First();
    }

    public void GenerateMapOnTop(bool isFirst)
    {
        
        Debug.Log("Runned");
        if (transform.parent.gameObject.GetComponentInChildren<EnterController>().isAlreadyActivated) return;
        int offsetLayout = 0;

        if (isFirst) offsetLayout = LevelHandler.instance.levelLayoutsCreated.Count-1;

       
        

        for (int i = 1; i < LevelHandler.instance.numberOfMapToGenerate; i++)
        {
           
            int number = Random.Range(0, upwardPossibilities.Length);
            int randomNum = upwardPossibilities[number];
            Transform currentParent = LevelHandler.instance.levelLayoutsCreated[i-1+ offsetLayout].gameObject.transform;
            BoxCollider2D currentBoxCollider= LevelHandler.instance.levelLayoutsCreated[i -1+ offsetLayout].GetComponentInChildren<LevelGenerator>().borderCollider;

            if (LevelHandler.instance.currentDirection == CurrentDirection.UP)
            {
                GameManager.instance.levelHandler.AddDistanceByLayout("Up");

                GameObject newLayout = Instantiate(GameAssets.i.levelLayoutsArray[randomNum].levelLayOutPrefab, new Vector3(currentParent.position.x, currentParent.position.y), Quaternion.identity);
                newLayout.transform.position = new Vector2(newLayout.transform.position.x, (newLayout.transform.position.y + currentBoxCollider.bounds.size.y) );
                newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID = +levelGeneratorID + i;
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.levelLayoutsArray[randomNum].direction + ")";
                SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[randomNum].direction);             
                if(i!= (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                GameManager.instance.levelHandler.levelLayoutsCreated.Add(newLayout);
                
            }
            else if (LevelHandler.instance.currentDirection == CurrentDirection.RIGHT)
            {
                GameManager.instance.levelHandler.AddDistanceByLayout("Right");
                float y = currentParent.position.y ;
                float desiredY = y + currentBoxCollider.bounds.size.y ;
                float desiredX = currentParent.position.x + currentBoxCollider.size.x / 3f - 0.28f;
                GameObject newLayout = Instantiate(GameAssets.i.levelLayoutsArray[randomNum].levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);
                newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID = +levelGeneratorID + i;
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.levelLayoutsArray[randomNum].direction + ")";
                SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[randomNum].direction);
                if (i != (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;           
                GameManager.instance.levelHandler.levelLayoutsCreated.Add(newLayout);
               

            }
            else if (LevelHandler.instance.currentDirection == CurrentDirection.LEFT)
            {
               GameManager.instance.levelHandler.AddDistanceByLayout("Left");
                float y = currentParent.position.y;
                float desiredY = y + currentBoxCollider.bounds.size.y ;
                float desiredX = currentParent.position.x - currentBoxCollider.size.x / 3f + 0.28f;
                GameObject newLayout = Instantiate(GameAssets.i.levelLayoutsArray[randomNum].levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);
                newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID = +levelGeneratorID + i;
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.levelLayoutsArray[randomNum].direction + ")";
                SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[randomNum].direction);
                if (i != (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                GameManager.instance.levelHandler.levelLayoutsCreated.Add(newLayout);
                

            }
        }

        LevelHandler.instance.CheckForCoinRespawn();
    }

    private  int Compare(GameObject _objA, GameObject _objB )
    {
        float t1 = Vector2.Distance(_objA.transform.position, new Vector2(borderCollider.bounds.min.x, borderCollider.bounds.min.y));
        float t2 = Vector2.Distance(_objB.transform.position, new Vector2(borderCollider.bounds.min.x, borderCollider.bounds.min.y));
        return t1.CompareTo(t2);
    }



}




