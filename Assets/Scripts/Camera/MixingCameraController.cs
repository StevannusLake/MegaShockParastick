using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public enum CameraFollowingState { NORMAL, ONMOVINGPLATFORM };
public class MixingCameraController : MonoBehaviour
{
   
    public GameObject Target;
    public CinemachineMixingCamera mixingCamera;
    public bool isInsideZoomArea = false;
    public bool isShaked = false;
    public CinemachineVirtualCamera backgroundCamera;
    private float shakeDuration=0;
    private float shakeTimer = 0;
    public float[] previousCameraOrto;
    public float prevousCameraOffset;
    public GameObject currentActiveLayout;
    public GameObject currentSurface;
    [Header("Amount to zoomout")]
    public float amountToZoomOut;

    public bool shouldGoToLeft = false;
    public bool shouldGoToRight = false;
    public bool shouldGoToDefaultOffset = false;
    public CameraFollowingState cameraState = CameraFollowingState.NORMAL;

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
        WaterClosingShake();
        switch (cameraState)
        {
            case CameraFollowingState.NORMAL:
                if (shouldGoToLeft) SlowlyOffsetToLeft();
                if (shouldGoToRight) SlowlyOffsetToRight();
                if (shouldGoToDefaultOffset) PositionOnDefaultCameraOffset();
               
                break;
            case CameraFollowingState.ONMOVINGPLATFORM:
                MoveWithMovingPlatform();
                break;

        }
        

    }

    private void FixedUpdate()
    {
         
        
    }
    void ChangeCameraZoom()
    {
        float zoom = GameManager.instance.playerMovement.CalculateCameraZoom();
        if(zoom>=0) mixingCamera.m_Weight1 = zoom;
      

    }

    private void OnDisable()
    {
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
         {
            if (Target != null)
            {
                float shakeAmplitude = Target.GetComponent<Movement>().CalculateCameraAmplitude();
                float shakeFrequency = Target.GetComponent<Movement>().CalculateCameraFrequency();
                CinemachineBasicMultiChannelPerlin noiseChannel = mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noiseChannel.m_AmplitudeGain = 0;
                noiseChannel.m_FrequencyGain = 0;
            }
         }
        
    }

    void MoveWithMovingPlatform()
    {
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            float speedOfMoving = Time.deltaTime * 1.5f;
            if (GameManager.instance.playerMovement.spawnDot) speedOfMoving = Time.deltaTime * 0.5f;
            mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition, currentSurface.transform.position.x, speedOfMoving);
           
        }
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

    public void WaterClosingShake()
    {
        float distanceTillPlayer = Vector2.Distance(GameManager.instance.player.transform.position, GameManager.instance.water.transform.position);
        float maxAmplitude = 1.5f;
        float maxFrequencty = 1.0f;
        if (distanceTillPlayer < 10 && Movement.deadState==0)
        {
            
            for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
            {
                float shakeAmplitude = Target.GetComponent<Movement>().CalculateCameraAmplitude();
                float shakeFrequency = Target.GetComponent<Movement>().CalculateCameraFrequency();
                CinemachineBasicMultiChannelPerlin noiseChannel = mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noiseChannel.m_AmplitudeGain = Mathf.MoveTowards(noiseChannel.m_AmplitudeGain, maxAmplitude / distanceTillPlayer, Time.deltaTime * 2f);
                noiseChannel.m_FrequencyGain = Mathf.MoveTowards(noiseChannel.m_FrequencyGain, 1.0f / distanceTillPlayer, Time.deltaTime*2f);
                noiseChannel.m_AmplitudeGain = Mathf.Clamp(noiseChannel.m_AmplitudeGain,0, 1.2f);
                noiseChannel.m_FrequencyGain = Mathf.Clamp(noiseChannel.m_FrequencyGain, 0, 1.2f);
            }
            
        /*    CinemachineBasicMultiChannelPerlin noiseChannelBack = backgroundCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noiseChannelBack.m_AmplitudeGain = Mathf.MoveTowards(noiseChannelBack.m_AmplitudeGain, maxAmplitude / distanceTillPlayer , Time.deltaTime * 2f);
            noiseChannelBack.m_FrequencyGain = Mathf.MoveTowards(noiseChannelBack.m_FrequencyGain, maxFrequencty / distanceTillPlayer, Time.deltaTime * 2f);*/
        }
        else
        {
            
                for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
                {
                    float shakeAmplitude = Target.GetComponent<Movement>().CalculateCameraAmplitude();
                    float shakeFrequency = Target.GetComponent<Movement>().CalculateCameraFrequency();
                    CinemachineBasicMultiChannelPerlin noiseChannel = mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    noiseChannel.m_AmplitudeGain = Mathf.MoveTowards(noiseChannel.m_AmplitudeGain, 0, Time.deltaTime * 2f);
                    noiseChannel.m_FrequencyGain = Mathf.MoveTowards(noiseChannel.m_FrequencyGain, 0, Time.deltaTime * 2f);
                    noiseChannel.m_AmplitudeGain = Mathf.Clamp(noiseChannel.m_AmplitudeGain, 0, maxAmplitude);
                    noiseChannel.m_FrequencyGain = Mathf.Clamp(noiseChannel.m_FrequencyGain, 0, maxFrequencty);
            }
           /* CinemachineBasicMultiChannelPerlin noiseChannelBack = backgroundCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noiseChannelBack.m_AmplitudeGain = Mathf.MoveTowards(noiseChannelBack.m_AmplitudeGain, 0, Time.deltaTime * 2f);
            noiseChannelBack.m_FrequencyGain = Mathf.MoveTowards(noiseChannelBack.m_FrequencyGain,0, Time.deltaTime * 2f);*/

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
            float speedOfMoving = Time.deltaTime * 3.5f;
            if (GameManager.instance.playerMovement.spawnDot) speedOfMoving = Time.deltaTime * 1.5f;
            mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition, prevousCameraOffset + 3.42f, speedOfMoving);
            if (mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition== prevousCameraOffset + 4.8f)
            {
                shouldGoToRight = false;
            }
        }
    }

    public void SlowlyOffsetToLeft()
    {
        
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            float speedOfMoving = Time.deltaTime * 3.5f;
            if(GameManager.instance.playerMovement.spawnDot)  speedOfMoving = Time.deltaTime * 1.5f;
            mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition, prevousCameraOffset - 3.5f, speedOfMoving);
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
            float speedOfMoving = Time.deltaTime * 4.5f;
            if (GameManager.instance.playerMovement.spawnDot) speedOfMoving = Time.deltaTime * 1.5f;
            mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<LockCameraX>().m_XPosition,
                defaultOffset, speedOfMoving);
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
            float speedOfMoving = Time.deltaTime * 4f;
            if (GameManager.instance.playerMovement.spawnDot) speedOfMoving = Time.deltaTime * 1.8f;
            mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize,
                  previousCameraOrto[i] + amountToZoomOut * GameManager.instance.player.GetComponentInChildren<ZoomRadiusController>().overlapColliderCounts, speedOfMoving);
                

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
            float speedOfMoving = Time.deltaTime * 4f;
            if (GameManager.instance.playerMovement.spawnDot) speedOfMoving = Time.deltaTime * 1.8f;
            mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.MoveTowards(mixingCamera.ChildCameras[i].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize,
                  previousCameraOrto[i], speedOfMoving);


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
