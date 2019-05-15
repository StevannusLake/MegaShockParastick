using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShakeVariation {HittingWall,Dying }
public class ScreenEffectManager : MonoBehaviour
{
    public static ScreenEffectManager instance;  
    Vector3 cameraInitialPosition;
    public MixingCameraController cameraController;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
   
    //Test for screen shake
   
   public void ShakeCamera(ShakeVariation variation)
    {

        switch(variation)
        {
            case ShakeVariation.HittingWall:
                {
                    cameraController.ShakeCamera(1.2f, 1.2f, 0.1f);
                    break;
                }

            case ShakeVariation.Dying:
                {
                    cameraController.ShakeCamera(3, 3f, 0.5f);
                    break;
                }

        }
    }


    
    //  CAMERA SHAKE
   


    
    

}
