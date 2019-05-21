﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject defaultSkin;
    public GameObject[] skinList;
    public GameObject skinSelecting;
    public GameObject skinUsing;
    public enum ShopState { parasite,place,coins};
    public ShopState shopState;
    public Text coinText;
    private GameObject mainCamera;
    public GameObject[] lockedMask;
    public GameObject[] holder;
    public GameObject defaultHolder;
    public Sprite defaultHolderSprite;
    public Sprite InUseImage;
    public Sprite lastHolder;
    public GameObject[] environmentSkin;
    public GameObject[] environmentLockedMask;
    public GameObject defaultEnvironment;
    public GameObject environmentDefaultMask;
    public GameObject environmentUsing;
    public Sprite InUseEnvironmentMask;
    public Sprite TransparentMask;

    public static Shop instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
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
    }

    private void Update()
    {
        if(UIManager.Instance.ShopMenu.activeInHierarchy)
        {
            coinText.text = ""+GameManager.instance.GetCoin();
            CheckIsBought(); // prevent multiple buying
        }
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
            for(int i =0;i<environmentLockedMask.Length;i++)
            {
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
    }

    //For button OnClick() function
    public void Buy()
    {
        if(skinSelecting.GetComponent<Skin>().skinType == Skin.SkinType.player)
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

    //For button OnClick() function
    public void CancelBuy()
    {
        // close the buy confirmation menu
        mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(false);
    }
}
