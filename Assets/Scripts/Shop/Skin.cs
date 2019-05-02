using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skin : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool isUnlocked = false;
    public int price;
    //public string skinName;
    public Sprite skinImage;
    public GameObject confirmationMenu;
    public GameObject mainCamera;
    public bool isBought = false;

    void Start()
    {
        skinImage = GetComponent<Button>().image.sprite;
        confirmationMenu = GameObject.FindWithTag("BuyConfirmationMenu");
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {

    }

    public bool CheckCoinEnough()
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

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {   
        if(!isBought)
        {
            if (CheckCoinEnough())
            {
                // Shows Confirmation Menu
                mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(true);
                Shop.instance.skinSelecting = this.gameObject;
            }
            else
            {
                // Shows Not Enough coin
                mainCamera.GetComponent<ShopButtonController>().ShowNotEnough();
            }
        }
    }
}
