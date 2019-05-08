using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    public AndroidJavaClass unityPlayer;
    public AndroidJavaObject currentActivity;
    public AndroidJavaObject sysService;

    public void Vibrate()
    {
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        sysService = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    }
    
    public void VibrateOnly()
    {
        sysService.Call("vibrate");
    }
    
    public void vibrate()
    {
        sysService.Call("vibrate");
    }


    public void vibrate(long milliseconds)
    {
        sysService.Call("vibrate", milliseconds);
    }

    public void vibrate(long[] pattern, int repeat)
    {
        sysService.Call("vibrate", pattern, repeat);
    }


    public void cancel()
    {
        sysService.Call("cancel");
    }

    public bool hasVibrator()
    {
        return sysService.Call<bool>("hasVibrator");
    }

    #region Call Vibration Function

    /*
        Vibrate vibrate = new Vibrate();

        if (vibrate.hasVibrator())
        {
            //Vibrate
            vibrate.vibrate();

            //Vibrate for 500 milliseconds
            vibrate.vibrate(500);

            // Start without a delay
            // Vibrate for 200 milliseconds
            // Sleep for 2000 milliseconds
            long[] pattern = { 0, 200, 2000 };
            vibrate.vibrate(pattern, 0);

            //Cancel Vibration
            vibrate.cancel();
        }
    */
    #endregion
}
