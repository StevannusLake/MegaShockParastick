using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZoomRadiusController : MonoBehaviour
{
    public List<string> overColliderList;
    private string[] zoomReqList;
    public int overlapColliderCounts = 0;

    private void Start()
    {
        overColliderList = new List<string>();
        zoomReqList = new string[] { "LeftLayout", "RightLayout" };
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ZoomOut"))
        {
            overColliderList.Add(other.transform.parent.gameObject.tag);
          CheckForZoomOutMultiplier();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("ZoomOut"))
        {

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
