using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    BoxCollider2D myCollider;
    List<SpriteRenderer> backgroundSprites;
    Sprite correctSprite;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        backgroundSprites = new List<SpriteRenderer>();
        SetCorrectBackground();
    }

    void SetCorrectBackground()
    {
        GameAssets.LevelTypes type = GameAssets.i.GetCorrectLevelType(LevelHandler.instance.levelType);
        
        foreach (Sprite sprite in type.sprites)
        {
            if (sprite.name == "Unique-Backgroundn") correctSprite = sprite;
        }
        
        for (int i=0;i<transform.childCount;i++)
        {
            backgroundSprites.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
        foreach (SpriteRenderer sprite in backgroundSprites)
        {
            sprite.sprite = correctSprite;
        }

    }

    
       
    
}
