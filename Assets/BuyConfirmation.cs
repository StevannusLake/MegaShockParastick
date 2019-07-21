using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyConfirmation : MonoBehaviour
{
    public Text priceText;
    public Button unlockButton;
    
    void Start()
    {
        if (Shop.instance.skinSelecting.GetComponent<Skin>().priceType == Skin.PriceType.coin)
            priceText.text = Shop.instance.skinSelecting.GetComponent<Skin>().price + " Coins";
        else
            priceText.text = Shop.instance.skinSelecting.GetComponent<Skin>().price + " Opals";
    }

    // Update is called once per frame
    void Update()
    {
        if (Shop.instance.skinSelecting.GetComponent<Skin>().CheckCoinEnough())
        {
            unlockButton.interactable = true;
        }
        else unlockButton.interactable = false;
    }
}
