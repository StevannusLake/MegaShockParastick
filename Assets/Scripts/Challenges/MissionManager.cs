using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    public List<int> missionListId = new List<int>();

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {   
        if(PlayerPrefs.GetInt("Mission1") == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                missionListId.Add((i*10)+1);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                missionListId.Add(PlayerPrefs.GetInt("Mission" + (i + 1)));
            }
        }
    }





    private void OnApplicationQuit()
    {
        for(int i=0;i < 5;i++)
        {
            PlayerPrefs.SetInt("Mission" + (i + 1), missionListId[i]);
        }
    }
}
