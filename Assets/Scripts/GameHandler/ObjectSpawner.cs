using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType { Coin,Opal }
public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectTypes;
    public bool shouldSpawnInStart = false;
    public bool canRespawnCoinsAround = false;
    public bool canRespawnCoinsMiddle = false;
    public bool canRespawnOpalMiddle = false;
    float SpawnRateInSeconds = 1.0f; // First time to start the spawning . For test only
    public static ObjectSpawner instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    void Start()
    {
        if(shouldSpawnInStart) Invoke("SpawnObject", SpawnRateInSeconds);

    }

   


    static void SpawnSpecificObject(GameObject obj,Vector3 customMin,Vector3 customMax)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //Vector2 min = customMin;
        //Vector2 max = customMax;
        
        GameObject aObject = (GameObject)Instantiate(obj);
        float width = aObject.GetComponent<Renderer>().bounds.size.y;
        aObject.transform.position = new Vector2(Random.Range(min.x, max.x), min.y-width ); // random range for now
        

    }

    public void SpawnObject()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));     
        GameObject aObject = (GameObject)Instantiate(objectTypes[0]);
        float width = aObject.GetComponent<Renderer>().bounds.size.y;
        aObject.transform.position = new Vector2(Random.Range(min.x, max.x), min.y - width/2f); // random range for now
        ScheduleNextObjectSpawn(); //if needed
    }
    void ScheduleNextObjectSpawn()
    {
        
        SpawnRateInSeconds = Random.Range(3.0f,6.0f);
        Invoke("SpawnObject", SpawnRateInSeconds);
    }



    public void CheckForSpawningCoinAround(GameObject layoutCreated,LevelGenerator generator)
    {
        Randomize:
        int randomNum = RandomNumGenerator(0, generator.platformList.Count);
        if (generator.platformList[randomNum].tag == "Deadly")
        {
            goto Randomize;
        }
        RespawnCoins(generator.platformList[randomNum], GameAssets.i.platformObjectsArray[0].radiusForCoins);
        
    }

    public void CheckForSpawningOpalInMiddle(GameObject layoutCreated, LevelGenerator generator)
    {
       
    RandomizeAgain:
        int randomNumCoinInMiddle = Random.Range(0, generator.platformList.Count - 1);
        if (generator.platformList[randomNumCoinInMiddle].tag == "Deadly" || generator.platformList[randomNumCoinInMiddle + 1].tag == "Deadly")
        {
            goto RandomizeAgain;
        }
       

        RespawnOpalInMiddle(generator.platformList[randomNumCoinInMiddle], generator.platformList[randomNumCoinInMiddle + 1]);
    }

    public void CheckForRespawnCoinInMiddle(GameObject layoutCreated, LevelGenerator generator)
    {
    RandomizeAgain:
        int randomNumCoinInMiddle = Random.Range(0, generator.platformList.Count - 1);
        if (generator.platformList[randomNumCoinInMiddle].tag == "Deadly" || generator.platformList[randomNumCoinInMiddle + 1].tag == "Deadly")
        {
            goto RandomizeAgain;
        }
        
        RespawnCoinsInMiddle(generator.platformList[randomNumCoinInMiddle], generator.platformList[randomNumCoinInMiddle + 1]);
        
    }

    public void RespawnCoins(GameObject surfacePos , float surfaceRadius)
    {
        Surfaces surface = surfacePos.GetComponent<Surfaces>();
        if (surface.alreadyRespawnedCoin) return;
        if(canRespawnCoinsAround)
        {
            int randomNumberOfCoins = Random.Range(3, 8);
            for (int i = 0; i < randomNumberOfCoins; i++)
            {
                float angle = i * Mathf.PI * 2f / randomNumberOfCoins;
                Vector3 newPos = new Vector2(surfacePos.transform.position.x + Mathf.Cos(angle) * surfaceRadius, surfacePos.transform.position.y + Mathf.Sin(angle) * surfaceRadius);
                GameObject go = Instantiate(GetGameObjectType(ItemType.Coin), newPos, Quaternion.identity, surfacePos.transform.parent);
                
            }
            surface.alreadyRespawnedCoin = true;
            surface.rotationSpeedRandom += (randomNumberOfCoins * 3f);
            canRespawnCoinsAround = false;

            


        }
       
    }

    public void RespawnOpalInMiddle(GameObject firstObject, GameObject secondObject)
    {
        RaycastHit2D hit = Physics2D.Raycast(firstObject.transform.position, (secondObject.transform.position - firstObject.transform.position));
        if (hit.transform.tag == "Deadly") return;


        Surfaces surface1 = firstObject.GetComponent<Surfaces>();
        Surfaces surface2 = secondObject.GetComponent<Surfaces>();
        
        int numberOfOpals = 1;
        
        if(canRespawnOpalMiddle)
        {
            for (int i = 0; i < numberOfOpals; i++)
            {
                Vector2 position = firstObject.transform.position + (secondObject.transform.position - firstObject.transform.position) * 1 / 2;
                Vector2 offsetPosition = position + (new Vector2(secondObject.GetComponent<SpriteRenderer>().bounds.size.x, secondObject.GetComponent<SpriteRenderer>().bounds.size.y))
                    - (new Vector2(firstObject.GetComponent<SpriteRenderer>().bounds.size.x, firstObject.GetComponent<SpriteRenderer>().bounds.size.y));
                if (checkIfPosEmpty(offsetPosition))
                {
                    GameObject go = Instantiate(GetGameObjectType(ItemType.Opal), offsetPosition, Quaternion.identity, firstObject.transform.parent);
                }
               

            }
            surface1.alreadyRespawnedCoin = true;
            surface2.alreadyRespawnedCoin = true;
            canRespawnOpalMiddle = false;
        }
        
        
          
        
    }
    public void RespawnCoinsInMiddle(GameObject firstObject, GameObject secondObject)
    {

        RaycastHit2D hit = Physics2D.Raycast(firstObject.transform.position, (secondObject.transform.position - firstObject.transform.position));
        if (hit.transform.tag == "Deadly") return;



            Surfaces surface1 = firstObject.GetComponent<Surfaces>();
        Surfaces surface2 = secondObject.GetComponent<Surfaces>();
        if (surface1.alreadyRespawnedCoin || surface1.alreadyRespawnedCoin) return;
        int randomNumberOfCoins = Random.Range(3, 6);
        int randomNumberForChance = Random.Range(0, 2);
        if (randomNumberForChance == 0) canRespawnCoinsMiddle = true;
        if(canRespawnCoinsMiddle)
        {
            for (int i = 1; i < randomNumberOfCoins; i++)
            {
                Vector2 position = firstObject.transform.position + (secondObject.transform.position - firstObject.transform.position) * 1 / randomNumberOfCoins * i;
                if(i==1) position= firstObject.transform.position + (secondObject.transform.position - firstObject.transform.position) * 1 / 2;
                Vector2 offsetPosition = position + (new Vector2(secondObject.GetComponent<SpriteRenderer>().bounds.size.x, secondObject.GetComponent<SpriteRenderer>().bounds.size.y))
                    - (new Vector2(firstObject.GetComponent<SpriteRenderer>().bounds.size.x, firstObject.GetComponent<SpriteRenderer>().bounds.size.y));
                if (checkIfPosEmpty(offsetPosition))
                {
                    GameObject go = Instantiate(GetGameObjectType(ItemType.Coin), offsetPosition, Quaternion.identity, firstObject.transform.parent);
                }
               
                
            }
            surface1.alreadyRespawnedCoin = true;
            surface2.alreadyRespawnedCoin = true;
            canRespawnCoinsMiddle = false;
            
        }


    }

  


public bool checkIfPosEmpty(Vector3 targetPos)
{
    GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Coin");
    foreach (GameObject current in allMovableThings)
    {
        if (current.transform.position == targetPos)
            return false;
    }
    return true;
}



public GameObject GetGameObjectType(ItemType typeObject)
    {
        foreach (GameAssets.Items itemGameObject in GameAssets.i.itemsArray)
        {
            if (itemGameObject.type == typeObject)
            {
                return itemGameObject.itemPrefab;
            }
        }
        Debug.LogError("Item" + typeObject + "wasnt found!");
        return null;
    }



    private int RandomNumGenerator(int min, int max)
    {

        int random = Random.Range(min, max);
        return random;
    }
}
