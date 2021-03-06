﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.SimpleAndroidNotifications;
using System;

public class Shop : MonoBehaviour
{
    public GameObject defaultSkin;
    public GameObject[] skinList;
    public GameObject skinSelecting;
    public GameObject skinUsing;
    public enum ShopState { parasite,place,coins};
    public ShopState shopState;
    public Text coinText;
    public Text opalText;
    private GameObject mainCamera;
    public GameObject[] lockedMask;
    public GameObject[] holder;
    public GameObject defaultHolder;
    public Sprite defaultHolderSprite;
    public Sprite InUseImage;
    public Sprite lastHolder;
    public GameObject[] environmentSkin;
    public GameObject[] environmentLockedMask;
    public GameObject[] waterSkin;
    public GameObject[] waterLockedSkin;
    public GameObject defaultEnvironment;
    public GameObject environmentDefaultMask;
    public GameObject defaultWater;
    public GameObject waterDefaultMask;
    public GameObject waterUsing;
    public GameObject environmentUsing;
    public Sprite InUseEnvironmentMask;
    public Sprite inUseWaterMask;
    public Sprite TransparentMask;
    private GameObject player;
   // public GameObject deathSprite;
    private Movement movement;
    private Animator anim;
    public Sprite[] environmentType1;
    public Sprite[] environmentType2;
    public Sprite[] environmentType3;
    public Sprite[] environmentType4;
    public Sprite[] environmentType5;
    public Sprite[] environmentType6;

    public static Shop instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        movement = player.GetComponent<Movement>();
       // anim = deathSprite.GetComponent<Animator>();

        //Load SkinUsing
        if(PlayerPrefs.HasKey("SkinUsing"))
        {
            if (PlayerPrefs.GetString("SkinUsing") == defaultSkin.name)
            {
                skinUsing = defaultSkin;
                defaultHolder.GetComponent<Image>().sprite = InUseImage;
            }
            else
            {
                for (int i = 0; i < skinList.Length; i++)
                {   
                    if (skinList[i].name == PlayerPrefs.GetString("SkinUsing"))
                    {
                        skinUsing = skinList[i];
                        ChangeSkin();
                        break;
                    }
                }
            }
        }
        else
        {
            skinUsing = defaultSkin;
            defaultHolder.GetComponent<Image>().sprite = InUseImage;
        }

        CheckEnvironmentBought();
        CheckWaterBought();

        if (PlayerPrefs.HasKey("EnvironmentUsing"))
        {
            if(PlayerPrefs.GetString("EnvironmentUsing") == defaultEnvironment.name)
            {
                environmentUsing = defaultEnvironment;
                environmentDefaultMask.GetComponent<Image>().sprite = InUseEnvironmentMask;
            }
            else
            {
                for(int i=0;i<environmentSkin.Length;i++)
                {
                    if(PlayerPrefs.GetString("EnvironmentUsing") == environmentSkin[i].name)
                    {
                        environmentUsing = environmentSkin[i];
                        environmentLockedMask[i].GetComponent<Image>().sprite = InUseEnvironmentMask;
                        break;
                    }
                }
            }
        }
        else
        {
            environmentUsing = defaultEnvironment;
            environmentDefaultMask.GetComponent<Image>().sprite = InUseEnvironmentMask;
        }

        if (PlayerPrefs.HasKey("WaterUsing"))
        {
            if (PlayerPrefs.GetString("WaterUsing") == waterUsing.name)
            {
                waterUsing = defaultWater;
                waterDefaultMask.GetComponent<Image>().sprite = inUseWaterMask;
            }
            else
            {
                for (int i = 0; i < waterSkin.Length; i++)
                {
                    if (PlayerPrefs.GetString("WaterUsing") == waterSkin[i].name)
                    {
                        waterUsing = waterSkin[i];
                        waterLockedSkin[i].GetComponent<Image>().sprite = inUseWaterMask;
                        break;
                    }
                }
            }
        }
        else
        {
            waterUsing = defaultWater;
          //  waterDefaultMask.GetComponent<Image>().sprite = inUseWaterMask;
        }

        if (skinUsing.GetComponent<Skin>().rarity == Skin.Rarity.Default)
        {
            movement.maxBounceCounter = 3;
            movement.doubleSlingshotCounter = 3;
            movement.INCREMENTSLINGSHOT = 1;
            movement.MAXSLINGSHOT = 3;
            movement.isRareSkin = false;
            movement.initialDistance = 0;
            movement.initialPosition = 0;
        }
        else if (skinUsing.GetComponent<Skin>().rarity == Skin.Rarity.Special)
        {
            movement.maxBounceCounter = 3;
            movement.doubleSlingshotCounter = 3;
            movement.INCREMENTSLINGSHOT = 1;
            movement.MAXSLINGSHOT = 15;
            movement.isRareSkin = false;
            movement.initialDistance = 15;
            movement.initialPosition = 15;
        }
        else if (skinUsing.GetComponent<Skin>().rarity == Skin.Rarity.Mission)
        {
            movement.maxBounceCounter = 4;
            movement.doubleSlingshotCounter = 3;
            movement.INCREMENTSLINGSHOT = 2;
            movement.MAXSLINGSHOT = 9;
            movement.isRareSkin = false;
            movement.initialDistance = 25;
            movement.initialPosition = 25;
        }
        else if (skinUsing.GetComponent<Skin>().rarity == Skin.Rarity.Video)
        {
            movement.maxBounceCounter = 4;
            movement.doubleSlingshotCounter = 9;
            movement.INCREMENTSLINGSHOT = 1;
            movement.MAXSLINGSHOT = 9;
            movement.isRareSkin = true;
            movement.initialDistance = 20;
            movement.initialPosition = 20;
        }
        else if (skinUsing.GetComponent<Skin>().rarity == Skin.Rarity.Secret)
        {
            movement.maxBounceCounter = 6;
            movement.doubleSlingshotCounter = 3;
            movement.INCREMENTSLINGSHOT = 1;
            movement.MAXSLINGSHOT = 12;
            movement.isRareSkin = true;
            movement.initialDistance = 50;
            movement.initialPosition = 50;
        }
        else if (skinUsing.GetComponent<Skin>().rarity == Skin.Rarity.Legendary)
        {
            movement.maxBounceCounter = 5;
            movement.doubleSlingshotCounter = 3;
            movement.INCREMENTSLINGSHOT = 1;
            movement.MAXSLINGSHOT = 12;
            movement.isRareSkin = true;
            movement.initialDistance = 100;
            movement.initialPosition = 100;
        }

        // check video skin
        for(int i = 0; i < skinList.Length; i++)
        {
            if(PlayerPrefs.HasKey(skinList[i].name+"WatchCount"))
            {
                skinList[i].GetComponent<Skin>().watchCount = PlayerPrefs.GetInt(skinList[i].name + "WatchCount");
            }
        }
    }

    private void Update()
    {
        if (UIManager.Instance.ShopMenu.activeInHierarchy)
        {
            coinText.text = ""+GameManager.instance.GetCoin();
            opalText.text = "" + GameManager.instance.GetPoints();
            CheckIsBought(); // prevent multiple buying
        }

        //if (movement.bounceCounter == movement.maxBounceCounter - 1)
        //{
        //    anim.SetBool("IsNearDeath", true);
        //}
        //else
        //{
        //    anim.SetBool("IsNearDeath", false);
        //}

        NotificationManager.Cancel(63);
        TimeSpan delayNotifyTime3 = new TimeSpan(48, 0, 0);
        NotificationManager.Send(63, TimeSpan.FromDays(2), "👑OUR LEGEND👑", "🤴Legend Never Quit!!🤔", Color.red, NotificationIcon.Heart);
    }

    public void CheckIsBought()
    {
        for (int i = 0; i < GameManager.instance.skinCollected.Count; i++)
        {
            for (int j = 0; j < skinList.Length; j++)
            {
                if (GameManager.instance.skinCollected[i].name == skinList[j].name)
                {
                    skinList[j].GetComponent<Skin>().isBought = true;
                    lockedMask[j].SetActive(false);
                    break;
                }
            }
        }
    }

    public void CheckEnvironmentBought()
    {
        //check environment
        for (int i = 0; i < environmentSkin.Length; i++)
        {
            if (PlayerPrefs.GetInt(environmentSkin[i].name) == 1)
            {
                environmentSkin[i].GetComponent<Skin>().isBought = true;
                environmentLockedMask[i].GetComponent<Image>().sprite = TransparentMask;
            }
        }
    }

    public void CheckWaterBought()
    {
        //check environment
        for (int i = 0; i < waterSkin.Length; i++)
        {
            if (PlayerPrefs.GetInt(waterSkin[i].name) == 1)
            {
                waterSkin[i].GetComponent<Skin>().isBought = true;
                waterLockedSkin[i].GetComponent<Image>().sprite = TransparentMask;
            }
        }
    }

    public void ResetInUseHolder()
    {
        if (lastHolder != null)
        {
            if (skinUsing == defaultSkin)
            {
                defaultHolder.GetComponent<Image>().sprite = lastHolder;
            }
            else
            {
                for (int i = 0; i < skinList.Length; i++)
                {
                    if (skinList[i].name == skinUsing.name)
                    {
                        holder[i].GetComponent<Image>().sprite = lastHolder;
                        break;
                    }
                }
            }
        }
        else
        {
            defaultHolder.GetComponent<Image>().sprite = defaultHolderSprite;
        }
    }

    public void ResetInUseEnvironment()
    {
        if (environmentUsing == defaultEnvironment)
        {
            environmentDefaultMask.GetComponent<Image>().sprite = InUseEnvironmentMask;
            for(int i = 0; i < environmentLockedMask.Length; i++)
            {
                if(environmentSkin[i].GetComponent<Skin>().isBought)
                environmentLockedMask[i].GetComponent<Image>().sprite = TransparentMask;
            }
        }
        else
        {
            environmentDefaultMask.GetComponent<Image>().sprite = TransparentMask;
            for (int i = 0; i < environmentSkin.Length; i++)
            {
                if (environmentUsing == environmentSkin[i])
                {
                    environmentLockedMask[i].GetComponent<Image>().sprite = InUseEnvironmentMask;
                }
                else
                {
                    if (environmentSkin[i].GetComponent<Skin>().isBought)
                    {
                        environmentLockedMask[i].GetComponent<Image>().sprite = TransparentMask;
                    }
                }
            }
        }        
    }

    public void ChangeWaterSkin()
    {
        if (waterUsing == defaultWater)
        {
            waterDefaultMask.GetComponent<Image>().sprite = inUseWaterMask;
            for (int i = 0; i < waterLockedSkin.Length; i++)
            {
                if (waterSkin[i].GetComponent<Skin>().isBought)
                    waterLockedSkin[i].GetComponent<Image>().sprite = TransparentMask;
            }
        }
        else
        {
            waterDefaultMask.GetComponent<Image>().sprite = TransparentMask;
            for (int i = 0; i < waterSkin.Length; i++)
            {
                if (environmentUsing == waterSkin[i])
                {
                    waterLockedSkin[i].GetComponent<Image>().sprite = inUseWaterMask;
                }
                else
                {
                    if (waterSkin[i].GetComponent<Skin>().isBought)
                    {
                        waterLockedSkin[i].GetComponent<Image>().sprite = TransparentMask;
                    }
                }
            }
        }
    }

    public void ChangeSkin()
    {   
        GameManager.instance.player.GetComponent<SpriteRenderer>().sprite = skinUsing.GetComponent<Skin>().skinImage;
        
        if(skinUsing == defaultSkin)
        {
            lastHolder = defaultHolder.GetComponent<Image>().sprite;
            defaultHolder.GetComponent<Image>().sprite = InUseImage;
        }
        else
        {
            for (int i = 0; i < skinList.Length; i++)
            {
                if (skinList[i].name == skinUsing.name)
                {
                    lastHolder = holder[i].GetComponent<Image>().sprite;
                    holder[i].GetComponent<Image>().sprite = InUseImage;
                    break;
                }
            }
        }
        PlayerPrefs.SetString("SkinUsing", skinUsing.name);
    }

    public void ChangeEnvironment()
    {
        if(environmentUsing == defaultEnvironment)
        {

        }
        else
        {

        }
        PlayerPrefs.SetString("EnvironmentUsing", environmentUsing.name);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("SkinUsing", skinUsing.name);
        PlayerPrefs.SetString("EnvironmentUsing", environmentUsing.name);
        PlayerPrefs.SetString("WaterUsing", waterUsing.name);
    }

    //For button OnClick() function
    public void Buy()
    {
        if(!skinSelecting.GetComponent<Skin>().notUnlockable)
        {
            if (skinSelecting.GetComponent<Skin>().skinType == Skin.SkinType.player)
            {
                if (skinSelecting.GetComponent<Skin>().priceType == Skin.PriceType.coin)
                {
                    GameManager.instance.DecreaseCoin(skinSelecting.GetComponent<Skin>().price);
                }
                else
                {
                    GameManager.instance.DecreasePoints(skinSelecting.GetComponent<Skin>().price);
                }
                GameManager.instance.skinCollected.Add(skinSelecting.gameObject);
                GameManager.instance.numOfSkinCollected++;
                CheckIsBought();
                // close the buy confirmation menu
                mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(false);
                GameManager.instance.SaveSkin();
            }
            else
            {
                PlayerPrefs.SetInt(skinSelecting.name, 1);
                CheckEnvironmentBought();
                mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(false);
            }
        }      
    }

    //For button OnClick() function
    public void CancelBuy()
    {
        // close the buy confirmation menu
        mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(false);
    }
}
