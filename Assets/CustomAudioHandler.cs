using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudioHandler : MonoBehaviour
{
    public static CustomAudioHandler instance;
    public float coinPitchRestartTime = 2f;
    float coinPickUpTimer = 0;
    bool pickedCoin = false;
    
    public float defultCoinPitch = 0.8f;
    private float coinPitch ;

    private void Awake()
    {
        coinPitch = defultCoinPitch;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        PickedUpCoin();
    }

    public void PickUpCoin()
    {
        pickedCoin = true;
        coinPickUpTimer = 0;
    }

    void PickedUpCoin()
    {
        if(pickedCoin)
        {
            coinPickUpTimer += Time.deltaTime;
            if(coinPickUpTimer> coinPitchRestartTime)
            {
                coinPickUpTimer = 0;
                coinPitch = defultCoinPitch;
                pickedCoin = false;
            }
        }
    }

    public void HandleCoinSound()
    {
        PickUpCoin();
        
        coinPitch += 0.1f;
        coinPitch = Mathf.Clamp(coinPitch, defultCoinPitch, 1f);
    }
    public float CoinPitch()
    {
        return coinPitch;
    }


}
