using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skin : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool isUnlocked = false;
    public int price;
    public string skinName;
    public Sprite skinImage;
    public GameObject confirmationMenu;

    void Start()
    {
        skinImage = GetComponent<Button>().image.sprite;
        confirmationMenu = GameObject.FindWithTag("BuyConfirmationMenu");
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
        if(CheckCoinEnough())
        {
            // Shows Confirmation Menu
            Shop.instance.BuyConfirmationMenu.SetActive(true);
            Shop.instance.skinSelecting = GetComponent<Skin>();
        }
        else
        {
            // Shows Not Enough coin
        }
    }
}
