using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceCounterBar : MonoBehaviour
{
    public Image[] bounceCounterBars = new Image[5];
    public Sprite BarOn;
    public Sprite BarOff;

    public Movement playerMovementScript;

    private void Update()
    {
        UpdateBar();
    }

    void UpdateBar()
    {
        if (playerMovementScript.maxBounceCounterBar <= 0)
        {
            TurnOffBar(0);
            TurnOffBar(1);
            TurnOffBar(2);
        }
        if (playerMovementScript.maxBounceCounterBar == 1)
        {
            TurnOnBar(0);
            TurnOffBar(1);
            TurnOffBar(2);
            TurnOffBar(3);
            TurnOffBar(4);
        }
        if (playerMovementScript.maxBounceCounterBar == 2)
        {
            TurnOnBar(0);
            TurnOnBar(1);
            TurnOffBar(2);
            TurnOffBar(3);
            TurnOffBar(4);
        }
        if (playerMovementScript.maxBounceCounterBar == 3)
        {
            TurnOnBar(0);
            TurnOnBar(1);
            TurnOnBar(2);
            TurnOffBar(3);
            TurnOffBar(4);
        }
        if (playerMovementScript.maxBounceCounterBar == 4)
        {
            TurnOnBar(0);
            TurnOnBar(1);
            TurnOnBar(2);
            TurnOnBar(3);
            TurnOffBar(4);
        }
        if (playerMovementScript.maxBounceCounterBar == 5)
        {
            TurnOnBar(0);
            TurnOnBar(1);
            TurnOnBar(2);
            TurnOnBar(3);
            TurnOnBar(4);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"> 0 is left and 2 is right </param>
    void TurnOffBar(int num)
    {
        bounceCounterBars[num].sprite = BarOff;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"> 0 is left and 2 is right </param>
    void TurnOnBar(int num)
    {
        bounceCounterBars[num].sprite = BarOn;
    }
}
