using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
   public void NextMission()
    {
        MissionManager.instance.NextMission();
    }
}
