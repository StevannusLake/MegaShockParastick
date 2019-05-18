using System.Collections;
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

    void CheckIsBought()
    {
        for (int i = 0; i < GameManager.instance.skinCollected.Count; i++)
        {
            for (int j = 0; j < skinList.Length; j++)
            {
                if (GameManager.instance.skinCollected[i].name == skinList[j].name)
                {
                    skinList[j].GetComponent<Skin>().isBought = true;
                    break;
                }
            }
        }
    }

    public void ChangeSkin()
    {
        GameManager.instance.player.GetComponent<SpriteRenderer>().sprite = skinUsing.GetComponent<Skin>().skinImage;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("SkinUsing", skinUsing.name);
    }

    //For button OnClick() function
    public void Buy()
    {
        GameManager.instance.skinCollected.Add(skinSelecting.gameObject);
        GameManager.instance.DecreaseCoin(skinSelecting.GetComponent<Skin>().price);
        GameManager.instance.numOfSkinCollected++;
        CheckIsBought();
        // close the buy confirmation menu
        mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(false);
        GameManager.instance.SaveSkin();
    }

    //For button OnClick() function
    public void CancelBuy()
    {
        // close the buy confirmation menu
        mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(false);
    }
}
