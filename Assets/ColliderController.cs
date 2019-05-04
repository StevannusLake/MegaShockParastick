using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ColliderController : MonoBehaviour
{
    public bool isInsideZoomOut = false;
    private string[] zoomReqList;
    public int overlapColliderCounts=0;
    private Movement movementScript;
    private GetSideHit getSideHit;
    public List<string> overColliderList;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<Movement>();
        getSideHit = GetComponent<GetSideHit>();
        overColliderList = new List<string>();
        zoomReqList = new string[] { "LeftLayout", "RightLayout" };
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
        }

        if(other.CompareTag("Opal") && Movement.deadState == 0)
        {
            GameManager.instance.AddPoints(1);
            Destroy(other.gameObject);
        }
    

        if (other.CompareTag("ZoomIn") )
        {
            if(!isInsideZoomOut)
            {
                if (!other.gameObject.GetComponent<ZoomController>().isAlreadyActivated)
                {
                    LevelHandler.instance.cameraController.isInsideZoomArea = false;
                    other.gameObject.GetComponent<ZoomController>().isAlreadyActivated = true;

                }
            }
           
    
        }
        if (other.CompareTag("ZoomOut") )
        {
            overColliderList.Add(other.transform.parent.gameObject.tag);
            
            if (!other.gameObject.GetComponent<ZoomController>().isAlreadyActivated)
            {         
                LevelHandler.instance.cameraController.isInsideZoomArea = true;
                other.gameObject.GetComponent<ZoomController>().isAlreadyActivated = true;
                
            }

            CheckForZoomOutMultiplier();

        }
        if (other.CompareTag("EnterGenerator"))
        {
            if (!other.gameObject.GetComponent<EnterController>().isAlreadyActivated)
            {
                if (other.gameObject.transform.parent.GetComponentInChildren<LevelGenerator>().levelGeneratorID == 0)
                {
                    LevelGenerator levelGenerator = other.transform.parent.GetComponentInChildren<LevelGenerator>();
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



        }
        if (other.CompareTag("RightMovementOffset"))
        {
            if(getSideHit.ReturnDirection(this.gameObject,other.gameObject)==HitDirection.Left)
            {
                //
            }
            
        }



    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ZoomOut")) isInsideZoomOut = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("ZoomOut"))
        {

            isInsideZoomOut = false;
            overColliderList.Remove(overColliderList.Last());
            CheckForZoomOutMultiplier();
        }



    }
    void CheckForZoomOutMultiplier()
    {
        if (overColliderList.Contains(zoomReqList[1]) && overColliderList.Contains(zoomReqList[0]))
        {
            overlapColliderCounts = 2;
        }
        else overlapColliderCounts = 1;

    }

    
}
