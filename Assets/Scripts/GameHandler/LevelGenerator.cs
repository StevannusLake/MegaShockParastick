using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public int levelGeneratorID = 0;
   [HideInInspector] public List<GameObject> platformPlacementList;
    public List<GameObject> platformList;
    public GameObject topObject;
    public GameObject bottomObject;
    public static BoxCollider2D borderCollider;
    public bool isAlreadyMade;
    public int numberOfMapToGenerate = 2;

    private void Awake()
    {
        AddChildsToList();

    }
    private void Start()
    {
        borderCollider = gameObject.GetComponent<BoxCollider2D>();   
        if (levelGeneratorID == 0) GenerateMapOnTop();    
        RespawnPlatforms();
        SortAllPlatfromsBasedOnDistance();
        GetTheToppestAndBottomPlatform();
        RemoveSpriteRenderers();
        AddSelfToLevelHandler();
    }

    void SendLastGeneratedLevel(LevelGenerator lastGenerator,CurrentDirection direction)
    {
        LevelHandler.instance.GetLastGeneratedLevel(lastGenerator, direction);
    }

    void AddChildsToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            platformPlacementList.Add(transform.GetChild(i).gameObject);
        }
    }
    void AddSelfToLevelHandler()
    {
        GameManager.instance.levelHandler.levelLayoutsCreated.Add(this.gameObject);
    }
    void RemoveSpriteRenderers()
    {
        foreach(GameObject obj in platformPlacementList)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void RespawnPlatforms()
    {
        for (int i = 0; i < platformPlacementList.Count; i++)
        {
            int randomPlatform = Random.Range(0, GameAssets.i.platformObjectsArray.Length);
            Renderer renderer = platformPlacementList[i].GetComponent<Renderer>();
            float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
            GameObject Platforms = Instantiate(GameAssets.i.platformObjectsArray[randomPlatform].surfaceGameObject,
                new Vector3(randXOnRenderer, platformPlacementList[i].transform.position.y),
                GameAssets.i.platformObjectsArray[randomPlatform].surfaceGameObject.transform.rotation,
                transform);
            //Add the platform to platform list
            platformList.Add(Platforms);
       
        }
    }
   

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,200,100,200),"Generate Map"))
        {
            GenerateMapOnTop();
        }
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

    void GenerateMapOnTop()
    {
        
        //
        for (int i=1; i<numberOfMapToGenerate+1;i++)
        {
            float y = transform.GetComponent<BoxCollider2D>().bounds.center.y * i;
            float desiredY = y + transform.GetComponent<BoxCollider2D>().bounds.size.y *i;
            GameObject newLayout=  Instantiate(GameAssets.i.levelLayoutsArray[0].levelLayOutPrefab, new Vector3(transform.position.x, desiredY), Quaternion.identity);       
            //Change level generator ID upon creation
            newLayout.GetComponentInChildren<LevelGenerator>().levelGeneratorID =+ levelGeneratorID+i;
            newLayout.name = "LevelLayout-"+(this.levelGeneratorID + 2)+"(" +GameAssets.i.levelLayoutsArray[0].direction+")" ;
            SendLastGeneratedLevel(newLayout.GetComponentInChildren<LevelGenerator>(), GameAssets.i.levelLayoutsArray[0].direction);
        }
        // Make sure to sort
       

    }

   


    [System.Serializable]
    private class sort : IComparer<GameObject>
    {
        
        int IComparer<GameObject>.Compare(GameObject _objA, GameObject _objB)
        {
            float t1 = Vector2.Distance(_objA.transform.position, new Vector2(LevelGenerator.borderCollider.bounds.min.x, LevelGenerator.borderCollider.bounds.min.y));
            float t2 = Vector2.Distance(_objB.transform.position, new Vector2(LevelGenerator.borderCollider.bounds.min.x, LevelGenerator.borderCollider.bounds.min.y));
            return t1.CompareTo(t2);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(levelGeneratorID!=0)
        {
            if (other.gameObject.tag == "Player" && !isAlreadyMade)
            {
                GenerateMapOnTop();
                isAlreadyMade = true;
            }
           
        }
       
    }
}
