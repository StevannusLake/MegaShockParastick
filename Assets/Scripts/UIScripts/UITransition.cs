using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        //if(uiManager.isMovingTo)
        //{
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 3f * Time.deltaTime);
        //    if(transform.localPosition == Vector3.zero)
        //    {
        //        uiManager.isMovingTo = false;
        //    }
        //}
        //else if(uiManager.isMovingBack)
        //{
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 3f * Time.deltaTime);
        //    if(transform.localPosition == startPos)
        //    {
        //        uiManager.isMovingBack = false;
        //    }
        //}
    }
}
