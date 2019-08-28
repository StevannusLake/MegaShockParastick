using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleAndroidNotifications;
using System;

public class InternetChecker : MonoBehaviour
{
    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2.0f;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public bool isConnect = false;
    private Ping ping;
    private float pingStartTime;
    public Text timerText;
    public Text timerText2;
    public Text timerText3;

    public void Start()
    {
        bool internetPossiblyAvailable;
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = allowCarrierDataNetwork;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
    }

    public void Update()
    {
        if (ping != null)
        {
            bool stopCheck = true;
            if (ping.isDone)
            {
                if (ping.time >= 0)
                    InternetAvailable();
                else
                    InternetIsNotAvailable();
            }
            else if (Time.time - pingStartTime < waitingTime)
                stopCheck = false;
            else
                InternetIsNotAvailable();
            if (stopCheck)
                ping = null;
        }

        NotificationManager.Cancel(62);
        TimeSpan delayNotifyTime = new TimeSpan(5, 0, 0);
        // schedule without icon
        NotificationManager.Send(62, TimeSpan.FromHours(5), "🏁FAR FAR AWAY🏁", "🎯Reach so far till nobody beat you!🥇", Color.red, NotificationIcon.Heart);
    }

    private void InternetIsNotAvailable()
    {
        Debug.Log("No Internet :(");
        button1.GetComponent<Button>().interactable = false;
        button2.GetComponent<Button>().interactable = false;
        button3.GetComponent<Button>().interactable = false;
        isConnect = false;
        timerText.GetComponent<Text>().text = "OFFLINE";
        timerText2.GetComponent<Text>().text = "OFFLINE";
        timerText3.GetComponent<Text>().text = "OFFLINE";
    }

    private void InternetAvailable()
    {
        Debug.Log("Internet is available! ;)");
        isConnect = true;
    }
}
