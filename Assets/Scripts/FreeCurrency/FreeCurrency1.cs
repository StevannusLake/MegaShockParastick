using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.SimpleAndroidNotifications;

public class FreeCurrency1 : MonoBehaviour
{
    ////============================================================================================================
    //// 29/5
    //private DateTime currentTime;
    //private DateTime currentTime2;
    //private DateTime currentTime3;
    //private DateTime oldTime;
    //private DateTime oldTime2;
    //private DateTime oldTime3;

    //private DateTime buttonPressedTime;
    //private DateTime buttonPressedTime2;
    //private DateTime buttonPressedTime3;
    //private DateTime sincePressedTime;
    //private DateTime sincePressedTime2;
    //private DateTime sincePressedTime3;
    //private TimeSpan differenceForMinutes;
    //private TimeSpan differenceForMinutes2;
    //private TimeSpan differenceForMinutes3;
    //private TimeSpan differenceForMinutesQ;
    //public TimeSpan differenceForMinutesQ2;
    //private TimeSpan differenceForMinutesQ3;
    //private DateTime leaveDateTime;
    //private bool isQuit = false;
    //private bool isQuit2 = false;
    //private bool isQuit3 = false;
    //public bool isPressed = false;
    //public bool isPressed2 = false;
    //public bool isPressed3 = false;

    //public int freeCurrencyTime;
    //public int freeCurrencyTime2;
    //public int freeCurrencyTime3;

    //public string myLocation;
    //public string myLocation2;
    //public string myLocation3;

    //public bool canGetFree;
    //public bool canGetFree2;
    //public bool canGetFree3;
    //public bool isStart = false;
    //public bool isStart2 = false;
    //public bool isStart3 = false;

    //public GameObject image1;
    //public GameObject image2;
    //public GameObject image3;

    //public GameObject button1;
    //public GameObject button2;
    //public GameObject button3;

    //public GameObject gameManager;
    //public Text timerText;
    //public Text timerText2;
    //public Text timerText3;
    //private int addValue;
    //public Text text;

    //private void Start()
    //{
    //    //============================================================================================================
    //    // 29/5
    //    CheckDate();
    //}

    ////============================================================================================================
    //// 29/5

    //private void Update()
    //{
    //    UpdatePassedTime();
    //}

    //void CheckDate()
    //{
    //    if (PlayerPrefs.GetInt(myLocation + "LoginTime") < 0)
    //    {
    //        PlayerPrefs.SetInt(myLocation + "LoginTime", 0);
    //        freeCurrencyTime = PlayerPrefs.GetInt(myLocation + "LoginTime");
    //        PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());
    //    }
    //    else
    //    {
    //        freeCurrencyTime = PlayerPrefs.GetInt(myLocation + "LoginTime");
    //    }

    //    if (PlayerPrefs.GetInt(myLocation2 + "LoginTime2") < 0)
    //    {
    //        PlayerPrefs.SetInt(myLocation2 + "LoginTime2", 0);
    //        freeCurrencyTime2 = PlayerPrefs.GetInt(myLocation2 + "LoginTime2");
    //        PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
    //    }
    //    else
    //    {
    //        freeCurrencyTime2 = PlayerPrefs.GetInt(myLocation + "LoginTime2");
    //    }

    //    if (PlayerPrefs.GetInt(myLocation3 + "LoginTime3") < 0)
    //    {
    //        PlayerPrefs.SetInt(myLocation3 + "LoginTime3", 0);
    //        freeCurrencyTime3 = PlayerPrefs.GetInt(myLocation3 + "LoginTime3");
    //        PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
    //    }
    //    else
    //    {
    //        freeCurrencyTime3 = PlayerPrefs.GetInt(myLocation3 + "LoginTime3");
    //    }

    //    currentTime = DateTime.Now;
    //    currentTime2 = DateTime.Now;
    //    currentTime3 = DateTime.Now;
    //    Debug.Log(myLocation + "currenDate: " + currentTime);
    //    if (PlayerPrefs.GetString(myLocation + "lastLoginTime") == "")
    //    {
    //        oldTime = DateTime.Now;
    //    }
    //    else
    //    {
    //        long temp = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "lastLoginTime"));
    //        oldTime = DateTime.FromBinary(temp);

    //        print(myLocation + "oldTime: " + oldTime);

    //        // find differene 
    //        TimeSpan difference = currentTime.Subtract(oldTime);
    //        print(myLocation + "Difference: " + difference);

    //        if (difference.Hours >= 2)
    //        {
    //            freeCurrencyTime = 0;
    //            canGetFree = true;
    //            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);
    //            if (currentTime > oldTime)
    //            {
    //                PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());
    //            }

    //            //=============================================================================================================
    //            NotificationManager.Cancel(67);
    //            TimeSpan delayNotifyTime = new TimeSpan(0, 0, 0);
    //            // schedule without icon
    //            NotificationManager.Send(67, TimeSpan.FromSeconds(5), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

    //            Debug.Log(myLocation + "2 Hours Has Passed");
    //        }
    //        else
    //        {
    //            Debug.Log(myLocation + "2 Hours Not Passed");
    //        }

    //        long temp4 = Convert.ToInt64(PlayerPrefs.GetString("sysString1"));

    //        DateTime oldDate = DateTime.FromBinary(temp4);

    //        differenceForMinutesQ = currentTime.Subtract(oldDate);

    //        if (differenceForMinutesQ.Minutes >= 1)
    //        {
    //            canGetFree = true;

    //            if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //            {
    //                button1.GetComponent<Button>().interactable = true;
    //            }
    //        }
    //        else
    //        {
    //         //   button1.GetComponent<Button>().interactable = false;
    //        }
    //        isStart = true;
    //    }

    //    if (PlayerPrefs.GetString(myLocation2 + "lastLoginTime2") == "")
    //    {
    //        oldTime2 = DateTime.Now;
    //    }
    //    else
    //    {
    //        long temp2 = Convert.ToInt64(PlayerPrefs.GetString(myLocation2 + "lastLoginTime2"));
    //        oldTime2 = DateTime.FromBinary(temp2);

    //        print(myLocation2 + "oldTime: " + oldTime2);

    //        // find differene 
    //        TimeSpan difference2 = currentTime2.Subtract(oldTime2);
    //        print(myLocation2 + "Difference: " + difference2);
    //        Debug.Log(difference2);
    //        if (difference2.Hours >= 2)
    //        {
    //            freeCurrencyTime2 = 0;
    //            canGetFree2 = true;
    //            PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrencyTime2);
    //            if (currentTime2 > oldTime2)
    //            {
    //                PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
    //            }

    //            //=============================================================================================================
    //            NotificationManager.Cancel(68);
    //            TimeSpan delayNotifyTime = new TimeSpan(0, 0, 0);
    //            // schedule without icon
    //            NotificationManager.Send(68, TimeSpan.FromSeconds(5), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

    //            Debug.Log(myLocation2 + "2 Hours Has Passed");
    //        }
    //        else
    //        {
    //            Debug.Log(myLocation2 + "2 Hours Not Passed");
    //        }

    //        long temp5 = Convert.ToInt64(PlayerPrefs.GetString("sysString2"));

    //        DateTime oldDate = DateTime.FromBinary(temp5);

    //        differenceForMinutesQ2 = currentTime2.Subtract(oldDate);
    //        Debug.Log(differenceForMinutesQ2);
    //        if (differenceForMinutesQ2.Minutes >= 1)
    //        {
    //            canGetFree2 = true;
    //            if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //            {
    //                button2.GetComponent<Button>().interactable = true;
    //            }
    //        }
    //        else
    //        {
    //         //   button2.GetComponent<Button>().interactable = false;
    //        }
    //        isStart2 = true;
    //    }

    //    if (PlayerPrefs.GetString(myLocation3 + "lastLoginTime3") == "")
    //    {
    //        oldTime3 = DateTime.Now;
    //    }
    //    else
    //    {
    //        long temp3 = Convert.ToInt64(PlayerPrefs.GetString(myLocation3 + "lastLoginTime3"));
    //        oldTime3 = DateTime.FromBinary(temp3);

    //        print(myLocation3 + "oldTime: " + oldTime3);

    //        // find differene 
    //        TimeSpan difference3 = currentTime3.Subtract(oldTime3);
    //        print(myLocation3 + "Difference: " + difference3);

    //        if (difference3.Hours >= 2)
    //        {
    //            freeCurrencyTime3 = 0;
    //            canGetFree3 = true;
    //            PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrencyTime3);
    //            if (currentTime3 > oldTime3)
    //            {
    //                PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
    //            }

    //            //=============================================================================================================
    //            NotificationManager.Cancel(69);
    //            TimeSpan delayNotifyTime = new TimeSpan(0, 0, 0);
    //            // schedule without icon
    //            NotificationManager.Send(69, TimeSpan.FromSeconds(5), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

    //            Debug.Log(myLocation3 + "2 Hours Has Passed");
    //        }
    //        else
    //        {
    //            Debug.Log(myLocation3 + "2 Hours Not Passed");
    //        }

    //        long temp6 = Convert.ToInt64(PlayerPrefs.GetString("sysString3"));

    //        DateTime oldDate = DateTime.FromBinary(temp6);

    //        differenceForMinutesQ3 = currentTime2.Subtract(oldDate);

    //        if (differenceForMinutesQ3.Minutes >= 1)
    //        {
    //            canGetFree3 = true;
    //            if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //            {
    //                button3.GetComponent<Button>().interactable = true;
    //            }
    //        }
    //        else
    //        {
    //           // button3.GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    isStart3 = true;
    //}

    //private void OnApplicationQuit()
    //{
    //    if (isQuit == false)
    //    {
    //        PlayerPrefs.SetString("sysString1", System.DateTime.Now.ToBinary().ToString());
    //        isQuit = true;
    //    }

    //    if (isQuit2 == false)
    //    {
    //        PlayerPrefs.SetString("sysString2", System.DateTime.Now.ToBinary().ToString());
    //        isQuit2 = true;
    //    }

    //    if (isQuit3 == false)
    //    {
    //        PlayerPrefs.SetString("sysString3", System.DateTime.Now.ToBinary().ToString());
    //        isQuit3 = true;
    //    }
    //}

    //void UpdatePassedTime()
    //{
    //    // find differene 
    //    TimeSpan difference = currentTime.Subtract(oldTime);
    //    TimeSpan difference2 = currentTime2.Subtract(oldTime2);
    //    TimeSpan difference3 = currentTime3.Subtract(oldTime3);

    //    if (difference.Hours >= 2)
    //    {
    //        canGetFree = true;
    //        freeCurrencyTime = 0;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button1.GetComponent<Button>().interactable = true;
    //        }
    //        PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);
    //        if (currentTime > oldTime)
    //        {
    //            PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());
    //        }
    //        Debug.Log(myLocation + "2 Hours Has Passed");
    //    }
    //    else
    //    {
    //        Debug.Log(myLocation + "2 Hours Not Passed");
    //    }

    //    if (difference2.Hours >= 2)
    //    {
    //        canGetFree2 = true;
    //        freeCurrencyTime2 = 0;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button2.GetComponent<Button>().interactable = true;
    //        }
    //        PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrencyTime2);
    //        if (currentTime2 > oldTime2)
    //        {
    //            PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
    //        }
    //        Debug.Log(myLocation2 + "2 Hours Has Passed");
    //    }
    //    else
    //    {
    //        Debug.Log(myLocation2 + "2 Hours Not Passed");
    //    }

    //    if (difference3.Hours >= 2)
    //    {
    //        canGetFree3 = true;
    //        freeCurrencyTime3 = 0;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button3.GetComponent<Button>().interactable = true;
    //        }
    //        PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrencyTime3);
    //        if (currentTime3 > oldTime3)
    //        {
    //            PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
    //        }
    //        Debug.Log(myLocation3 + "2 Hours Has Passed");
    //    }
    //    else
    //    {
    //        Debug.Log(myLocation3 + "2 Hours Not Passed");
    //    }

    //    if (isPressed == true)
    //    {
    //        isQuit = false;
    //    }
    //    else
    //    {
    //        isQuit = true;
    //    }

    //    if (isPressed2 == true)
    //    {
    //        isQuit2 = false;
    //    }
    //    else
    //    {
    //        isQuit2 = true;
    //    }

    //    if (isPressed3 == true)
    //    {
    //        isQuit3 = false;
    //    }
    //    else
    //    {
    //        isQuit3 = true;
    //    }

    //    long temp4 = Convert.ToInt64(PlayerPrefs.GetString("sysString1"));

    //    DateTime oldDate = DateTime.FromBinary(temp4);

    //    differenceForMinutesQ = sincePressedTime2.Subtract(oldDate);

    //    if (differenceForMinutesQ.Minutes >= 1 && isStart == true)
    //    {
    //        canGetFree = true;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button1.GetComponent<Button>().interactable = true;
    //        }
    //     //   PlayerPrefs.SetString("sysString1", System.DateTime.Now.ToBinary().ToString());
    //        isStart = false;
    //    }

    //    long temp5 = Convert.ToInt64(PlayerPrefs.GetString("sysString2"));

    //    DateTime oldDate2 = DateTime.FromBinary(temp5);

    //    differenceForMinutesQ2 = sincePressedTime2.Subtract(oldDate2);

    //    if (differenceForMinutesQ2.Minutes >= 1 && isStart2 == true)
    //    {
    //        canGetFree2 = true;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button2.GetComponent<Button>().interactable = true;
    //        }
    //      //  PlayerPrefs.SetString("sysString2", System.DateTime.Now.ToBinary().ToString());
    //        isStart2 = false;
    //    }

    //    long temp6 = Convert.ToInt64(PlayerPrefs.GetString("sysString3"));

    //    DateTime oldDate3 = DateTime.FromBinary(temp6);

    //    differenceForMinutesQ3 = sincePressedTime2.Subtract(oldDate3);

    //    if (differenceForMinutesQ3.Minutes >= 1 && isStart3 == true)
    //    {
    //        canGetFree3 = true;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button3.GetComponent<Button>().interactable = true;
    //        }
    //       // PlayerPrefs.SetString("sysString3", System.DateTime.Now.ToBinary().ToString());
    //        isStart3 = false;
    //    }

    //    long tempTime = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "PressButtonTime"));
    //    buttonPressedTime = DateTime.FromBinary(tempTime);

    //    long tempTime2 = Convert.ToInt64(PlayerPrefs.GetString(myLocation2 + "PressButtonTime2"));
    //    buttonPressedTime2 = DateTime.FromBinary(tempTime2);

    //    long tempTime3 = Convert.ToInt64(PlayerPrefs.GetString(myLocation3 + "PressButtonTime3"));
    //    buttonPressedTime3 = DateTime.FromBinary(tempTime3);

    //    // cache current time
    //    sincePressedTime = DateTime.Now;
    //    sincePressedTime2 = DateTime.Now;
    //    sincePressedTime3 = DateTime.Now;

    //    // get difference between last pressed (buttonPressedTime) and current time ()
    //    if (isPressed == true)
    //    {
    //        differenceForMinutes = sincePressedTime.Subtract(buttonPressedTime);
    //    }

    //    if (isPressed2 == true)
    //    {
    //        differenceForMinutes2 = sincePressedTime2.Subtract(buttonPressedTime2);
    //    }

    //    if (isPressed3 == true)
    //    {
    //        differenceForMinutes3 = sincePressedTime3.Subtract(buttonPressedTime3);
    //    }

    //    if (differenceForMinutes.Minutes >= 1)
    //    {
    //        canGetFree = true;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button1.GetComponent<Button>().interactable = true;
    //        }
    //    }
    //    if (differenceForMinutes2.Minutes >= 1)
    //    {
    //        canGetFree2 = true;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button2.GetComponent<Button>().interactable = true;
    //        }
    //    }
    //    if (differenceForMinutes3.Minutes >= 1)
    //    {
    //        canGetFree3 = true;
    //        if (gameManager.GetComponent<InternetChecker>().isConnect == true)
    //        {
    //            button3.GetComponent<Button>().interactable = true;
    //        }
    //    }

    //    timerText.GetComponent<Text>().text = differenceForMinutesQ.ToString();
    //   // timerText2.GetComponent<Text>().text = differenceForMinutesQ2.ToString();
    //    timerText3.GetComponent<Text>().text = differenceForMinutesQ3.ToString();
    //}

    //public void GetFreeButtonS()
    //{
    //    if (canGetFree == true && freeCurrencyTime < 3)
    //    {
    //        //=============================================================================================================
    //        NotificationManager.Cancel(64);
    //        TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
    //        // schedule without icon
    //        NotificationManager.Send(64, TimeSpan.FromHours(2), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

    //        button1.GetComponent<Button>().interactable = false;
    //        canGetFree = false;
    //        isPressed = true;

    //        // button pressed, save and set press button time to buttonPressedTime
    //        PlayerPrefs.SetString(myLocation + "PressButtonTime", System.DateTime.Now.ToBinary().ToString());


    //        freeCurrencyTime++;
    //        PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrencyTime);

    //        // get free currency code go here

    //        Debug.Log("Get free");
    //        GameManager.instance.AddPoints(2);
    //        GameManager.instance.SavePoints();
    //        image1.SetActive(true);
    //    }
    //}

    //public void GetFreeButtonM()
    //{
    //    if (canGetFree2 == true && freeCurrencyTime2 < 3)
    //    {
    //        //=============================================================================================================
    //        NotificationManager.Cancel(65);
    //        TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
    //        // schedule without icon
    //        NotificationManager.Send(65, TimeSpan.FromHours(2), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

    //        button2.GetComponent<Button>().interactable = false;
    //        canGetFree2 = false;
    //        isPressed2 = true;

    //        // button pressed, save and set press button time to buttonPressedTime
    //        PlayerPrefs.SetString(myLocation2 + "PressButtonTime2", System.DateTime.Now.ToBinary().ToString());


    //        freeCurrencyTime2++;
    //        PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrencyTime2);

    //        // get free currency code go here

    //        Debug.Log("Get free2");

    //        addValue = UnityEngine.Random.Range(1, 5);

    //        GameManager.instance.AddPoints(addValue);
    //        GameManager.instance.SavePoints();
    //        image2.SetActive(true);
    //    }
    //}

    //public void GetFreeButtonL()
    //{
    //    if (canGetFree3 == true && freeCurrencyTime3 < 3)
    //    {
    //        //=============================================================================================================
    //        NotificationManager.Cancel(66);
    //        TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
    //        // schedule without icon
    //        NotificationManager.Send(66, TimeSpan.FromHours(2), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

    //        button3.GetComponent<Button>().interactable = false;
    //        canGetFree3 = false;
    //        isPressed3 = true;

    //        // button pressed, save and set press button time to buttonPressedTime
    //        PlayerPrefs.SetString(myLocation3 + "PressButtonTime3", System.DateTime.Now.ToBinary().ToString());
    //        long tempTime = Convert.ToInt64(PlayerPrefs.GetString(myLocation3 + "PressButtonTime3"));
    //        buttonPressedTime3 = DateTime.FromBinary(tempTime);

    //        freeCurrencyTime3++;
    //        PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrencyTime3);

    //        // get free currency code go here

    //        Debug.Log("Get free3");
    //        GameManager.instance.AddPoints(2);
    //        GameManager.instance.SavePoints();
    //        image3.SetActive(true);
    //    }
    //}

    //public void CloseAds1()
    //{
    //    image1.SetActive(false);
    //}

    //public void CloseAds2()
    //{
    //    image2.SetActive(false);
    //}

    //public void CloseAds3()
    //{
    //    image3.SetActive(false);
    //}

    private DateTime currentTime;
    public DateTime oldTime;
    private DateTime sincePressedTime;
    private DateTime buttonPressedTime;
    public TimeSpan differenceForMin;
    public TimeSpan differenceForMinQ;

    public int freeCurrency;
    public string myLocation;

    public bool canGetFree = false;
    public bool isPressed = false;
    public bool isQuit = false;
    public bool stopTimer = false;
    public GameObject image1;
    private int addValue;
    public Text timerText;
    public TimeSpan difference;

    void Start()
    {
        CheckDate();
    }

    private void Update()
    {
        UpdatePassedTime();
        
        Debug.Log(differenceForMinQ);
    }

    private void OnApplicationQuit()
    {
        if (isQuit == false)
        {
            PlayerPrefs.SetString("sysString1", System.DateTime.Now.ToBinary().ToString());
            isQuit = true;
        }
    }

    void CheckDate()
    {
        if (PlayerPrefs.GetInt(myLocation + "LoginTime") < 0)
        {
            PlayerPrefs.SetInt(myLocation + "LoginTime", 0);
            freeCurrency = PlayerPrefs.GetInt(myLocation + "LoginTime");
            PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            freeCurrency = PlayerPrefs.GetInt(myLocation + "LoginTime");
        }

        currentTime = DateTime.Now;

        //if (PlayerPrefs.GetString(myLocation + "lastLoginTime") == "")
        //{
        //    oldTime = DateTime.Now;
        //}
        //else
        //{
            long temp = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "lastLoginTime"));
            oldTime = DateTime.FromBinary(temp);

            print(myLocation + "oldTime: " + oldTime);

            // find differene 
            difference = currentTime.Subtract(oldTime);
            print(myLocation + "Difference: " + difference);
            
            if (difference.Hours >= 2)
            {
                freeCurrency = 0;
               // canGetFree = true;
                PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrency);
                if (currentTime > oldTime)
                {
                    PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());
                }

                //=============================================================================================================
                NotificationManager.Cancel(67);
                TimeSpan delayNotifyTime = new TimeSpan(0, 0, 0);
                // schedule without icon
                NotificationManager.Send(67, TimeSpan.FromSeconds(5), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

                Debug.Log(myLocation + "2 Hours Has Passed");
            }
            else
            {
                Debug.Log(myLocation + "2 Hours Not Passed");
            }

            long temp4 = Convert.ToInt64(PlayerPrefs.GetString("sysString1"));

            DateTime oldDate = DateTime.FromBinary(temp4);

            differenceForMinQ = currentTime.Subtract(oldDate);

            if (differenceForMinQ.Minutes >= 1)
            {
                canGetFree = true;
            }
            stopTimer = true;
       // }
    }

    void UpdatePassedTime()
    {
        TimeSpan difference = currentTime.Subtract(oldTime);
        
        if (difference.Hours >= 2)
        {
            //canGetFree = true;
            freeCurrency = 0;

            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrency);
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

        long tempTime = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "PressButtonTime"));
        buttonPressedTime = DateTime.FromBinary(tempTime);

        sincePressedTime = DateTime.Now;

        if(isPressed == true)
        {
            differenceForMin = sincePressedTime.Subtract(buttonPressedTime);
            isQuit = false;
        }
        else
        {
            isQuit = true;
        }

        if (differenceForMin.Minutes >= 1)
        {
            canGetFree = true;
        }

        long temp4 = Convert.ToInt64(PlayerPrefs.GetString("sysString1"));

        DateTime oldDate = DateTime.FromBinary(temp4);

        differenceForMinQ = sincePressedTime.Subtract(oldDate);

        if (differenceForMinQ.Minutes >= 1 && stopTimer == true)
        {
            canGetFree = true;
            stopTimer = false;
        }
    }

    public void GetFreeOpalMainMenu()
    {
        if (canGetFree == true && freeCurrency < 3)
        {
            //=============================================================================================================
            NotificationManager.Cancel(65);
            TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
            // schedule without icon
            NotificationManager.Send(65, TimeSpan.FromHours(2), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

            canGetFree = false;
            isPressed = true;

            // button pressed, save and set press button time to buttonPressedTime
            PlayerPrefs.SetString(myLocation + "PressButtonTime", System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("sysString1", System.DateTime.Now.ToBinary().ToString());

            freeCurrency++;
            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrency);

            // get free currency code go here

            Debug.Log("Get free2");

            addValue = UnityEngine.Random.Range(1, 5);

            GameManager.instance.AddPoints(addValue);
            GameManager.instance.SavePoints();
        }
    }
}
