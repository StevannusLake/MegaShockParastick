using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject[] skinList;
    public Skin skinSelecting;
    public enum ShopState { parasite,place,coins};
    public ShopState shopState;
    public Text coinText;

    public static Shop instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            coinText.text = ""+PlayerPrefs.GetInt("Coin", 0);
        }
    }

    void Display()
    {
        // Shows all available skins
    }

    //For button OnClick() function
    public void Buy()
    {
        GameManager.instance.skinCollected.Add(skinSelecting);
        GameManager.instance.DecreaseCoin(skinSelecting.price);
        GameManager.instance.numOfSkinCollected++;
        // close the buy confirmation menu
    }

    //For button OnClick() function
    public void CancelBuy()
    {
        // close the buy confirmation menu
    }
}
