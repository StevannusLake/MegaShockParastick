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

    public DateTime currentTime;
    public DateTime oldTime;
    public DateTime oldTime2;
    public DateTime oldTime3;
    private DateTime sincePressedTime;
    public TimeSpan differenceForMinQ;
    public TimeSpan differenceForMinQ2;
    public TimeSpan differenceForMinQ3;

    public int freeCurrency;
    public int freeCurrency2;
    public int freeCurrency3;
    public string myLocation;
    public string myLocation2;
    public string myLocation3;

    public bool canGetFree = false;
    public bool canGetFree2 = false;
    public bool canGetFree3 = false;
    public bool stopTimer = false;
    public bool stopTimer2 = false;
    public bool stopTimer3 = false;
    public bool isFirst;
    public bool isFirst2;
    public bool isFirst3;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    private int addValue;
    public TimeSpan difference;
    public TimeSpan difference2;
    public TimeSpan difference3;
    public TimeSpan differenceQ;
    public TimeSpan differenceQ2;
    public TimeSpan differenceQ3;
    public GameObject mainButton;
    public GameObject mainButton2;
    public GameObject shopButton;
    public GameObject shopButton2;
    public GameObject loseButton;
    public GameObject loseButton2;

    void Start()
    {
        CheckDate();

        //------------Main Menu----------\\

        if (isFirst = (PlayerPrefs.GetInt("GetFreeCurrency9") == 0))
        {
            mainButton.SetActive(true);
            canGetFree = true;
            PlayerPrefs.SetString(myLocation + "lastLoginTime", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            mainButton.SetActive(false);
            mainButton2.SetActive(true);
        }

        //------------Main Menu----------\\

        //------------Shop Menu----------\\

        if (isFirst2 = (PlayerPrefs.GetInt("GetFreeCurrency4") == 0))
        {
            shopButton2.SetActive(true);
            canGetFree2 = true;
            PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            shopButton.SetActive(false);
            shopButton2.SetActive(true);
        }

        //------------Shop Menu----------\\

        //------------Lose Menu----------\\

        if (isFirst3 = (PlayerPrefs.GetInt("GetFreeCurrency5") == 0))
        {
            loseButton2.SetActive(true);
            canGetFree3 = true;
            PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            loseButton.SetActive(false);
            loseButton2.SetActive(true);
        }

        //------------Lose Menu----------\\
    }

    private void Update()
    {
        UpdatePassedTime();
    }

    void CheckDate()
    {
        //------------Main Menu----------\\
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
            difference = currentTime.Subtract(oldTime);
            print(myLocation + "Difference: " + difference);

            if (difference.Hours >= 2)
            {
                freeCurrency = 0;
                canGetFree = true;
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
            stopTimer = true;
        }
        //------------Main Menu----------\\

        //------------Shop Menu----------\\

        if (PlayerPrefs.GetInt(myLocation2 + "LoginTime2") < 0)
        {
            PlayerPrefs.SetInt(myLocation2 + "LoginTime2", 0);
            freeCurrency2 = PlayerPrefs.GetInt(myLocation2 + "LoginTime2");
            PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            freeCurrency2 = PlayerPrefs.GetInt(myLocation2 + "LoginTime2");
        }

        if (PlayerPrefs.GetString(myLocation2 + "lastLoginTime2") == "")
        {
            oldTime2 = DateTime.Now;
        }
        else
        {
            long temp2 = Convert.ToInt64(PlayerPrefs.GetString(myLocation2 + "lastLoginTime2"));
            oldTime2 = DateTime.FromBinary(temp2);

            print(myLocation2 + "oldTime: " + oldTime2);

            // find differene 
            difference2 = currentTime.Subtract(oldTime2);
            print(myLocation2 + "Difference: " + difference2);

            if (difference2.Hours >= 2)
            {
                freeCurrency2 = 0;
                canGetFree2 = true;
                PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrency2);
                if (currentTime > oldTime2)
                {
                    PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
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
            stopTimer2 = true;
        }

        //------------Shop Menu----------\\

        //------------Lose Menu----------\\

        if (PlayerPrefs.GetInt(myLocation3 + "LoginTime3") < 0)
        {
            PlayerPrefs.SetInt(myLocation3 + "LoginTime3", 0);
            freeCurrency3 = PlayerPrefs.GetInt(myLocation3 + "LoginTime3");
            PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            freeCurrency3 = PlayerPrefs.GetInt(myLocation3 + "LoginTime3");
        }

        if (PlayerPrefs.GetString(myLocation3 + "lastLoginTime3") == "")
        {
            oldTime3 = DateTime.Now;
        }
        else
        {
            long temp3= Convert.ToInt64(PlayerPrefs.GetString(myLocation3 + "lastLoginTime3"));
            oldTime3 = DateTime.FromBinary(temp3);

            print(myLocation3 + "oldTime: " + oldTime3);

            // find differene 
            difference3 = currentTime.Subtract(oldTime3);
            print(myLocation3 + "Difference: " + difference3);

            if (difference3.Hours >= 2)
            {
                freeCurrency3 = 0;
                canGetFree3 = true;
                PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrency3);
                if (currentTime > oldTime3)
                {
                    PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
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
            stopTimer3 = true;
        }

        //------------Lose Menu----------\\
    }

    void UpdatePassedTime()
    {
        //--------------Main Menu---------------\\
        sincePressedTime = DateTime.Now;

        long temp = Convert.ToInt64(PlayerPrefs.GetString(myLocation + "lastLoginTime"));
        oldTime = DateTime.FromBinary(temp);

        differenceQ = sincePressedTime.Subtract(oldTime);
        
        if (differenceQ.Hours >= 2)
        {
            freeCurrency = 0;
            canGetFree = true;
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

        long temp4 = Convert.ToInt64(PlayerPrefs.GetString("sysString1"));

        DateTime oldDate = DateTime.FromBinary(temp4);

        oldDate = oldDate.AddMinutes(1);

        differenceForMinQ = oldDate.Subtract(sincePressedTime);

        if (differenceForMinQ.Seconds < 1 && stopTimer == true)
        {
            mainButton2.GetComponent<Button>().interactable = true;
            canGetFree = true;
            stopTimer = false;
        }
        else if(differenceForMinQ.Seconds > 1)
        {
            mainButton2.GetComponent<Button>().interactable = false;
        }

        //--------------Main Menu---------------\\

        //--------------Shop Menu---------------\\

        long temp2 = Convert.ToInt64(PlayerPrefs.GetString(myLocation2 + "lastLoginTime2"));
        oldTime2 = DateTime.FromBinary(temp2);

        differenceQ2 = sincePressedTime.Subtract(oldTime2);

        if (differenceQ2.Hours >= 2)
        {
            freeCurrency2 = 0;
            canGetFree2 = true;
            PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrency2);
            if (currentTime > oldTime2)
            {
                PlayerPrefs.SetString(myLocation2 + "lastLoginTime2", System.DateTime.Now.ToBinary().ToString());
            }
            Debug.Log(myLocation2 + "2 Hours Has Passed");
        }
        else
        {
            Debug.Log(myLocation2 + "2 Hours Not Passed");
        }

        long temp5 = Convert.ToInt64(PlayerPrefs.GetString("sysString2"));

        DateTime oldDate2 = DateTime.FromBinary(temp5);

        oldDate2 = oldDate2.AddMinutes(1);

        differenceForMinQ2 = oldDate2.Subtract(sincePressedTime);

        if (differenceForMinQ2.Seconds < 1 && stopTimer2 == true)
        {
            shopButton2.GetComponent<Button>().interactable = true;
            canGetFree2 = true;
            stopTimer2 = false;
        }
        else if (differenceForMinQ2.Seconds > 1)
        {
            shopButton2.GetComponent<Button>().interactable = false;
        }
        //--------------Shop Menu---------------\\

        //--------------Lose Menu---------------\\

        long temp3 = Convert.ToInt64(PlayerPrefs.GetString(myLocation3 + "lastLoginTime3"));
        oldTime3 = DateTime.FromBinary(temp3);

        differenceQ3 = sincePressedTime.Subtract(oldTime3);

        if (differenceQ3.Hours >= 2)
        {
            freeCurrency3 = 0;
            canGetFree3 = true;
            PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrency3);
            if (currentTime > oldTime3)
            {
                PlayerPrefs.SetString(myLocation3 + "lastLoginTime3", System.DateTime.Now.ToBinary().ToString());
            }
            Debug.Log(myLocation3 + "2 Hours Has Passed");
        }
        else
        {
            Debug.Log(myLocation3 + "2 Hours Not Passed");
        }

        long temp6 = Convert.ToInt64(PlayerPrefs.GetString("sysString3"));

        DateTime oldDate3 = DateTime.FromBinary(temp6);

        oldDate3 = oldDate3.AddMinutes(1);

        differenceForMinQ3 = oldDate3.Subtract(sincePressedTime);

        if (differenceForMinQ3.Seconds < 1 && stopTimer3 == true)
        {
            loseButton2.GetComponent<Button>().interactable = true;
            canGetFree3 = true;
            stopTimer3 = false;
        }
        else if (differenceForMinQ3.Seconds > 1)
        {
            loseButton2.GetComponent<Button>().interactable = false;
        }

        //--------------Lose Menu---------------\\
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
            stopTimer = true;

            // button pressed, save and set press button time to buttonPressedTime
            PlayerPrefs.SetString(myLocation + "PressButtonTime", System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("sysString1", System.DateTime.Now.ToBinary().ToString());

            freeCurrency++;
            PlayerPrefs.SetInt(myLocation + "LoginTime", freeCurrency);

            // get free currency code go here

            Debug.Log("Get free");

            addValue = UnityEngine.Random.Range(1, 5);

            GameManager.instance.AddPoints(addValue);
            GameManager.instance.SavePoints();
            image1.SetActive(true);
        }
    }

    public void GetFreeOpalShopMenu()
    {
        if (canGetFree2 == true && freeCurrency2 < 3)
        {
            //=============================================================================================================
            NotificationManager.Cancel(65);
            TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
            // schedule without icon
            NotificationManager.Send(65, TimeSpan.FromHours(2), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

            canGetFree2 = false;
            stopTimer2 = true;

            // button pressed, save and set press button time to buttonPressedTime
            PlayerPrefs.SetString(myLocation2 + "PressButtonTime2", System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("sysString2", System.DateTime.Now.ToBinary().ToString());

            freeCurrency2++;
            PlayerPrefs.SetInt(myLocation2 + "LoginTime2", freeCurrency2);

            // get free currency code go here

            Debug.Log("Get free2");

            addValue = UnityEngine.Random.Range(1, 5);

            GameManager.instance.AddPoints(addValue);
            GameManager.instance.SavePoints();
            image2.SetActive(true);
        }
    }

    public void GetFreeOpalLoseMenu()
    {
        if (canGetFree3 == true && freeCurrency3 < 3)
        {
            //=============================================================================================================
            NotificationManager.Cancel(65);
            TimeSpan delayNotifyTime = new TimeSpan(2, 0, 0);
            // schedule without icon
            NotificationManager.Send(65, TimeSpan.FromHours(2), "💎FREE OPALS!!💎", "💰Collect Free Opals Now and SHOW OFF your skins!😎", Color.red, NotificationIcon.Heart);

            canGetFree3 = false;
            stopTimer3 = true;

            // button pressed, save and set press button time to buttonPressedTime
            PlayerPrefs.SetString(myLocation3 + "PressButtonTime3", System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("sysString3", System.DateTime.Now.ToBinary().ToString());

            freeCurrency3++;
            PlayerPrefs.SetInt(myLocation3 + "LoginTime3", freeCurrency3);

            // get free currency code go here

            Debug.Log("Get free2");

            addValue = UnityEngine.Random.Range(1, 5);

            GameManager.instance.AddPoints(addValue);
            GameManager.instance.SavePoints();
            image3.SetActive(true);
        }
    }

    public void RunOnce()
    {
        PlayerPrefs.SetInt("GetFreeCurrency9", (isFirst ? 1 : 0));
        mainButton2.SetActive(true);
        mainButton.SetActive(false);
    }

    public void RunOnce2()
    {
        PlayerPrefs.SetInt("GetFreeCurrency4", (isFirst2 ? 1 : 0));
        shopButton2.SetActive(true);
        shopButton.SetActive(false);
    }

    public void RunOnce3()
    {
        PlayerPrefs.SetInt("GetFreeCurrency5", (isFirst3 ? 1 : 0));
        loseButton2.SetActive(true);
        loseButton.SetActive(false);
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
