using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ColliderController : MonoBehaviour
{
    public bool isInsideZoomOut = false;
    
    private Movement movementScript;
    private GetSideHit getSideHit;
    
    public PhysicsMaterial2D bounceMaterial;

    public static int tempCollectedCoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<Movement>();
        getSideHit = GetComponent<GetSideHit>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Coin") && Movement.deadState == 0)
        {
            AudioManager.PlaySound(AudioManager.Sound.CollectCoin);
            AudioManager.PlaySound(AudioManager.Sound.CollectCoinMain);

            GameManager.instance.AddCoin(1);
            Destroy(other.gameObject);

            //! For Coin Multiplier
            tempCollectedCoin += 2;
        }

        if (other.CompareTag("Opal") && Movement.deadState == 0)
        {
            GameManager.instance.AddPoints(1);
            Destroy(other.gameObject);
        }


        if (other.CompareTag("ZoomIn"))
        {
            if (!isInsideZoomOut)
            {
                if (!other.gameObject.GetComponent<ZoomController>().isAlreadyActivated)
                {
                    LevelHandler.instance.cameraController.isInsideZoomArea = false;
                    other.gameObject.GetComponent<ZoomController>().isAlreadyActivated = true;

                }
            }


        }
        if (other.CompareTag("ZoomOut"))
        {
           

            if (!other.gameObject.GetComponent<ZoomController>().isAlreadyActivated)
            {
                LevelHandler.instance.cameraController.isInsideZoomArea = true;
                other.gameObject.GetComponent<ZoomController>().isAlreadyActivated = true;

            }

            

        }
        if (other.CompareTag("EnterGenerator"))
        {
            if (!other.gameObject.GetComponent<EnterController>().isAlreadyActivated)
            {
                if (other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID == 0)
                {
                    LevelGenerator levelGenerator = other.transform.parent.GetComponentInChildren<LevelGenerator>();
                    levelGenerator.Initialize();
                    levelGenerator.PostInitialize();
                    levelGenerator.GenerateMapOnTop(false);
                    other.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                }
                else if (other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID != 0 && !other.gameObject.transform.parent.GetComponentInChildren<EnterController>().isAlreadyActivated)
                {
                    EnterController LastLayoutEnter = LevelHandler.instance.levelLayoutsCreated.Last().GetComponentInChildren<EnterController>();
                    LevelGenerator LastLayoutGenerator = LevelHandler.instance.levelLayoutsCreated.Last().GetComponentInChildren<LevelGenerator>();
                    LastLayoutEnter.isAlreadyActivated = false;
                    LastLayoutGenerator.GenerateMapOnTop(true);
                    LastLayoutEnter.isAlreadyActivated = true;
                    other.GetComponentInChildren<EnterController>().isAlreadyActivated = true;
                }
                
            }
            LevelHandler.instance.layoutPlayerIsIn = other.gameObject.transform.parent.parent.gameObject;


        }


        if (other.CompareTag("RightMovementOffset"))
        {
            #region CustomizationForMovingCamera
            GameObject prevousLayoutBeforeThis = LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID - 1];

            if (prevousLayoutBeforeThis.tag == "LeftLayout"
                && other.gameObject.transform.parent.parent.tag == "RightLayout" &&
                LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID - 1].transform.parent.Find("OffsetCameraArea").GetComponent<ZoomController>().isAlreadyActivated == true)
            {
                Debug.Log("Return Happend");
                return;
               
            }
           
            if (prevousLayoutBeforeThis.tag == "RightLayout"
                && other.gameObject.transform.parent.parent.tag == "LeftLayout" &&
                LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID - 1].transform.parent.Find("OffsetCameraArea").GetComponent<ZoomController>().isAlreadyActivated == true)
            {
                Debug.Log("Return Happend");
                return;

            }
            #endregion

            if (!other.GetComponent<ZoomController>().isAlreadyActivated )
                {
                    other.GetComponent<ZoomController>().isAlreadyActivated = true;
                    LevelHandler.instance.cameraController.CapturePrevOffset();
                    LevelHandler.instance.cameraController.shouldGoToRight = true;
                }
                
            

        }
        /////// DefaultOffset
        if (other.CompareTag("LeftMovementOffset"))
        {
            #region CustomizationForMovingCamera
            GameObject prevousLayoutBeforeThis = LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID - 1];

            if (prevousLayoutBeforeThis.tag == "LeftLayout"
                && other.gameObject.transform.parent.parent.tag == "RightLayout" &&
                LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID - 1].transform.parent.Find("OffsetCameraArea").GetComponent<ZoomController>().isAlreadyActivated == true) return;
            if (prevousLayoutBeforeThis.tag == "RightLayout"
                && other.gameObject.transform.parent.parent.tag == "LeftLayout" &&
                LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID - 1].transform.parent.Find("OffsetCameraArea").GetComponent<ZoomController>().isAlreadyActivated == true) return;

            #endregion


            if (!other.GetComponent<ZoomController>().isAlreadyActivated)
            {
                other.GetComponent<ZoomController>().isAlreadyActivated = true;
                LevelHandler.instance.cameraController.CapturePrevOffset();
                LevelHandler.instance.cameraController.shouldGoToLeft = true;

            }

        }
    }

   
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ZoomOut")) isInsideZoomOut = true;


        //

    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("ZoomOut"))
        {

            isInsideZoomOut = false;
            
        }



        if (other.CompareTag("RightMovementOffset"))
        {
            if (LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID+1].tag == "RightLayout" 
                && other.gameObject.transform.parent.parent.tag == "LeftLayout") return;
             if (LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID+1].tag == "LeftLayout" 
                && other.gameObject.transform.parent.parent.tag == "RightLayout") return;

            if (this.transform.position.x- other.transform.position.x >0 )
            {
                LevelHandler.instance.cameraController.GetCurrentActiveLayout();
                LevelHandler.instance.cameraController.shouldGoToDefaultOffset = true;
                LevelHandler.instance.cameraController.shouldGoToLeft = false;
                LevelHandler.instance.cameraController.shouldGoToRight = false;
            }
          

        }
        /////// DefaultOffset
        if (other.CompareTag("LeftMovementOffset"))
        {
            if (LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID+1].tag == "RightLayout" 
                && other.gameObject.transform.parent.parent.tag == "LeftLayout") return;
            if (LevelHandler.instance.levelLayoutsCreated[other.gameObject.transform.parent.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID + 1].tag == "LeftLayout"
                && other.gameObject.transform.parent.parent.tag == "RightLayout") return;
            
               // if (getSideHit.ReturnDirection(this.gameObject, other.gameObject) == HitDirection.Left)
               if(this.transform.position.x-other.transform.position.x <0)
                {
                    LevelHandler.instance.cameraController.GetCurrentActiveLayout();
                    LevelHandler.instance.cameraController.shouldGoToDefaultOffset = true;
                    LevelHandler.instance.cameraController.shouldGoToLeft = false;
                    LevelHandler.instance.cameraController.shouldGoToRight = false;
                }
            
          
           

        }



    }
   
    
}
