using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlingshotBar : MonoBehaviour
{
    public Image[] slingshotBars = new Image[3];
    public Sprite BarOn;
    public Sprite BarOff;

    public Movement playerMovementScript;

    private void Update()
    {
        UpdateBar();
    }

    void UpdateBar()
    {
        if (playerMovementScript.doubleSlingshotCounter < 3)
        {
            TurnOffBar(0);
            TurnOffBar(1);
            TurnOffBar(2);
        }
        if (playerMovementScript.doubleSlingshotCounter == 3)
        {
            TurnOnBar(0);
            TurnOffBar(1);
            TurnOffBar(2);
        }
        if (playerMovementScript.doubleSlingshotCounter == 6)
        {
            TurnOnBar(0);
            TurnOnBar(1);
            TurnOffBar(2);
        }
        if (playerMovementScript.doubleSlingshotCounter == 9)
        {
            TurnOnBar(0);
            TurnOnBar(1);
            TurnOnBar(2);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"> 0 is left and 2 is right </param>
    void TurnOffBar(int num)
    {
        slingshotBars[num].sprite = BarOff;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"> 0 is left and 2 is right </param>
    void TurnOnBar(int num)
    {
        slingshotBars[num].sprite = BarOn;
    }
}
