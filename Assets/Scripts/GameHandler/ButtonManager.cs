﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Assets.SimpleAndroidNotifications;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public bool secondlife = false;
    public bool tryagain = false;
    public float TempScore;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public void Start()
    { 
        // Get boolean using PlayerPrefs
        secondlife = PlayerPrefs.GetInt("SecondLife") == 1 ? true : false;
        tryagain = PlayerPrefs.GetInt("TryAgain") == 1 ? true : false;

        if(tryagain)
        {
            AudioManager.StopSound(AudioManager.Sound.MainMenuBGM);
            AudioManager.StopSound(AudioManager.Sound.LoseMenuBGM);

            UIManager.Instance.CloseMainMenu();

            tryagain = false;
            // Save boolean using PlayerPrefs
            PlayerPrefs.SetInt("TryAgain", tryagain ? 1 : 0);

            secondlife = false;
            // Save boolean using PlayerPrefs
            PlayerPrefs.SetInt("SecondLife", secondlife ? 1 : 0);
        }

        if(secondlife)
        {
            AudioManager.StopSound(AudioManager.Sound.MainMenuBGM);

            UIManager.Instance.CloseMainMenu();

            secondlife = false;
            // Save boolean using PlayerPrefs
            PlayerPrefs.SetInt("SecondLife", secondlife ? 1 : 0);
        }

        TempScore = PlayerPrefs.GetFloat("TempScore", TempScore);

    }

    private void Update()
    {
        //CheckMainMenu();

        NotificationManager.Cancel(62);
        TimeSpan delayNotifyTime2 = new TimeSpan(5, 0, 0);
        NotificationManager.Send(62, delayNotifyTime2, "🏁FAR FAR AWAY🏁", "🎯Reach so far away until nobody can beat you!🥇", Color.red, NotificationIcon.Heart);
    }

    public void TryAgain()
    {
        PostRestartDataHolder.instance.secondLifeUsed = false;

        tryagain = true;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TryAgain", tryagain ? 1 : 0);

        PlayerPrefs.SetFloat("TempScore", 0);

        UIManager.Instance.secondChanceCalled = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondChanceCalled", UIManager.Instance.secondChanceCalled ? 1 : 0);
        GameObject dataHolder = GameObject.Find("PostRestartDataHolder");
        Destroy(dataHolder);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WatchVideo()
    {
        GameManager.instance.AddCoin(20);
    }

    public void MainMenu()
    {
        PostRestartDataHolder.instance.secondLifeUsed = false;

        secondlife = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondLife", secondlife ? 1 : 0);

        tryagain = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TryAgain", tryagain ? 1 : 0);

        UIManager.Instance.secondChanceCalled = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondChanceCalled", UIManager.Instance.secondChanceCalled ? 1 : 0);

        //GameManager.instance.uiManager.GarageTransition.SetActive(true);
       // GameManager.instance.uiManager.GarageAnim.SetBool("OpenGarage", false);

        //Invoke("ReloadScene", 0.2f);
        ReloadScene();
    }

    void CheckMainMenu()
    {
        if(UIManager.Instance.LoseMenu.activeSelf)
        {
            PlayerPrefs.SetFloat("TempScore", 0);
            TempScore = PlayerPrefs.GetFloat("TempScore", TempScore);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
