using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectTypes;
    public bool shouldSpawnInStart = false;
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
}
