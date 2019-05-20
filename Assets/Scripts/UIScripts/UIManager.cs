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
        ShopMenu.SetActive(false);

        highScoreInMainMenu.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("F1") + " mm";

        TutorialScreen.SetActive(false);

        secondChanceCalled = PlayerPrefs.GetInt("SecondChanceCalled") == 1 ? true : false;

        #region Settings Screen

        SettingsScreen.SetActive(false);
        settingsScreenAnim = SettingsScreen.GetComponent<Animator>();

        #endregion

        PauseMenu.SetActive(false);

        CoinMultiplyPanel.SetActive(false);

        continueFillTimer = continueFillDuration;

        QuitPrompt.SetActive(false);

        MainMenuAnim = MainMenu.GetComponent<Animator>();

        #region Sound & Vibrate Button in Settings Menu

        // Get boolean using PlayerPrefs
        TurnOnSound = PlayerPrefs.GetInt("TurnOnSound") == 1 ? true : false;
        TurnOnVibration = PlayerPrefs.GetInt("TurnOnVibration") == 1 ? true : false;

        #endregion

        OpalCounterAnim = GameObject.FindGameObjectWithTag("OpalCounter").GetComponent<Animator>();
        OpalEffectAnim = GameObject.FindGameObjectWithTag("UIOpal").GetComponent<Animator>();

        #region Garage Transitioning

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
    }

    private void Update()
    {
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

        if(ContinueFill.activeInHierarchy)
        {
            continueFillTimer -= Time.deltaTime;
            ContinueFill.GetComponent<Image>().fillAmount = continueFillTimer / continueFillDuration;
        }
        else
        {
            if(continueFillTimer != continueFillDuration)
            {
                continueFillTimer = continueFillDuration;
            }
        }

        if(continueFillTimer <= 0f)
        {
            //closingSecondChanceMenu = true;

            CallLoseMenu();
        }

        //if (callSecondChanceMenu)
        //{
        //    for (int i = 0; i < ContinueUI.Length; i++)
        //    {
        //        ContinueUI[i].GetComponent<RectTransform>().position = Vector3.Lerp(ContinueUI[i].GetComponent<RectTransform>().position, new Vector3(Screen.width/2f,Screen.height/2f,0f), 4f * Time.deltaTime);
        //    }
        //    if (Vector3.Distance(ContinueUI[0].GetComponent<RectTransform>().position, new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)) < 10f)
        //    {
        //        for (int i = 0; i < ContinueUI.Length; i++)
        //        {
        //            ContinueUI[i].GetComponent<RectTransform>().position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        //        }
        //        callSecondChanceMenu = false;
        //    }
        //}

        //if (closingSecondChanceMenu)
        //{
        //    for (int i = 0; i < ContinueUI.Length; i++)
        //    {
        //        ContinueUI[i].GetComponent<RectTransform>().position = Vector3.Lerp(ContinueUI[i].GetComponent<RectTransform>().position, ContinueUI[i].GetComponent<UITransition>().startPos, 4f * Time.deltaTime);
        //    }
        //    if (Vector3.Distance(ContinueUI[0].GetComponent<RectTransform>().position, ContinueUI[0].GetComponent<UITransition>().startPos) < 13f)
        //    {
        //        for (int i = 0; i < ContinueUI.Length; i++)
        //        {
        //            ContinueUI[i].GetComponent<RectTransform>().position = ContinueUI[i].GetComponent<UITransition>().startPos;
        //        }
        //        closingSecondChanceMenu = false;
        //        if (ButtonManager.instance.secondlife == true)
        //        {
        //            SecondChanceMenu.SetActive(false);
        //            ReloadScene();
        //        }
        //        else
        //        {
        //            SecondChanceMenu.SetActive(false);
        //            callLoseMenu = true;
        //        }
        //    }
        //}

        //if(callLoseMenu)
        //{
        //    LoseUI[0].GetComponent<RectTransform>().position = Vector3.Lerp(LoseUI[0].GetComponent<RectTransform>().position, new Vector3(Screen.width / 2f, Screen.height / 2f, 0f), 4f * Time.deltaTime);
        //    if(Vector3.Distance(LoseUI[0].GetComponent<RectTransform>().position, new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)) < 8f)
        //    {
        //        LoseUI[0].GetComponent<RectTransform>().position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        //        callLoseMenu = false;
        //    }
        //}

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPrompt.SetActive(true);
        }

        CheckSoundVibrationSetting();

        CheckOpalUIAnimation();

        CheckPauseButton();
    }

    public void ClosePrompt()
    {
        QuitPrompt.SetActive(false);
    }

    public void CallLoseMenu()
    {
        secondChanceMenuAnim.SetBool("OpenSecondChanceMenu", false);

        AudioManager.PlaySound(AudioManager.Sound.Lose);
        closingSecondChanceMenu = true;
        //callSecondChanceMenu = false;
        
        // Update score
        GameManager.instance.SaveCoin();
        GameManager.instance.SaveScore();
        scoreInGameUI.SetActive(false);
        currentScore.text = player.GetComponent<Movement>().playerDistance.ToString("F1") + " mm";
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
        MainMenuAnim.SetBool("CloseMenu",true);
        LevelHandler.instance.cameraController.gameObject.GetComponent<MixingCameraController>().enabled = true;
        Invoke("CallClosingMainMenu", 1.2f);
        playerMovement.enabled = true;
        playerColliderConroller.enabled = true;

        Invoke("OpeningInGameUI", 0.6f);

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
        GameManager.instance.SaveCoin();
        GameManager.instance.SaveScore();
        //callSecondChanceMenu = true;
        //GameManager.instance.LoadData();

        AudioManager.PlaySound(AudioManager.Sound.Continue);
        continueScore.text = player.GetComponent<Movement>().playerDistance.ToString("F1") + " mm";
        SecondChanceMenu.SetActive(true);

        secondChanceCalled = true;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondChanceCalled", secondChanceCalled ? 1 : 0);

        ClosingInGameUI();
    }

    public void CloseSecondChanceMenu()
    {
        // if (GameManager.instance.GetPoints() >= 4)
        //{

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
       // }
    }

    void TurnOffSecondChanceMenu()
    {
        SecondChanceMenu.SetActive(false);
        ReloadScene();
    }

    public void CheckSecondChanceButton()
    {
        if(GameManager.instance.GetPoints() >= 4)
        {
            SecondChanceButton.interactable = true;
        }
        else if (GameManager.instance.GetPoints() < 4)
        {
            SecondChanceButton.interactable = false;
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
        PauseMenu.SetActive(true);
        Invoke("TimeScale0", 0.5f);

        playerMovement.enabled = false;
        playerColliderConroller.enabled = false;
    }

    public void ResumeGame()
    {
        PauseMenuAnim.SetBool("OpenPauseMenu", false);
        Time.timeScale = 1f;

        Invoke("TurnOffPauseMenu", 0.5f);

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
        SettingsScreen.SetActive(true);
        //MainMenu.SetActive(false);
    }

    public void CloseSettingsScreen()
    {
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

            
            
        }
        else if(!TurnOnSound)
        {
            OnSoundButton.GetComponent<Image>().sprite = OnIdleSoundButton;
            OffSoundButton.GetComponent<Image>().sprite = OffPressedSoundButton;

           
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
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnSound", TurnOnSound ? 1 : 0);
    }

    public void DeactivateSound()
    {
        TurnOnSound = false;
        CustomAudioHandler.instance.Mute();
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnSound", TurnOnSound ? 1 : 0);
    }

    public void ActivateVibration()
    {
        TurnOnVibration = true;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnVibration", TurnOnVibration ? 1 : 0);

        playerMovement.VibrateNow();
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
        if (OpalCounterAnim.GetCurrentAnimatorStateInfo(0).IsName("OnOpalCounter"))
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

    void OpalEffectTransitionBack()
    {
        OpalEffectAnim.SetBool("OpenOpalIcon", false);
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
        CreditsMenu.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
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

    public void MegaShockFBLink()
    {
        Application.OpenURL("https://www.facebook.com/Mega-Shock-Entertainment-2503342479678228/?modal=admin_todo_tour");
    }

    public void MegaShockIGLink()
    {
        Application.OpenURL("https://www.instagram.com/megashockentertainment/");
    }

    public void ShowShopMenu()
    {
        ShopMenu.SetActive(true);
    }

    public void ShowChallengesMenu()
    {
        ChallengesMenu.SetActive(true);
    }

    public void CloseChallengesMenu()
    {
        ChallengesMenu.SetActive(false);
    }

    public void CloseShopMenu()
    {
        ShopMenu.SetActive(false);
    }
}
