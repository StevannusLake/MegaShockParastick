using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MixingCameraController : MonoBehaviour
{
    public GameObject Target;
    public CinemachineMixingCamera mixingCamera;
    public bool isInsideZoomArea = false;
    public bool isShaked = false;
   
    private float shakeDuration=0;
    private float shakeTimer = 0;
    public float[] previousCameraOrto;
    public float prevousCameraOffset;
    public GameObject currentActiveLayout;
    [Header("Amount to zoomout")]
    public int amountToZoomOut;

    public bool shouldGoToLeft = false;
    public bool shouldGoToRight = false;
    public bool shouldGoToDefaultOffset = false;

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
         
        
        if (isShaked) StopShake();


        if (shouldGoToLeft) SlowlyOffsetToLeft();
        if (shouldGoToRight) SlowlyOffsetToRight();
        if (shouldGoToDefaultOffset) PositionOnDefaultCameraOffset();

        
    }

    private void FixedUpdate()
    {
        if (!isShaked) isDraggingShake();
        
    }
    void ChangeCameraZoom()
    {
        float zoom = Target.GetComponent<Movement>().CalculateCameraZoom();
        if(zoom>=0) mixingCamera.m_Weight1 = zoom;
      

    }
   
    public void ShakeCamera(float shakeAmplitude, float shakeFrequency, float duration)
    {
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            CinemachineBasicMultiChannelPerlin noiseChannel = mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noiseChannel.m_AmplitudeGain = shakeAmplitude;
            noiseChannel.m_FrequencyGain = shakeFrequency;
        }
        isShaked = true;
        shakeDuration = duration;
    }

    public void isDraggingShake()
    {
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            float shakeAmplitude = Target.GetComponent<Movement>().CalculateCameraAmplitude();
            float shakeFrequency = Target.GetComponent<Movement>().CalculateCameraFrequency();
            CinemachineBasicMultiChannelPerlin noiseChannel = mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noiseChannel.m_AmplitudeGain = shakeAmplitude;
            noiseChannel.m_FrequencyGain = shakeFrequency;
        }
        
    }

    public void StopShake()
    {
        shakeTimer += Time.deltaTime;
        if(shakeTimer>=shakeDuration)
        {
            for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
            {
                CinemachineBasicMultiChannelPerlin noiseChannel = mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noiseChannel.m_AmplitudeGain = Mathf.MoveTowards(noiseChannel.m_AmplitudeGain, 0, Time.fixedTime*2f);
                noiseChannel.m_FrequencyGain = Mathf.MoveTowards(noiseChannel.m_FrequencyGain, 0, Time.fixedTime * 2f);

                if (noiseChannel.m_AmplitudeGain == 0 &&
                noiseChannel.m_FrequencyGain == 0) isShaked = false;
                
            }
            shakeTimer = 0;
        }

    }
    
    
   
    public void SlowlyOffsetToRight()
    {
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition, prevousCameraOffset + 3f,Time.deltaTime* 2.3f);
            if (mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition== prevousCameraOffset + 5f)
            {
                shouldGoToRight = false;
            }
        }
    }

    public void SlowlyOffsetToLeft()
    {
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition, prevousCameraOffset - 3f, Time.deltaTime * 2.3f);
            if (mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition == prevousCameraOffset - 5f)
            {
                shouldGoToLeft = false;
            }
        }
    }


    public void PositionOnDefaultCameraOffset()
    {
       
        float defaultOffset = currentActiveLayout.GetComponentInChildren<LevelGenerator>().defaultOffset.position.x;
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            
            mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition,
                defaultOffset, Time.deltaTime * 2.5f);
            if (mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition == defaultOffset)
            {
                shouldGoToDefaultOffset = false;
            }
        }
    }

    public void CapturePrevOffset()
    {
        prevousCameraOffset = mixingCamera.ChildCameras[0].GetComponent<LockCameraX>().m_XPosition;
    }

    public void GetCurrentActiveLayout()
    {
        currentActiveLayout = LevelHandler.instance.layoutPlayerIsIn;
    }

    public void EnteredTransitionArea()
    {
       
       
        for(int i=0; i<mixingCamera.ChildCameras.Length;i++)
        {
            mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize,
                  previousCameraOrto[i] + amountToZoomOut * GameManager.instance.player.GetComponentInChildren<ZoomRadiusController>().overlapColliderCounts, Time.deltaTime * 4f);
                

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
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {          
            mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Follow = null;
        }
        // mixingCamera.gameObject.SetActive(false);

    }
   
   
}
