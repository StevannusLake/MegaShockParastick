using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSkin : MonoBehaviour
{
    public enum PipeType { LeftUp, RightUp,UpLeft,UpRight,Up};
    public PipeType pipeType;
    public int tempType;

    // Update is called once per frame
    void Update()
    {
        tempType = Shop.instance.environmentUsing.GetComponent<Skin>().environmentType;
        if (tempType == 1)
        {
            if (pipeType == PipeType.LeftUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType1[0];
            }
            else if(pipeType == PipeType.RightUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType1[1];
            }
            else if (pipeType == PipeType.UpLeft)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType1[2];
            }
            else if (pipeType == PipeType.UpRight)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType1[3];
            }
            else if (pipeType == PipeType.Up)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType1[4];
            }
        }
        else if (tempType == 2)
        {
            if (pipeType == PipeType.LeftUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType2[0];
            }
            else if (pipeType == PipeType.RightUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType2[1];
            }
            else if (pipeType == PipeType.UpLeft)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType2[2];
            }
            else if (pipeType == PipeType.UpRight)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType2[3];
            }
            else if (pipeType == PipeType.Up)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType2[4];
            }
        }
        else if (tempType == 3)
        {
            if (pipeType == PipeType.LeftUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType3[0];
            }
            else if (pipeType == PipeType.RightUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType3[1];
            }
            else if (pipeType == PipeType.UpLeft)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType3[2];
            }
            else if (pipeType == PipeType.UpRight)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType3[3];
            }
            else if (pipeType == PipeType.Up)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType3[4];
            }
        }
        else if (tempType == 4)
        {
            if (pipeType == PipeType.LeftUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType4[0];
            }
            else if (pipeType == PipeType.RightUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType4[1];
            }
            else if (pipeType == PipeType.UpLeft)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType4[2];
            }
            else if (pipeType == PipeType.UpRight)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType4[3];
            }
            else if (pipeType == PipeType.Up)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType4[4];
            }
        }
        else if (tempType == 5)
        {
            if (pipeType == PipeType.LeftUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType5[0];
            }
            else if (pipeType == PipeType.RightUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType5[1];
            }
            else if (pipeType == PipeType.UpLeft)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType5[2];
            }
            else if (pipeType == PipeType.UpRight)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType5[3];
            }
            else if (pipeType == PipeType.Up)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType5[4];
            }
        }
        else if (tempType == 6)
        {
            if (pipeType == PipeType.LeftUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType6[0];
            }
            else if (pipeType == PipeType.RightUp)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType6[1];
            }
            else if (pipeType == PipeType.UpLeft)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType6[2];
            }
            else if (pipeType == PipeType.UpRight)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType6[3];
            }
            else if (pipeType == PipeType.Up)
            {
                GetComponent<SpriteRenderer>().sprite = Shop.instance.environmentType6[4];
            }
        }
    }
}
