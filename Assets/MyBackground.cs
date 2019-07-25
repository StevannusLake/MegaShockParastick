﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBackground : MonoBehaviour
{
    public GameObject initialBackgroundPos;
    public GameObject backgroundPackPrefab;
    public Movement movementScript;
    private float distanceOfNewBackground;
    private float distanceOfOldBackground;

    public List<GameObject> listOfBackgroundPrefabs;
    private int newBackground;
    private int oldBackground;

    public bool justSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        listOfBackgroundPrefabs.Add(initialBackgroundPos);

        distanceOfNewBackground = 3;
        distanceOfOldBackground = 15;
        newBackground = 1; //! 1 is because of initialBackgroundPos that has been placed before start
        oldBackground = 0; //! Always set to 0 because of always removing the last one

        justSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPos();
    }

    void CheckPlayerPos()
    {
        if(movementScript.distanceCounter > distanceOfNewBackground)
        {
            listOfBackgroundPrefabs.Add(Instantiate(backgroundPackPrefab, new Vector2(initialBackgroundPos.transform.position.x, initialBackgroundPos.transform.position.y + 21.6f), Quaternion.identity, gameObject.transform));

            initialBackgroundPos = listOfBackgroundPrefabs[newBackground];
            newBackground += 1;

            justSpawned = true;

            distanceOfNewBackground = distanceOfNewBackground + movementScript.distanceCounter;
        }

        if(movementScript.distanceCounter - distanceOfOldBackground > 10 && justSpawned)
        {
            Destroy(listOfBackgroundPrefabs[oldBackground]);
            listOfBackgroundPrefabs.Remove(listOfBackgroundPrefabs[oldBackground]);
            
            newBackground -= 1;

            justSpawned = false;

            distanceOfOldBackground = distanceOfOldBackground + movementScript.distanceCounter;
        }
    }
}
