using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyCoin()
    {
        GameManager.instance.AddCoin(value);
        GameManager.instance.SaveCoin();
    }

    public void BuyOpal()
    {
        GameManager.instance.AddPoints(value);
        GameManager.instance.SavePoints();
    }
}
