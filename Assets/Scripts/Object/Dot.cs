using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public float num;
    public bool onSprite;

    [HideInInspector]public SpriteRenderer mySR;

    private void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(onSprite)
        {
            mySR.enabled = true;
        }
        else
        {
            mySR.enabled = false;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            num = 0;
        }
    }
    */
}
