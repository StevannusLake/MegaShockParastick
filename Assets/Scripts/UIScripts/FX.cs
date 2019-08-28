using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    public GameObject secondLifeFX;
    public GameObject mainMenu;
    public GameObject garage;
    public bool isShow = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isShow == true && secondLifeFX != null)
        {
            secondLifeFX.SetActive(true);
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
}
