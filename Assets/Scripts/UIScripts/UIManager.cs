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

    public GameObject[] ContinueUI;
    public GameObject[] LoseUI;

    public GameObject SettingsScreen;

    public GameObject FirstInitialPlatform;
    public GameObject SecondLifeInitialPlatform;

    public GameObject SecondChanceUI;
    public GameObject GreenLight;
    public GameObject RedLight;

    public GameObject CoinMultiplyPanel;
    public Text DoubleCoinText;

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
        highScoreInMainMenu.text = PlayerPrefs.GetFloat("HighScore",0).ToString("F1")+" mm";

        TutorialScreen.SetActive(false);

        secondChanceCalled = PlayerPrefs.GetInt("SecondChanceCalled") == 1 ? true : false;

        SettingsScreen.SetActive(false);

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
    }

    private void Update()
    {
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
    }

    public void ClosePrompt()
    {
        QuitPrompt.SetActive(false);
    }

    public void CallLoseMenu()
    {
        SecondChanceMenu.SetActive(false);

        AudioManager.PlaySound(AudioManager.Sound.Lose);
        closingSecondChanceMenu = true;
        //callSecondChanceMenu = false;
        LoseMenu.SetActive(true);
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

        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        LoseMenu.SetActive(false);
        playerMovement.enabled = false;
        playerColliderConroller.enabled = false;

        secondChanceCalled = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondChanceCalled", secondChanceCalled ? 1 : 0);
    }

    public void CloseMainMenu()
    {
        MainMenuAnim.SetBool("CloseMenu",true);
        Invoke("CallClosingMainMenu", 1.2f);
            
    }

    void CallClosingMainMenu()
    {
        MainMenu.SetActive(false);
        playerMovement.enabled = true;
        playerColliderConroller.enabled = true;
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
    }

    public void CloseSecondChanceMenu()
    {
       // if (GameManager.instance.GetPoints() >= 4)
        //{
            AudioManager.PlaySound(AudioManager.Sound.Reborn);
            
            GameManager.instance.DecreasePoints(4);
            GameManager.instance.SavePoints();
            GameManager.instance.LoadData();
            PostRestartDataHolder.instance.UseSecondLife();

            ButtonManager.instance.secondlife = true;
            // Save boolean using PlayerPrefs
            PlayerPrefs.SetInt("SecondLife", ButtonManager.instance.secondlife ? 1 : 0);

            ButtonManager.instance.TempScore = player.GetComponent<Movement>().playerDistance;
            //! Save Temporary Player Distance
            PlayerPrefs.SetFloat("TempScore", ButtonManager.instance.TempScore);

            SecondChanceMenu.SetActive(false);
            Movement.deadState = 0;
            ReloadScene();
       // }
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
        Invoke("TimeScale0", 0.7f);
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenSettingsScreen()
    {
        SettingsScreen.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void CloseSettingsScreen()
    {
        SettingsScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

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

        GameManager.instance.AddCoin(ColliderController.tempCollectedCoin);
        GameManager.instance.SaveCoin();
        GameManager.instance.GetCoin();
        CoinMultiplyPanel.SetActive(false);

        ColliderController.tempCollectedCoin = 0;
    }

    public void CloseCoinMultiplyPanel()
    {
        CoinMultiplyPanel.SetActive(false);

        ColliderController.tempCollectedCoin = 0;
    }

    #region Sound & Vibrate Button in Settings Menu

    void CheckSoundVibrationSetting()
    {
        if (TurnOnSound)
        {
            OnSoundButton.GetComponent<Image>().sprite = OnPressedSoundButton;
            OffSoundButton.GetComponent<Image>().sprite = OffIdleSoundButton;

            GameManager.instance.audioSourcePlayer.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            OnSoundButton.GetComponent<Image>().sprite = OnIdleSoundButton;
            OffSoundButton.GetComponent<Image>().sprite = OffPressedSoundButton;

            GameManager.instance.audioSourcePlayer.GetComponent<AudioSource>().enabled = false;
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
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnSound", TurnOnSound ? 1 : 0);
    }

    public void DeactivateSound()
    {
        TurnOnSound = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("TurnOnSound", TurnOnSound ? 1 : 0);
    }

    public void ActivateVibration()
    {
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
        if (OpalCounterAnim.GetCurrentAnimatorStateInfo(0).IsName("OnOpalCounter"))
        {
            Invoke("OpalCounterTransitionBack", 3f);
            Invoke("OpalEffectTransitionBack", 3f);
        }
    }

    void OpalCounterTransitionBack()
    {
        OpalCounterAnim.SetBool("OpenCounter", false);
    }

    void OpalEffectTransitionBack()
    {
        OpalEffectAnim.SetBool("OpenOpalIcon", false);
    }
}
