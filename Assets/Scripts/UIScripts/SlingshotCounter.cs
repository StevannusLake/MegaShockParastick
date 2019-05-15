using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlingshotCounter : MonoBehaviour
{
    public Image[] slingshotCounters = new Image[9];

    public Sprite RecoveringColor;
    public Sprite OnColor;
    public Sprite OffColor;

    public Movement playerMovementScript;

    private void Update()
    {
        UpdateCounter();
    }

    void UpdateCounter()
    {
        // recovering with grey color to show recovered and black/offcolor to show off
        if (playerMovementScript.doubleSlingshot == 2 && playerMovementScript.doubleSlingshotCounter - 1 >= 0)
        {
            slingshotCounters[playerMovementScript.doubleSlingshotCounter - 1].sprite = RecoveringColor;
        }
        else if(playerMovementScript.doubleSlingshot != 2)
        {
            if(playerMovementScript.doubleSlingshotCounter == 6)
            {
                slingshotCounters[8].sprite = OffColor;
                slingshotCounters[7].sprite = OffColor;
                slingshotCounters[6].sprite = OffColor;
            }
            if (playerMovementScript.doubleSlingshotCounter == 3)
            {
                slingshotCounters[5].sprite = OffColor;
                slingshotCounters[4].sprite = OffColor;
                slingshotCounters[3].sprite = OffColor;
            }
            if (playerMovementScript.doubleSlingshotCounter < 3)
            {
                slingshotCounters[2].sprite = OffColor;
                slingshotCounters[1].sprite = OffColor;
                slingshotCounters[0].sprite = OffColor;
            }
        }

        if (playerMovementScript.doubleSlingshot == 0 && playerMovementScript.doubleSlingshotCounter == 9)
        {
            for(int i=0; i< slingshotCounters.Length; i++)
            {
                slingshotCounters[i].sprite = OnColor;
            }
        }
    }
}
