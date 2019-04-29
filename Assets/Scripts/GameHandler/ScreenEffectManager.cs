using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShakeType { CameraPosition, CameraAnimation }
public class ScreenEffectManager : MonoBehaviour
{
    public static ScreenEffectManager instance;
    public ShakeType shakeType = ShakeType.CameraPosition;
    Vector3 cameraInitialPosition;
    public float shakeMagnitude = 0.01f, shakeTime = 0.3f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
   
    //Test for screen shake
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 50, 100), "Test Camera Shake"))
        {
            ShakeIt(ShakeType.CameraPosition);
        }

    }

    //  CAMERA SHAKE
    public void ShakeIt(ShakeType type) //Use to shake camera from other scripts
    {
        switch(shakeType)
        {
            case ShakeType.CameraPosition:
                cameraInitialPosition = Camera.main.transform.position;
                InvokeRepeating("StartCameraShaking", 0f, 0.005f);
                Invoke("StopCameraShaking", shakeTime);
                break;
            case ShakeType.CameraAnimation:
                //

                break;
        }    
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = UnityEngine.Random.value * shakeMagnitude * 2.0f- shakeMagnitude;
        float cameraShakingOffsetY = UnityEngine.Random.value * shakeMagnitude * 2.0f - shakeMagnitude;
        Vector3 cameraIntermadiatePosition = Camera.main.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        Camera.main.transform.position = cameraIntermadiatePosition;
    }
    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        Camera.main.transform.position = cameraInitialPosition;
    }


    
    

}
