using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceIndicator : MonoBehaviour
{
    public Image[] bounceImg;
    public Movement playerMovementScript;
    
    // Update is called once per frame
    void Update()
    {
        UpdateImage();
    }

    void UpdateImage()
    {
        if(playerMovementScript.doubleSlingshotCounter >= 9)
        {
            bounceImg[0].enabled = true;
            bounceImg[1].enabled = true;
            bounceImg[2].enabled = true;
        }
        else if (playerMovementScript.doubleSlingshotCounter == 6)
        {
            bounceImg[0].enabled = true;
            bounceImg[1].enabled = true;
            bounceImg[2].enabled = false;
        }
        else if (playerMovementScript.doubleSlingshotCounter == 3)
        {
            bounceImg[0].enabled = true;
            bounceImg[1].enabled = false;
            bounceImg[2].enabled = false;
        }
        else if (playerMovementScript.doubleSlingshotCounter < 3)
        {
            bounceImg[0].enabled = false;
            bounceImg[1].enabled = false;
            bounceImg[2].enabled = false;
        }
    }
}
