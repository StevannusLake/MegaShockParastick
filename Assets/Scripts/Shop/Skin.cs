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
    public GameObject confirmationMenu;
    public GameObject mainCamera;
    public bool isBought = false;

    void Awake()
    {   
        skinImage = GetComponent<Button>().image.sprite;
    }

    void Start()
    {
        confirmationMenu = GameObject.FindWithTag("BuyConfirmationMenu");
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        if(Shop.instance.skinUsing == this.gameObject)
        {
            // enable highlight
        }
        else
        {
            // disable highlight
        }

        if(isBought)
        {
            GetComponent<Button>().image.color = new Color(1f, 1f, 1f,1f);
        }
        else
        {
            GetComponent<Button>().image.color = new Color(0.4f, 0.4f, 0.4f,0.5f);
        }
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
        else
        {
            Shop.instance.ResetInUseHolder();
            Shop.instance.skinUsing = this.gameObject;
            Shop.instance.ChangeSkin();
            Debug.Log("ChangeSkin : " + this.name);
        }
    }
}
