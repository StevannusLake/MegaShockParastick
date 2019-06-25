using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    Animator myAnimator;
    Animator UICoin;
    bool dieBool;

    float counter, duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        UICoin = GameObject.FindGameObjectWithTag("UICoin").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dieBool)
        {
            if (counter >= duration)
            {
                counter = 0;
                UICoin.SetBool("GetCoin", false);
                Destroy(gameObject);
            }
            else
            {
                counter += Time.deltaTime;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UICoin.SetBool("GetCoin", false);

            if (!dieBool)
            {
                CustomAudioHandler.instance.HandleCoinSound();
                AudioManager.PlaySoundCustom(AudioManager.Sound.CollectCoin, AudioManager.GetAudioClipVolume(AudioManager.Sound.CollectCoin), CustomAudioHandler.instance.CoinPitch(),false);
                AudioManager.PlaySoundCustom(AudioManager.Sound.CollectCoinMain, AudioManager.GetAudioClipVolume(AudioManager.Sound.CollectCoinMain), CustomAudioHandler.instance.CoinPitch(), false);

                GameManager.instance.AddCoin(1);
                GameManager.instance.coinCollectedInAGame++;
                UICoin.SetBool("GetCoin", true);
            }

            myAnimator.SetBool("PlayerCoin", true);
            dieBool = true;
        }
    }
}
