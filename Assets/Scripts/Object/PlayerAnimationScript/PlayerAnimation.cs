using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator myAnimator;
    Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpToFall();
    }

    void JumpToFall()
    {
        if(myRigidbody.velocity.y < 0 && myAnimator.GetBool("isJump"))
        {
            PlayFall();
        }
    }

    // catapult
    public void PlayHold()
    {
        myAnimator.SetBool("isHold", true);
        // back to false
        myAnimator.SetBool("isIdle", false);
        myAnimator.SetBool("isJump", false);
        myAnimator.SetBool("isFall", false);

    }

    // flying/jumping
    public void PlayJump()
    {
        myAnimator.SetBool("isJump", true);

        // back to false
        myAnimator.SetBool("isHold", false);
        myAnimator.SetBool("isIdle", false);
        myAnimator.SetBool("isFall", false);
    }

    // falling
    public void PlayFall()
    {
        myAnimator.SetBool("isFall", true);
        // back to false
        myAnimator.SetBool("isHold", false);
        myAnimator.SetBool("isJump", false);
        myAnimator.SetBool("isIdle", false);
    }

    // idle
    public void PlayIdle()
    {
        myAnimator.SetBool("isIdle", true);
        // back to false
        myAnimator.SetBool("isFall", false);
        myAnimator.SetBool("isJump", false);
        myAnimator.SetBool("isHold", false);
    }
}
