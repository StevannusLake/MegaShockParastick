using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MoveState
{
    // control it sticks on any platform once and control the sticking setting, like set gravity
    STICK,
    FLYING
}

public class Movement : MonoBehaviour
{
    PlayerAnimation myAnimation;
    public PlayerEmotion myEmotion;
    Transform myTransform;
    Rigidbody2D myRigidBody;
    CircleCollider2D myCollider;
    Vector2 facingVector;
    Vector2 initialInputPosition;
    Vector2 finalInputPosition;
    Vector2 currentInputPosition;
    Vector2 slingshotVelocity;
    public float SlingshotForce;
    public float MaxSlingshotForce;
    public float prevMagnitude;
    public float prevSlingShotVelocity;
    public MoveState myMoveStick;

    static bool isCancel = false;
    static bool mousePressed;

    public Transform initialGroundTransform;

    Transform surfaceTransform;
    int surfaceStickCount;

    string surfaceTag = "Surface";
    string deadlyTag = "Deadly";
    string horizontalWall = "HorizontalWall";

    //=======================================================================================================================
    // Trajectory dots
    int numDots = 10;
    [Header("seconds: if 10s, means the distance between two dots takes 10 seconds to reach")]
    public float dotsPositionOverTime;
    Vector2 GRAVITY = new Vector2(0, -45f);
    public GameObject[] trajectoryDots = new GameObject[10];
    bool spawnDot;

    //Calculate Player Distance
    public float playerDistance;
    public Text distanceCounterText;
    public float initialPosition = 0;

    public GameObject MainMenu;
    public GameObject SecondChanceMenu;

    /// <summary>
    /// 0 : alive, 1 : dead animation, 2 : die and call menu
    /// </summary>
    public static int deadState = 0;
    public float deadVelocity;
    bool isDead;
    // fix dead update too fast for falling off camera bottom edge
    float deadTimer = 0.5f;
    float deadCounter;
    bool deadFix;
    // cancel indicator
    public GameObject cancelIndicator;

    public GameObject Cinemachine;

    // Start is called before the first frame update
    void Start()
    {
        //myAnimation = GetComponent<PlayerAnimation>();
        myMoveStick = MoveState.STICK;
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();

        surfaceTransform = initialGroundTransform;

        facingVector = (Vector2)myTransform.right;
        
        initialPosition = this.gameObject.transform.position.y;

        playerDistance = ButtonManager.instance.TempScore;
        distanceCounterText.text = PlayerPrefs.GetFloat("TempScore", ButtonManager.instance.TempScore).ToString("F1") + " mm";
    }

    // Update is called once per frame
    void Update()
    {
        if(!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && deadState == 0)
        {
            SlingShot();
            DotsSpawner();
            if (myMoveStick == MoveState.FLYING)
            {
                myTransform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (!spawnDot) DecreaseCameraZoomMagnitude();

            DistanceCounter();

            Falling();
        }
        else if(!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && deadState == 1)
        {
            myEmotion.EmoteDeath();
            Cinemachine.SetActive(false);
            myCollider.isTrigger = true;
            PlayDead();
        }
        else if(!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && deadState == 2)
        {
            UIManager.Instance.CallSecondChanceMenu();
            myEmotion.EmoteIdle();
            isDead = false;
            deadState = 0;
            myCollider.isTrigger = false;
            Cinemachine.SetActive(true);
        }

        if (!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf)
        {
            DropDead();
        }
        else if(UIManager.Instance.LoseMenu.activeSelf || MainMenu.activeSelf || SecondChanceMenu.activeSelf)
        {
            deadState = 0;
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.gravityScale = 0;
            if (myCollider.isTrigger == true)
            {
                myCollider.isTrigger = false;
            }
            mousePressed = false;
        }
    }

    void SlingShot()
    {
        if(myMoveStick == MoveState.STICK)
        {
            // use mouse to test movement without concerning control
            if (Input.GetMouseButtonDown(0))
            {
                initialInputPosition = (Vector2)Input.mousePosition;
                initialInputPosition = Camera.main.ScreenToWorldPoint(initialInputPosition);
                spawnDot = true;
                isCancel = true;
                mousePressed = true;

               // myAnimation.PlayHold();
                myEmotion.EmoteBeforeFlying();

                cancelIndicator.transform.position = initialInputPosition;
                cancelIndicator.SetActive(true);
            }

            if(mousePressed)
            {
                CancelSlingShot();
            }

            if (Input.GetMouseButtonUp(0) && !isCancel)
            {
                finalInputPosition = (Vector2)Input.mousePosition;
                finalInputPosition = Camera.main.ScreenToWorldPoint(finalInputPosition);
                slingshotVelocity = SlingshotVelocityCalculation();

                myRigidBody.velocity = slingshotVelocity;

                myMoveStick = MoveState.FLYING;

                // reset gravity
                myTransform.SetParent(null);
                myRigidBody.gravityScale = 1;
                spawnDot = false;
                isCancel = false;

               // myAnimation.PlayJump();
                myEmotion.EmoteFlying();

                AudioManager.PlaySound(AudioManager.Sound.PlayerUnstick);
            }

            if(Input.GetMouseButtonUp(0))
            {
                mousePressed = false;
                spawnDot = false;
                myEmotion.EmoteIdle();

                cancelIndicator.SetActive(false);
            }
        }
    }

    void CancelSlingShot()
    {
        Vector2 currentMousePos = Input.mousePosition;
        currentMousePos = Camera.main.ScreenToWorldPoint(currentMousePos);
        Vector2 launchVelocity = currentMousePos - initialInputPosition;
        
        if (launchVelocity.magnitude >= 1.0)
        {
            isCancel = false;
            spawnDot = true;

           // myAnimation.PlayHold();
            myEmotion.EmoteBeforeFlying();
        }

        if(isCancel == false && launchVelocity.magnitude <= 0.9)
        {
            spawnDot = false;
            isCancel = true;

           // myAnimation.PlayIdle();
            myEmotion.EmoteIdle();
        }
    }

    
    Vector2 SlingshotVelocityCalculation()
    {
        Vector2 resultantVelocity, displacementVector;
        displacementVector = finalInputPosition - initialInputPosition;
        displacementVector *= -1;
        resultantVelocity = displacementVector;
        
        resultantVelocity *= SlingshotForce;
        
        float i = MaxSlingshotForce / resultantVelocity.magnitude;
        float control = i > 1 ? 1 : i;
        resultantVelocity *= control;

        //keep the slingshot velocity (reza)
        prevSlingShotVelocity = resultantVelocity.magnitude;
        return resultantVelocity;
    }

    #region Camera Zoom Calculations
    // calculate how much player charged the slingshot
    public float CalculateCameraZoom()
    {
        if (spawnDot)
        {
            float magnitude = (currentInputPosition - initialInputPosition).magnitude;
            prevMagnitude = magnitude;
            return magnitude;
        }
       else if(!spawnDot)
        {    
            return prevMagnitude;
        }
        return 0;
      
    }
    void DecreaseCameraZoomMagnitude()
    {
        if (prevMagnitude>=0 && !spawnDot)
        {
            prevMagnitude = Mathf.MoveTowards(prevMagnitude, 0, Time.deltaTime * prevSlingShotVelocity *2f);
           
        }
    }

    #endregion  ///////////////////////////////////


    // stop movement once touch something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!MainMenu.activeSelf && deadState == 0)
        {
            if (collision.collider.CompareTag(deadlyTag))
            {
                myRigidBody.velocity = Vector2.zero;
                AudioManager.PlaySound(AudioManager.Sound.PlayerDie);
                
                // Die and Second Chance Menu pop out
                //UIManager.Instance.CallSecondChanceMenu();
                if (deadState == 0)
                {
                    deadState = 1;
                }
            }
            else if(collision.collider.CompareTag(surfaceTag))
            {
                myRigidBody.velocity = Vector2.zero;
                surfaceStickCount = collision.gameObject.GetComponent<Surfaces>().stickCount;
            }

            // stick on the surface
            if (collision.collider.CompareTag(surfaceTag) && myMoveStick == MoveState.FLYING && surfaceStickCount == 0)
            {
                myRigidBody.velocity = Vector2.zero;
                AudioManager.PlaySound(AudioManager.Sound.PlayerStick);

                surfaceTransform = collision.gameObject.transform;
                myTransform.SetParent(surfaceTransform);
                myRigidBody.velocity = Vector2.zero;
                myRigidBody.angularVelocity = 0;
                myRigidBody.gravityScale = 0;
                myMoveStick = MoveState.STICK;
                surfaceStickCount = 1;
                collision.gameObject.GetComponent<Surfaces>().stickCount = surfaceStickCount;

               // myAnimation.PlayIdle();
                myEmotion.EmoteIdle();
            }
       }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 cameraBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (other.CompareTag(deadlyTag))
        {
            AudioManager.PlaySound(AudioManager.Sound.PlayerDie);
            
            // Die and Second Chance Menu pop out
            //UIManager.Instance.CallSecondChanceMenu();
            if (deadState == 0)
            {
                deadState = 1;
            }
        }
        if(other.CompareTag("Coin") && deadState == 0)
        {
            AudioManager.PlaySound(AudioManager.Sound.CollectCoin);
            AudioManager.PlaySound(AudioManager.Sound.CollectCoinMain);

            GameManager.instance.AddCoin(1);
            Destroy(other.gameObject);
        }
    }


    private void DotsSpawner()
    {
        if(spawnDot)
        {
            Vector2 myPosition = myTransform.position;
            currentInputPosition = Input.mousePosition;
            currentInputPosition = Camera.main.ScreenToWorldPoint(currentInputPosition);

            for (int i = 0; i < numDots; i++)
            {
                // set position based on calculation the position of dots over time
                Vector2 tempDotPosition = CalculatePosition(dotsPositionOverTime * (i+1)) + myPosition;
                trajectoryDots[i].transform.position = tempDotPosition;
                GameObject previousDotObject;
                if (i - 1 > 0)
                {
                    previousDotObject = trajectoryDots[i - 1];
                }
                else
                {
                    previousDotObject = gameObject;
                }

                Vector2 temp = CalculateDotHitWall(trajectoryDots[i]);

                if(temp != Vector2.zero)
                {
                    float bounceXPos = trajectoryDots[i].transform.position.x + ( (temp.x - trajectoryDots[i].transform.position.x) * 2);
                    trajectoryDots[i].transform.position = new Vector2(bounceXPos, trajectoryDots[i].transform.position.y);
                }

        if (!DotHitsSurface(previousDotObject, trajectoryDots[i]))
                {
                    trajectoryDots[i].SetActive(true);
                }
                else
                {
                    for(int j=i; j < numDots; j++)
                    {
                        trajectoryDots[j].SetActive(false);
                    }
                    return;
                }
            }
        }
        else
        {
            foreach (GameObject Dots in trajectoryDots)
            {
                Dots.SetActive(false);
            }
        }
    }

    // calculate the position of dots over time
    private Vector2 CalculatePosition(float elapsedTime)
    {
        Vector2 gravity = new Vector2(0, Physics2D.gravity.magnitude);
        Vector2 launchVelocity = currentInputPosition - initialInputPosition;
        
        launchVelocity *= SlingshotForce;

        float i = MaxSlingshotForce / launchVelocity.magnitude;
        float control = i > 1 ? 1 : i;
        launchVelocity *= control;

        Vector2 resultVector = (gravity * elapsedTime * elapsedTime * 0.5f + launchVelocity * elapsedTime) * -1;
        
        return resultVector;
    }

    // The WALL ================================================================================================
    // calculate the position of dots over time when hit wall
    private Vector2 CalculateDotHitWall(GameObject currentDot)
    {
        RaycastHit2D hit;

        Vector2 direction = currentDot.transform.position - myTransform.position;
        float distance = direction.magnitude;

        hit = Physics2D.Raycast(myTransform.position, direction, distance);
        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;
        
        CircleCollider2D currentDotCollider = currentDot.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(myCollider, currentDotCollider);

        // The WALL ================================================================================================
        if (hit && hit.collider.gameObject.CompareTag(horizontalWall))
        {
            return hit.point;
        }

        return Vector2.zero;
    }
    
    bool DotHitsSurface(GameObject prevDot, GameObject currentDot)
    {
        RaycastHit2D hit;
        
        Vector2 direction = currentDot.transform.position - prevDot.transform.position;
        float distance = direction.magnitude;

        hit = Physics2D.Raycast(prevDot.transform.position, direction, distance);
        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;
        
        CircleCollider2D prevDotCollider = prevDot.GetComponent<CircleCollider2D>();
        CircleCollider2D currentDotCollider = currentDot.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(prevDotCollider, currentDotCollider);

        if (hit && !hit.collider.gameObject.CompareTag(horizontalWall))
        {
            return true;
        }
        
        return false;
    }


    public void DistanceCounter()
    {
        float tempCurrentDistance = this.transform.position.y;

        if (tempCurrentDistance > initialPosition)
        {
            playerDistance += (tempCurrentDistance - initialPosition);
            initialPosition = this.transform.position.y;

            distanceCounterText.text = playerDistance.ToString("F1") + " mm";
            GameManager.instance.playerDistanceTraveled = playerDistance;
        }
    }

    // dead state == 1
    void PlayDead()
    {
        if(!isDead)
        {
            isDead = true;
            myRigidBody.velocity = new Vector2(0, deadVelocity);

            AudioManager.PlaySound(AudioManager.Sound.PlayerDie);
        }
    }

    void DropDead()
    {
        Vector2 cameraBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if(deadFix)
        {
            if (deadCounter >= deadTimer)
            {
                deadCounter = 0;
                deadFix = false;
            }
            else
            {
                deadCounter += Time.deltaTime;
            }
        }

        if(myTransform.position.y <= cameraBottom.y)
        {
            if (deadState == 0)
            {
                myRigidBody.velocity = Vector2.zero;
                deadState = 1;
                deadFix = true;
                //Debug.Log("DropDead 0 to 1");
            }
            else if(deadState == 1 && !deadFix)
            {
                deadState = 2;
                //Debug.Log("DropDead 1 to 2");
            }
            else if(deadState == 2)
            {
                myRigidBody.velocity = Vector2.zero;
                //Debug.Log("DropDead 2");
            }
        }
    }

    void Falling()
    {
        if(myRigidBody.velocity.y != 0)
        {
            myEmotion.EmoteFlying();
        }
    }
}

/*
 * 
 *
 * 
 */