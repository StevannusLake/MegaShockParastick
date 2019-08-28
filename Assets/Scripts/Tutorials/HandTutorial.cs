using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutorial : MonoBehaviour
{
    static HandTutorial instance;

    public SpriteRenderer handSR;
    Transform myTransform;
    public static int tutorialCounter;

    public Animator handAnim;

    public GameObject hand;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    
    /*
    public void OnTutorial(Vector2 pos)
    {
        
        if (myTransform.gameObject != null)
        {
            myTransform.position = pos;
            if (hand != null)
            {
                handSR.enabled = true;

                handAnim.Play("handTutorial");
            }
        }
        
    }
    
    
    public void OnSprite()
    {
        
        if (hand != null)
        {
            handSR.enabled = true;
        }
        
    }

    public void OffSprite()
    {
        if (hand != null)
        {
            handSR.enabled = false;
        }
    }

    public void OffTutorial()
    {

        tutorialCounter++;

        if (hand != null)
        {
            handSR.enabled = false;
        }

        if (tutorialCounter >= 2)
        {
            Destroy(hand);
        }
    }
    */
}
