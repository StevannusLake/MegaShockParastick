using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.SimpleAndroidNotifications;

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

            if (difference.Days >= 1)
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

    void Update()
    {
        if (currentDate > oldDate)
        {
            PlayerPrefs.SetString("lastLogin", System.DateTime.Now.ToBinary().ToString());
        }
    }

    private void OnDisable()
    {
        //=============================================================================================================TimeSpan delayNotifyTime = new TimeSpan(0, 0, 3);

        NotificationManager.CancelAll();
        TimeSpan delayNotifyTime = new TimeSpan(0, 0, 10);
        Debug.Log(delayNotifyTime);
        // schedule without icon
        NotificationManager.Send(delayNotifyTime, "Parastick", "It's been A Thousand Year since you last visit me. Did you forget me? :'( ", Color.red, NotificationIcon.Heart);

    }

    void GiveRewards(int day)
    {
        if(day == 1)
        {
            GameManager.instance.AddCoin(5);
            GameManager.instance.totalCoinCollected += 5;
        }
        else if(day == 2)
        {
            GameManager.instance.AddCoin(10);
            GameManager.instance.totalCoinCollected += 10;
        }
        else if (day == 3)
        {
            GameManager.instance.AddCoin(15);
            GameManager.instance.totalCoinCollected += 15;
        }
        else if (day == 4)
        {
            GameManager.instance.AddCoin(20);
            GameManager.instance.totalCoinCollected += 20;
        }
        else if (day == 5)
        {
            GameManager.instance.AddCoin(25);
            GameManager.instance.totalCoinCollected += 25;
        }
        else if (day == 6)
        {
            GameManager.instance.AddCoin(30);
            GameManager.instance.totalCoinCollected += 30;
        }
        else if (day == 7)
        {
            GameManager.instance.AddPoints(5);
            GameManager.instance.totalPoints += 5;
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
