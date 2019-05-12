using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    Animator myAnimator;
    Transform myTransform;
    SpriteRenderer mySR;

    float counter, duration = 0.5f;

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
            Countdown();
        }

    }

    void Countdown()
    {
        if (counter >= duration)
        {
            counter = 0;
            mySR.enabled = false;
            myAnimator.SetInteger("state", 0);
        }
        else
        {
            counter += Time.deltaTime;
        }
    }

    /// <summary>
    /// 1 = normal, 2 = moving
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name=""></param>
    public void SpawnSmoke(Vector2 spawnPosition, int smokeState, float angle)
    {
        myTransform.position = spawnPosition;
        myTransform.rotation = Quaternion.Euler(0,0, angle);
        mySR.enabled = true;
        myAnimator.SetInteger("state", smokeState);
    }
    
}
