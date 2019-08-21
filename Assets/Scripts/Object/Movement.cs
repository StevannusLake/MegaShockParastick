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

    public int bounceCounter;
    public int maxBounceCounter;
   // public int maxBounceCounterBar;

    public Transform initialGroundTransform;

    Transform surfaceTransform;
    int surfaceStickCount;

    string surfaceTag = "Surface";
    string deadlyTag = "Deadly";
    string horizontalWall = "HorizontalWall";
    string smallCollider = "SmallCollider";
    string bigCollider = "BigCollider";

    //=======================================================================================================================
    // Trajectory dots
    const int numDots = 10;
    [Header("seconds: if 10s, means the distance between two dots takes 10 seconds to reach")]
    public float dotsPositionOverTime;
    Vector2 GRAVITY = new Vector2(0, -45f);
    public GameObject[] trajectoryDots = new GameObject[10];
    public bool spawnDot;

    //Calculate Player Distance
    public float playerDistance;
    public float initialDistance;
    public Text distanceCounterText;
    public float initialPosition = 0;
    public float tempCurrentDistance;
    public float distanceCounter = 0;

    public GameObject MainMenu;
    public GameObject SecondChanceMenu;
    public GameObject CoinMultiplyMenu;
    private Shop shop;

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
    
    GameObject previousDotObject;
    Dot[] dots = new Dot[10];
    //=======================================================================================================================

    public bool playerJustDied = false;
    public LayerMask lm;

    //=======================================================================================================================
    /// <summary>
    /// 0 = can, 1 = slingshot once ald, 2 = recover/ cannot
    /// </summary>
    public int doubleSlingshot = 0;
    public int MAXSLINGSHOT;
    const int DECREMENTSLINGSHOT = 3;
    public int INCREMENTSLINGSHOT;
    public int doubleSlingshotCounter;
    //=======================================================================================================================

    public GameObject PauseScreen;

    //=======================================================================================================================
    public SmokeEffect mySmokeEffect;

    Vector2 collideWallPoint;
    //=======================================================================================================================
    public DangerPlatformEffect[] dangerEffects = new DangerPlatformEffect[3];
    Vector2 currentVelocity;
    //=======================================================================================================================
    public PlayerParticleSystem myParticleSystem;
    public ParticleSystem myDeadParticleSystem;
    ParticleSystem.EmissionModule dpsEmission;
    bool doubleSSEffect;
    //=======================================================================================================================
    // bounce counter delay timer
    static bool isBounceRecover;
    static float bounceRecoverCounter, bounceRecoverTimer = 0.5f;
    //=======================================================================================================================
    public HandTutorial hand;
    // 23/5=======================================================================================================================
    List<GameObject> dotList;
    //=======================================================================================================================
    Vector2 screenMid;
    public bool isRareSkin = false;
    public Vector3 scale;
    private float curScale;
    private Vector3 baseScale;
    public float doubleSlingshotCharge;
    public bool isSticking = false;
    private float ripplePeriod;
    private Vector2 distToSmallCol;
    public GameObject mainMenu;

    public MyBackground myBackground;

    float period = 0;
    float delay = 0.3f;

    public ParticleSystem flyingParticleSystem;

    public SpriteRenderer emotionSpriteRend;
    public ParticleSystem deadEffect2;

    public GameObject inGameMenu;
    private TutorialManager tutorialManager;
    public int stick;

    public ParticleSystem absorbBiggerEffect;
    private ParticleSystem.ShapeModule absorbBiggerShape;
    public ParticleSystem absorbSmallerEffect;
    private ParticleSystem.ShapeModule absorbSmallerShape;

    public ParticleSystem StickOnStaticSafePlatformEffect;
    public ParticleSystem StickOnMovingSafePlatformEffect;
    public ParticleSystem UnstickFromStaticSafePlatformEffect;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.coinCollectedInAGame = 0; // reset for missions
        GameManager.instance.bounceCounterInAGame = 0; // reset for missions
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
        //maxBounceCounterBar = maxBounceCounter;
        initialPosition = this.gameObject.transform.position.y;
        curScale = 0.1f;
        baseScale = transform.localScale;
        transform.localScale = baseScale * 1;
        scale = baseScale * 1;
        playerDistance = ButtonManager.instance.TempScore;
        myBackground.tempExtraDistance = playerDistance;
        distanceCounterText.text = playerDistance.ToString("F1") + " mm";
        tutorialManager = inGameMenu.GetComponent<TutorialManager>();

        //distanceCounterText.text = PlayerPrefs.GetFloat("TempScore", ButtonManager.instance.TempScore).ToString("F1") + " mm";

        playerJustDied = true;

        for(int i=0; i<numDots; i++)
        {
            dots[i] = trajectoryDots[i].GetComponent<Dot>();
            dots[i].mySR.sprite = Shop.instance.skinUsing.GetComponent<Skin>().tradectoryDotsSprite;
        }

        screenMid = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));

        //========== 20/5 ====================================================================================== 
        if(hand != null && HandTutorial.tutorialCounter < 2)
        {
            hand.OnTutorial(new Vector2(screenMid.x, screenMid.y));
        }
        myParticleSystem.myParticleSystem.GetComponent<ParticleSystemRenderer>().material = Shop.instance.skinUsing.GetComponent<Skin>().doubleSlingShotMat;
        myTrailRenderer.material = Shop.instance.skinUsing.GetComponent<Skin>().trailMat;

        flyingParticleSystem.Stop();

        MissionManager.instance.LoadInGameProgress();

        GetComponent<SpriteRenderer>().enabled = true;
        emotionSpriteRend.enabled = true;
        deadEffect2.Stop();

        GetComponent<SpriteRenderer>().sprite = Shop.instance.skinUsing.GetComponent<Skin>().skinImage;

        absorbBiggerEffect.Stop();
        absorbBiggerShape = absorbBiggerEffect.shape;
        absorbSmallerEffect.Stop();
        absorbSmallerShape = absorbSmallerEffect.shape;
    }
    
    // Update is called once per frame
    void Update()
    {
        TrailSizing();

        currentVelocity = myRigidBody.velocity;
        if (!UIManager.Instance.LoseMenu.activeSelf && deadState == 0) // && !MainMenu.activeSelf 
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

            // dead particle system off
            dpsEmission = myDeadParticleSystem.emission;
            dpsEmission.enabled = false;
            myDeadParticleSystem.Stop();

            GetComponent<SpriteRenderer>().enabled = true;
            emotionSpriteRend.enabled = true;

            deadEffect2.Stop();
        }
        else if (!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && deadState == 1)
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }
            
            myTransform.SetParent(null);

            // gravity scale = 1 so it will fall
            myRigidBody.gravityScale = 1;

            // disable dot
            spawnDot = false;
            myEmotion.EmoteDeath();
            LevelHandler.instance.cameraController.StopFollowing();
            myCollider.isTrigger = true;
            PlayDead();

            if (!playerJustDied)
            {
                if (GameManager.instance.uiManager.TurnOnVibration)
                {
                    //VibrateNow();
                    //Invoke("CancelVibration", 0.2f);
                }
                playerJustDied = true;
            }
        }
        else if (!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && !CoinMultiplyMenu.activeSelf && deadState == 2)
        {
            if (!UIManager.Instance.secondChanceCalled)
            {
                UIManager.Instance.CallSecondChanceMenu();
                deadState = 0;
            }
            else
            {
                UIManager.Instance.CallLoseMenu();
                deadState = 0;
            }

            doubleSlingshotCounter = MAXSLINGSHOT;
            doubleSlingshot = 0;
            myEmotion.EmoteIdle();
            isDead = false;
            //deadState = 0; move up to after the menu call, so in second life, it wont play dead movement/animation function twice
            // above theory failed, so ignore that in terms of fixing the mentioned bug
            cancelSlingshotState = 0;
            myCollider.isTrigger = true;
            Cinemachine.SetActive(true);
        }
        
        if (!UIManager.Instance.LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && !CoinMultiplyMenu.activeSelf)
        {
            DropDead();
        }
        else if (UIManager.Instance.LoseMenu.activeSelf || SecondChanceMenu.activeSelf || CoinMultiplyMenu.activeSelf || PostRestartDataHolder.instance.secondLifeUsed)
        {
            // off dead particle system
            dpsEmission = myDeadParticleSystem.emission;
            dpsEmission.enabled = true;

            //GetComponent<SpriteRenderer>().enabled = false;
            //emotionSpriteRend.enabled = false;
            

            //reset
            deadState = 0;
            myRigidBody.velocity = Vector2.zero;
            //myRigidBody.gravityScale = 0; if 0, the emote wont change back to idle
            //========== 20/5 ====================================================================================== 
            spawnDot = false;
            
            //========== 23/5 ======================================================================================
            cancelIndicator.SetActive(false);

            myEmotion.EmoteIdle();
            myRigidBody.gravityScale = 1;

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

        if(deadState == 0 && bounceCounter >= maxBounceCounter)
        {
            myDeadParticleSystem.Play();
            dpsEmission = myDeadParticleSystem.emission;
            dpsEmission.enabled = true;

            GetComponent<SpriteRenderer>().enabled = false;
            emotionSpriteRend.enabled = false;

            deadEffect2.Play();

            deadState = 1;
            
            playerJustDied = false;
        }
        //=======================================================================================================================
        ConfigureTrail();
        //=======================================================================================================================
        BounceRecoverCountdown();
        // 23 / 5 ======================================================================================================================
        if(deadState != 0)
        {
            for (int m = 0; m < numDots; m++)
            {
                dots[m].mySR.enabled = false;
            }
            spawnDot = false;
            cancelIndicator.SetActive(false);
        }

        if(doubleSlingshotCounter > MAXSLINGSHOT)
        {
            doubleSlingshotCounter = MAXSLINGSHOT;
        }

        if(doubleSlingshotCharge == 5)
        {
            doubleSlingshotCounter += 3;
            doubleSlingshotCharge = 0;
        }
        
        CheckScaling();
    }

    void SlingShot()
    {
        if (doubleSlingshotCounter <= 0)
        {
            doubleSlingshot = 2;
        }
        else if (doubleSlingshotCounter >= MAXSLINGSHOT)
        {
            doubleSlingshot = 0;
        }

        if (mainMenu.GetComponent<AnimationEvent>().canDrag == true)
        {
            if (myMoveStick == MoveState.STICK)
            {
                if (Time.timeScale != 1)
                {
                    Time.timeScale = 1;
                }

                if (bounceCounter != 0 && deadState == 0)
                {
                    bounceCounter = 0;
                    //maxBounceCounterBar = maxBounceCounter;
                }

                // use mouse to test movement without concerning control
                if (Input.GetMouseButtonDown(0) && !PauseScreen.activeInHierarchy)
                {
                    initialInputPosition = (Vector2)Input.mousePosition;
                    //initialInputPosition = Camera.main.ScreenToWorldPoint(initialInputPosition);
                    spawnDot = true;
                    //isCancel = true;
                    mousePressed = true;

                    // myAnimation.PlayHold();
                    myEmotion.EmoteBeforeFlying();

                    cancelIndicator.GetComponent<CancelIndicator>().screenPos = initialInputPosition;
                    cancelIndicator.SetActive(true);
                }

                if (mousePressed)
                {
                    CancelSlingShot();
                }

                if (Input.GetMouseButtonUp(0) && !isCancel)
                {
                    finalInputPosition = (Vector2)Input.mousePosition;
                    //finalInputPosition = Camera.main.ScreenToWorldPoint(finalInputPosition);
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
                    isSticking = false;
                    AudioManager.PlaySound(AudioManager.Sound.PlayerUnstick);

                    //========== 20/5 ====================================================================================== 
                    if (hand != null && HandTutorial.tutorialCounter == 0)
                    {
                        hand.OffTutorial();
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    mousePressed = false;
                    spawnDot = false;
                    myEmotion.EmoteIdle();

                    

                    cancelIndicator.SetActive(false);
                }
            }
            else if (myMoveStick == MoveState.FLYING && doubleSlingshot == 0)
            {
                //========== 20/5 ====================================================================================== 
                if (hand != null)
                {
                    screenMid = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
                    hand.OnTutorial(new Vector2(screenMid.x, screenMid.y));
                }

                // use mouse to test movement without concerning control
                if (Input.GetMouseButtonDown(0))
                {
                    initialInputPosition = (Vector2)Input.mousePosition;
                    //initialInputPosition = Camera.main.ScreenToWorldPoint(initialInputPosition);
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
                    //finalInputPosition = Camera.main.ScreenToWorldPoint(finalInputPosition);
                    slingshotVelocity = SlingshotVelocityCalculation();

                    myRigidBody.velocity = slingshotVelocity;

                    // reset gravity
                    myTransform.SetParent(null);
                    myRigidBody.gravityScale = 1;
                    spawnDot = false;
                    isCancel = false;

                    // slingshot once
                    doubleSlingshot = 1;
                    doubleSlingshotCounter -= DECREMENTSLINGSHOT;



                    //========== 20/5 ====================================================================================== 
                    if (HandTutorial.tutorialCounter < 2)
                    {
                        hand.OffTutorial();
                    }
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
        
    }

    void Ripple()
    {
        //! RIPPLE EFFECT
        FindObjectOfType<RippleEffect>().refractionStrength = 0.1f;
        FindObjectOfType<RippleEffect>().reflectionStrength = 0.3f;
        FindObjectOfType<RippleEffect>().waveSpeed = 1.0f;
        FindObjectOfType<RippleEffect>().dropInterval = 0.3f;
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

    }

    public void CancelSlingShot()
    {
        Vector2 currentMousePos = Input.mousePosition;
        //currentMousePos = Camera.main.ScreenToWorldPoint(currentMousePos);
        Vector2 tempMousePos = Camera.main.ScreenToWorldPoint(currentMousePos);
        Vector2 tempInputPos = Camera.main.ScreenToWorldPoint(initialInputPosition);
        Vector2 launchVelocity = tempMousePos - tempInputPos;

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
            magnitude = magnitude / 450f;
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
            prevMagnitude = Mathf.MoveTowards(prevMagnitude, 0, Time.deltaTime * prevSlingShotVelocity * 1.6f);

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
                    //==================================================================================================
                    // hit dangerous platform effects code go here
                    /*
                    for(int i=0; i< dangerEffects.Length; i++)
                    {
                        dangerEffects[i].TouchedDangerousEffect(myTransform.position, currentVelocity);
                    }
                    */
                    // change to particle system for now
                    myDeadParticleSystem.Play();
                    dpsEmission = myDeadParticleSystem.emission;
                    dpsEmission.enabled = true;

                    GetComponent<SpriteRenderer>().enabled = false;
                    emotionSpriteRend.enabled = false;

                    deadEffect2.Play();

                    //==================================================================================================
                    deadState = 1;

                    playerJustDied = false;
                }
            }
            else if (collision.collider.CompareTag(surfaceTag))
            {
                myRigidBody.velocity = Vector2.zero;
                surfaceStickCount = collision.gameObject.GetComponent<Surfaces>().stickCount;
                isSticking = true;
                stick++;
                if (isRareSkin == true)
                {
                    doubleSlingshotCounter += INCREMENTSLINGSHOT;
                }

                // reset slingshot after once double slingshot if not in recover
                if (doubleSlingshot != 2)
                {
                    doubleSlingshot = 0;
                }

                if (tutorialManager.isTutorial == true)
                {
                    if (stick == 1)
                    {
                        tutorialManager.ShowTutorial2();
                    }
                    else if (stick == 2)
                    {
                        tutorialManager.ShowTutorial3();
                    }
                    else if (stick == 3)
                    {
                        tutorialManager.ShowTutorial4();
                    }
                    else if (stick == 4)
                    {
                        tutorialManager.ShowTutorial5();
                    }
                    else
                    {
                        tutorialManager.ShowTutorial6();
                    }
                }
            }
            else if (collision.collider.CompareTag(horizontalWall))
            {
                if(!isBounceRecover && isSticking == false)
                {
                    isBounceRecover = true;
                    bounceCounter++;
                    Debug.Log("RUn");
                    GameManager.instance.bounceCounterInAGame++;
                }
                
                if (isSticking == false)
                {
                    doubleSlingshotCharge += INCREMENTSLINGSHOT;
                }
               

                ScreenEffectManager.instance.ShakeCamera(ShakeVariation.HittingWall);

                // ===================================================================================================================================
                // get info to spawn relative smoke effect
                Vector2 pos = collision.GetContact(0).point;
                collideWallPoint = pos;
                float angle = Mathf.Atan2(pos.y - myTransform.position.y, pos.x - myTransform.position.x);
                angle *= Mathf.Rad2Deg;
                angle += 90;
                mySmokeEffect.SpawnSmoke(myTransform.position, 3, angle, "WallBounce");
                
                FindObjectOfType<BGMAudioManager>().Play("WallBouncing");
                // ===================================================================================================================================


            }

            if (collision.collider.gameObject.name == "FirstInitialPlatform")
            {
                myEmotion.EmoteIdle();
            }

            // stick on the surface
            if (collision.collider.CompareTag(surfaceTag) && myMoveStick == MoveState.FLYING && surfaceStickCount == 0) 
            {
                //========== 20/5 ====================================================================================== 
                if (hand != null && HandTutorial.tutorialCounter < 2)
                {
                    hand.OffSprite();
                }

                myRigidBody.velocity = Vector2.zero;
                AudioManager.PlaySound(AudioManager.Sound.PlayerStick);
                isSticking = true;
                GameManager.instance.stickCounterInAGame++;
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
                    /*
                    float angle = Mathf.Atan2(currentSurface.gameObject.transform.position.y - myTransform.position.y, currentSurface.gameObject.transform.position.x - myTransform.position.x);
                    angle *= Mathf.Rad2Deg;
                    angle += 90;
                    mySmokeEffect.SpawnSmoke(collision.GetContact(0).point, 1, angle, "SafePlatform"); */
                    StickOnStaticSafePlatformEffect.Play();
                }
                else if(currentSurface.thisType == Surfaces.SurfaceTypes.Moving)
                {
                    /*
                    float angle = Mathf.Atan2(currentSurface.gameObject.transform.position.y - myTransform.position.y, currentSurface.gameObject.transform.position.x - myTransform.position.x);
                    angle *= Mathf.Rad2Deg;
                    angle += 90;
                    mySmokeEffect.SpawnSmoke(collision.GetContact(0).point, 2, angle, "MovingPlatform"); */
                    StickOnMovingSafePlatformEffect.Play();

                   LevelHandler.instance.cameraController.currentSurface = currentSurface.gameObject;
                 //  LevelHandler.instance.cameraController.cameraState = CameraFollowingState.ONMOVINGPLATFORM;
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
        if (collision.collider.CompareTag(surfaceTag) && deadState != 1)
        {
            surfaceStickCount = collision.gameObject.GetComponent<Surfaces>().stickCount;

            if(doubleSlingshot != 2)
            {
                doubleSlingshot = 0;
            }
            
            // ===================================================================================================================================
            // get info to spawn relative smoke effect
            Surfaces currentSurface = collision.collider.gameObject.GetComponent<Surfaces>();
            if (currentSurface.thisType == Surfaces.SurfaceTypes.Safe)
            {
                /*
                float angle = Mathf.Atan2(currentSurface.gameObject.transform.position.y - myTransform.position.y, currentSurface.gameObject.transform.position.x - myTransform.position.x);
                angle *= Mathf.Rad2Deg;
                angle -= 90;
                //mySmokeEffect.SpawnSmoke(myTransform.position, 1, angle, "SafePlatform"); */
                UnstickFromStaticSafePlatformEffect.Play();
            }
            else if (currentSurface.thisType == Surfaces.SurfaceTypes.Moving)
            {
                /*
                float angle = Mathf.Atan2(currentSurface.gameObject.transform.position.y - myTransform.position.y, currentSurface.gameObject.transform.position.x - myTransform.position.x);
                angle *= Mathf.Rad2Deg;
                angle -= 90;
                mySmokeEffect.SpawnSmoke(myTransform.position, 2, angle, "MovingPlatform"); */
            }
            // ===================================================================================================================================
        }
        else if(collision.collider.CompareTag(horizontalWall))
        {
            // ===================================================================================================================================
            // get info to spawn relative smoke effect
            Vector2 pos = collideWallPoint;
            float angle = Mathf.Atan2(pos.y - myTransform.position.y, pos.x - myTransform.position.x);
            angle *= Mathf.Rad2Deg;
            angle -= 90;
            mySmokeEffect.SpawnSmoke(myTransform.position, 3, angle, "WallBounce");
            // ===================================================================================================================================

        }

        if (collision.collider.CompareTag(surfaceTag) && surfaceStickCount == 1 && myMoveStick == MoveState.FLYING)
        {
            surfaceStickCount = 2;
            collision.gameObject.GetComponent<Surfaces>().stickCount = surfaceStickCount;
            // myAnimation.PlayIdle();
            myEmotion.EmoteIdle();
           // LevelHandler.instance.cameraController.cameraState = CameraFollowingState.NORMAL;
            LevelHandler.instance.cameraController.currentSurface = null;

            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 cameraBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (other.CompareTag(deadlyTag))
        {
            myRigidBody.velocity = Vector2.zero;

            AudioManager.PlaySound(AudioManager.Sound.PlayerDie);

            // Die and Second Chance Menu pop out
            //UIManager.Instance.CallSecondChanceMenu();
            if (deadState == 0)
            {
                myDeadParticleSystem.Play();
                dpsEmission = myDeadParticleSystem.emission;
                dpsEmission.enabled = true;

                GetComponent<SpriteRenderer>().enabled = false;
                emotionSpriteRend.enabled = false;

                deadEffect2.Play();

                deadState = 1;

                playerJustDied = false;
            }
        }

        

    }

    private void OnTriggerStay2D(Collider2D other)
    {
            if (other.CompareTag(smallCollider))
            {
                Debug.Log("Small");
                ChangeSize(false);
                // this.gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.0f);
                transform.localScale = Vector3.MoveTowards(transform.localScale, scale, 2.0f * Time.deltaTime);

                //! To Move Player Towards Platform, So there is no gap because of Sucking Behaviour
                distToSmallCol = Vector2.LerpUnclamped(transform.position, other.transform.position, 0.02f);
                transform.position = Vector2.MoveTowards(transform.position, distToSmallCol, 0.5f);
            }

            if (other.CompareTag(bigCollider))
            {
                Debug.Log("Big");
                ChangeSize(true);
                //this.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.0f);
                transform.localScale = Vector3.MoveTowards(transform.localScale, scale, 2.0f * Time.deltaTime);
            }
        

        // If you don't want an eased scaling, replace the above line with the following line
        //   and change speed to suit:
        // transform.localScale = Vector3.MoveTowards (transform.localScale, targetScale, speed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(smallCollider))
        {
            absorbSmallerEffect.Stop();
            transform.localScale = new Vector2 (transform.localScale.x,transform.localScale.y);
        }

        if (collision.CompareTag(bigCollider))
        {
            absorbBiggerEffect.Stop();
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
    }

    void ChangeSize(bool bigger)
    {
        if (tutorialManager.isTutorial == false)
        {
            if (bigger == true)
            {
                curScale = curScale + (0.5f * Time.deltaTime);

                if (transform.localScale.x < 2.69f)
                {
                    absorbBiggerEffect.Play();
                }
            }

            if (bigger == false)
            {
                curScale = curScale - (0.5f * Time.deltaTime);

                if (transform.localScale.x > 0.81f)
                {
                    absorbSmallerEffect.Play();
                }
            }

            curScale = Mathf.Clamp(curScale, 0.8f, 2.7f);

            scale = baseScale * curScale;
        }

        CheckScaling();
    }

    void CheckScaling()
    {
        if (transform.localScale.x < 0.81f)
        {
            absorbSmallerEffect.Stop();
        }

        if(transform.localScale.x > 2.69f)
        {
            absorbBiggerEffect.Stop();
        }
        
        absorbBiggerShape.radius = transform.localScale.x * 0.22f;
        absorbSmallerShape.radius = transform.localScale.x * 0.22f;
    }

    private void DotsSpawner()
    {
        // 23/5=======================================================================================================================
        if (dotList == null)
        {
            dotList = new List<GameObject>();
        }

        if (spawnDot)
        {
            
            Vector2 myPosition = myTransform.position;
            currentInputPosition = Input.mousePosition;
            //currentInputPosition = Camera.main.ScreenToWorldPoint(currentInputPosition);
            
            for(int h=0; h<numDots; h++)
            {
                float temp = dots[h].num;
                if (temp >= 10)
                {
                    temp = 0;
                }
                else
                {
                    temp += Time.unscaledDeltaTime;
                }
                dots[h].num = temp;

                // set position based on calculation the position of dots over time
                Vector2 tempDotPosition = CalculatePosition(temp * dotsPositionOverTime) + myPosition;
                
                if (h - 1 > 0)
                {
                    previousDotObject = trajectoryDots[h - 1];
                }
                else
                {
                    previousDotObject = gameObject;
                }

                if(temp < 1)
                {
                    previousDotObject = gameObject;
                }

                CalculateDotAngle(previousDotObject.transform.position, trajectoryDots[h].transform.position);

                trajectoryDots[h].transform.SetPositionAndRotation(tempDotPosition, Quaternion.AngleAxis(dotAngle, new Vector3(0.0f, 0.0f, 1.0f)));

            }

            float cache = 11;
            for (int i = 0; i < numDots; i++)
            {
                if (i - 1 >= 0)
                {
                    if (dots[i].num < dots[i - 1].num)
                    {
                        previousDotObject = gameObject;
                    }
                    else if (dots[i].num > dots[i - 1].num)
                    {
                        previousDotObject = trajectoryDots[i - 1];
                    }
                }
                else
                {
                    if (dots[i].num < dots[numDots - 1].num)
                    {
                        previousDotObject = gameObject;
                    }
                    else if (dots[i].num > dots[numDots - 1].num)
                    {
                        previousDotObject = trajectoryDots[numDots - 1];
                    }
                }

                #region old wall
                /* 
                Vector2 temp = CalculateDotHitWall(previousDotObject, trajectoryDots[i]);

                if (temp != Vector2.zero)
                {
                    float bounceXPos = trajectoryDots[i].transform.position.x + ((temp.x - trajectoryDots[i].transform.position.x) * 2.0f); 
                    trajectoryDots[i].transform.position = new Vector2(bounceXPos, trajectoryDots[i].transform.position.y);

                }

                // second check
                Vector2 temp2 = CalculateDotHitWall(previousDotObject, trajectoryDots[i]);

                if (temp2 != Vector2.zero)
                {
                    float bounceXPos2 = trajectoryDots[i].transform.position.x + ((temp2.x - trajectoryDots[i].transform.position.x) * 2.0f);
                    trajectoryDots[i].transform.position = new Vector2(bounceXPos2, trajectoryDots[i].transform.position.y);

                }

                // third check
                // second check
                Vector2 temp3 = CalculateDotHitWall(previousDotObject, trajectoryDots[i]);
                bool wallDie = false;
                if (temp3 != Vector2.zero)
                {
                    wallDie = true;
                }
                */
                #endregion

                // if (DotHitsSurface(previousDotObject, trajectoryDots[i]))
                //  {
                // 23/5=======================================================================================================================
                DotHitsSurface(previousDotObject, trajectoryDots[i]);
            }
            
            if (dotList != null)
            {
                for (int l = 0; l < dotList.Count(); l++)
                {
                    if (dotList[l].GetComponent<Dot>().num < cache)
                    {
                        cache = dotList[l].GetComponent<Dot>().num;
                    }
                }

                for (int j = 0; j < numDots; j++)
                {
                    if (dots[j].num >= cache)
                    {
                        dots[j].mySR.enabled = false;
                    }
                    else
                    {
                        dots[j].mySR.enabled = true;
                    }
                }


            }

            for (int j = 0; j < numDots; j++)
            {
                if (dots[j].num >= cache)
                {
                    dots[j].mySR.enabled = false;
                }
                else
                {
                    dots[j].mySR.enabled = true;
                }
            }
            
            dotList.Clear();


            //     return;
            //   }
            /*
            else
            {
                dots[i].mySR.enabled = true;
            }
            */
            /*  if (!DotHitsSurface(previousDotObject, trajectoryDots[i]) )// && !wallDie)
              {
                  dots[i].mySR.enabled = true;
              }
              */
            //}
        }
        else
        {
            for(int m=0; m<numDots; m++)
            {
                dots[m].mySR.enabled = false;
                trajectoryDots[m].GetComponent<Dot>().num = m;
            }
            shakeTimer = 0;

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
    private Vector2 CalculateDotHitWall(GameObject prevDot, GameObject currentDot)
    {
        RaycastHit2D hit;
        RaycastHit2D wphit;

        Vector2 direction = currentDot.transform.position - prevDot.transform.position;
        float distance = direction.magnitude;


        Vector2 direction2 = currentDot.transform.position - myTransform.position;
        float distance2 = direction2.magnitude;

        // layer mask 12 == wall
        hit = Physics2D.Raycast(prevDot.transform.position, direction, distance, lm.value);
        wphit = Physics2D.Raycast(myTransform.position, direction2, distance2, lm.value);

        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;

        CircleCollider2D prevDotCollider = prevDot.GetComponent<CircleCollider2D>();
        CircleCollider2D currentDotCollider = currentDot.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(myCollider, currentDotCollider);
        Physics2D.IgnoreCollision(prevDotCollider, currentDotCollider);

        // The WALL ================================================================================================
        if (hit && hit.collider.gameObject.CompareTag(horizontalWall) && wphit.collider.gameObject.CompareTag(horizontalWall))
        {
            return hit.point;
        }

        return Vector2.zero;
    }

    void DotHitsSurface(GameObject prevDot, GameObject currentDot)
    {
        RaycastHit2D hit;

        Vector2 direction = currentDot.transform.position - prevDot.transform.position;
        float distance = direction.magnitude;

        hit = Physics2D.Raycast(prevDot.transform.position, direction, distance);
        Debug.DrawRay(prevDot.transform.position, direction);

        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;

        CircleCollider2D prevDotCollider = prevDot.GetComponent<CircleCollider2D>();
        CircleCollider2D currentDotCollider = currentDot.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(prevDotCollider, currentDotCollider);

        // hit wall and disappear
        // no bounce for trajectory dot, mainly because corner cannot be identified and raycast checking issues
        if (hit)// && !hit.collider.gameObject.CompareTag(horizontalWall))
        {
            // 23/5=======================================================================================================================
            if (dotList != null)
            {
                if(dotList.Count() == 0)
                {
                    dotList.Add(currentDot);
                }
                else
                {
                    for (int i = 0; i < dotList.Count(); i++)
                    {
                        if (dotList[i] == currentDot)
                        {
                            break;
                        }
                        else
                        {
                            if (i <= dotList.Count() - 1)
                            {
                                dotList.Add(currentDot);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        
                    }
                }

            }
            else
            {

                Debug.Log("NULLNULLNULL");
            }

            //return true;
        }

        //return false;
    }

    public void DistanceCounter()
    {
        tempCurrentDistance = this.gameObject.transform.position.y;

        

        if (tempCurrentDistance > initialPosition)
        {
            playerDistance += (tempCurrentDistance - initialPosition);
            initialPosition = this.transform.position.y;

            distanceCounter = initialDistance + playerDistance;

            distanceCounterText.text = distanceCounter.ToString("F1") + " mm";
            GameManager.instance.playerDistanceTraveled = distanceCounter;
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
            Cinemachine.GetComponent<MixingCameraController>().enabled = false;
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
                myDeadParticleSystem.Play();
                dpsEmission = myDeadParticleSystem.emission;
                dpsEmission.enabled = true;

                GetComponent<SpriteRenderer>().enabled = false;
                emotionSpriteRend.enabled = false;

                deadEffect2.Play();

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

    //public void VibrateNow()
    //{
    //    Vibrator.Vibrate(200);
    //}

    //void CancelVibration()
    //{
    //    Vibrator.Cancel();
    //}

    void Falling()
    {
        if (myRigidBody.velocity.y != 0)
        {
            myEmotion.EmoteFlying();
        }
    }

    void TrailSizing()
    {
        myTrailRenderer.widthMultiplier = transform.localScale.x;
    }

    void ConfigureTrail()
    {
        if(myMoveStick == MoveState.STICK || deadState != 0)
        {
            myTrailRenderer.enabled = false;
            myParticleSystem.OffParticleSystem();
            doubleSSEffect = false;

            flyingParticleSystem.Stop();

            FindObjectOfType<RippleEffect>().refractionStrength = 0.4f;

        }
        else if(myMoveStick == MoveState.FLYING && deadState == 0)
        {
            if(Time.timeScale == 1 && !doubleSSEffect)
            {
                myTrailRenderer.enabled = true;

                flyingParticleSystem.Play();


            }
            else
            {
                doubleSSEffect = true;
                myTrailRenderer.enabled = false;
                myParticleSystem.DoubleSlingshotEffect();

                flyingParticleSystem.Stop();

                if (ripplePeriod > 0.1f)
                {
                    Ripple();
                    ripplePeriod = 0;
                }

                ripplePeriod += Time.deltaTime;

                

                if (period > delay)
                {
                    FindObjectOfType<BGMAudioManager>().Play("DoubleSlingshot");

                    period = 0;
                }

                period += Time.deltaTime;

                
            }
        }

        if (myTrailRenderer.enabled == false)
        {
            myTrailRenderer.Clear();

            
        }
    }

    void BounceRecoverCountdown()
    {
        if(isBounceRecover)
        {
            if(bounceRecoverCounter > bounceRecoverTimer)
            {
                isBounceRecover = false;
                bounceRecoverCounter = 0;
            }
            else
            {
                bounceRecoverCounter += Time.unscaledDeltaTime;
            }
        }
    }
}

/*
 * 
 *
 * 
 */
