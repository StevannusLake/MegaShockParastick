using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    BoxCollider2D myCollider;
    List<SpriteRenderer> backgroundSprites;
    Sprite correctSprite;
    public int numberOfBackground = 3;

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

        //Create First Layout in bottom left

        GameObject firstBackground = new GameObject("FirstBackground");
        firstBackground.transform.SetParent(transform);
        firstBackground.AddComponent<SpriteRenderer>();
        
        firstBackground.GetComponent<SpriteRenderer>().sprite = correctSprite;
        SpriteRenderer firstSpriteRenderer = firstBackground.GetComponent<SpriteRenderer>();
        firstBackground.SetActive(false);
        
        firstBackground.transform.position = (new Vector2(transform.position.x - firstSpriteRenderer.bounds.size.x, transform.position.y - firstSpriteRenderer.bounds.size.y));


        for(int i=0;i<numberOfBackground;i++)
        {
            for(int j=0;j<numberOfBackground;j++)
            {
                GameObject background = new GameObject("Background"+i+","+j);
                background.AddComponent<SpriteRenderer>();
                background.transform.SetParent(transform);
                SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
                backgroundSpriteRenderer.sprite = correctSprite;
                backgroundSpriteRenderer.sortingOrder = -20;
                background.transform.localPosition = new Vector2(firstBackground.transform.localPosition.x + firstSpriteRenderer.bounds.size.x * i,
                    firstBackground.transform.localPosition.y + firstSpriteRenderer.bounds.size.y * j);
            }
            
        }
        
        
        //for (int i=0;i<transform.childCount;i++)
        //{
        //    backgroundSprites.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        //}
       // foreach (SpriteRenderer sprite in backgroundSprites)
       // {
        //    sprite.sprite = correctSprite;
       // }

    }

    
       
    
}
