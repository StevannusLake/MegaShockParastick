using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpalEffect : MonoBehaviour
{
    Animator myAnimator;
    Animator UIOpal;
    bool dieBool;

    float counter, duration = 0.5f;
    
    private Animator OpalCounterAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        UIOpal = GameObject.FindGameObjectWithTag("UIOpal").GetComponent<Animator>();
        
        OpalCounterAnim = GameObject.FindGameObjectWithTag("OpalCounter").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dieBool)
        {
            if (counter >= duration)
            {
                counter = 0;
                //UIOpal.SetBool("GetOpal", false);
                Destroy(gameObject);

                Invoke("TurnOffOpalCounter", 2f);
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
            UIOpal.SetBool("GetOpal", false);

            if (!dieBool)
            {
                AudioManager.PlaySound(AudioManager.Sound.CollectCoin);
                AudioManager.PlaySound(AudioManager.Sound.CollectCoinMain);

                GameManager.instance.AddCoin(1);

                OpalCounterAnim.SetBool("OpenCounter", true);

                UIOpal.SetBool("OpenOpalIcon", true);
                UIOpal.SetBool("GetEffect", true);
                UIOpal.SetBool("GetOpal", true);

                //Invoke("TurnOffOpalCounter", 2f);
            }

            myAnimator.SetBool("PlayerCoin", true);
            dieBool = true;
        }
    }

    //void TurnOffOpalCounter()
    //{
    //    OpalCounterAnim.SetBool("OpenCounter", false);
    //    UIOpal.SetBool("OpenOpalIcon", false);
    //}
}
