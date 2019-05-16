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
    public List<GameObject> platformPlacementListGreen;
    public List<GameObject> platformPlacementListRedMoving;
    public List<GameObject> platformList;
    public List<SpriteRenderer> backgroundSprites;
    public GameObject topObject;
    public GameObject bottomObject;
    public BoxCollider2D borderCollider;
    public Transform pivotAnchor;
    public Transform nextLayoutAnchor;
    public Transform defaultOffset;
    public bool isDummy = false;
 
    private bool DangerAlreadyMade = false;
    public bool test;

    public static CurrentDirection[] upwardPossibilities;

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

    private void Update()
    {
       
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
    public void ChangeBackgroundOrders()
    {
        for (int i = 1; i < backgroundSprites.Count; i++)
        {
            backgroundSprites[i].sortingOrder = backgroundSprites[i - 1].sortingOrder - 1;

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

    public void PostInitialize()
    {
        ChangeBackgroundOrders();
    }



    void SendLastGeneratedLevel(LevelGenerator lastGenerator, CurrentDirection direction)
    {
        LevelHandler.instance.GetLastGeneratedLevel(lastGenerator, direction);
    }

    void AddChildsToList()
    {
        Transform difficultyBasedTransform = DifficultyBasedLayoutChildTransform(LevelHandler.instance.levelDifficulty);
        difficultyBasedTransform.gameObject.SetActive(true);
        for (int i = 0; i < difficultyBasedTransform.childCount; i++)
        {
            if (difficultyBasedTransform.GetChild(i).gameObject.tag == "PlatformChildWhite")
            {
                platformPlacementListWhite.Add(difficultyBasedTransform.GetChild(i).gameObject);
            }
            if (difficultyBasedTransform.GetChild(i).gameObject.tag == "PlatformChildRed")
            {
                platformPlacementListRed.Add(difficultyBasedTransform.GetChild(i).gameObject);
            }
             if (difficultyBasedTransform.GetChild(i).gameObject.tag == "PlatformChildBlue")
            {
                platformPlacementListBlue.Add(difficultyBasedTransform.GetChild(i).gameObject);
            }
            if (difficultyBasedTransform.GetChild(i).gameObject.tag == "PlatformChildGreen")
            {
                platformPlacementListGreen.Add(difficultyBasedTransform.GetChild(i).gameObject);
            }
            if (difficultyBasedTransform.GetChild(i).gameObject.tag == "PlatformChildRedMoving")
            {
                platformPlacementListRedMoving.Add(difficultyBasedTransform.GetChild(i).gameObject);
            }


        }
    }

    Transform DifficultyBasedLayoutChildTransform(LevelDifficulty levelDifficulty)
    {
        List<Transform> possibleTransforms = new List<Transform>();
        int randomLayoutChooser;

        switch (levelDifficulty)
        {
             
            case LevelDifficulty.A:
                
                for (int i=0; i<transform.childCount;i++)
                {
                    if (transform.GetChild(i).name.Contains("A")) possibleTransforms.Add(transform.GetChild(i).transform);

                }
                randomLayoutChooser = Random.Range(0, possibleTransforms.Count);
                return possibleTransforms[randomLayoutChooser];
            case LevelDifficulty.B:
                
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).name.Contains("B")) possibleTransforms.Add(transform.GetChild(i).transform);

                }
                 randomLayoutChooser = Random.Range(0, possibleTransforms.Count);
                return possibleTransforms[randomLayoutChooser];

            case LevelDifficulty.C:

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).name.Contains("C")) possibleTransforms.Add(transform.GetChild(i).transform);

                }
                randomLayoutChooser = Random.Range(0, possibleTransforms.Count);
                return possibleTransforms[randomLayoutChooser];

            case LevelDifficulty.D:

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).name.Contains("D")) possibleTransforms.Add(transform.GetChild(i).transform);

                }
                randomLayoutChooser = Random.Range(0, possibleTransforms.Count);
                return possibleTransforms[randomLayoutChooser];

            case LevelDifficulty.E:

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).name.Contains("E")) possibleTransforms.Add(transform.GetChild(i).transform);

                }
                randomLayoutChooser = Random.Range(0, possibleTransforms.Count);
                return possibleTransforms[randomLayoutChooser];
            case LevelDifficulty.F:

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).name.Contains("F")) possibleTransforms.Add(transform.GetChild(i).transform);

                }
                randomLayoutChooser = Random.Range(0, possibleTransforms.Count);
                return possibleTransforms[randomLayoutChooser];
        }
        return null;
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
        foreach (GameObject obj in platformPlacementListRedMoving)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject obj in platformPlacementListGreen)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void RespawnPlatforms()
    {
        
        // White Platforms
        //Respawn one dangerous randomly
        #region White Platforms
        int randomDangerousPlatform = Random.Range(1, platformPlacementListWhite.Count);

        for (int i = 0; i < platformPlacementListWhite.Count; i++)
        {
            if (i == randomDangerousPlatform)
            {

                BoxCollider2D collider = platformPlacementListWhite[i].GetComponent<BoxCollider2D>();
                float randXOnRenderer = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                float randYOnRenderer = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Dangerous),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Safe).transform.rotation,
                    transform);

                //Add the platform to platform list
                platformList.Add(Platforms);
            }
            else
            {

                BoxCollider2D collider = platformPlacementListWhite[i].GetComponent<BoxCollider2D>();
                float randXOnRenderer = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                float randYOnRenderer = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Safe),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Dangerous).transform.rotation,
                    transform);
                //Add the platform to platform list
                platformList.Add(Platforms);

            }


        }
        #endregion 

        //Respawn Only Dangerous
        #region Red Platforms
        if (platformPlacementListRed.Count > 0)
        {
            for (int i = 0; i < platformPlacementListRed.Count; i++)
            {
                BoxCollider2D collider = platformPlacementListRed[i].GetComponent<BoxCollider2D>();
                float randXOnRenderer = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                float randYOnRenderer = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Dangerous),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Dangerous).transform.rotation,
                    transform);

                //Add the platform to platform list
                platformList.Add(Platforms);

            }
        }


        #endregion //Respawn Dangerous Only

        //Respawn moving platform
        #region Blue Platforms
        if (platformPlacementListBlue.Count > 0)
        {
            for (int i = 0; i < platformPlacementListBlue.Count; i++)
            {
                BoxCollider2D collider = platformPlacementListBlue[i].GetComponent<BoxCollider2D>();
                float randXOnRenderer = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                float randYOnRenderer = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Moving),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Moving).transform.rotation,
                    transform);
                Platforms.GetComponent<Surfaces>().platformPlacementTransform = platformPlacementListBlue[i].transform;
                Platforms.GetComponent<Surfaces>().isMover = true;
                //Add the platform to platform list
                platformList.Add(Platforms);

            }
        }


        #endregion

        //Respawn Only Safe
        #region Silver Platforms
        if (platformPlacementListGreen.Count > 0)
        {
            for (int i = 0; i < platformPlacementListGreen.Count; i++)
            {
                BoxCollider2D collider = platformPlacementListGreen[i].GetComponent<BoxCollider2D>();
                float randXOnRenderer = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                float randYOnRenderer = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Safe),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.Safe).transform.rotation,
                    transform);
               
                //Add the platform to platform list
                platformList.Add(Platforms);

            }
        }


        #endregion

        //Respawn Dangerous which they move
        #region RedMoving Platforms
        if (platformPlacementListRedMoving.Count > 0)
        {
            for (int i = 0; i < platformPlacementListRedMoving.Count; i++)
            {
                BoxCollider2D collider = platformPlacementListRedMoving[i].GetComponent<BoxCollider2D>();
                float randXOnRenderer = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                float randYOnRenderer = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                GameObject Platforms = Instantiate(GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.DangerousMoving),
                    new Vector3(randXOnRenderer, randYOnRenderer),
                    GameAssets.i.GetDesiredPlatform(Surfaces.SurfaceTypes.DangerousMoving).transform.rotation,
                    transform);
                Platforms.GetComponent<Surfaces>().platformPlacementTransform = platformPlacementListRedMoving[i].transform;
                Platforms.GetComponent<Surfaces>().isMover = true;
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

                GameObject newLayout = Instantiate(GameAssets.i.GetDesiredLevelLayout( randomDirection,LevelHandler.instance.levelType).levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);
                LevelGenerator newLayoutGenerator = newLayout.GetComponentInChildren<LevelGenerator>();
                newLayoutGenerator.pivotAnchor.position = new Vector2(desiredX, desiredY);            
                newLayoutGenerator.levelGeneratorID = +levelGeneratorID + i;
                newLayoutGenerator.Initialize();
                ChangeNextLayoutBackgroundLayers(newLayoutGenerator.backgroundSprites, currentParent.GetComponentInChildren<LevelGenerator>().backgroundSprites.Last().sortingOrder, i);
                newLayoutGenerator.PostInitialize();
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.GetDesiredLevelLayout( randomDirection, LevelHandler.instance.levelType).direction + ")";
                SendLastGeneratedLevel(newLayoutGenerator, GameAssets.i.GetDesiredLevelLayout( randomDirection, LevelHandler.instance.levelType).direction);             
                if(i!= (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                LevelHandler.instance.LevelAdded(newLayout, newLayoutGenerator, "Up");

            }
            else if (LevelHandler.instance.currentDirection == CurrentDirection.RIGHT)
            {

               
                float desiredY = currentAnchor.position.y;
                float desiredX = currentAnchor.position.x;


                GameObject newLayout = Instantiate(GameAssets.i.GetDesiredLevelLayout( randomDirection, LevelHandler.instance.levelType).levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);

                LevelGenerator newLayoutGenerator = newLayout.GetComponentInChildren<LevelGenerator>();
                newLayoutGenerator.pivotAnchor.position = new Vector2(desiredX, desiredY);              
                newLayoutGenerator.levelGeneratorID = +levelGeneratorID + i;
                newLayoutGenerator.Initialize();
                ChangeNextLayoutBackgroundLayers(newLayoutGenerator.backgroundSprites, currentParent.GetComponentInChildren<LevelGenerator>().backgroundSprites.Last().sortingOrder, i);
                newLayoutGenerator.PostInitialize();
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.GetDesiredLevelLayout(randomDirection, LevelHandler.instance.levelType).direction + ")";
                SendLastGeneratedLevel(newLayoutGenerator, GameAssets.i.GetDesiredLevelLayout(randomDirection, LevelHandler.instance.levelType).direction);
                if (i != (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                LevelHandler.instance.LevelAdded(newLayout, newLayoutGenerator, "Right");

            }
            else if (LevelHandler.instance.currentDirection == CurrentDirection.LEFT)
            {

                
                float desiredY = currentAnchor.position.y;
                float desiredX = currentAnchor.position.x;


                GameObject newLayout = Instantiate(GameAssets.i.GetDesiredLevelLayout( randomDirection, LevelHandler.instance.levelType).levelLayOutPrefab, new Vector3(desiredX, desiredY), Quaternion.identity);

                LevelGenerator newLayoutGenerator = newLayout.GetComponentInChildren<LevelGenerator>();
                newLayoutGenerator.pivotAnchor.position = new Vector2(desiredX, desiredY);          
                newLayoutGenerator.levelGeneratorID = +levelGeneratorID + i;
                newLayoutGenerator.Initialize();
                ChangeNextLayoutBackgroundLayers(newLayoutGenerator.backgroundSprites, currentParent.GetComponentInChildren<LevelGenerator>().backgroundSprites.Last().sortingOrder, i);
                newLayoutGenerator.PostInitialize();
                newLayout.name = "LevelLayout-" + (this.levelGeneratorID + 2) + "(" + GameAssets.i.GetDesiredLevelLayout(randomDirection, LevelHandler.instance.levelType).direction + ")";
                SendLastGeneratedLevel(newLayoutGenerator, GameAssets.i.GetDesiredLevelLayout(randomDirection, LevelHandler.instance.levelType).direction);
                if (i != (int)LevelHandler.instance.numberOfMapToGenerate - LevelHandler.instance.whenToGenerateMoreMaps) newLayout.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                LevelHandler.instance.LevelAdded(newLayout, newLayoutGenerator, "Left");

            }
        }
        
        
    }

    void ChangeNextLayoutBackgroundLayers(List<SpriteRenderer> listOfBackground,int prevLayoutBackgroundLayer,int numberOfMapGenerated)
    {
        
        
            listOfBackground[0].sortingOrder = prevLayoutBackgroundLayer - (1* numberOfMapGenerated);
            
        
    

    }
    private  int Compare(GameObject _objA, GameObject _objB )
    {
        float t1 = Vector2.Distance(_objA.transform.position, new Vector2(borderCollider.bounds.min.x, borderCollider.bounds.min.y));
        float t2 = Vector2.Distance(_objB.transform.position, new Vector2(borderCollider.bounds.min.x, borderCollider.bounds.min.y));
        return t1.CompareTo(t2);
    }



}




