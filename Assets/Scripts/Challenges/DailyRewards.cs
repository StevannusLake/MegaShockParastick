using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Assets.SimpleAndroidNotifications;

public class DailyRewards : MonoBehaviour
{
    public GameObject[] greenTicks;
    private DateTime currentDate;
    private DateTime oldDate;
    private int numOfDay;
    public GameObject NotificationWindow;
    public Text notificationText;
    private Animator anim;
    public bool isShowed = false;
    public GameObject exclamationMark;

    public bool activateBlocker;
    public GameObject dailyRewardBlocker;

    // Start is called before the first frame update
    void Start()
    {   
        //CheckDate();
        //CheckGreenTick();
        anim = GetComponent<Animator>();

        activateBlocker = false;
        
    }

    private void Update()
    {
        CheckBlocker();

        long temp = Convert.ToInt64(PlayerPrefs.GetString("lastLogin"));
        oldDate = DateTime.FromBinary(temp);

        // find differene 
        TimeSpan difference = currentDate.Subtract(oldDate);
        if (difference.Days >= 1)
        {
            exclamationMark.SetActive(true);
        }
        else exclamationMark.SetActive(false);

        NotificationManager.Cancel(60);
        TimeSpan delayNotifyTime = new TimeSpan(24, 0, 0);
        // schedule without icon
        NotificationManager.Send(60, TimeSpan.FromDays(1), "🎁IT'S YOURS!🎁", "🤑Don't miss out your DAILY REWARD!!💸", Color.red, NotificationIcon.Heart);
    }

    void CheckBlocker()
    {
        if(activateBlocker)
        {
            dailyRewardBlocker.SetActive(true);
        }
        else
        {
            dailyRewardBlocker.SetActive(false);
        }
    }

    public void CheckDate()
    {
        if(PlayerPrefs.GetInt("LoginDay") == 0)
        {
            PlayerPrefs.SetInt("LoginDay", 1);
            numOfDay = PlayerPrefs.GetInt("LoginDay");
            GiveRewards(numOfDay);
            PlayerPrefs.SetString("lastLogin", System.DateTime.Now.ToBinary().ToString());
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
            //long temp = Convert.ToInt64(PlayerPrefs.GetString("lastLogin"));
            //oldDate = DateTime.FromBinary(temp);

            print("oldDate: " + oldDate);

            // find differene 
            TimeSpan difference = currentDate.Subtract(oldDate);
            print("Difference: " + difference);

            if (difference.Days >= 1 && difference.Days < 2)
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
                if (currentDate > oldDate)
                {
                    PlayerPrefs.SetString("lastLogin", System.DateTime.Now.ToBinary().ToString());

                }
                Debug.Log("One Day Has Passed");
            }
            else if(difference.Days >= 2)
            {
                numOfDay = 1;
                PlayerPrefs.SetInt("LoginDay", numOfDay);
                GiveRewards(numOfDay);
                if (currentDate > oldDate)
                {
                    PlayerPrefs.SetString("lastLogin", System.DateTime.Now.ToBinary().ToString());

                }
            }
        }
        
    }

    void GiveRewards(int day)
    {
        NotificationWindow.SetActive(true);
        if(day == 1)
        {
            GameManager.instance.AddCoin(5);
            GameManager.instance.totalCoinCollected += 5;
            notificationText.text = "You Received 10 Plasmas!";
        }
        else if(day == 2)
        {
            GameManager.instance.AddCoin(10);
            GameManager.instance.totalCoinCollected += 10;
            notificationText.text = "You Received 15 Plasmas!";
        }
        else if (day == 3)
        {
            GameManager.instance.AddCoin(15);
            GameManager.instance.totalCoinCollected += 15;
            notificationText.text = "You Received 30 Plasmas!";
        }
        else if (day == 4)
        {
            GameManager.instance.AddCoin(20);
            GameManager.instance.totalCoinCollected += 20;
            notificationText.text = "You Received 50 Plasmas!";
        }
        else if (day == 5)
        {
            GameManager.instance.AddCoin(25);
            GameManager.instance.totalCoinCollected += 25;
            notificationText.text = "You Received 75 Plasmas!";
        }
        else if (day == 6)
        {
            GameManager.instance.AddCoin(30);
            GameManager.instance.totalCoinCollected += 30;
            notificationText.text = "You Received 100 Plasmas!";
        }
        else if (day == 7)
        {
            GameManager.instance.AddSpin(2);
            notificationText.text = "You Received 2 Free Lucky Spins!";
        }
        GameManager.instance.SaveCoin();
        GameManager.instance.SaveSpin();
    }

    public void CheckGreenTick()
    {
        for(int i=0;i<numOfDay;i++)
        {
            greenTicks[i].SetActive(true);
        }
        isShowed = true;
    }

    public void CloseNotificationWindow()
    {
        NotificationWindow.SetActive(false);
    }

    public void ShowDailyWindow()
    {
        if (!isShowed)
        {
            anim.Play("DailyRewards");
            CheckDate();
            CheckGreenTick();

            activateBlocker = true;
        }
        else
        {
            anim.Play("DailyRewardsOff");
            isShowed = false;

            activateBlocker = false;
        }
            
    }

    public void SlideDailyRewardOff()
    {
        anim.Play("DailyRewardsButtonClose");
    }
}
