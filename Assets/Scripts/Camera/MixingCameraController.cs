using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MixingCameraController : MonoBehaviour
{
    public GameObject Target;
    public CinemachineMixingCamera mixingCamera;
    

    // Start is called before the first frame update
    void Start()
    {
       
        mixingCamera = GetComponent<CinemachineMixingCamera>();
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log(Target.GetComponent<Movement>().CalculateCameraZoom());      
        ChangeCameraZoom();
    }
    void ChangeCameraZoom()
    {
        float zoom = Target.GetComponent<Movement>().CalculateCameraZoom();
        if(zoom>=0) mixingCamera.m_Weight1 = zoom;
      

    }

   
}
