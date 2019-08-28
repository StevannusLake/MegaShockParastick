using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    public GameObject secondLifeFX;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnSecondLifeFX()
    {
        
        if (!mainMenu.activeInHierarchy)
        {
            Debug.Log("Turn");
            secondLifeFX.SetActive(true);
        }
    }
}
