﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmotion : MonoBehaviour
{
    public Sprite BeforeFlying;
    public Sprite Flying;
    public Sprite Death;
    public Sprite Idle;
    public Sprite Stuck;

    SpriteRenderer mySprite;

    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    public void EmoteBeforeFlying()
    {

    }

    public void EmoteFlying()
    {

    }

    public void EmoteDeath()
    {

    }

    public void EmoteIdle()
    {

    }

    public void EmoteStuck()
    {

    }
}
