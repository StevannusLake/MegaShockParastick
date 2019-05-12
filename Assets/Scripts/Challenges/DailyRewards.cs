using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyRewards : MonoBehaviour
{
    public GameObject[] greenTicks;
    private DateTime currentDate;
    private DateTime oldDate;
    private int numOfDay;

    // Start is called before the first frame update
    void Start()
    {   
        CheckDate();
        CheckGreenTick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckDate()
    {
        if(PlayerPrefs.GetInt("LoginDay") == 0)
        {
            PlayerPrefs.SetInt("LoginDay", 1);
            numOfDay = PlayerPrefs.GetInt("LoginDay");
            GiveRewards(numOfDay);
            Debug.Log("Give First Reward");
        }
        else
        {
            numOfDay = PlayerPrefs.GetInt("LoginDay");
        }
        currentDate = DateTime.Now;
        Debug.Log("currenDate: " + currentDate);
        if (PlayerPrefs.GetString("lastLogin") == "")
        {
            oldDate = DateTime.Now;
        }
        else
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString("lastLogin"));
            oldDate = DateTime.FromBinary(temp);

            print("oldDate: " + oldDate);

            // find differene 
            TimeSpan difference = currentDate.Subtract(oldDate);
            print("Difference: " + difference);

            if (difference.Days == 1)
            {   
                if(numOfDay == 7)
                {
                    numOfDay = 1;
                }
                else
                {
                    numOfDay++;
                }
                PlayerPrefs.SetInt("LoginDay", numOfDay);
                GiveRewards(numOfDay);
                Debug.Log("One Day Has Passed");
            }
            else
            {
                Debug.Log("Day Not Passed");
            }
        }
    }

    void OnApplicationQuit()
    {
        if (currentDate > oldDate)
        {
            PlayerPrefs.SetString("lastLogin", System.DateTime.Now.ToBinary().ToString());
            Debug.Log("Save Date");
        }
        else
        {
            Debug.Log("Cheat");
        }
    }

    void GiveRewards(int day)
    {
        if(day == 1)
        {
            GameManager.instance.AddCoin(5);
        }
        else if(day == 2)
        {
            GameManager.instance.AddCoin(10);
        }
        else if (day == 3)
        {
            GameManager.instance.AddCoin(15);
        }
        else if (day == 4)
        {
            GameManager.instance.AddCoin(20);
        }
        else if (day == 5)
        {
            GameManager.instance.AddCoin(25);
        }
        else if (day == 6)
        {
            GameManager.instance.AddCoin(30);
        }
        else if (day == 7)
        {
            GameManager.instance.AddPoints(5);
        }
        GameManager.instance.SaveCoin();
        GameManager.instance.SavePoints();
    }

    void CheckGreenTick()
    {
        for(int i=0;i<numOfDay;i++)
        {
            greenTicks[i].SetActive(true);
        }
    }
}
