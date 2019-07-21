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
    public GameObject buyButton;
    public Text priceText;
    public GameObject luckySpinMenu;
    public Text coin;
    public GameObject abilityWindowDefault;
    public GameObject abilityWindowSpecial;
    public GameObject abilityWindowMission;
    public GameObject abilityWindowVideo;
    public GameObject abilityWindowSecret;
    public GameObject abilityWindowLegendary;

    private void Update()
    {
        coin.text = ""+GameManager.instance.GetCoin();
    }

    private void Start()
    {
        Shop.instance.shopState = Shop.ShopState.parasite;
        placeMenu.SetActive(false);
        coinsMenu.SetActive(false);
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

    public void ShowLuckySpinMenu()
    {
        luckySpinMenu.SetActive(true);
    }

    public void CloseLuckySpinMenu()
    {
        luckySpinMenu.SetActive(false);
    }

    public void ShowAbilityDefault()
    {
        abilityWindowDefault.SetActive(true);
    }

    public void ShowAbilitySpecial()
    {
        abilityWindowSpecial.SetActive(true);
    }

    public void ShowAbilityMission()
    {
        abilityWindowMission.SetActive(true);
    }

    public void ShowAbilityVideo()
    {
        abilityWindowVideo.SetActive(true);
    }

    public void ShowAbilitySecret()
    {
        abilityWindowSecret.SetActive(true);
    }

    public void ShowAbilityLegendary()
    {
        abilityWindowLegendary.SetActive(true);
    }

    public void CloseAbilityDefault()
    {
        abilityWindowDefault.SetActive(false);
    }

    public void CloseAbilitySpecial()
    {
        abilityWindowSpecial.SetActive(false);
    }

    public void CloseAbilityMission()
    {
        abilityWindowMission.SetActive(false);
    }

    public void CloseAbilityVideo()
    {
        abilityWindowVideo.SetActive(false);
    }

    public void CloseAbilityLegendary()
    {
        abilityWindowLegendary.SetActive(false);
    }

    public void CloseAbilitySecret()
    {
        abilityWindowSecret.SetActive(false);
    }

    public void RemoveAds()
    {
        Shop.instance.skinSelecting = Shop.instance.skinList[34];
        GameManager.instance.skinCollected.Add(Shop.instance.skinSelecting.gameObject);
        GameManager.instance.numOfSkinCollected++;
        Shop.instance.CheckIsBought();
        GameManager.instance.SaveSkin();
        PlayerPrefs.SetInt("secondDiscount", 1);
    }

    public void StarterPack()
    {
        GameManager.instance.AddCoin(250);
        GameManager.instance.AddPoints(20);
        Shop.instance.skinSelecting = Shop.instance.skinList[11];
        GameManager.instance.skinCollected.Add(Shop.instance.skinSelecting.gameObject);
        GameManager.instance.numOfSkinCollected++;
        Shop.instance.CheckIsBought();
        GameManager.instance.SaveSkin();
    }

    public void ProPack()
    {
        GameManager.instance.AddCoin(1000);
        GameManager.instance.AddPoints(100);
        // Player Skin
        Shop.instance.skinSelecting = Shop.instance.skinList[32];
        GameManager.instance.skinCollected.Add(Shop.instance.skinSelecting.gameObject);
        GameManager.instance.numOfSkinCollected++;
        Shop.instance.CheckIsBought();
        // Environment
        Shop.instance.skinSelecting = Shop.instance.environmentSkin[4];
        PlayerPrefs.SetInt(Shop.instance.skinSelecting.name, 1);
        Shop.instance.CheckEnvironmentBought();
    }
}
