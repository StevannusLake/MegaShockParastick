using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    public GameObject parasiteMenu;
    public GameObject placeMenu;
    public GameObject coinsMenu;
    public GameObject notEnoughMenu;
    public GameObject buyConfirmationMenu;
    public Text coin;

    private void Update()
    {
        coin.text = ""+GameManager.instance.GetCoin();
    }

    private void Start()
    {
        Shop.instance.shopState = Shop.ShopState.parasite;
    }

    public void ShowParasiteMenu()
    {
        Shop.instance.shopState = Shop.ShopState.parasite;
        parasiteMenu.SetActive(true);
        placeMenu.SetActive(false);
        coinsMenu.SetActive(false);
    }

    public void ShowPlaceMenu()
    {
        Shop.instance.shopState = Shop.ShopState.place;
        parasiteMenu.SetActive(false);
        placeMenu.SetActive(true);
        coinsMenu.SetActive(false);
    }

    public void ShowCoinsMenu()
    {
        Shop.instance.shopState = Shop.ShopState.coins;
        parasiteMenu.SetActive(false);
        placeMenu.SetActive(false);
        coinsMenu.SetActive(true);
    }

    public void WatchVideo()
    {
        GameManager.instance.AddCoin(50);
    }

    public void ShowNotEnough()
    {
        notEnoughMenu.SetActive(true);
    }

    public void CloseNotEnough()
    {
        notEnoughMenu.SetActive(false);
    }

    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
