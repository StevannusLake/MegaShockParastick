using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public enum MoveState
{
    // control it sticks on any platform once and control the sticking setting, like set gravity
    STICK,
    FLYING
}

public class Movement : MonoBehaviour
{
    PlayerAnimation myAnimation;
    ZoomRadiusController zoomRadiusController;
    ColliderController colliderController;
    GetSideHit getSideHit;
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
    private float prevMagnitude;
    private float prevSlingShotVelocity;
    private float shakeTimer = 0;
    public MoveState myMoveStick;

    static bool isCancel = false;
    static bool mousePressed;

    int bounceCounter;

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
    public bool spawnDot;

    //Calculate Player Distance
    public float playerDistance;
    public Text distanceCounterText;
    public float initialPosition = 0;

    public GameObject MainMenu;
    public GameObject SecondChanceMenu;

    /// <summary>
    /// 0 : alive, 1 : dead animation, 2 : die and call menu
    /// </summary>
    [SerializeField]public static int deadState = 0;
    
    public float deadVelocity;
    bool isDead;
    // fix dead update too fast for falling off camera bottom edge
    float deadTimer = 0.5f;
    float deadCounter;
    bool deadFix;
    // cancel indicator
    public GameObject cancelIndicator;
    // cancel slingshot state
    int cancelSlingshotState = 0;

    public GameObject Cinemachine;
    //=======================================================================================================================
    TrailRenderer myTrailRenderer;
    //=======================================================================================================================
    float dotAngle;

    float dotCounterIncrement;
    //=======================================================================================================================

    public bool playerJustDied = false;
    public LayerMask lm;

    //=======================================================================================================================
    /// <summary>
    /// 0 = cannot, 1 = can, 2 = recover
    /// </summary>
    public int doubleSlingshot = 0;
    const int MAXSLINGSHOT = 9;
    const int DECREMENTSLINGSHOT = 3;
    const int INCREMENTSLINGSHOT = 1;
    int doubleSlingshotCounter = MAXSLINGSHOT;
    //=======================================================================================================================

    public GameObject PauseScreen;

    //=======================================================================================================================
    public SmokeEffect mySmokeEffect;
    //=======================================================================================================================

    // Start is called before the first frame update
    void Start()
    {
        float randXPos = Random.Range(-1.72f, 2.04f);
        transform.position = new Vector3(randXPos, transform.position.y, 0f);
        //myAnimation = GetComponent<PlayerAnimation>();
        myMoveStick = MoveState.STICK;
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        myTrailRenderer = GetComponent<TrailRenderer>();
        colliderController = GetComponent<ColliderController>();
        getSideHit = GetComponent<GetSideHit>();
        surfaceTransform = initialGroundTransform;
        zoomRadiusController = GetComponentInChildren<ZoomRadiusController>();
        facingVector = (Vector2)myTransform.right;

        initialPosition = this.gameObject.transform.position.y;

        playerDistance = ButtonManager.instance.TempScore;
        distanceCounterText.text = PlayerPrefs.GetFloat("TempScore", ButtonManager.instance.TempScore).ToString("F1") + " mm";

        playerJustDied = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(deadState);
        if (!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && deadState == 0)
        {
            if (myCollider.isTrigger)
            {
                myCollider.isTrigger = false;
            }
            SlingShot();
            DotsSpawner();
            if (myMoveStick == MoveState.FLYING)
            {
                myTransform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (!spawnDot)
            {
                DecreaseCameraZoomMagnitude();
            }


            DistanceCounter();

            Falling();
        }
        else if (!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && deadState == 1)
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }
            // disable dot
            spawnDot = false;
            myEmotion.EmoteDeath();
            LevelHandler.instance.cameraController.StopFollowing();
            myCollider.isTrigger = true;
            PlayDead();

            if (!playerJustDied)
            {
                VibrateNow();
                //Invoke("CancelVibration", 0.2f);

                playerJustDied = true;
            }
        }
        else if (!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && deadState == 2)
        {
            if (!UIManager.Instance.secondChanceCalled)
            {
                UIManager.Instance.CallSecondChanceMenu();
            }
            else
            {
                UIManager.Instance.CallLoseMenu();
            }

            myEmotion.EmoteIdle();
            isDead = false;
            deadState = 0;
            cancelSlingshotState = 0;
            myCollider.isTrigger = true;
            Cinemachine.SetActive(true);
        }

        if (!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf)
        {
            DropDead();
        }
        else if (UIManager.Instance.LoseMenu.activeSelf || MainMenu.activeSelf || SecondChanceMenu.activeSelf || PostRestartDataHolder.instance.secondLifeUsed)
        {
            //reset
            deadState = 0;
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.gravityScale = 0;
            if (myCollider.isTrigger == true)
            {
                myCollider.isTrigger = false;
            }
            mousePressed = false;

            if (bounceCounter != 0)
            {
                bounceCounter = 0;
            }

            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }
        }

        if(deadState == 0 && bounceCounter >= 3)
        {
            deadState = 1;

            playerJustDied = false;
        }
        //=======================================================================================================================
        ConfigureTrail();
    }

    void SlingShot()
    {
        if(doubleSlingshotCounter <= 0)
        {
            doubleSlingshot = 2;
        }
        else if(doubleSlingshotCounter >= MAXSLINGSHOT)
        {
            doubleSlingshot = 1;
        }

        if (myMoveStick == MoveState.STICK)
        {
            if(Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }

            if(bounceCounter != 0 && deadState == 0)
            {
                bounceCounter = 0;
            }

            // use mouse to test movement without concerning control
            if (Input.GetMouseButtonDown(0) && !PauseScreen.activeInHierarchy)
            {
                initialInputPosition = (Vector2)Input.mousePosition;
                initialInputPosition = Camera.main.ScreenToWorldPoint(initialInputPosition);
                spawnDot = true;
                //isCancel = true;
                mousePressed = true;

                // myAnimation.PlayHold();
                myEmotion.EmoteBeforeFlying();

                cancelIndicator.transform.position = initialInputPosition;
                cancelIndicator.SetActive(true);
            }

            if (mousePressed)
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

            if (Input.GetMouseButtonUp(0))
            {
                mousePressed = false;
                spawnDot = false;
                myEmotion.EmoteIdle();

                cancelIndicator.SetActive(false);
            }
        }
        else if (myMoveStick == MoveState.FLYING && doubleSlingshot == 1)
        {
            // use mouse to test movement without concerning control
            if (Input.GetMouseButtonDown(0))
            {
                initialInputPosition = (Vector2)Input.mousePosition;
                initialInputPosition = Camera.main.ScreenToWorldPoint(initialInputPosition);
                spawnDot = true;
                //isCancel = true;
                mousePressed = true;
                
                cancelIndicator.transform.position = initialInputPosition;
                cancelIndicator.SetActive(true);

                Time.timeScale = 0.4f;
            }

            if (mousePressed)
            {
                CancelSlingShot();
            }

            if (Input.GetMouseButtonUp(0) && !isCancel)
            {
                finalInputPosition = (Vector2)Input.mousePosition;
                finalInputPosition = Camera.main.ScreenToWorldPoint(finalInputPosition);
                slingshotVelocity = SlingshotVelocityCalculation();

                myRigidBody.velocity = slingshotVelocity;
                
                // reset gravity
                myTransform.SetParent(null);
                myRigidBody.gravityScale = 1;
                spawnDot = false;
                isCancel = false;

                //doubleSlingshot = 0;
                doubleSlingshotCounter -= DECREMENTSLINGSHOT;
            }

            if (Input.GetMouseButtonUp(0))
            {
                mousePressed = false;
                spawnDot = false;

                cancelIndicator.SetActive(false);
                Time.timeScale = 1.0f;
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

            cancelSlingshotState = 1;

            // myAnimation.PlayHold();
            myEmotion.EmoteBeforeFlying();
        }

        if (isCancel == false && launchVelocity.magnitude <= 0.9 && cancelSlingshotState != 0)
        {
            spawnDot = false;
            isCancel = true;

            cancelSlingshotState = 0;

            // myAnimation.PlayIdle();
            myEmotion.EmoteIdle();
        }
        else if(isCancel == false && launchVelocity.magnitude <= 0.9 && cancelSlingshotState == 0)
        {
            isCancel = false;
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
        else if (!spawnDot)
        {
            return prevMagnitude;
        }
        return 0;

    }
    void DecreaseCameraZoomMagnitude()
    {
        if (prevMagnitude >= 0 && !spawnDot)
        {
            prevMagnitude = Mathf.MoveTowards(prevMagnitude, 0, Time.deltaTime * prevSlingShotVelocity * 2f);

        }
    }

    #endregion  ///////////////////////////////////







    #region Camera Amplitude And Frequency
    public float CalculateCameraAmplitude()
    {
        if (spawnDot) shakeTimer += Time.fixedDeltaTime;
        if (shakeTimer >= 15)
        {
            if (spawnDot)
            {
                float amplitude = 0;
                amplitude = Mathf.MoveTowards(amplitude, 5f, Time.fixedTime * 0.1f);
                return amplitude;
            }
            else if (!spawnDot)
            {
                return 0;
            }

            
        }
        return 0;
    }
    public float CalculateCameraFrequency()
    {
        if (spawnDot) shakeTimer += Time.fixedDeltaTime;

        if (shakeTimer >= 15)
        {
            if (spawnDot)
            {
                float frequency = 0;
                frequency = Mathf.MoveTowards(frequency, 5f, Time.fixedTime * 0.1f);
                return frequency;
            }
            else if (!spawnDot)
            {
                return 0;
            }
            
        }
        return 0;

    }
   

    #endregion




    // stop movement once touch something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!MainMenu.activeSelf && deadState == 0)
        {
            if (collision.collider.CompareTag(deadlyTag))
            {
                myRigidBody.velocity = Vector2.zero;
                AudioManager.PlaySound(AudioManager.Sound.PlayerDie);
                ScreenEffectManager.instance.ShakeCamera(ShakeVariation.Dying);
                // Die and Second Chance Menu pop out
                //UIManager.Instance.CallSecondChanceMenu();
                if (deadState == 0)
                {
                    deadState = 1;
               
                    playerJustDied = false;
                }
            }
            else if (collision.collider.CompareTag(surfaceTag))
            {
                myRigidBody.velocity = Vector2.zero;
                surfaceStickCount = collision.gameObject.GetComponent<Surfaces>().stickCount;

                //doubleSlingshot = 0;
            }
            else if (collision.collider.CompareTag(horizontalWall))
            {
                bounceCounter++;
                if(doubleSlingshot == 2)
                {
                    doubleSlingshotCounter += INCREMENTSLINGSHOT;
                }
                ScreenEffectManager.instance.ShakeCamera(ShakeVariation.HittingWall);
            }

            if (collision.collider.gameObject.name == "FirstInitialPlatform")
            {
                myEmotion.EmoteIdle();
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

                // get info to spawn relative smoke effect
                Surfaces currentSurface = collision.collider.gameObject.GetComponent<Surfaces>();
                if(currentSurface.thisType == Surfaces.SurfaceTypes.Safe)
                {
                    float angle = Mathf.Atan2(currentSurface.gameObject.transform.position.y - myTransform.position.y, currentSurface.gameObject.transform.position.x - myTransform.position.x);
                    angle *= Mathf.Rad2Deg;
                    angle += 90;
                    mySmokeEffect.SpawnSmoke(collision.GetContact(0).point, 2, angle);
                }

                // myAnimation.PlayIdle();
                myEmotion.EmoteIdle();
            }
        }
        
        if(collision.collider.name == "FirstInitialPlatform" && deadState == 0)
        {
            myEmotion.EmoteIdle();
        }
        if (collision.collider.name == "SecondLifeInitialPlatform" && deadState == 0)
        {
            myEmotion.EmoteIdle();
        }
    }

    // use stay to check collision after super finished
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.collider.CompareTag(deadlyTag) && !isSuper)
    //    {
    //        myRigidBody.velocity = Vector2.zero;
    //        AudioManager.PlaySound(AudioManager.Sound.PlayerDie);
    //        ScreenEffectManager.instance.ShakeCamera(ShakeVariation.Dying);
    //        // Die and Second Chance Menu pop out
    //        //UIManager.Instance.CallSecondChanceMenu();
    //        if (deadState == 0)
    //        {
    //            deadState = 1;

    //            playerJustDied = false;
    //        }
    //    }
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(surfaceTag))
        {
            surfaceStickCount = collision.gameObject.GetComponent<Surfaces>().stickCount;

            doubleSlingshot = 1;
        }
        if (collision.collider.CompareTag(surfaceTag) && surfaceStickCount == 1 && myMoveStick == MoveState.FLYING)
        {
            surfaceStickCount = 2;
            collision.gameObject.GetComponent<Surfaces>().stickCount = surfaceStickCount;

            // myAnimation.PlayIdle();
            myEmotion.EmoteIdle();
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

                playerJustDied = false;
            }
        }
    }


    private void DotsSpawner()
    {
        if (spawnDot)
        {
            Vector2 myPosition = myTransform.position;
            currentInputPosition = Input.mousePosition;
            currentInputPosition = Camera.main.ScreenToWorldPoint(currentInputPosition);

            dotCounterIncrement += Time.unscaledDeltaTime * 0.5f;

            for (int i = 0; i < numDots; i++)
            {
                if(dotCounterIncrement > 1)
                {
                    dotCounterIncrement = 0;
                }

                // set position based on calculation the position of dots over time
                Vector2 tempDotPosition = CalculatePosition(dotCounterIncrement * dotsPositionOverTime * (i + 1)) + myPosition;
                // trajectoryDots[i].transform.position = tempDotPosition;
                GameObject previousDotObject;
                if (i - 1 > 0)
                {
                    previousDotObject = trajectoryDots[i - 1];
                }
                else
                {
                    previousDotObject = gameObject;
                }

                CalculateDotAngle(previousDotObject.transform.position, trajectoryDots[i].transform.position);
                
                trajectoryDots[i].transform.SetPositionAndRotation(tempDotPosition, Quaternion.AngleAxis(dotAngle, new Vector3(0.0f, 0.0f, 1.0f)));
                
                Vector2 temp = CalculateDotHitWall(trajectoryDots[i]);

                if (temp != Vector2.zero)
                {
                    float bounceXPos = trajectoryDots[i].transform.position.x + ((temp.x - trajectoryDots[i].transform.position.x) * 2.0f); 
                    trajectoryDots[i].transform.position = new Vector2(bounceXPos, trajectoryDots[i].transform.position.y);

                }

                // second check
                Vector2 temp2 = CalculateDotHitWall(trajectoryDots[i]);

                if (temp2 != Vector2.zero)
                {
                    float bounceXPos2 = trajectoryDots[i].transform.position.x + ((temp2.x - trajectoryDots[i].transform.position.x) * 2.0f);
                    trajectoryDots[i].transform.position = new Vector2(bounceXPos2, trajectoryDots[i].transform.position.y);

                }

                // third check
                // second check
                Vector2 temp3 = CalculateDotHitWall(trajectoryDots[i]);
                bool wallDie = false;
                if (temp3 != Vector2.zero)
                {
                    wallDie = true;
                }

                if (!DotHitsSurface(previousDotObject, trajectoryDots[i]) && !wallDie)
                {
                    trajectoryDots[i].SetActive(true);
                }
                else
                {
                    for (int j = i; j < numDots; j++)
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
            shakeTimer = 0;

            dotCounterIncrement = 0;
        }
    }

    // calculate the position of dots over time
    private Vector2 CalculatePosition(float elapsedTime)
    {
        Vector2 gravity = new Vector2(0, Physics2D.gravity.magnitude);
        Vector2 launchVelocity = currentInputPosition - initialInputPosition;

        launchVelocity *= SlingshotForce;

        if (launchVelocity.magnitude != 0)
        {
            float i = MaxSlingshotForce / launchVelocity.magnitude;
            float control = i > 1 ? 1 : i;
            launchVelocity *= control;
        }

        Vector2 resultVector = (gravity * elapsedTime * elapsedTime * 0.5f + launchVelocity * elapsedTime) * -1;
        
        return resultVector;
    }

    void CalculateDotAngle(Vector2 prevDot, Vector2 currentDot)
    {
        dotAngle = Mathf.Atan2(currentDot.y - prevDot.y, currentDot.x - prevDot.x) * Mathf.Rad2Deg - 90;
    }
    
    // The WALL ================================================================================================
    // calculate the position of dots over time when hit wall
    private Vector2 CalculateDotHitWall(GameObject currentDot)
    {
        RaycastHit2D hit;

        Vector2 direction = currentDot.transform.position - myTransform.position;
        float distance = direction.magnitude;

        // layer mask 12 == wall
        hit = Physics2D.Raycast(myTransform.position, direction, distance, lm.value);
        
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
        if (!isDead)
        {
            isDead = true;
            myRigidBody.velocity = new Vector2(0, deadVelocity);
            zoomRadiusController.enabled = false;
            colliderController.enabled = false;
            myCollider.enabled = false;
            AudioManager.PlaySound(AudioManager.Sound.PlayerDie);
        }
    }

    void DropDead()
    {
        Vector2 cameraBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 cameraTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (deadFix)
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

        if (myTransform.position.y <= cameraBottom.y || myTransform.position.x <= cameraBottom.x || myTransform.position.y >= cameraTop.y || myTransform.position.x >= cameraTop.x)
        {
            if (deadState == 0)
            {
                myRigidBody.velocity = Vector2.zero;
                deadState = 1;
                deadFix = true;
                //Debug.Log("DropDead 0 to 1");

                playerJustDied = false;
            }
            else if (deadState == 1 && !deadFix && myTransform.position.y <= cameraBottom.y)
            {
                deadState = 2;
                //Debug.Log("DropDead 1 to 2");
            }
            else if (deadState == 2)
            {
                myRigidBody.velocity = Vector2.zero;
                //Debug.Log("DropDead 2");
            }

            
            
        }
    }

    void VibrateNow()
    {
        Vibrator.Vibrate(200);
    }

    void CancelVibration()
    {
        Vibrator.Cancel();
    }

    void Falling()
    {
        if (myRigidBody.velocity.y != 0)
        {
            myEmotion.EmoteFlying();
        }
    }

    void ConfigureTrail()
    {
        if(myMoveStick == MoveState.STICK || deadState != 0)
        {
            myTrailRenderer.enabled = false;
        }
        else if(myMoveStick == MoveState.FLYING && deadState == 0)
        {
            myTrailRenderer.enabled = true;
        }

        if (myTrailRenderer.enabled == false)
        {
            myTrailRenderer.Clear();
        }
    }
}

/*
 * 
 *
 * 
 */
