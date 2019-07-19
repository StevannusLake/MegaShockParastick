using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.SimpleAndroidNotifications;

public class FreeCurrency1 : MonoBehaviour
{
    //============================================================================================================
    // 29/5
    private DateTime currentTime;
    private DateTime currentTime2;
    private DateTime currentTime3;
    private DateTime oldTime;
    private DateTime oldTime2;
    private DateTime oldTime3;

    private DateTime buttonPressedTime;
    private DateTime buttonPressedTime2;
    private DateTime buttonPressedTime3;
    private DateTime sincePressedTime;
    private DateTime sincePressedTime2;
    private DateTime sincePressedTime3;

    public int freeCurrencyTime;
    public int freeCurrencyTime2;
    public int freeCurrencyTime3;

    public string myLocation;
    public string myLocation2;
    public string myLocation3;

    public bool canGetFree;
    public bool canGetFree2;
    public bool canGetFree3;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

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
        if (PlayerPrefs.GetInt(myLocation + "LoginTime") < 0)
        {
            PlayerPrefs.SetInt(myLocation + "LoginTime", 0);
            freeCurrencyTime = PlayerPrefs.GetInt(myLocation + "LoginTime");
            PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            freeCurrencyTime = PlayerPrefs.GetInt(myLocation + "LoginTime");
        }

        if (PlayerPrefs.GetInt(myLocation2 + "LoginTime2") < 0)
        {
            PlayerPrefs.SetInt(myLocation2 + "LoginTime2", 0);
            freeCurrencyTime2 = PlayerPrefs.GetInt(myLocation2 + "LoginTime2");
            PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            freeCurrencyTime2 = PlayerPrefs.GetInt(myLocation + "LoginTime2");
        }

        if (PlayerPrefs.GetInt(myLocation3 + "LoginTime3") < 0)
        {
            PlayerPrefs.SetInt(myLocation3 + "LoginTime3", 0);
            freeCurrencyTime3 = PlayerPrefs.GetInt(myLocation3 + "LoginTime3");
            PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            freeCurrencyTime3 = PlayerPrefs.GetInt(myLocation3 + "LoginTime3");
        }

        currentTime = DateTime.Now;
        currentTime2 = DateTime.Now;
        currentTime3 = DateTime.Now;
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

                PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);
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

            if (difference.Minutes >= 1)
            {
                canGetFree = true;
            }
            if (difference.Minutes < 1)
            {
                canGetFree = false;
            }
        }

        if (PlayerPrefs.GetString(myLocation + "lastLoginTime") == "")
        {
            oldTime2 = DateTime.Now;
        }
        else
        {
            long temp2 = Convert.ToInt64(PlayerPrefs.GetString(myLocation2 + "lastLoginTime2"));
            oldTime2 = DateTime.FromBinary(temp2);

            print(myLocation2 + "oldTime: " + oldTime2);

            // find differene 
            TimeSpan difference2 = currentTime.Subtract(oldTime2);
            print(myLocation2 + "Difference: " + difference2);
            Debug.Log(difference2);
            if (difference2.Hours >= 2)
            {
                freeCurrencyTime2 = 0;

                PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrencyTime2);
                if (currentTime2 > oldTime2)
                {
                    PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
                }
                Debug.Log(myLocation2 + "2 Hours Has Passed");
            }
            else
            {
                Debug.Log(myLocation2 + "2 Hours Not Passed");
            }

            if (difference2.Minutes >= 1)
            {
                canGetFree = true;
            }
            if (difference2.Minutes < 1)
            {
                canGetFree = false;
            }
        }

        if (PlayerPrefs.GetString(myLocation3 + "lastLoginTime3") == "")
        {
            oldTime3 = DateTime.Now;
        }
        else
        {
            long temp3 = Convert.ToInt64(PlayerPrefs.GetString(myLocation3 + "lastLoginTime3"));
            oldTime3 = DateTime.FromBinary(temp3);

            print(myLocation3 + "oldTime: " + oldTime3);

            // find differene 
            TimeSpan difference3 = currentTime3.Subtract(oldTime3);
            print(myLocation3 + "Difference: " + difference3);

            if (difference3.Hours >= 2)
            {
                freeCurrencyTime3 = 0;

                PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrencyTime3);
                if (currentTime3 > oldTime3)
                {
                    PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
                }
                Debug.Log(myLocation3 + "2 Hours Has Passed");
            }
            else
            {
                Debug.Log(myLocation3 + "2 Hours Not Passed");
            }

            if (difference3.Minutes >= 1)
            {
                canGetFree3 = true;
            }
            if (difference3.Minutes < 1)
            {
                canGetFree3 = false;
            }
        }
    }

    void UpdatePassedTime()
    {
        // find differene 
        TimeSpan difference = currentTime.Subtract(oldTime);
        TimeSpan difference2 = currentTime2.Subtract(oldTime2);
        TimeSpan difference3 = currentTime3.Subtract(oldTime3);

        if (difference.Hours >= 2)
        {
            canGetFree = true;
            freeCurrencyTime = 0;

            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);
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
        Debug.Log(difference);
        Debug.Log(difference2);
        Debug.Log(difference3);
        if (difference2.Hours >= 2)
        {
            canGetFree2 = true;
            freeCurrencyTime2 = 0;

            PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrencyTime2);
            if (currentTime2 > oldTime2)
            {
                PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
            }
            Debug.Log(myLocation2 + "2 Hours Has Passed");
        }
        else
        {
            Debug.Log(myLocation2 + "2 Hours Not Passed");
        }

        if (difference3.Hours >= 2)
        {
            canGetFree3 = true;
            freeCurrencyTime3 = 0;

            PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrencyTime3);
            if (currentTime3 > oldTime3)
            {
                PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
            }
            Debug.Log(myLocation3 + "2 Hours Has Passed");
        }
        else
        {
            Debug.Log(myLocation3 + "2 Hours Not Passed");
        }

        // cache current time
        sincePressedTime = DateTime.Now;
        sincePressedTime2 = DateTime.Now;
        sincePressedTime3 = DateTime.Now;

        // get difference between last pressed (buttonPressedTime) and current time ()
        TimeSpan differenceForMinutes = sincePressedTime.Subtract(buttonPressedTime);
        TimeSpan differenceForMinutes2 = sincePressedTime2.Subtract(buttonPressedTime2);
        TimeSpan differenceForMinutes3 = sincePressedTime3.Subtract(buttonPressedTime3);

        if (differenceForMinutes.Minutes >= 1)
        {
            canGetFree = true;
        }
        if (differenceForMinutes2.Minutes >= 1)
        {
            canGetFree2 = true;
        }
        if (differenceForMinutes3.Minutes >= 1)
        {
            canGetFree3 = true;
        }
    }

    public void GetFreeButtonS()
    {
        if (canGetFree && freeCurrencyTime < 3)
        {
            if (freeCurrencyTime == 0)
            {
                //=============================================================================================================
                NotificationManager.Cancel(61);
                TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
                // schedule without icon
                NotificationManager.Send(61, delayNotifyTime, "💎OPALS💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);
                Debug.Log(delayNotifyTime);
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
            image1.SetActive(true);
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
            PlayerPrefs.SetString(myLocation2 + "PressButtonTime2", System.DateTime.Now.ToBinary().ToString());
            long tempTime = Convert.ToInt64(PlayerPrefs.GetString(myLocation2 + "PressButtonTime2"));
            buttonPressedTime2 = DateTime.FromBinary(tempTime);

            freeCurrencyTime2++;
            PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrencyTime2);

            // get free currency code go here

            Debug.Log("Get free2");

            addValue = UnityEngine.Random.Range(1, 5);

            GameManager.instance.AddPoints(addValue);
            GameManager.instance.SavePoints();
            image2.SetActive(true);
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
            PlayerPrefs.SetString(myLocation3 + "PressButtonTime3", System.DateTime.Now.ToBinary().ToString());
            long tempTime = Convert.ToInt64(PlayerPrefs.GetString(myLocation3 + "PressButtonTime3"));
            buttonPressedTime3 = DateTime.FromBinary(tempTime);

            freeCurrencyTime3++;
            PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrencyTime3);

            // get free currency code go here

            Debug.Log("Get free3");
            GameManager.instance.AddPoints(2);
            GameManager.instance.SavePoints();
            image3.SetActive(true);
        }
    }

    public void CloseAds1()
    {
        image1.SetActive(false);
    }

    public void CloseAds2()
    {
        image2.SetActive(false);
    }

    public void CloseAds3()
    {
        image3.SetActive(false);
    }
}
