using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleSSUIBlink : MonoBehaviour
{
    [HideInInspector] public bool isBlink;

    Image myImage;
    float counter, duration = 0.5f;
    float r, g, b;

    private void Start()
    {
        myImage = GetComponent<Image>();
        r = myImage.color.r;
        g = myImage.color.g;
        b = myImage.color.b;
    }

    void FixedUpdate()
    {
        if (isBlink)
        {
            Blinking();
        }
        else
        {
            // reset
            if (myImage.color.a == 0.5f)
            {
                myImage.color = new Color(r, g, b, 1);
            }

            counter = 0;
        }
    }

    void Blinking()
    {
        if(counter >= duration)
        {
            counter = 0;

            if(myImage.color.a == 0.5f)
            {
                myImage.color = new Color(r, g, b, 1);
            }
            else if(myImage.color.a == 1)
            {
                myImage.color = new Color(r, g, b, 0.5f);
            }
        }
        else
        {
            counter += Time.deltaTime;
        }
    }
    
}
