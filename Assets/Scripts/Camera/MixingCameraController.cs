using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MixingCameraController : MonoBehaviour
{
    public GameObject Target;
    public CinemachineMixingCamera mixingCamera;
    public bool isInsideZoomArea = false;
    public float[] previousCameraOrto;
    [Header("Amount to zoomout")]
    public int amountToZoomOut;

    // Start is called before the first frame update
    void Start()
    {  
        mixingCamera = GetComponent<CinemachineMixingCamera>();
        previousCameraOrto = new float[mixingCamera.ChildCameras.Length];
        CapturePrevCameraOrt();
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log(Target.GetComponent<Movement>().CalculateCameraZoom());      
        ChangeCameraZoom();
        if (isInsideZoomArea) EnteredTransitionArea();
        if (!isInsideZoomArea) ExitedTransitionArea();
    }
    void ChangeCameraZoom()
    {
        float zoom = Target.GetComponent<Movement>().CalculateCameraZoom();
        if(zoom>=0) mixingCamera.m_Weight1 = zoom;
      

    }

    public void EnteredTransitionArea()
    {
       
       
        for(int i=0; i<mixingCamera.ChildCameras.Length;i++)
        {
            mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize,
                  previousCameraOrto[i] + amountToZoomOut * GameManager.instance.player.GetComponent<ColliderController>().overlapColliderCounts, Time.deltaTime * 4f);
                

        }
    }

    public void CapturePrevCameraOrt()
    {
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            previousCameraOrto[i]= mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize ;


        }
    }
    public void ExitedTransitionArea()
    {


        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize,
                  previousCameraOrto[i], Time.deltaTime * 4f);


        }
    }

    public void StopFollowing()
    {
        mixingCamera.gameObject.SetActive(false);
       
    }
   
}
