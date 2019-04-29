using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    Transform myTransform;
    public float offsetY;
    float defaultCameraX;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //FollowTarget();    
    }


    void FollowTarget()
    {
        if(Target.transform.position.y + offsetY > myTransform.position.y)
        {
            myTransform.position = new Vector3(myTransform.position.x, Target.transform.position.y + offsetY, myTransform.position.z);
        }
    }
}
