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

    int layer_mask;
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



    public void CheckForSpawningCoinAround(GameObject layoutCreated,LevelGenerator generator )
    {
        int randomized = 0;
    Randomize:
       
      
        if (generator.platformList[randomized].tag == "Deadly")
        {
            randomized += 1;
            if (randomized<generator.platformList.Count-1) goto Randomize;
            else return;


        }
        RespawnCoins(generator.platformList[randomized], GameAssets.i.thisPlatformType(generator.platformList[randomized].gameObject).radiusForCoins);
        
    }

    public void CheckForSpawningOpalInMiddle(GameObject layoutCreated, LevelGenerator generator)
    {
        int randomized = 0;

    RandomizeAgain:
       
      
        if (generator.platformList[randomized].tag == "Deadly" || generator.platformList[randomized + 1].tag == "Deadly")
        {
            randomized += 1;
            if (randomized < generator.platformList.Count - 1) goto RandomizeAgain;
            else return;

        }
       

        RespawnOpalInMiddle(generator.platformList[randomized], generator.platformList[randomized + 1]);
    }

    public void CheckForRespawnCoinInMiddle(GameObject layoutCreated, LevelGenerator generator)
    {
        int randomized = 0;
    RandomizeAgain:
       
       
        if (generator.platformList[randomized].tag == "Deadly" || generator.platformList[randomized + 1].tag == "Deadly")
        {
            randomized += 1;
            if (randomized < generator.platformList.Count - 1) goto RandomizeAgain;
            else return;
        }
        
        RespawnCoinsInMiddle(generator.platformList[randomized], generator.platformList[randomized + 1]);
        
    }

    public void RespawnCoins(GameObject surfacePos , float surfaceRadius)
    {
        Surfaces surface = surfacePos.GetComponent<Surfaces>();
       
        if (surface.alreadyRespawnedCoin) return;
        if(canRespawnCoinsAround)
        {
            GameObject mainParent = Instantiate(new GameObject("SurfaceParent"), surfacePos.transform.position, surfacePos.transform.rotation);
            surfacePos.transform.SetParent(mainParent.transform);
            int randomNumberOfCoins = Random.Range(3, 8);
            for (int i = 0; i < randomNumberOfCoins; i++)
            {
                float angle = i * Mathf.PI * 2f / randomNumberOfCoins;
                Vector3 newPos = new Vector2(surfacePos.transform.position.x + Mathf.Cos(angle) * surfaceRadius, surfacePos.transform.position.y + Mathf.Sin(angle) * surfaceRadius);
                GameObject go = Instantiate(GetGameObjectType(ItemType.Coin), newPos, Quaternion.identity, surfacePos.transform.parent);
                go.GetComponent<ObjectOverlap>().priority = 0;

            }
            surface.alreadyRespawnedCoin = true;
            surface.rotationSpeedRandom += (randomNumberOfCoins * 15f);
            canRespawnCoinsAround = false;

            


        }
       
    }

    public void RespawnOpalInMiddle(GameObject firstObject, GameObject secondObject)
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(firstObject.transform.position, 0.2f, (secondObject.transform.position - firstObject.transform.position), Vector2.Distance(secondObject.transform.position, firstObject.transform.position));
        for(int i =0; i<hit.Length;i++)
        {
            if(hit[i].collider.gameObject!=firstObject.gameObject)
            {
                if (hit[i].transform.gameObject.layer == 12 || hit[i].collider.gameObject.tag == "Deadly")
                {
                    GameObject collided = new GameObject("collided");
                    collided.transform.position = hit[i].transform.position;
                    return;
                }
               
            }
        }
        



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
                GameObject go = Instantiate(GetGameObjectType(ItemType.Opal), offsetPosition, Quaternion.identity, firstObject.transform.parent);
                go.GetComponent<ObjectOverlap>().priority = 2;



            }
            
            canRespawnOpalMiddle = false;
        }
        
        
          
        
    }
   
    public void RespawnCoinsInMiddle(GameObject firstObject, GameObject secondObject)
    {

        RaycastHit2D[] hit = Physics2D.CircleCastAll(firstObject.transform.position, 0.2f, (secondObject.transform.position - firstObject.transform.position), Vector2.Distance(secondObject.transform.position ,firstObject.transform.position));
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject != firstObject.gameObject && hit[i].collider.gameObject != secondObject.gameObject)
            {
                if (hit[i].transform.gameObject.layer == 12 || hit[i].collider.gameObject.tag == "Deadly" || hit[i].collider.gameObject.tag == "Surface")
                {
                    GameObject collided = new GameObject("collided");
                    collided.transform.position = hit[i].transform.position;
                    return;
                }

            }
        }




        Surfaces surface1 = firstObject.GetComponent<Surfaces>();
        Surfaces surface2 = secondObject.GetComponent<Surfaces>();
        if (surface1.alreadyRespawnedCoin || surface1.alreadyRespawnedCoin) return;
        int randomNumberOfCoins = Random.Range(3, 6);
        int randomNumberForChance = Random.Range(0, 2);
        //if (randomNumberForChance == 0)
        canRespawnCoinsMiddle = true;
        if(canRespawnCoinsMiddle)
        {
            for (int i = 1; i < randomNumberOfCoins; i++)
            {
                Vector2 position = firstObject.transform.position + (secondObject.transform.position - firstObject.transform.position) * 1 / randomNumberOfCoins * i;
                
                Vector2 offsetPosition = position + (new Vector2(secondObject.GetComponent<SpriteRenderer>().bounds.size.x, secondObject.GetComponent<SpriteRenderer>().bounds.size.y))
                    - (new Vector2(firstObject.GetComponent<SpriteRenderer>().bounds.size.x, firstObject.GetComponent<SpriteRenderer>().bounds.size.y));
                 GameObject go = Instantiate(GetGameObjectType(ItemType.Coin), offsetPosition, Quaternion.identity, firstObject.transform.parent);
                go.GetComponent<ObjectOverlap>().priority = 1;



            }
           
            canRespawnCoinsMiddle = false;
            
        }


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
