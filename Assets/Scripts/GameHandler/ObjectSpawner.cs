using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType { Coin }
public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectTypes;
    public bool shouldSpawnInStart = false;
    public bool canRespawnCoins = true;
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



    public void RespawnCoins(Transform surfacePos , float surfaceRadius)
    {
        if(canRespawnCoins)
        {
            int randomNumberOfCoins = Random.Range(3, 10);
            for (int i = 0; i < randomNumberOfCoins; i++)
            {
                float angle = i * Mathf.PI * 2f / randomNumberOfCoins;
                Vector3 newPos = new Vector2(surfacePos.position.x + Mathf.Cos(angle) * surfaceRadius, surfacePos.position.y + Mathf.Sin(angle) * surfaceRadius);
                GameObject go = Instantiate(GetGameObjectType(ItemType.Coin), newPos, Quaternion.identity, surfacePos.parent);
                
            }
            canRespawnCoins = false;
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
}
