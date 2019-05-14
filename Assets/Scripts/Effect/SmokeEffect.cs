using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    Animator myAnimator;
    Transform myTransform;
    SpriteRenderer mySR;

    float counter, duration = 0.42f;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        mySR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mySR.enabled)
        {
            Debug.Log(counter);
            Countdown();
        }

    }

    void Countdown()
    {
        if (counter >= duration)
        {
            counter = 0;
            mySR.sprite = null;
            mySR.enabled = false;
            myAnimator.SetInteger("state", 0);
        }
        else
        {
            counter += Time.deltaTime;
        }
    }

    //1 = normal, 2 = moving
    /// <summary>
    /// state name = MovingPlatform, SafePlatform, WallBounce
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name=""></param>
    public void SpawnSmoke(Vector2 spawnPosition, int smokeState, float angle, string stateName)
    {
        counter = 0;
        myTransform.position = spawnPosition;
        myTransform.rotation = Quaternion.Euler(0,0, angle);
        mySR.enabled = true;
        myAnimator.SetInteger("state", smokeState);
        //myAnimator.Play(stateName,0, 0);
    }
    
}
