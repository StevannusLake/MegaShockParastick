using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CallAd());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CallAd()
    {
        yield return new WaitForSeconds(2f);

        //! Call The Ads
        AdController.Instance.ShowVideoAd();
    }
}
