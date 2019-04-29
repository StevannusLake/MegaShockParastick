using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPModal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy500Coins()
    {
        IAPManager.Instance.Buy500Coins();
    }

    public void Buy200Coins()
    {
        IAPManager.Instance.Buy200Coins();
    }

    public void BuyRemoveAds()
    {
        IAPManager.Instance.BuyRemoveAds();
    }
}
