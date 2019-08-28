using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    public GameObject secondLifeFX;
    public GameObject mainMenu;
    public GameObject garage;
    public GameObject uiManager;
    public GameObject loseMenu;
    public GameObject secondChanceMenu;
    public bool isShow = false;
    public bool isShow2 = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "InGame")
        {
            if (isShow == true && secondLifeFX != null && uiManager.GetComponent<UIManager>().reviveCount == 1)
            {
                secondLifeFX.SetActive(true);
            }

            if (isShow2 == true)
            {
                if (loseMenu.activeInHierarchy || secondChanceMenu.activeInHierarchy)
                {
                    uiManager.GetComponent<UIManager>().reviveCount = 0;
                    PlayerPrefs.SetInt("ReviveCount", uiManager.GetComponent<UIManager>().reviveCount);
                    isShow2 = false;
                }
            }
        }
    }

    public void TurnOnSecondLifeFX()
    {
        isShow = true;
    }

    public void TurnOffSecondLifeFx()
    {
        garage.GetComponent<FX>().isShow = false;
        this.gameObject.SetActive(false);
    }

    public void TurnOffBool()
    {
        isShow = false;
    }

    public void TurnOnBool()
    {
        isShow2 = true;
    }
}
