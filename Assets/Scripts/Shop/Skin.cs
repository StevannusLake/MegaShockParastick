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

    void Start()
    {
        skinImage = GetComponent<Button>().image.sprite;
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
            Shop.instance.skinSelecting = GetComponent<Skin>();
            Debug.Log("Selected");
        }
        else
        {
            // Shows Not Enough coin
        }
    }
}
