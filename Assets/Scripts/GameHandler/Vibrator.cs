//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Vibrator : MonoBehaviour
//{
//#if UNITY_ANDROID && !UNITY_EDITOR
//    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
//    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
//#else
//    public static AndroidJavaClass unityPlayer;
//    public static AndroidJavaObject currentActivity;
//    public static AndroidJavaObject vibrator;
//#endif

//    public static void Vibrate()
//    {
//        if (isAndroid())
//            vibrator.Call("vibrate");
//        else
//            Handheld.Vibrate();
//    }


//    public static void Vibrate(long milliseconds)
//    {
//        if (isAndroid())
//            vibrator.Call("vibrate", milliseconds);
//        //else
//        //    Handheld.Vibrate();
//    }

//    public static void Vibrate(long[] pattern, int repeat)
//    {
//        if (isAndroid())
//            vibrator.Call("vibrate", pattern, repeat);
//        //else
//        //    Handheld.Vibrate();
//    }

//    public static bool HasVibrator()
//    {
//        return isAndroid();
//    }

//    public static void Cancel()
//    {
//        if (isAndroid())
//            vibrator.Call("cancel");
//    }

//    private static bool isAndroid()
//    {
//#if UNITY_ANDROID && !UNITY_EDITOR
//	return true;
//#else
//        return false;
//#endif
//    }

//    #region Call Vibration Function

//    /*
//        Vibrate vibrate = new Vibrate();

//        if (vibrate.hasVibrator())
//        {
//            //Vibrate
//            vibrate.vibrate();

//            //Vibrate for 500 milliseconds
//            vibrate.vibrate(500);

//            // Start without a delay
//            // Vibrate for 200 milliseconds
//            // Sleep for 2000 milliseconds
//            long[] pattern = { 0, 200, 2000 };
//            vibrate.vibrate(pattern, 0);

//            //Cancel Vibration
//            vibrate.cancel();
//        }
//    */
//    #endregion
//}
