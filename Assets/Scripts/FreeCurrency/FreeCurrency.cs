using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.SimpleAndroidNotifications;

public class FreeCurrency : MonoBehaviour
{
    //============================================================================================================
    // 29/5
    private DateTime currentTime;
    private DateTime oldTime;

    private DateTime buttonPressedTime;
    private DateTime sincePressedTime;

    public int freeCurrencyTime;
    public int freeCurrencyTime2;
    public int freeCurrencyTime3;

    public string myLocation;

    public bool canGetFree;
    public bool canGetFree2;
    public bool canGetFree3;

    private int addValue;
    
    private void Start()
    {
        //============================================================================================================
        // 29/5
        CheckDate();
    }

    //============================================================================================================
    // 29/5

    private void Update()
    {
        UpdatePassedTime();
    }

    void CheckDate()
    {
        if (PlayerPrefs.GetInt(myLocation + "LoginTime") == 0)
        {
            PlayerPrefs.SetInt(myLocation + "LoginTime", 1);
            freeCurrencyTime = PlayerPrefs.GetInt(myLocation + "LoginTime");
            PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            freeCurrencyTime = PlayerPrefs.GetInt(myLocation + "LoginTime");
        }

        currentTime = DateTime.Now;
        Debug.Log(myLocation + "currenDate: " + currentTime);
        if (PlayerPrefs.GetString(myLocation + "lastLoginTime") == "")
        {
            oldTime = DateTime.Now;
        }
        else
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "lastLoginTime"));
            oldTime = DateTime.FromBinary(temp);

            print(myLocation + "oldTime: " + oldTime);

            // find differene 
            TimeSpan difference = currentTime.Subtract(oldTime);
            print(myLocation + "Difference: " + difference);

            if (difference.Hours >= 2)
            {
                freeCurrencyTime = 0;
                freeCurrencyTime2 = 0;
                freeCurrencyTime3 = 0;

                PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);
                PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime2);
                PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime3);
                if (currentTime > oldTime)
                {
                    PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());

                }
                Debug.Log(myLocation + "2 Hours Has Passed");
            }
            else
            {
                Debug.Log(myLocation + "2 Hours Not Passed");
            }

            if(difference.Minutes >= 1)
            {
                canGetFree = true;
                canGetFree2 = true;
                canGetFree3 = true;
            }
        }

    }

    void UpdatePassedTime()
    {
        // find differene 
        TimeSpan difference = currentTime.Subtract(oldTime);

        if (difference.Hours >= 2)
        {
            canGetFree = true;
            freeCurrencyTime = 0;
            freeCurrencyTime2 = 0;
            freeCurrencyTime3 = 0;

            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);
            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime2);
            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime3);
            if (currentTime > oldTime)
            {
                PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());

            }
            Debug.Log(myLocation + "2 Hours Has Passed");
        }
        else
        {
            Debug.Log(myLocation + "2 Hours Not Passed");
        }
        
        // cache current time
        sincePressedTime = DateTime.Now;

        // get difference between last pressed (buttonPressedTime) and current time ()
        TimeSpan differenceForMinutes = sincePressedTime.Subtract(buttonPressedTime);

        if (differenceForMinutes.Minutes >= 1)
        {
            canGetFree = true;
            canGetFree2 = true;
            canGetFree3 = true;
        }
    }

    public void GetFreeButtonS()
    {
        if(canGetFree && freeCurrencyTime < 3)
        {
            if(freeCurrencyTime == 0)
            {
                //=============================================================================================================
                NotificationManager.Cancel(61);
                TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
                // schedule without icon
                NotificationManager.Send(61, delayNotifyTime, "Parastick", "Collect Free Opals Now and SHOW OFF your skins!", Color.red, NotificationIcon.Heart);
            }

            canGetFree = false;
            
            // button pressed, save and set press button time to buttonPressedTime
            PlayerPrefs.SetString(myLocation + "PressButtonTime", System.DateTime.Now.ToBinary().ToString());
            long tempTime = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "PressButtonTime"));
            buttonPressedTime = DateTime.FromBinary(tempTime);
            
            freeCurrencyTime++;
            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);

            // get free currency code go here

            Debug.Log("Get free");
            GameManager.instance.AddPoints(2);
            GameManager.instance.SavePoints();
        }
    }

    public void GetFreeButtonM()
    {
        if (canGetFree2 && freeCurrencyTime2 < 3)
        {
            if (freeCurrencyTime2 == 0)
            {
                //=============================================================================================================
                NotificationManager.Cancel(61);
                TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
                // schedule without icon
                NotificationManager.Send(61, delayNotifyTime, "Parastick", "Collect Free Opals Now and SHOW OFF your skins!", Color.red, NotificationIcon.Heart);
            }

            canGetFree2 = false;

            // button pressed, save and set press button time to buttonPressedTime
            PlayerPrefs.SetString(myLocation + "PressButtonTime", System.DateTime.Now.ToBinary().ToString());
            long tempTime = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "PressButtonTime"));
            buttonPressedTime = DateTime.FromBinary(tempTime);

            freeCurrencyTime2++;
            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);

            // get free currency code go here

            Debug.Log("Get free2");

            addValue = UnityEngine.Random.Range(1, 5);

            GameManager.instance.AddPoints(addValue);
            GameManager.instance.SavePoints();
        }
    }

    public void GetFreeButtonL()
    {
        if (canGetFree3 && freeCurrencyTime3 < 3)
        {
            if (freeCurrencyTime3 == 0)
            {
                //=============================================================================================================
                NotificationManager.Cancel(61);
                TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
                // schedule without icon
                NotificationManager.Send(61, delayNotifyTime, "Parastick", "Collect Free Opals Now and SHOW OFF your skins!", Color.red, NotificationIcon.Heart);
            }

            canGetFree3 = false;

            // button pressed, save and set press button time to buttonPressedTime
            PlayerPrefs.SetString(myLocation + "PressButtonTime", System.DateTime.Now.ToBinary().ToString());
            long tempTime = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "PressButtonTime"));
            buttonPressedTime = DateTime.FromBinary(tempTime);

            freeCurrencyTime3++;
            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);

            // get free currency code go here

            Debug.Log("Get free3");
            GameManager.instance.AddPoints(2);
            GameManager.instance.SavePoints();
        }
    }
}
