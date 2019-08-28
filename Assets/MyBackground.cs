using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBackground : MonoBehaviour
{
    public GameObject initialBackgroundPos;
    public GameObject backgroundPackPrefab;
    public Movement movementScript;
    public float distanceOfNewBackground;
    public float distanceOfOldBackground;
    public float rangeOfRemovingOldBackground;

    public List<GameObject> listOfBackgroundPrefabs;
    private int newBackground;
    private int oldBackground;

    public bool justSpawned = false;

    public float tempExtraDistance;

    public GameObject inGameMenu;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        listOfBackgroundPrefabs.Add(initialBackgroundPos);
        
        distanceOfNewBackground = 3;
        distanceOfOldBackground = 15;
        rangeOfRemovingOldBackground = 30;
        newBackground = 1; //! 1 is because of initialBackgroundPos that has been placed before start
        oldBackground = 0; //! Always set to 0 because of always removing the last one

        justSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGameMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("InGameOpen") && !mainMenu.activeSelf)
        {
            //CheckInitDistance();
        }

        CheckPlayerPos();
        for (int i = 0; i < listOfBackgroundPrefabs.Count; i++)
        {
            for (int j = 0; j < listOfBackgroundPrefabs[i].transform.childCount; j++)
            {
                listOfBackgroundPrefabs[i].transform.GetChild(j).GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentUsing.GetComponent<Skin>().backGroundImage;
            }
        }
    }

    void CheckPlayerPos()
    {
        if((movementScript.distanceCounter - tempExtraDistance) > distanceOfNewBackground)
        {
            listOfBackgroundPrefabs.Add(Instantiate(backgroundPackPrefab, new Vector2(initialBackgroundPos.transform.position.x, initialBackgroundPos.transform.position.y + 21.6f), Quaternion.identity, gameObject.transform));

            initialBackgroundPos = listOfBackgroundPrefabs[newBackground];
            newBackground += 1;

            justSpawned = true;

            distanceOfNewBackground = 10 + distanceOfNewBackground;
        }

        if(((movementScript.distanceCounter - distanceOfOldBackground) - tempExtraDistance) > rangeOfRemovingOldBackground && justSpawned)
        {
            Destroy(listOfBackgroundPrefabs[oldBackground]);
            listOfBackgroundPrefabs.Remove(listOfBackgroundPrefabs[oldBackground]);
            
            newBackground -= 1;

            justSpawned = false;

            distanceOfOldBackground = 20 + distanceOfOldBackground + movementScript.distanceCounter;
        }
    }

    void CheckInitDistance()
    {
        distanceOfNewBackground = 3 + movementScript.distanceCounter;
        distanceOfOldBackground = 15 + movementScript.distanceCounter;
        rangeOfRemovingOldBackground = 30 + movementScript.distanceCounter;
    }
}
