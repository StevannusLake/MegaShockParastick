using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public int levelGeneratorID = 0;
    public List<GameObject> platformPlacementListWhite;
    public List<GameObject> platformPlacementListBlue;
    public List<GameObject> platformPlacementListRed;
    public List<GameObject> platformList;
    public List<SpriteRenderer> backgroundSprites;
    public GameObject topObject;
    public GameObject bottomObject;
    public BoxCollider2D borderCollider;
    public Transform pivotAnchor;
    public Transform nextLayoutAnchor;
    public Transform defaultOffset;
 
    private bool DangerAlreadyMade = false;
    public bool test;

    public CurrentDirection[] upwardPossibilities;

    private void Awake()
    {

        
        platformList = new List<GameObject>();
        platformPlacementListWhite = new List<GameObject>(); //Only one is dangerous
        platformPlacementListRed = new List<GameObject>();// for sure dangerous
        platformPlacementListBlue = new List<GameObject>();//Moving platform
        borderCollider = transform.parent.Find("PipeShape").GetComponent<BoxCollider2D>();
        pivotAnchor = transform.parent;
        nextLayoutAnchor = transform.parent.Find("NextLayoutAnchor").transform;
        defaultOffset= transform.parent.Find("DefaultOffset").transform;
        backgroundSprites = new List<SpriteRenderer>();

    }
    private void Start()
    {
        upwardPossibilities = new CurrentDirection[] { CurrentDirection.UP, CurrentDirection.LEFT, CurrentDirection.RIGHT };
        

    }

    void GetAllBackgrounds()
    {
        //
       GameObject brownBackgroundParent = transform.parent.Find("BrownBackground").gameObject;
        for (int i = 0; i < brownBackgroundParent.transform.childCount; i++)
        {
            if (brownBackgroundParent.transform.GetChild(i).gameObject.layer == 10)
            {
                backgroundSprites.Add(brownBackgroundParent.transform.GetChild(i).GetComponent<SpriteRenderer>());
            }
 
        }
        
    }


    public void Initialize()
    {
        GetAllBackgrounds();
        AddChildsToList();
        RespawnPlatforms();
        SortAllPlatfromsBasedOnDistance();
        GetTheToppestAndBottomPlatform();
        RemoveSpriteRenderers();
        if (levelGeneratorID == 0) AddSelfToLevelHandler();
    }



    void SendLastGeneratedLevel(LevelGenerator lastGenerator, CurrentDirection direction)
    {
        LevelHandler.instance.GetLastGeneratedLevel(lastGenerator, direction);
    }

    void AddChildsToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "PlatformChildWhite")
            {
                platformPlacementListWhite.Add(transform.GetChild(i).gameObject);
            }
            if (transform.GetChild(i).gameObject.tag == "PlatformChildRed")
            {
                platformPlacementListRed.Add(transform.GetChild(i).gameObject);
            }
             if (transform.GetChild(i).gameObject.tag == "PlatformChildBlue")
            {
                platformPlacementListBlue.Add(transform.GetChild(i).gameObject);
            }

        }
    }
    void AddSelfToLevelHandler()
    {
        GameManager.instance.levelHandler.levelLayoutsCreated.Add(transform.parent.parent.gameObject);

    }
    void RemoveSpriteRenderers()
    {
        foreach (GameObject obj in platformPlacementListWhite)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject obj in platformPlacementListRed)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject obj in platformPlacementListBlue)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void RespawnPlatforms()
    {

        // White Platforms
        #region White Platforms
        int randomDangerousPlatform = Random.Range(1, platformPlacementListWhite.Count);

        for (int i = 0; i < platformPlacementListWhite.Count; i++)
        {
            if (i == randomDangerousPlatform)
            {

                Renderer renderer = platformPlacementListWhite[i].GetComponent<Renderer>();
                float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
                float randYOnRenderer = Random.Range(renderer.bounds.min.y, renderer.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Dangerous),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Safe).transform.rotation,
                    transform);

                //Add the platform to platform list
                platformList.Add(Platforms);
            }
            else
            {

                Renderer renderer = platformPlacementListWhite[i].GetComponent<Renderer>();
                float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
                float randYOnRenderer = Random.Range(renderer.bounds.min.y, renderer.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Safe),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Dangerous).transform.rotation,
                    transform);
                //Add the platform to platform list
                platformList.Add(Platforms);

            }


        }
        #endregion

        #region Red Platforms
        if (platformPlacementListRed.Count > 0)
        {
            for (int i = 0; i < platformPlacementListRed.Count; i++)
            {
                Renderer renderer = platformPlacementListRed[i].GetComponent<Renderer>();
                float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
                float randYOnRenderer = Random.Range(renderer.bounds.min.y, renderer.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Dangerous),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Dangerous).transform.rotation,
                    transform);

                //Add the platform to platform list
                platformList.Add(Platforms);

            }
        }


        #endregion

        #region Blue Platforms
        if (platformPlacementListBlue.Count > 0)
        {
            for (int i = 0; i < platformPlacementListBlue.Count; i++)
            {
                Renderer renderer = platformPlacementListBlue[i].GetComponent<Renderer>();
                float randXOnRenderer = Random.Range(renderer.bounds.min.x, renderer.bounds.max.x);
                float randYOnRenderer = Random.Range(renderer.bounds.min.y, renderer.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Moving),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Moving).transform.rotation,
                    transform);
                Platforms.GetComponent<Surfaces>().platformPlacementTransform = platformPlacementListBlue[i].transform;
                //Add the platform to platform list
                platformList.Add(Platforms);

            }
        }


        #endregion



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
        
        //initializing
        if (transform.parent.gameObject.GetComponentInChildren<EnterController>().isAlreadyActivated) return;
        int offsetLayout = 0;
        if (isFirst) offsetLayout = LevelHandler.instance.levelLayoutsCreated.Count-1;

       
        
        
        for (int i = 1; i < LevelHandler.instance.numberOfMapToGenerate; i++)
        {
            ///
          


            int number = Random.Range(0, upwardPossibilities.Length);
            CurrentDirection randomDirection = upwardPossibilities[number];
            Transform currentParent = LevelHandler.instance.levelLayoutsCreated[i-1+ offsetLayout].gameObject.transform;
            Transform currentAnchor = currentParent.GetComponentInChildren<LevelGenerator>().nextLayoutAnchor;
            BoxCollider2D currentBoxCollider= LevelHandler.instance.levelLayoutsCreated[i -1+ offsetLayout].GetComponentInChildren<LevelGenerator>().borderCollider;

            if (LevelHandler.instance.currentDirection == CurrentDirection.UP)
            {
                
                float desiredY = currentAnchor.position.y;
                float desiredX = currentAnchor.position.x;
                Debug.Log(currentParent);

                GameObject newLayout = Instantiate(GameAssets.i.GetDesiredLevelLayout( randomDirection).levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);
                LevelGenerator newLayoutGenerator = newLayout.GetComponentInChildren<LevelGenerator>();
                newLayoutGenerator.pivotAnchor.position = new Vector2(desiredX, desiredY);            
                newLayoutGenerator.levelGeneratorID = +levelGeneratorID + i;
                newLayoutGenerator.Initialize();
                ChangeNextLayoutBackgroundLayers(newLayoutGenerator.backgroundSprites, backgroundSprites[0].sortingOrder,i);
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.GetDesiredLevelLayout( randomDirection).direction + ")";
                SendLastGeneratedLevel(newLayoutGenerator, GameAssets.i.GetDesiredLevelLayout( randomDirection).direction);             
                if(i!= (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                LevelHandler.instance.LevelAdded(newLayout, newLayoutGenerator, "Up");

            }
            else if (LevelHandler.instance.currentDirection == CurrentDirection.RIGHT)
            {

               
                float desiredY = currentAnchor.position.y;
                float desiredX = currentAnchor.position.x;


                GameObject newLayout = Instantiate(GameAssets.i.GetDesiredLevelLayout( randomDirection).levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);

                LevelGenerator newLayoutGenerator = newLayout.GetComponentInChildren<LevelGenerator>();
                newLayoutGenerator.pivotAnchor.position = new Vector2(desiredX, desiredY);              
                newLayoutGenerator.levelGeneratorID = +levelGeneratorID + i;
                newLayoutGenerator.Initialize();
                ChangeNextLayoutBackgroundLayers(newLayoutGenerator.backgroundSprites, backgroundSprites[0].sortingOrder,i);
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.GetDesiredLevelLayout(randomDirection).direction + ")";
                SendLastGeneratedLevel(newLayoutGenerator, GameAssets.i.GetDesiredLevelLayout(randomDirection).direction);
                if (i != (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                LevelHandler.instance.LevelAdded(newLayout, newLayoutGenerator, "Right");

            }
            else if (LevelHandler.instance.currentDirection == CurrentDirection.LEFT)
            {

                
                float desiredY = currentAnchor.position.y;
                float desiredX = currentAnchor.position.x;


                GameObject newLayout = Instantiate(GameAssets.i.GetDesiredLevelLayout( randomDirection).levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);

                LevelGenerator newLayoutGenerator = newLayout.GetComponentInChildren<LevelGenerator>();
                newLayoutGenerator.pivotAnchor.position = new Vector2(desiredX, desiredY);          
                newLayoutGenerator.levelGeneratorID = +levelGeneratorID + i;
                newLayoutGenerator.Initialize();
                ChangeNextLayoutBackgroundLayers(newLayoutGenerator.backgroundSprites, backgroundSprites[0].sortingOrder,i);
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.GetDesiredLevelLayout(randomDirection).direction + ")";
                SendLastGeneratedLevel(newLayoutGenerator, GameAssets.i.GetDesiredLevelLayout(randomDirection).direction);
                if (i != (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                LevelHandler.instance.LevelAdded(newLayout, newLayoutGenerator, "Left");

            }
        }
        
        
    }

    void ChangeNextLayoutBackgroundLayers(List<SpriteRenderer> listOfBackground,int prevLayoutBackgroundLayer,int numberOfMapGenerated)
    {
        Debug.Log("ChangedOrder");
        for (int i=0;i<listOfBackground.Count;i++)
        {
            listOfBackground[i].sortingOrder = prevLayoutBackgroundLayer - (1* numberOfMapGenerated);
            
        }
    

    }
    private  int Compare(GameObject _objA, GameObject _objB )
    {
        float t1 = Vector2.Distance(_objA.transform.position, new Vector2(borderCollider.bounds.min.x, borderCollider.bounds.min.y));
        float t2 = Vector2.Distance(_objB.transform.position, new Vector2(borderCollider.bounds.min.x, borderCollider.bounds.min.y));
        return t1.CompareTo(t2);
    }



}




