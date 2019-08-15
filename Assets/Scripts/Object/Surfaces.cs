using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Surfaces : MonoBehaviour
{
    public enum SurfaceTypes {Safe,Dangerous,Moving ,DangerousMoving,Initial}
    public enum DangerType {NONE,FAST,FADE }
    private enum PingPongDirection {Forward,Back }
    private PingPongDirection pingpongDirection = PingPongDirection.Forward;
    private DangerType dangerType = DangerType.NONE;
    public bool isMover = false;
    public SurfaceTypes thisType;
    public float rotationSpeed;
    public float pingpongSpeed;
    Transform myTransform;
    CapsuleCollider2D myCollider;
    Rigidbody2D myRigidbody;
    SpriteRenderer myRenderer;
    static float zRotation;
    public float rotationSpeedRandom;
    public int stickCount = 0;
    private bool reachedDestination = false;
    private bool startedToFade = false;
    private Transform destination;
    string nSurfaceTag = "NSurface";
    private Transform[] pingpongObjects;
    public Transform platformPlacementTransform;
    public float aboutToDieTimer = 0;
    GameObject player;
    CircleCollider2D playerCollider;
    
    public bool alreadyRespawnedCoin = false;
    private bool foundDestination = false;
    public bool OnRotation = false;
    DangerType[] dangers = new DangerType[] { DangerType.FADE, DangerType.FAST };
    private Animator anim;
    private bool stopTimer;

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
        if (isMover) FindPingPongObjects();
        ChooseRandomDangerType();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<CircleCollider2D>();

        anim = GetComponent<Animator>();
    }

    void ChooseRandomDangerType()
    {

        int randomDanger = Random.Range(0, dangers.Length);
        dangerType = dangers[randomDanger];
    }



    // Update is called once per frame
    void Update()
    {
        if (OnRotation)
        {
            Rotation();
        }
        
        DropAfter();
       if (startedToFade) FadeOutAndDie();
        if (isMover && foundDestination) MoveBetweenPingPongs();
    }

    void FindPingPongObjects()
    {
        pingpongObjects = new Transform[platformPlacementTransform.childCount];

      //  for (int i=0; i< platformPlacementTransform.childCount;i++)
       // {
       //     pingpongObjects[i]= platformPlacementTransform.Find("PingPong"+(i+1));
       // }
       // destination = pingpongObjects[1];
       // foundDestination = true;
        
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
        if (platformPlacementTransform.childCount == 2)
        {
            pingpongObjects[0] = platformPlacementTransform.Find("PingPong1");
            pingpongObjects[1] = platformPlacementTransform.Find("PingPong2");
            destination = pingpongObjects[1];
            foundDestination = true;
        }
        if (platformPlacementTransform.childCount == 4)
        {
            pingpongObjects[0] = platformPlacementTransform.Find("PingPong1");
            pingpongObjects[1] = platformPlacementTransform.Find("PingPong2");
            pingpongObjects[2] = platformPlacementTransform.Find("PingPong3");
            pingpongObjects[3] = platformPlacementTransform.Find("PingPong4");
            destination = pingpongObjects[1];
            foundDestination = true;
        }
        if (platformPlacementTransform.childCount == 5)
        {
            pingpongObjects[0] = platformPlacementTransform.Find("PingPong1");
            pingpongObjects[1] = platformPlacementTransform.Find("PingPong2");
            pingpongObjects[2] = platformPlacementTransform.Find("PingPong3");
            pingpongObjects[3] = platformPlacementTransform.Find("PingPong4");
            pingpongObjects[4] = platformPlacementTransform.Find("PingPong5");
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
            PlatformCondition condition = platformPlacementTransform.GetComponent<PlatformCondition>();
            bool shouldLoop = condition.isLoop;
            ///
            if (transform.position != destination.position)
            {
                 transform.position = Vector3.MoveTowards(transform.position, destination.position, Time.deltaTime * 1.2f);


            }
            if (transform.position == destination.position)
            {
                switch (shouldLoop)
                {
                    case false:
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
                        break;

                    case true:
                        if (System.Array.IndexOf(pingpongObjects, destination) == System.Array.IndexOf(pingpongObjects, pingpongObjects.Last()))
                        {
                            
                            destination = pingpongObjects[0];
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
                            
                        }
                        break;

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
            if (stickCount ==1 && thisType != SurfaceTypes.Initial)
            {
                if(!startedToFade) startedToFade = true;

            }
        }


        
    }

    void FadeOutAndDie()
    {
        
        bool createRandomSpeed = false;
        if(!createRandomSpeed)
        {
            float randomSpeed = Random.Range(30f, 50f);
            createRandomSpeed = true;
            if (myRenderer != null)
            {
                switch(dangerType)
                {
                    case DangerType.FAST:
                    rotationSpeedRandom += Time.deltaTime * randomSpeed;
                        rotationSpeedRandom = Mathf.Clamp(rotationSpeedRandom,0f, 480f);
                        break;
                    case DangerType.FADE:
                        aboutToDieTimer += Time.deltaTime;
                        if (aboutToDieTimer >= 2f)
                        {
                            anim.SetBool("AboutToDie", true);
                        }
                        if (aboutToDieTimer >= 4f && stopTimer == false)
                        {
                            stickCount = 3;
                            player.transform.parent = null;
                            player.GetComponent<Rigidbody2D>().gravityScale = 1;
                            stopTimer = true;
                            aboutToDieTimer = 0;
                        }
                        break;
                }

                
               

            }
        }
        if(myRenderer==null)
        {
            myRenderer = GetComponent<SpriteRenderer>();
        }
      
    }
}
