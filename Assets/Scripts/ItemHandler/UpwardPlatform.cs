using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlatformType {Normal,Zigzag,Type3 }
public class UpwardPlatform : MonoBehaviour
{
    public PlatformType platformType = PlatformType.Normal;
    public float speed=2.0f;
    private float widthOfGameObject;
    public bool playerAttached = false;
    private void Start()
    {
        widthOfGameObject=GetComponent<Renderer>().bounds.size.y;
    }
    void Update()
    {
        MoveUpward(platformType);
        //temperory elimination after leaving screen
        DestoryOnLeave();
        //elimination after player attach
        if (playerAttached) DestoryAfterPlayerAttach();
    }

    private void MoveUpward(PlatformType type)
    {
        switch(type)
        {
            case PlatformType.Normal:
                     transform.position += Vector3.up * Time.deltaTime * speed;
                break;
            case PlatformType.Zigzag:
                transform.position = new Vector2(Mathf.PingPong(Time.time * 2.5f, 1), transform.position.y + speed * Time.deltaTime);
                break;
        }
       
    }
    private void DestoryOnLeave()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y > min.y+ widthOfGameObject)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        int randomType = Random.Range(0, 2);
        if (randomType == 0) platformType = PlatformType.Normal;
        else if(randomType==1) platformType = PlatformType.Zigzag;
    }
    private void DestoryAfterPlayerAttach()
    {

    }
}
