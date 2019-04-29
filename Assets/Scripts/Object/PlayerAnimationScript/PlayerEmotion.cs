using System.Collections;
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
        mySprite.sprite = BeforeFlying;
    }

    public void EmoteFlying()
    {
        mySprite.sprite = Flying;
    }

    public void EmoteDeath()
    {
        mySprite.sprite = Death;
    }

    public void EmoteIdle()
    {
        mySprite.sprite = Idle;
    }

    public void EmoteStuck()
    {
        mySprite.sprite = Stuck;
    }
}
