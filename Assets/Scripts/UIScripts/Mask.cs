using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    private SpriteMask spriteMask;

    private void Start()
    {
        spriteMask = GetComponent<SpriteMask>();
    }

    // Update is called once per frame
    void Update()
    {
        if(UIManager.Instance.ShopMenu.activeInHierarchy || UIManager.Instance.CreditsMenu.activeInHierarchy)
        {
            spriteMask.enabled = false;
        }
        else
        {
            spriteMask.enabled = true;
        }
    }
}
