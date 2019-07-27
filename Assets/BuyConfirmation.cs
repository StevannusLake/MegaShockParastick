using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyConfirmation : MonoBehaviour
{
    public Text priceText;
    public GameObject unlockButton;
    public GameObject watchButton;
    
    // Update is called once per frame
    void Update()
    {   
        if(Shop.instance.skinSelecting.GetComponent<Skin>().rarity != Skin.Rarity.Video && Shop.instance.skinSelecting.GetComponent<Skin>().rarity != Skin.Rarity.Mission)
        {
            unlockButton.SetActive(true);
            watchButton.SetActive(false);
            if (Shop.instance.skinSelecting.GetComponent<Skin>().CheckCoinEnough())
            {
                if (Shop.instance.skinSelecting.name != "Skin 12" || Shop.instance.skinSelecting.name != "Skin 33" || Shop.instance.skinSelecting.name != "LegendaryEnvironment")
                    unlockButton.GetComponent<Button>().interactable = true;
                else unlockButton.GetComponent<Button>().interactable = false;
            }
            else unlockButton.GetComponent<Button>().interactable = false;
        }
        else if(Shop.instance.skinSelecting.GetComponent<Skin>().rarity == Skin.Rarity.Video)
        {
            unlockButton.SetActive(false);
            watchButton.SetActive(true);
            if (Shop.instance.skinSelecting.name == "Skin 35")
            {
                watchButton.GetComponent<Button>().interactable = false;
            }
            else watchButton.GetComponent<Button>().interactable = true;
        }

        if (Shop.instance.skinSelecting.GetComponent<Skin>().rarity != Skin.Rarity.Video && Shop.instance.skinSelecting.GetComponent<Skin>().rarity != Skin.Rarity.Mission)
        {
            if (Shop.instance.skinSelecting.GetComponent<Skin>().priceType == Skin.PriceType.coin)
            {
                if (Shop.instance.skinSelecting.name == "Skin 12")
                {
                    priceText.text = "Purchase Starter Pack";
                }
                else if (Shop.instance.skinSelecting.name == "Skin 33" || Shop.instance.skinSelecting.name == "LegendaryEnvironment")
                {
                    priceText.text = "Purchase Pro Pack";
                }
                else
                {
                    priceText.text = Shop.instance.skinSelecting.GetComponent<Skin>().price + " Coins";
                }
            }
            else
                priceText.text = Shop.instance.skinSelecting.GetComponent<Skin>().price + " Opals";
        }
        else if (Shop.instance.skinSelecting.GetComponent<Skin>().rarity == Skin.Rarity.Video)
        {
            if (Shop.instance.skinSelecting.name == "Skin 35")
            {
                priceText.text = "Purchase Remove Ads Pack";
            }
            else
                priceText.text = "Watch " + Shop.instance.skinSelecting.GetComponent<Skin>().watchNeeded + " Videos";
        }
        else if (Shop.instance.skinSelecting.GetComponent<Skin>().rarity == Skin.Rarity.Mission)
        {
            unlockButton.SetActive(false);
            watchButton.SetActive(false);
            priceText.text = Shop.instance.skinSelecting.GetComponent<Skin>().unlockRequirement;
        }
    }
}
