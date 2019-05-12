using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Surfaces : MonoBehaviour
{
    public enum SurfaceTypes {Safe,Dangerous,Moving}
    private enum PingPongDirection {Forward,Back }
    private PingPongDirection pingpongDirection = PingPongDirection.Forward;
    public SurfaceTypes thisType;
    public float rotationSpeed;
    public float pingpongSpeed;
    Transform myTransform;
    CapsuleCollider2D myCollider;
    Rigidbody2D myRigidbody;
    static float zRotation;
    public float rotationSpeedRandom;
    public int stickCount = 0;
    private bool reachedDestination = false;
    private Transform destination;
    string nSurfaceTag = "NSurface";
    private Transform[] pingpongObjects;
    public Transform platformPlacementTransform;

    GameObject player;
    CircleCollider2D playerCollider;
    
    public bool alreadyRespawnedCoin = false;
    private bool foundDestination = false;
    public bool OnRotation = false;

    private Animator anim;

    // Start is called before the first frame update

    private void Awake()
    {
       
    }
    private void OnEnable()
    {
        rotationSpeedRandom = Random.Range(90, 150);
    }
    void Start()
    {
       
        myTransform = GetComponent<Transform>();
        if (thisType == SurfaceTypes.Moving) FindPingPongObjects();

        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<CircleCollider2D>();

        anim = GetComponent<Animator>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (OnRotation)
        {
            Rotation();
        }
        
        DropAfter();
        if (thisType == SurfaceTypes.Moving && foundDestination) MoveBetweenPingPongs();
    }

    void FindPingPongObjects()
    {
        pingpongObjects = new Transform[platformPlacementTransform.childCount];
        if(platformPlacementTransform.childCount==2)
        {
            pingpongObjects[0] = platformPlacementTransform.Find("PingPong1");
            pingpongObjects[1] = platformPlacementTransform.Find("PingPong2");
            destination = pingpongObjects[1];
            foundDestination = true;
        }
        if (platformPlacementTransform.childCount == 3)
        {
            pingpongObjects[0] = platformPlacementTransform.Find("PingPong1");
            pingpongObjects[1] = platformPlacementTransform.Find("PingPong2");
            pingpongObjects[2] = platformPlacementTransform.Find("PingPong3");
            destination = pingpongObjects[1];
            foundDestination = true;
        }
        
       
        

    }

    void MoveBetweenPingPongs()
    {
        if(alreadyRespawnedCoin)
        {
            if (transform.parent.position != destination.position)
            {
                 transform.parent.position = Vector3.MoveTowards(transform.parent.position, destination.position, Time.deltaTime * 1.2f);


            }
            if (transform.parent.position == destination.position)
            {
                if (System.Array.IndexOf(pingpongObjects, destination) == System.Array.IndexOf(pingpongObjects, pingpongObjects.Last()))
                {
                    pingpongDirection = PingPongDirection.Back;
                    destination = pingpongObjects[DestinationCurrentIndex(destination) - 1];
                }
                else if (System.Array.IndexOf(pingpongObjects, destination) == System.Array.IndexOf(pingpongObjects, pingpongObjects.First()))
                {
                    pingpongDirection = PingPongDirection.Forward;

                    destination = pingpongObjects[DestinationCurrentIndex(destination) + 1];
                }
                else
                {
                    if (pingpongDirection == PingPongDirection.Forward)
                    {
                        destination = pingpongObjects[DestinationCurrentIndex(destination) + 1];
                    }
                    else if (pingpongDirection == PingPongDirection.Back)
                    {
                        destination = pingpongObjects[DestinationCurrentIndex(destination) - 1];
                    }
                }
            }

        }
         if(!alreadyRespawnedCoin)
        {

            if (transform.position != destination.position)
            {
                 transform.position = Vector3.MoveTowards(transform.position, destination.position, Time.deltaTime * 1.2f);


            }
            if (transform.position == destination.position)
            {
                if (System.Array.IndexOf(pingpongObjects, destination) == System.Array.IndexOf(pingpongObjects, pingpongObjects.Last()))
                {
                    pingpongDirection = PingPongDirection.Back;
                    destination = pingpongObjects[DestinationCurrentIndex(destination) - 1];
                }
                else if (System.Array.IndexOf(pingpongObjects, destination) == System.Array.IndexOf(pingpongObjects, pingpongObjects.First()))
                {
                    pingpongDirection = PingPongDirection.Forward;

                    destination = pingpongObjects[DestinationCurrentIndex(destination) + 1];
                }
                else
                {
                    if (pingpongDirection == PingPongDirection.Forward)
                    {
                        destination = pingpongObjects[DestinationCurrentIndex(destination) + 1];
                    }
                    else if (pingpongDirection == PingPongDirection.Back)
                    {
                        destination = pingpongObjects[DestinationCurrentIndex(destination) - 1];
                    }
                }
            }
        }
       
      //  float pingPong = Mathf.PingPong(Time.time * pingpongSpeed, 1);

    }

    int DestinationCurrentIndex(Transform destination)

    {
        foreach(Transform transform in pingpongObjects)
        {
            if (transform == destination)
            {
                return System.Array.IndexOf(pingpongObjects, transform);

            }

            
           
           
        }
        return 0;
    }

    // rotation
    void Rotation()
    {
        if(zRotation >= 360)
        {
            zRotation = 0;
        }

        transform.Rotate(Vector3.forward* rotationSpeedRandom*Time.deltaTime);

      
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
                Physics2D.IgnoreCollision(myCollider,playerCollider);
               // myCollider.isTrigger = true;
                myRigidbody.isKinematic = false;
                

                anim.SetBool("Die", true);

                Destroy(this.gameObject, 2f);
            }
        }
    }
}
