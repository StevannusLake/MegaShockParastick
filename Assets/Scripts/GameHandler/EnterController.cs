using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterController : MonoBehaviour
{
    private LevelGenerator levelGenerator;

    private void Awake()
    {
       
        levelGenerator = transform.parent.gameObject.GetComponentInChildren<LevelGenerator>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
     if(other.gameObject.tag=="Player")
        {
            if(levelGenerator.levelGeneratorID!=0 && !levelGenerator.isAlreadyMade)
            {
                levelGenerator.GenerateMapOnTop();
                levelGenerator.isAlreadyMade = true;
            }
        }
    }
}
