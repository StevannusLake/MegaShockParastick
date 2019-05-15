using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerPlatformEffect : MonoBehaviour
{
    SpriteRenderer mySR;
    Rigidbody2D myRB;
    Transform myTransform;

    public float angleFromMid;

    float counter, duration = 0.3f;
    bool onTimer;

    private void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        myRB = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(onTimer)
        {
            Countdown();
        }
    }

    void Countdown()
    {
        if(counter >= duration)
        {
            counter = 0;
            onTimer = false;

            mySR.enabled = false;
        }
        else
        {
            counter += Time.deltaTime;
        }
    }

    public void TouchedDangerousEffect(Vector2 position, Vector2 velocity)
    {
        mySR.enabled = true;
        onTimer = true;
        Vector2 myVelocity;

        myTransform.position = position;

        velocity *= -1;
        angleFromMid *= Mathf.Deg2Rad;

        myVelocity = new Vector2(velocity.x * Mathf.Cos(angleFromMid) + velocity.y * Mathf.Sin(angleFromMid), -velocity.x * Mathf.Sin(angleFromMid) + velocity.y * Mathf.Cos(angleFromMid));

        myVelocity.Normalize();

        myRB.velocity = myVelocity * 10;
    }
}
