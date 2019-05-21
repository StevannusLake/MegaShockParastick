using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpalAdding : MonoBehaviour
{
    public void AddPoint()
    {
        GameManager.instance.AddPoints(1);
    }
}
