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
    public static BoxCollider2D borderCollider;
    public bool isAlreadyMade;
    private bool DangerAlreadyMade = false;
    public int numberOfMapToGenerate = 2;
    
    private void Awake()
    {


        platformList = new List<GameObject>();
        platformPlacementList = new List<GameObject>();

    }
    private void Start()
    {
        borderCollider = transform.parent.Find("PipeShape").GetComponent<BoxCollider2D>();
        AddChildsToList();
        RespawnPlatforms();
        SortAllPlatfromsBasedOnDistance();
        GetTheToppestAndBottomPlatform();
        RemoveSpriteRenderers();
        AddSelfToLevelHandler();
        if (levelGeneratorID == 0) GenerateMapOnTop();
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
        GameManager.instance.levelHandler.levelLayoutsCreated.Add(this.gameObject);
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



        for (int i = 0; i < platformPlacementList.Count; i++)
        {
            if (i == (int)((platformPlacementList.Count) / 2f))
            {

                Renderer renderer = platformPlacementList[i].GetComponent<Renderer>();
                float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
                GameObject Platforms = Instantiate(GameAssets.i.platformObjectsArray[0].surfaceGameObject,
                    new Vector3(randXOnRenderer, platformPlacementList[i].transform.position.y),
                    GameAssets.i.platformObjectsArray[0].surfaceGameObject.transform.rotation,
                    transform);

                //Add the platform to platform list
                platformList.Add(Platforms);
            }
            else
            {

                Renderer renderer = platformPlacementList[i].GetComponent<Renderer>();
                float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
                GameObject Platforms = Instantiate(GameAssets.i.platformObjectsArray[1].surfaceGameObject,
                    new Vector3(randXOnRenderer, platformPlacementList[i].transform.position.y),
                    GameAssets.i.platformObjectsArray[1].surfaceGameObject.transform.rotation,
                    transform);

                
                //Add the platform to platform list
                platformList.Add(Platforms);

            }
            //RespawnCoins
            RespawnCoinAround();
            ////////////////////////////////////  
        }
        //Respawn Coin in Middle
        RespawnCoinInMiddle();

    }
    
    void RespawnCoinAround()
    {
        int randomNum = RandomNumGenerator(0, platformList.Count);
        if (platformList[randomNum].tag == "Deadly")
        {
            randomNum = RandomNumGenerator(0, platformList.Count);
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
        Debug.Log(randomNumCoinInMiddle);
    }
    



    private int RandomNumGenerator(int min, int max)
    {

        int random = Random.Range(min, max);
        return random;
    }

    void SortAllPlatfromsBasedOnDistance()
    {
        platformList.Sort((IComparer<GameObject>)new sort());
    }

    void GetTheToppestAndBottomPlatform()
    {
        topObject = platformList.Last();
        bottomObject = platformList.First();
    }

    public void GenerateMapOnTop()
    {
        int randomNum = 0;
        if (LevelHandler.instance.currentDirection == CurrentDirection.UP)
        {
            randomNum = Random.Range(0, 3);
        }
        if (LevelHandler.instance.currentDirection == CurrentDirection.RIGHT)
        {
            randomNum = 2;
        }
        if (LevelHandler.instance.currentDirection == CurrentDirection.LEFT)
        {
            randomNum = 3;
        }
        // 

        for (int i = 1; i < numberOfMapToGenerate + 1; i++)
        {
            if (LevelHandler.instance.currentDirection == CurrentDirection.UP)
            {
                CreateLayoutAtTop(randomNum,i);
            }
            else if (LevelHandler.instance.currentDirection == CurrentDirection.RIGHT)
            {
                CreateLayoutAtRight(randomNum,i);
            }
            else if (LevelHandler.instance.currentDirection == CurrentDirection.LEFT)
            {
                CreateLayoutAtLeft(randomNum, i);
            }
        }
    }

    void CreateLayoutAtTop(int randomNum , int i)
    {
        /*
        float y = transform.parent.position.y - 0.01f;
        float desiredY = y + borderCollider.bounds.size.y * i;
        GameObject newLayout = Instantiate(GameAssets.i.levelLayoutsArray[randomNum].levelLayOutPrefab, new Vector3(transform.parent.position.x, desiredY), Quaternion.identity);
        newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID = +levelGeneratorID + i;
        newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.levelLayoutsArray[randomNum].direction + ")";
        SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[randomNum].direction);*/
        GameObject newLayout = Instantiate(GameAssets.i.levelLayoutsArray[randomNum].levelLayOutPrefab, new Vector3(transform.parent.position.x, transform.parent.position.y), Quaternion.identity);
        newLayout.transform.position = new Vector2(newLayout.transform.position.x, (newLayout.transform.position.y + borderCollider.bounds.size.y) * i);
        newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID = +levelGeneratorID + i;
        newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.levelLayoutsArray[randomNum].direction + ")";
        SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[randomNum].direction); 
    }

    void CreateLayoutAtRight(int randomNum, int i)
    {
        /*
        float x = transform.position.x + (borderCollider.bounds.size.x - 0.01f) * i;
        float desiredY = transform.parent.position.y;
        GameObject newLayout = Instantiate(GameAssets.i.levelLayoutsArray[randomNum].levelLayOutPrefab, new Vector3(x, desiredY), Quaternion.identity);
        newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID = +levelGeneratorID + i;
        newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.levelLayoutsArray[2].direction + ")";
        SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[2].direction);*/
        float y = transform.parent.position.y;
        float desiredY = y + borderCollider.bounds.size.y * i;
        float desiredX = transform.parent.position.x + borderCollider.size.x /3f -0.28f;
        GameObject newLayout = Instantiate(GameAssets.i.levelLayoutsArray[0].levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);
        newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID = +levelGeneratorID + i;
        newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.levelLayoutsArray[0].direction + ")";
        SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[0].direction);
    }

    void  CreateLayoutAtLeft(int randomNum, int i)
    {
        float y = transform.parent.position.y;
        float desiredY = y + borderCollider.bounds.size.y * i;
        float desiredX = transform.parent.position.x - borderCollider.size.x / 3f + 0.28f;
        GameObject newLayout = Instantiate(GameAssets.i.levelLayoutsArray[0].levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);
        newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID = +levelGeneratorID + i;
        newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.levelLayoutsArray[0].direction + ")";
        SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[0].direction);
    }

}



public class sort : IComparer<GameObject>
{

    int IComparer<GameObject>.Compare(GameObject _objA, GameObject _objB)
    {
        float t1 = Vector2.Distance(_objA.transform.position, new Vector2(LevelGenerator.borderCollider.bounds.min.x, LevelGenerator.borderCollider.bounds.min.y));
        float t2 = Vector2.Distance(_objB.transform.position, new Vector2(LevelGenerator.borderCollider.bounds.min.x, LevelGenerator.borderCollider.bounds.min.y));
        return t1.CompareTo(t2);
    }
}
