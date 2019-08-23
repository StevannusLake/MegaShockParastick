using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    public GameObject LoseMenu;
    public GameObject MainMenu;
    public Movement playerMovement;
    public ColliderController playerColliderConroller;
    public GameObject SecondChanceMenu;
    public GameObject ChallengesMenu;
    public GameObject ShopMenu;
    public Text currentScore;
    public Text highScore;
    public GameObject player;
    public GameObject scoreInGameUI;
    public Text highScoreInMainMenu;
    public Text coinText;
    public Text continueScore;
    public GameObject PauseMenu;
    public Text coinCounterInGame;
    public Text pointCounterInGame;
    public Text pointCounterInSecondChance;
    public bool isPaused = false;
    public GameObject mgFbCoin;
    public GameObject mgInstaCoin;

    //private float delayTimer = 0f;
    //private bool isPaused = false;

    public GameObject TutorialScreen;

    public Button SecondChanceButton;

    public bool secondChanceCalled = false;

    // for transition
    public bool callSecondChanceMenu = false;
    public bool callLoseMenu = false;
    public bool closingSecondChanceMenu = false;

    private Animator secondChanceMenuAnim;

    public GameObject[] ContinueUI;
    public GameObject[] LoseUI;

    #region Settings Screen

    public GameObject SettingsScreen;
    private Animator settingsScreenAnim;

    #endregion

    #region Challenges Screen
    
    private Animator challengesScreenAnim;

    #endregion

    private Animator shopMenuAnim;

    public GameObject FirstInitialPlatform;
    public GameObject SecondLifeInitialPlatform;

    public GameObject SecondChanceUI;
    public GameObject GreenLight;
    public GameObject RedLight;

    public GameObject CoinMultiplyPanel;
    public Text DoubleCoinText;
    private Animator coinMultiplyPanelAnim;

    public GameObject ContinueFill;
    public float continueFillDuration;
    private float continueFillTimer;

    public GameObject QuitPrompt;
    private Animator QuitPromptAnim;

    private Animator MainMenuAnim;

    #region Sound & Vibrate Button in Settings Menu

    public Button OnSoundButton;
    public Button OffSoundButton;
    public Button OnVibrateButton;
    public Button OffVibrateButton;
    
    [HideInInspector] public bool TurnOnSound = false;
    [HideInInspector] public bool TurnOnVibration = false;

    public Sprite OnIdleSoundButton;
    public Sprite OnPressedSoundButton;
    public Sprite OffIdleSoundButton;
    public Sprite OffPressedSoundButton;

    public Sprite OnIdleVibrateButton;
    public Sprite OnPressedVibrateButton;
    public Sprite OffIdleVibrateButton;
    public Sprite OffPressedVibrateButton;

    #endregion

    private Animator OpalCounterAnim;
    private Animator OpalEffectAnim;

    #region Garage Transitioning

    public GameObject GarageTransitioning;
    private Animator GarageAnim;

    #endregion

    #region Credits Menu

    public GameObject CreditsMenu;
    private Animator creditsMenuAnim;

    #endregion

    public GameObject InGameUI;
    private Animator inGameUIAnim;

    private Animator PauseMenuAnim;

    public Button PauseButton;

    public Animator dailyRewardsAnim;

    #region Buttons Disabling
    [Header("ButtonsDisabling")]
    public Button PlayButton;
    public Button ShopButton;
    public Button ChallengesButton;
    public Button LeaderboardButton;
    public Button FreeOpalsMainMenuButton;
    public Button RateUsButton;
    public Button CreditsButton;
    public Button SettingsButton;
    public Button DailyRewardsButton;
    public Button CurrenciesMainMenuButton;

    #endregion

    public RippleEffect rippleEffect;

    public GameObject ShopMenuBlocker;

    public Text timerText;
    public Text timerText2;
    public Text timerText3;
    public Text timerText4;
    public Text timerText5;

    public GameObject SecondChanceBlocker;

    public BGMAudioManager bgmAudioManager;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EnableMenuButtons();

        rippleEffect.enabled = true;

        ShopMenu.SetActive(false);

        ChallengesMenu.SetActive(false);

        highScoreInMainMenu.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("F1") + " mm";

        TutorialScreen.SetActive(false);

        secondChanceCalled = PlayerPrefs.GetInt("SecondChanceCalled") == 1 ? true : false;

        #region Settings Screen

        SettingsScreen.SetActive(false);
        settingsScreenAnim = SettingsScreen.GetComponent<Animator>();

        #endregion

        #region Challenges Screen

        challengesScreenAnim = ChallengesMenu.GetComponent<Animator>();

        #endregion

        shopMenuAnim = ShopMenu.GetComponent<Animator>();

        PauseMenu.SetActive(false);

        CoinMultiplyPanel.SetActive(false);

        continueFillTimer = continueFillDuration;

        QuitPrompt.SetActive(false);
        QuitPromptAnim = QuitPrompt.GetComponent<Animator>();

        MainMenuAnim = MainMenu.GetComponent<Animator>();

        #region Sound & Vibrate Button in Settings Menu

        // Get boolean using PlayerPrefs
        TurnOnSound = PlayerPrefs.GetInt("TurnOnSound") == 1 ? true : false;
        TurnOnVibration = PlayerPrefs.GetInt("TurnOnVibration") == 1 ? true : false;

        #endregion

        OpalCounterAnim = GameObject.FindGameObjectWithTag("OpalCounter").GetComponent<Animator>();
        OpalEffectAnim = GameObject.FindGameObjectWithTag("UIOpal").GetComponent<Animator>();

        #region Garage Transitioning

        GarageTransitioning.SetActive(true);
        GarageAnim = GarageTransitioning.GetComponent<Animator>();

        #endregion

        #region Credits Menu

        CreditsMenu.SetActive(false);
        creditsMenuAnim = CreditsMenu.GetComponent<Animator>();

        #endregion

        secondChanceMenuAnim = SecondChanceMenu.GetComponent<Animator>();

        coinMultiplyPanelAnim = CoinMultiplyPanel.GetComponent<Animator>();

        inGameUIAnim = InGameUI.GetComponent<Animator>();

        PauseMenuAnim = PauseMenu.GetComponent<Animator>();

        CoinCounterSelfOpen();
        OpalCounterTransitionOpen();
        OpalEffectTransitionOpen();

        //AudioManager.StopSound(AudioManager.Sound.LoseMenuBGM);
        //AudioManager.StopSound(AudioManager.Sound.InGameBGM);

        if (MainMenuAnim.GetCurrentAnimatorStateInfo(0).IsName("GameTitleAnim") && !LoseMenu.activeSelf)
        {
            //AudioManager.PlaySound(AudioManager.Sound.MainMenuBGM);
            FindObjectOfType<BGMAudioManager>().Play("MainMenuBGM");
        }

        SecondChanceBlocker.SetActive(false);
    }

    private void Update()
    {
        CheckBGMs();

        ButtonManager.instance.TempScore = player.GetComponent<Movement>().playerDistance;

        //GameManager.instance.SaveData();

        //CheckSecondChanceButton();



        coinCounterInGame.text = "" + GameManager.instance.GetCoin();
        pointCounterInGame.text = "" + GameManager.instance.GetPoints();

        if (SecondChanceMenu.activeInHierarchy)
        {
            pointCounterInSecondChance.text = "" + GameManager.instance.GetPoints();
        }

        ButtonManager.instance.TempScore = player.GetComponent<Movement>().playerDistance;

        if (ContinueFill.activeInHierarchy)
        {

            continueFillTimer -= Time.deltaTime;
            ContinueFill.GetComponent<Image>().fillAmount = continueFillTimer / continueFillDuration;
            
        }
        else
        {
            if (continueFillTimer != continueFillDuration)
            {
                continueFillTimer = continueFillDuration;
            }
        }

        if (continueFillTimer <= 0f)
        {
            //closingSecondChanceMenu = true;

            CallLoseMenu();
        }


        if (secondChanceCalled)
        {
            if (FirstInitialPlatform != null)
            {
                FirstInitialPlatform.SetActive(false);
            }

            if (SecondLifeInitialPlatform != null)
            {
                SecondLifeInitialPlatform.SetActive(true);
            }
        }
        else
        {
            if (FirstInitialPlatform != null)
            {
                FirstInitialPlatform.SetActive(true);
            }

            if (SecondLifeInitialPlatform != null)
            {
                SecondLifeInitialPlatform.SetActive(false);
            }
        }

        LightIndicatorCheck();

        DoubleCoinText.text = ColliderController.tempCollectedCoin.ToString();

        AndroidEscapeButtonCheck();

        CheckSoundVibrationSetting();

        CheckOpalUIAnimation();

        CheckPauseButton();

        //! OOI FREE CURRENCY 
        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", this.gameObject.GetComponent<FreeCurrency1>().differenceForMinQ.Hours, this.gameObject.GetComponent<FreeCurrency1>().differenceForMinQ.Minutes, this.gameObject.GetComponent<FreeCurrency1>().differenceForMinQ.Seconds);
        string timeText2 = string.Format("{0:D2}:{1:D2}:{2:D2}", this.gameObject.GetComponent<FreeCurrency1>().differenceQ.Hours, this.gameObject.GetComponent<FreeCurrency1>().differenceQ.Minutes, this.gameObject.GetComponent<FreeCurrency1>().differenceQ.Seconds);
        string timeText3 = string.Format("{0:D2}:{1:D2}:{2:D2}", this.gameObject.GetComponent<FreeCurrency1>().differenceForMinQ2.Hours, this.gameObject.GetComponent<FreeCurrency1>().differenceForMinQ2.Minutes, this.gameObject.GetComponent<FreeCurrency1>().differenceForMinQ2.Seconds);
        string timeText4 = string.Format("{0:D2}:{1:D2}:{2:D2}", this.gameObject.GetComponent<FreeCurrency1>().differenceQ2.Hours, this.gameObject.GetComponent<FreeCurrency1>().differenceQ2.Minutes, this.gameObject.GetComponent<FreeCurrency1>().differenceQ2.Seconds);

        if (this.gameObject.GetComponent<InternetChecker>().isConnect == true)
        {
            if (this.gameObject.GetComponent<FreeCurrency1>().differenceForMinQ.Seconds > 0)
            {
                timerText4.text = timeText;
            }
            else
            {
                timerText4.text = "Free!";
                
            }
           
            if (this.gameObject.GetComponent<FreeCurrency1>().freeCurrency == 3)
            {
                timerText4.text = timeText2;
            }

            /////////////////////////////////////////////////

            if (this.gameObject.GetComponent<FreeCurrency1>().differenceForMinQ2.Seconds > 0)
            {
                timerText3.text = timeText3;
            }
            else
            {
                timerText3.text = "Free!";
            }

            if (this.gameObject.GetComponent<FreeCurrency1>().freeCurrency2 == 3)
            {
                timerText3.text = timeText4;
            }
        }

        if(PlayerPrefs.GetInt("MSFB") == 1)
        {
            mgFbCoin.SetActive(false);
        }

        if(PlayerPrefs.GetInt("MSIG") == 1)
        {
            mgInstaCoin.SetActive(false);
        }
    }

    public void DisableMenuButtons()
    {
        PlayButton.interactable = false;
        ShopButton.interactable = false;
        ChallengesButton.interactable = false;
        LeaderboardButton.interactable = false;
        FreeOpalsMainMenuButton.interactable = false;
        RateUsButton.interactable = false;
        CreditsButton.interactable = false;
        SettingsButton.interactable = false;
        DailyRewardsButton.interactable = false;
        CurrenciesMainMenuButton.interactable = false;
    }

    public void EnableMenuButtons()
    {
        PlayButton.interactable = true;
        ShopButton.interactable = true;
        ChallengesButton.interactable = true;
        LeaderboardButton.interactable = true;
        FreeOpalsMainMenuButton.interactable = true;
        RateUsButton.interactable = true;
        CreditsButton.interactable = true;
        SettingsButton.interactable = true;
        DailyRewardsButton.interactable = true;
        CurrenciesMainMenuButton.interactable = true;
    }

    void AndroidEscapeButtonCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MainMenu.activeSelf && !SettingsScreen.activeSelf && !CreditsMenu.activeSelf && !ShopMenu.activeSelf && !ChallengesMenu.activeSelf)
        {
            OpenQuitPrompt();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SettingsScreen.activeSelf)
        {
            CloseSettingsScreen();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && CreditsMenu.activeSelf)
        {
            CloseCreditsMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && ShopMenu.activeSelf)
        {
            CloseShopMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && ChallengesMenu.activeSelf)
        {
            CloseChallengesMenu();
        }
    }

    public void OpenQuitPrompt()
    {
        QuitPrompt.SetActive(true);
        QuitPromptAnim.SetBool("OpenQuitMenu", true);
    }

    public void CloseQuitPrompt()
    {
        QuitPromptAnim.SetBool("OpenQuitMenu",false);

        Invoke("TurnOffQuitPrompt",0.5f);
    }

    void TurnOffQuitPrompt()
    {
        QuitPrompt.SetActive(false);
    }

    public void CallLoseMenu()
    {
        SecondChanceBlocker.SetActive(true);

        CoinCounterSelfClose();

        secondChanceMenuAnim.SetBool("OpenSecondChanceMenu", false);

        //AudioManager.PlaySound(AudioManager.Sound.Lose);
        closingSecondChanceMenu = true;
        //callSecondChanceMenu = false;
        
        // Update score
        GameManager.instance.SaveCoin();
        GameManager.instance.SaveScore();
        scoreInGameUI.SetActive(false);
        currentScore.text = player.GetComponent<Movement>().distanceCounter.ToString("F1") + " mm";
        highScore.text = "" + PlayerPrefs.GetFloat("HighScore", 0).ToString("F1") + " mm";
        coinText.text = "$ " + PlayerPrefs.GetInt("Coin", 0);
        //SecondChanceMenu.SetActive(false);

        secondChanceCalled = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondChanceCalled", secondChanceCalled ? 1 : 0);

        CoinMultiplyPanel.SetActive(true);

        //Check mission
        MissionManager.instance.CheckMissionEnd(MissionManager.instance.missions);
        GameManager.instance.SaveTotalValues();

        ClosingInGameUI();

        Invoke("TurnOffSecondChanceMenuToLoseMenu", 1.2f);

        //AudioManager.StopSound(AudioManager.Sound.MainMenuBGM);
        FindObjectOfType<BGMAudioManager>().Stop("MainMenuBGM");
        //AudioManager.StopSound(AudioManager.Sound.InGameBGM);
        FindObjectOfType<BGMAudioManager>().Stop("InGameBGM");
        //AudioManager.PlaySound(AudioManager.Sound.LoseMenuBGM);
        FindObjectOfType<BGMAudioManager>().Play("LoseMenuBGM");

        MissionManager.instance.ResetInGameProgress();
    }

    void TurnOffSecondChanceMenuToLoseMenu()
    {
        SecondChanceMenu.SetActive(false);
        LoseMenu.SetActive(true);
    }

    public void CloseLoseMenu()
    {
        LoseMenu.SetActive(false);

        ButtonManager.instance.secondlife = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondLife", ButtonManager.instance.secondlife ? 1 : 0);

        //! Save Temporary Player Distance
        //PlayerPrefs.SetFloat("TempScore", 0);

        secondChanceCalled = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondChanceCalled", secondChanceCalled ? 1 : 0);
    }

    public void CallMainMenu()
    {
        rippleEffect.enabled = true;

        Time.timeScale = 1f;

        //MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        LoseMenu.SetActive(false);
        playerMovement.enabled = false;
        playerColliderConroller.enabled = false;

        secondChanceCalled = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondChanceCalled", secondChanceCalled ? 1 : 0);

        ClosingGarage();

        Invoke("ReloadScene",1.2f);
    }

    public void CloseMainMenu()
    {
        if (!LoseMenu.activeSelf)
        {
            //AudioManager.PlaySound(AudioManager.Sound.InGameBGM);
            FindObjectOfType<BGMAudioManager>().Play("InGameBGM");
            FindObjectOfType<BGMAudioManager>().Play("WaterBGM");
            Debug.Log("NewWaterJustIN!");
        }

        dailyRewardsAnim.Play("DailyRewardsButtonClose");

        MainMenuAnim.SetBool("CloseMenu",true);
        LevelHandler.instance.cameraController.gameObject.GetComponent<MixingCameraController>().enabled = true;
        Invoke("CallClosingMainMenu", 1.2f);
        playerMovement.enabled = true;
        playerColliderConroller.enabled = true;
        
        Invoke("OpeningInGameUI", 0.6f);

        //CoinCounterSelfClose();
        OpalCounterTransitionBack();
        OpalEffectTransitionBack();
        
        DisableMenuButtons();
    }

    void CheckBGMs()
    {
        //! STOPPING SOUND
        if (secondChanceMenuAnim.GetCurrentAnimatorStateInfo(0).IsName("SecondChanceAnimClose"))
        {
            //AudioManager.StopSound(AudioManager.Sound.InGameBGM);
            FindObjectOfType<BGMAudioManager>().Stop("InGameBGM");
        }

        if (LoseMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("LoseScreenAnim"))
        {
            //AudioManager.StopSound(AudioManager.Sound.InGameBGM);
            FindObjectOfType<BGMAudioManager>().Stop("InGameBGM");
        }

        if (coinMultiplyPanelAnim.GetCurrentAnimatorStateInfo(0).IsName("CoinMultiplyAnim"))
        {
            //AudioManager.StopSound(AudioManager.Sound.InGameBGM);
            FindObjectOfType<BGMAudioManager>().Stop("InGameBGM");
        }

        
        if (MainMenuAnim.GetCurrentAnimatorStateInfo(0).IsName("GameTitleAnimClosing"))
        {
            //AudioManager.StopSound(AudioManager.Sound.MainMenuBGM);
            FindObjectOfType<BGMAudioManager>().Stop("MainMenuBGM");
        }

        /*
        if (shopMenuAnim.GetCurrentAnimatorStateInfo(0).IsName("ShopMenuAnim"))
        {
            //AudioManager.StopSound(AudioManager.Sound.MainMenuBGM);
            FindObjectOfType<BGMAudioManager>().Stop("MainMenuBGM");
        }

        if (challengesScreenAnim.GetCurrentAnimatorStateInfo(0).IsName("ChallengesMenuAnim"))
        {
            //AudioManager.StopSound(AudioManager.Sound.MainMenuBGM);
            FindObjectOfType<BGMAudioManager>().Stop("MainMenuBGM");
        }
        */
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        /*
        //! PLAYING SOUND
        if (shopMenuAnim.GetCurrentAnimatorStateInfo(0).IsName("ShopMenuClose"))
        {
            //AudioManager.PlaySound(AudioManager.Sound.MainMenuBGM);
            FindObjectOfType<BGMAudioManager>().Play("MainMenuBGM");
        }

        if (challengesScreenAnim.GetCurrentAnimatorStateInfo(0).IsName("ChallengesMenuClose"))
        {
            //AudioManager.PlaySound(AudioManager.Sound.MainMenuBGM);
            FindObjectOfType<BGMAudioManager>().Play("MainMenuBGM");
        }
        */
    }

    void CallClosingMainMenu()
    {
        
        MainMenu.SetActive(false);
        
        //! CALL ANIMATOR BOOLEAN BACK
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CallSecondChanceMenu()
    {
        CoinCounterSelfClose();

        GameManager.instance.SaveCoin();
        GameManager.instance.SaveScore();
        //callSecondChanceMenu = true;
        //GameManager.instance.LoadData();

        AudioManager.PlaySound(AudioManager.Sound.Continue);
        continueScore.text = player.GetComponent<Movement>().distanceCounter.ToString("F1") + " mm";
        SecondChanceMenu.SetActive(true);

        secondChanceCalled = true;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondChanceCalled", secondChanceCalled ? 1 : 0);

        ClosingInGameUI();
    }

    public void CloseSecondChanceMenu()
    {
        if (GameManager.instance.GetPoints() >= 4)
        {
            SecondChanceBlocker.SetActive(true);

            secondChanceMenuAnim.SetBool("OpenSecondChanceMenu", false);

            AudioManager.PlaySound(AudioManager.Sound.Reborn);

            GameManager.instance.DecreasePoints(4);
            GameManager.instance.SavePoints();
            GameManager.instance.LoadData();
            PostRestartDataHolder.instance.UseSecondLife();

            ButtonManager.instance.secondlife = true;
            // Save boolean using PlayerPrefs
            PlayerPrefs.SetInt("SecondLife", ButtonManager.instance.secondlife ? 1 : 0);

            //ButtonManager.instance.TempScore = player.GetComponent<Movement>().playerDistance;
            //! Save Temporary Player Distance
            PlayerPrefs.SetFloat("TempScore", player.GetComponent<Movement>().playerDistance);
            
            Movement.deadState = 0;

            ClosingGarage();
            Invoke("TurnOffSecondChanceMenu", 1.2f);
        }
    }

    void TurnOffSecondChanceMenu()
    {
        SecondChanceMenu.SetActive(false);
        ReloadScene();
    }

    public void CheckSecondChanceButton()
    {   
        if(GameManager.instance.secondChanceDiscount == 0)
        {
            if (GameManager.instance.GetPoints() >= 4)
            {
                SecondChanceButton.interactable = true;
            }
            else if (GameManager.instance.GetPoints() < 4)
            {
                SecondChanceButton.interactable = false;
            }
        }
        else
        {
            if (GameManager.instance.GetPoints() >= 2)
            {
                SecondChanceButton.interactable = true;
            }
            else if (GameManager.instance.GetPoints() < 2)
            {
                SecondChanceButton.interactable = false;
            }
        }
    }

    public void ButtonSound()
    {
        AudioManager.PlaySound(AudioManager.Sound.Button);
    }

    public void OpenTutorial()
    {
        TutorialScreen.SetActive(true);
    }

    public void CloseTutorial()
    {
        TutorialScreen.SetActive(false);
    }

    void TimeScale0()
    {
        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        rippleEffect.enabled = false;

        PauseMenu.SetActive(true);
        Invoke("TimeScale0", 0.5f);

        isPaused = true;
        playerMovement.enabled = false;
        playerColliderConroller.enabled = false;
    }

    public void ResumeGame()
    {
        rippleEffect.enabled = true;

        PauseMenuAnim.SetBool("OpenPauseMenu", false);
        Time.timeScale = 1f;

        Invoke("TurnOffPauseMenu", 0.5f);

        isPaused = false;
        playerMovement.enabled = true;
        
        playerColliderConroller.enabled = true;

        
    }

    void TurnOffPauseMenu()
    {
        PauseMenu.SetActive(false);
    }

    void CheckPauseButton()
    {
        if(PauseMenu.activeSelf || SecondChanceMenu.activeSelf || LoseMenu.activeSelf)
        {
            PauseButton.interactable = false;
        }
        else
        {
            PauseButton.interactable = true;
        }
    }

    #region Settings Screen

    public void OpenSettingsScreen()
    {
        DisableMenuButtons();

        SettingsScreen.SetActive(true);
        //MainMenu.SetActive(false);
    }

    public void CloseSettingsScreen()
    {
        EnableMenuButtons();

        settingsScreenAnim.SetBool("OpenSettings", false);
        //MainMenu.SetActive(true);

        Invoke("TurnOffSettingsScreen", 1.2f);
    }

    void TurnOffSettingsScreen()
    {
        SettingsScreen.SetActive(false);
    }

    #endregion

    public void LightIndicatorCheck()
    {
        if(Movement.deadState == 0 && !PauseMenu.activeSelf && !SecondChanceUI.activeSelf && !LoseMenu.activeSelf && !MainMenu.activeSelf)
        {
            GreenLight.SetActive(true);
            RedLight.SetActive(false);
        }
        else
        {
            GreenLight.SetActive(false);
            RedLight.SetActive(true);
        }
    }

    public void GetDoubleCoin()
    {
        //! Play Ad Video

        coinMultiplyPanelAnim.SetBool("OpenCoinMultiplyPanel", false);

        GameManager.instance.AddCoin(ColliderController.tempCollectedCoin);
        GameManager.instance.SaveCoin();
        GameManager.instance.GetCoin();

        Invoke("TurnOffCoinMultiplyPanel", 1.2f);
    }

    public void CloseCoinMultiplyPanel()
    {
        coinMultiplyPanelAnim.SetBool("OpenCoinMultiplyPanel", false);

        Invoke("TurnOffCoinMultiplyPanel", 1.2f);
    }

    void TurnOffCoinMultiplyPanel()
    {
        ColliderController.tempCollectedCoin = 0;
        CoinMultiplyPanel.SetActive(false);
    }

    #region Sound & Vibrate Button in Settings Menu

    void CheckSoundVibrationSetting()
    {
        if (TurnOnSound)
        {
            OnSoundButton.GetComponent<Image>().sprite = OnPressedSoundButton;
            OffSoundButton.GetComponent<Image>().sprite = OffIdleSoundButton;

            CustomAudioHandler.instance.Unmute();
            bgmAudioManager.Unmute();
        }
        else if(!TurnOnSound)
        {
            OnSoundButton.GetComponent<Image>().sprite = OnIdleSoundButton;
            OffSoundButton.GetComponent<Image>().sprite = OffPressedSoundButton;

            CustomAudioHandler.instance.Mute();
            bgmAudioManager.Mute();
        }

        if (TurnOnVibration)
        {
            OnVibrateButton.GetComponent<Image>().sprite = OnPressedVibrateButton;
            OffVibrateButton.GetComponent<Image>().sprite = OffIdleVibrateButton;
        }
        else
        {
            OnVibrateButton.GetComponent<Image>().sprite = OnIdleVibrateButton;
            OffVibrateButton.GetComponent<Image>().sprite = OffPressedVibrateButton;
        }
    }

    public void ActivateSound()
    {
        TurnOnSound = true;

        CustomAudioHandler.instance.Unmute();
        bgmAudioManager.Unmute();

        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnSound", TurnOnSound ? 1 : 0);
    }

    public void DeactivateSound()
    {
        TurnOnSound = false;

        CustomAudioHandler.instance.Mute();
        bgmAudioManager.Mute();

        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnSound", TurnOnSound ? 1 : 0);
    }

    public void ActivateVibration()
    {
        if (OnVibrateButton.GetComponent<Image>().sprite != OnPressedVibrateButton)
        {
            Debug.Log("vibrate testing");
           // playerMovement.VibrateNow();
        }

        TurnOnVibration = true;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnVibration", TurnOnVibration ? 1 : 0);
    }

    public void DeactivateVibration()
    {
        TurnOnVibration = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnVibration", TurnOnVibration ? 1 : 0);
    }

    #endregion

    void CheckOpalUIAnimation()
    {
        if (OpalCounterAnim.GetCurrentAnimatorStateInfo(0).IsName("OnOpalCounter") && !MainMenu.activeSelf)
        {
            Invoke("OpalJumpingEffect", 1f);
            Invoke("OpalCounterTransitionBack", 3f);
            Invoke("OpalEffectTransitionBack", 3f);
        }
    }

    void OpalJumpingEffect()
    {
        OpalEffectAnim.SetBool("GetOpal", true);
    }

    void OpalCounterTransitionBack()
    {
        OpalCounterAnim.SetBool("OpenCounter", false);
    }

    void OpalCounterTransitionOpen()
    {
        OpalCounterAnim.SetBool("OpenCounter", true);
    }

    void OpalEffectTransitionBack()
    {
        OpalEffectAnim.SetBool("OpenOpalIcon", false);
    }

    void OpalEffectTransitionOpen()
    {
        OpalEffectAnim.SetBool("OpenOpalIcon", true);
    }



    #region Garage Transitioning

    public void OpeningGarage()
    {
        GarageAnim.SetBool("OpenGarage", true);
    }

    public void ClosingGarage()
    {
        GarageAnim.SetBool("OpenGarage", false);
    }

    #endregion

    #region Credits Menu

    public void OpenCreditsMenu()
    {
        DisableMenuButtons();

        CreditsMenu.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        EnableMenuButtons();

        creditsMenuAnim.SetBool("OpenCredits",false);
        Invoke("TurnOffCreditsMenu", 1.2f);
    }

    void TurnOffCreditsMenu()
    {
        CreditsMenu.SetActive(false);
    }

    #endregion

    public void OpeningInGameUI()
    {
        inGameUIAnim.SetBool("OpenInGameUI", true);
    }

    public void ClosingInGameUI()
    {
        inGameUIAnim.SetBool("OpenInGameUI", false);
    }

    void CoinCounterSelfOpen()
    {
        inGameUIAnim.SetBool("OpenCoinCounterUI", true);
    }

    void CoinCounterSelfClose()
    {
        inGameUIAnim.SetBool("OpenCoinCounterUI", false);
    }

    public void MegaShockFBLink()
    {   
        if(PlayerPrefs.GetInt("MSFB") == 0)
        {
            GameManager.instance.AddCoin(25);
            PlayerPrefs.SetInt("MSFB", 1);
        }
        
        Application.OpenURL("https://www.facebook.com/Mega-Shock-Entertainment-2503342479678228/?modal=admin_todo_tour");
    }

    public void MegaShockIGLink()
    {
        if(PlayerPrefs.GetInt("MSIG") == 0)
        {
            GameManager.instance.AddCoin(25);
            PlayerPrefs.SetInt("MSIG", 1);
        }      
        Application.OpenURL("https://www.instagram.com/megashockentertainment/");
    }

    public void FeedbackFormLink()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdUg0Og8ut5G6NRw2Ph9N3Cl7lKNclYxcydNldIVKZJFLTWsg/viewform?usp=sf_link");
    }

    public void ShowShopMenu()
    {
        DisableMenuButtons();

        //AudioManager.StopSound(AudioManager.Sound.MainMenuBGM);
        FindObjectOfType<BGMAudioManager>().Stop("MainMenuBGM");
        //AudioManager.PlaySound(AudioManager.Sound.ShopChallengesBGM);
        FindObjectOfType<BGMAudioManager>().Play("ShopChallengesBGM");

        ShopMenu.SetActive(true);
    }

    public void CloseShopMenu()
    {
        EnableMenuButtons();

        ShopMenuBlocker.SetActive(true);

        //AudioManager.StopSound(AudioManager.Sound.ShopChallengesBGM);
        FindObjectOfType<BGMAudioManager>().Stop("ShopChallengesBGM");
        //AudioManager.PlaySound(AudioManager.Sound.MainMenuBGM);
        FindObjectOfType<BGMAudioManager>().Play("MainMenuBGM");

        shopMenuAnim.SetBool("OpenShop", false);
        //MainMenu.SetActive(true);
        
        Invoke("TurnOffShopMenu", 1.2f);
    }

    void TurnOffShopMenu()
    {
        ShopMenuBlocker.SetActive(false);

        ShopMenu.SetActive(false);
    }

    public void ShowChallengesMenu()
    {
        DisableMenuButtons();

        //AudioManager.StopSound(AudioManager.Sound.MainMenuBGM);
        FindObjectOfType<BGMAudioManager>().Stop("MainMenuBGM");

        if (!PauseMenu.activeSelf)
        {
            //AudioManager.PlaySound(AudioManager.Sound.ShopChallengesBGM);
            FindObjectOfType<BGMAudioManager>().Play("ShopChallengesBGM");
        }

        ChallengesMenu.SetActive(true);
        GameManager.instance.achievementMenu.GetComponent<DragController>().initPos = GameManager.instance.achievementMenu.transform.position;
        GameManager.instance.missionMenu.GetComponent<DragController>().initPos = GameManager.instance.missionMenu.transform.position;
    }

    public void CloseChallengesMenu()
    {
        EnableMenuButtons();

        if (PauseMenu.activeSelf)
        {
            ChallengesMenu.SetActive(false);
        }
        else
        {
            challengesScreenAnim.SetBool("OpenChallenges", false);
            //MainMenu.SetActive(true);

            Invoke("TurnOffChallengesScreen", 1.2f);
            
            //AudioManager.StopSound(AudioManager.Sound.ShopChallengesBGM);
            FindObjectOfType<BGMAudioManager>().Stop("ShopChallengesBGM");
            //AudioManager.PlaySound(AudioManager.Sound.MainMenuBGM);
            FindObjectOfType<BGMAudioManager>().Play("MainMenuBGM");
        }
    }

    void TurnOffChallengesScreen()
    {
        ChallengesMenu.SetActive(false);
    }

    public void GarageClosingSound()
    {
        FindObjectOfType<BGMAudioManager>().Stop("InGameBGM");
        AudioManager.PlaySound(AudioManager.Sound.Reborn);
    }
    
}
