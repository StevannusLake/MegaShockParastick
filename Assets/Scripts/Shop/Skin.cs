using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skin : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public int price;
    //public string skinName;
    public Sprite skinImage;
    public Sprite backGroundImage;
    public int environmentType;
    public GameObject confirmationMenu;
    public GameObject mainCamera;
    public bool isBought = false;
    public enum SkinType { player,environment};
    public SkinType skinType;
    public enum PriceType { coin,opal};
    public PriceType priceType;
    public enum Rarity { Default,Special,Mission,Video,Secret,Legendary};
    public Rarity rarity;
    public string abilityDescription;
    private GameObject player;
    public int watchNeeded;
    public int watchCount;
    public bool notUnlockable;
    public string unlockRequirement;
    public Material doubleSlingShotMat;
    public Sprite tradectoryDotsSprite;
    public Color flyingMaxColor; // upper
    public Color flyingMinColor; // bottom
    public Color absorbBigMaxColor;
    public Color absorbBigMinColor;
    public Color absorbSmallMaxColor;
    public Color absorbSmallMinColor;
    public Color dieEffect1Color;
    public Color dieEffect2Color;
    public Color rebornMaxColor;
    public Color rebornMinColor;
    public Color doubleSlingShotMaxColor;
    public Color doubleSlingShotMinColor;
    public Material trailMat;

    void Awake()
    {   
        skinImage = GetComponent<Button>().image.sprite;
    }

    void Start()
    {
        confirmationMenu = GameObject.FindWithTag("BuyConfirmationMenu");
        mainCamera = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        watchCount = PlayerPrefs.GetInt(gameObject.name + "WatchCount");
    }

    void Update()
    {
        
    }

    public bool CheckCoinEnough()
    {   
        if(priceType == PriceType.coin)
        {
            if (GameManager.instance.GetCoin() >= price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (GameManager.instance.GetPoints() >= price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {   
        if(!GameManager.instance.isDragging)
        {
            if (skinType == SkinType.player)
            {
                if (!isBought)
                {
                   
                        Shop.instance.skinSelecting = this.gameObject;
                        mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(true);
                      
                    //else if(rarity == Rarity.Video)
                    //{
                    //    watchCount += 1;
                    //    PlayerPrefs.SetInt(gameObject.name + "WatchCount", watchCount);
                    //    // Show ads here
                    //    if(watchCount >= watchNeeded)
                    //    {
                    //        Shop.instance.skinSelecting = this.gameObject;
                    //        GameManager.instance.skinCollected.Add(Shop.instance.skinSelecting.gameObject);
                    //        GameManager.instance.numOfSkinCollected++;
                    //        Shop.instance.CheckIsBought();
                    //        GameManager.instance.SaveSkin();
                    //    }
                    //}
                }
                else
                {
                    Shop.instance.ResetInUseHolder();
                    Shop.instance.skinUsing = this.gameObject;
                    Shop.instance.ChangeSkin();
                    Debug.Log("ChangeSkin : " + this.name);

                    if (rarity == Rarity.Default)
                    {
                        player.GetComponent<Movement>().maxBounceCounter = 3;
                        player.GetComponent<Movement>().doubleSlingshotCounter = 3;
                        player.GetComponent<Movement>().INCREMENTSLINGSHOT = 1;
                        player.GetComponent<Movement>().isRareSkin = false;
                        player.GetComponent<Movement>().MAXSLINGSHOT = 3;
                        player.GetComponent<Movement>().initialDistance = 0;
                        player.GetComponent<Movement>().initialPosition = 0;
                    }
                    else if (rarity == Rarity.Special)
                    {
                        player.GetComponent<Movement>().maxBounceCounter = 3;
                        player.GetComponent<Movement>().doubleSlingshotCounter = 3;
                        player.GetComponent<Movement>().INCREMENTSLINGSHOT = 1;
                        player.GetComponent<Movement>().isRareSkin = false;
                        player.GetComponent<Movement>().MAXSLINGSHOT = 15;
                        player.GetComponent<Movement>().initialDistance = 15;
                        player.GetComponent<Movement>().initialPosition = 15;
                    }
                    else if (rarity == Rarity.Mission)
                    {
                        player.GetComponent<Movement>().maxBounceCounter = 4;
                        player.GetComponent<Movement>().doubleSlingshotCounter = 3;
                        player.GetComponent<Movement>().INCREMENTSLINGSHOT = 2;
                        player.GetComponent<Movement>().isRareSkin = false;
                        player.GetComponent<Movement>().MAXSLINGSHOT = 9;
                        player.GetComponent<Movement>().initialDistance = 25;
                        player.GetComponent<Movement>().initialPosition = 25;
                    }
                    else if (rarity == Rarity.Video)
                    {
                        player.GetComponent<Movement>().maxBounceCounter = 4;
                        player.GetComponent<Movement>().doubleSlingshotCounter = 9;
                        player.GetComponent<Movement>().INCREMENTSLINGSHOT = 1;
                        player.GetComponent<Movement>().isRareSkin = true;
                        player.GetComponent<Movement>().MAXSLINGSHOT = 9;
                        player.GetComponent<Movement>().initialDistance = 20;
                        player.GetComponent<Movement>().initialPosition = 20;
                    }
                    else if (rarity == Rarity.Secret)
                    {
                        player.GetComponent<Movement>().maxBounceCounter = 6;
                        player.GetComponent<Movement>().doubleSlingshotCounter = 3;
                        player.GetComponent<Movement>().INCREMENTSLINGSHOT = 1;
                        player.GetComponent<Movement>().isRareSkin = true;
                        player.GetComponent<Movement>().MAXSLINGSHOT = 12;
                        player.GetComponent<Movement>().initialDistance = 50;
                        player.GetComponent<Movement>().initialPosition = 50;
                    }
                    else if (rarity == Rarity.Legendary)
                    {
                        player.GetComponent<Movement>().maxBounceCounter = 5;
                        player.GetComponent<Movement>().doubleSlingshotCounter = 3;
                        player.GetComponent<Movement>().INCREMENTSLINGSHOT = 1;
                        player.GetComponent<Movement>().isRareSkin = true;
                        player.GetComponent<Movement>().MAXSLINGSHOT = 12;
                        player.GetComponent<Movement>().initialDistance = 100;
                        player.GetComponent<Movement>().initialPosition = 100;
                    }
                }
            }
            else if (skinType == SkinType.environment)
            {
                if (!isBought)
                {
                    Shop.instance.skinSelecting = this.gameObject;
                    mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(true);                  
                    //mainCamera.GetComponent<ShopButtonController>().priceText.text = price + " Opals";
                    //if (GameManager.instance.GetPoints() >= price)
                    //{
                    //    mainCamera.GetComponent<ShopButtonController>().buyButton.GetComponent<Button>().interactable = true;
                    //}
                    //else
                    //{
                    //    mainCamera.GetComponent<ShopButtonController>().buyButton.GetComponent<Button>().interactable = false;
                    //}
                }
                else
                {
                    Shop.instance.environmentUsing = this.gameObject;
                    Shop.instance.ResetInUseEnvironment();
                    Shop.instance.ChangeEnvironment();
                }
            }
        } 
    }
}
