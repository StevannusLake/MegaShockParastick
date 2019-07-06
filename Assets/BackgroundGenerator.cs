using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    BoxCollider2D myCollider;
    List<SpriteRenderer> backgroundSprites;
    Sprite correctSprite;
    public int numberOfBackground = 3;
    public float playerDistanceX = 0;
    public float playerDistanceY = 0;
    private float topCreated ;
    private void Start()
    {
        topCreated = 1;
        myCollider = GetComponent<BoxCollider2D>();
        backgroundSprites = new List<SpriteRenderer>();    
        SetCorrectBackground();
        CreateNewBackground("Top");
        ChangeScale();
    }

    void Update()
    {
        

    }

    private void LateUpdate()
    {
       
    }

    void ChangeScale()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }


    private void FixedUpdate()
    {
        Vector3 desiredPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10f, 0);
        transform.position = desiredPos;
    }

    void CheckPlayerDistance()
    {
        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 originPos = transform.position;
        playerPos.x = 0;
        originPos.x = 0;
        float distanceYFromOrigin = Vector2.Distance(playerPos, originPos);
        playerPos = GameManager.instance.player.transform.position;
        originPos = transform.position;
        playerPos.y = 0;
        originPos.y = 0;
        float distanceXFromOrigin = Vector2.Distance(playerPos, originPos);
        playerDistanceX = distanceXFromOrigin;
        playerDistanceY = distanceYFromOrigin;
        if (distanceYFromOrigin %10==0)
        {
            Debug.Log("dISTANCEHAPPEND");
            topCreated += 1;
            CreateNewBackground("Top");
        }
       
    }

    void SetCorrectBackground()
    {
        GameAssets.LevelTypes type = GameAssets.i.GetCorrectLevelType(LevelHandler.instance.levelType);
        
        foreach (Sprite sprite in type.sprites)
        {
            if (sprite.name == "Unique-Backgroundn") correctSprite = sprite;
        }

        //Create First Layout in bottom left
    }


    void CreateNewBackground(string direction)
    {
        bool isTopCreated = false;
        if(direction=="Top")
        {
            Vector2 firstLayoutPos = new Vector2(transform.position.x - correctSprite.bounds.size.x  ,
             transform.position.x - correctSprite.bounds.size.y * -topCreated);
            for (int i = 0; i < numberOfBackground; i++)
            {
                for (int j = 0; j < numberOfBackground; j++)
                {
                   
                        GameObject background = new GameObject("Background" + i + "," + j);
                    background.AddComponent<SpriteRenderer>();
                    background.transform.SetParent(transform);
                    SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
                    backgroundSpriteRenderer.sprite = correctSprite;
                    backgroundSpriteRenderer.sortingOrder = -20;
                    background.transform.localPosition = new Vector2(firstLayoutPos.x + correctSprite.bounds.size.x * i,
                        firstLayoutPos.y + correctSprite.bounds.size.y * j);
                    background.layer = 10;

                    //Top left
                    /*if (j == numberOfBackground-1 && i==1)
                    {
                        Vector2 offset = new Vector2(0, -4f);
                        Vector2 backgroundPosition = background.transform.position;
                        GameObject topCollider = new GameObject("TopCollider");
                        topCollider.transform.position = backgroundPosition + offset;
                        BoxCollider2D collider =topCollider.AddComponent<BoxCollider2D>();
                        collider.size = new Vector2(correctSprite.bounds.size.x * numberOfBackground, 2f);

                    }*/

                }

            }
        }
      

    }









}
