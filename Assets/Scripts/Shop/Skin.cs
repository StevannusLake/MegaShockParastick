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
    public enum SkinType { player,environment};
    public SkinType skinType;
    public enum PriceType { coin,opal};
    public PriceType priceType;

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
        if(skinType == SkinType.player)
        {
            if (!isBought)
            {   
                if(priceType == PriceType.coin)
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
                else if(priceType == PriceType.opal)
                {
                    if(GameManager.instance.GetPoints() >= price)
                    {
                        mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(true);
                        Shop.instance.skinSelecting = this.gameObject;
                    }
                    else
                    {
                        mainCamera.GetComponent<ShopButtonController>().ShowNotEnough();
                    }
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
        else if(skinType == SkinType.environment)
        {
            if (!isBought)
            {
                if(GameManager.instance.GetPoints() >= price)
                {
                    mainCamera.GetComponent<ShopButtonController>().buyConfirmationMenu.SetActive(true);
                    Shop.instance.skinSelecting = this.gameObject;
                }
                else
                {
                    mainCamera.GetComponent<ShopButtonController>().ShowNotEnough();
                }
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
