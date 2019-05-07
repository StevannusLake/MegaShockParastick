using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfaces : MonoBehaviour
{
    public enum SurfaceTypes {Safe,Dangerous,Moving}
    public SurfaceTypes thisType;
    public float rotationSpeed;
    public float pingpongSpeed;
    Transform myTransform;
    CapsuleCollider2D myCollider;
    Rigidbody2D myRigidbody;
    static float zRotation;
    private int rotationSpeedRandom;
    public int stickCount = 0;
    string nSurfaceTag = "NSurface";
    private Transform[] pingpongObjects;
    public Transform platformPlacementTransform;


    public bool alreadyRespawnedCoin = false;
       

    public bool OnRotation = false;

    // Start is called before the first frame update

    private void Awake()
    {
       
    }
    void Start()
    {
        rotationSpeedRandom = Random.Range(20, 40);
        myTransform = GetComponent<Transform>();
        if (thisType == SurfaceTypes.Moving) FindPingPongObjects();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnRotation)
        {
            Rotation();
        }
        
        DropAfter();
        if (thisType == SurfaceTypes.Moving) MoveBetweenPingPongs();
    }

    void FindPingPongObjects()
    {
        pingpongObjects = new Transform[2];
        pingpongObjects[0] = platformPlacementTransform.Find("PingPong1");
        pingpongObjects[1] = platformPlacementTransform.Find("PingPong2");

    }

    void MoveBetweenPingPongs()
    {
        float pingPong = Mathf.PingPong(Time.time * pingpongSpeed, 1);
        transform.position = Vector3.Lerp(pingpongObjects[0].position, pingpongObjects[1].position, pingPong);
    }

    // rotation
    void Rotation()
    {
        if(zRotation >= 360)
        {
            zRotation = 0;
        }

        transform.Rotate(Vector3.forward);

      
    }

    void DropAfter()
    {
        if(myCollider == null)
        {
            myCollider = GetComponent<CapsuleCollider2D>();
        }
        if (myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }

        if(myCollider != null && myRigidbody != null)
        {
            if (stickCount >= 2)
            {
                myCollider.isTrigger = true;
                myRigidbody.isKinematic = false;
                Destroy(this.gameObject, 2f);
            }
        }
    }
}
