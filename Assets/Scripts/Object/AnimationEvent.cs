using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public bool canDrag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDrag()
    {
        canDrag = true;
    }

    public void StopDrag()
    {
        canDrag = false;
    }
}
