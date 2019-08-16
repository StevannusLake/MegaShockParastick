using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Assets.SimpleAndroidNotifications;

public class GameManager : MonoBehaviour
{
    public List<string> soundSourcesCreated;
    public int coin = 0;
    public List<GameObject> skinCollected;
    public int numOfSkinCollected = 0;
    public MixingCameraController mixingCameraController;
    public float playerDistanceTraveled;
    public int coinCollectedInAGame = 0;
    public int bounceCounterInAGame = 0;
    public int stickCounterInAGame = 0;
    public GameObject player;
    public GameObject water;
    
    public Movement playerMovement;
    public ColliderController playerColliderController;
    public static GameManager instance;
    public float currentActiveLevelGeneratorID = 0;
    public LevelHandler levelHandler;
    public GameObject audioSourcePlayer;
    public float highScore;
    public int spinCount;
    public int points;
    public UIManager uiManager;
    public float totalDistanceTravelled;
    public int totalCoinCollected;
    public int totalBounce;
    public int totalStick;
    public int totalPlay;
    public int totalPoints;
    public int totalSpin;
    public int secondChanceDiscount = 0;
    public GameObject ParasiteMenu;
    public GameObject PlaceMenu;
    public GameObject CoinMenu;
    public GameObject achievementMenu;
    public GameObject missionMenu;
    public GameObject achievementPos; //position object
    public GameObject missionPos;
    public Vector3 challengesPos;
    public GameObject parasitePosObject; //position object
    public GameObject placePosObject; // position object
    public GameObject coinPosObject; // position object
    public Vector3 parasitePos;
    public Vector3 placePos;
    public Vector3 coinPos;


    public bool isDragging = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        AudioManager.Initialize();

        Input.multiTouchEnabled = false;
    }

    void Start()
    {
        if (skinCollected == null)
        {
            skinCollected = new List<GameObject>();
            // skinCollected[0] = default skin  // Initialize skin   
        }
        LoadData();
        soundSourcesCreated = new List<string>();


        challengesPos = achievementPos.transform.position;
        parasitePos = parasitePosObject.transform.position;
        placePos = placePosObject.transform.position;
        coinPos = coinPosObject.transform.position;

        //Refrences to Player
        playerMovement = player.GetComponent<Movement>();
        playerColliderController = player.GetComponent<ColliderController>();
    }


    private void Update()
    {
        
        //=============================================================================================================

        NotificationManager.Cancel(60);
        TimeSpan delayNotifyTime = new TimeSpan(1, 0, 0);
        // schedule without icon
        NotificationManager.Send(60, delayNotifyTime, "🏆HEY ACHIEVER!🏆", "🧗You know you can achieve more than this!🧗‍♀️", Color.red, NotificationIcon.Heart);

       

       

        ResetDragTimer();
    }



    #region Change Scene


    //Use to change scene from any script
    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(StartFade(sceneIndex));
    }
    IEnumerator StartFade(int sceneIndex)
    {
        Time.timeScale = 1.0f; //in case game is paused.
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSecondsRealtime(fadeTime);
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion    //Change Scene Region

    public int GetPoints()
    {
        return points;
    }

    public void AddPoints(int n)
    {
        points += n;
    }

    public void DecreasePoints(int n)
    {
        points -= n;
    }

    public int GetSpin()
    {
        return spinCount;
    }

    public void AddSpin(int n)
    {
        spinCount += n;
    }

    public void DecreaseSpin(int n)
    {
        spinCount -= n;
    }

    public int GetCoin()
    {
        return coin;
    }

    public void AddCoin(int n)
    {
        coin += n;
    }

    public void DecreaseCoin(int n)
    {
        coin -= n;
    }

    public void SavePoints()
    {
        PlayerPrefs.SetInt("Points", points);
    }

    public void SaveSpin()
    {
        PlayerPrefs.SetInt("Spin", spinCount);
    }

    public void SaveCoin()
    {       
        PlayerPrefs.SetInt("Coin", coin);
    }

    public void SaveScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (player.GetComponent<Movement>().playerDistance > PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.SetFloat("HighScore", player.GetComponent<Movement>().playerDistance);
            }
        }
    }

    public void SaveSkin()
    {
        PlayerPrefs.SetInt("NumOfSkin", numOfSkinCollected);
        for (int i = 0; i < numOfSkinCollected; ++i)
        {
            PlayerPrefs.SetString("Skin[" + i + "].name", skinCollected[i].name);
        }
    }

    public void LoadData()
    {
        numOfSkinCollected = PlayerPrefs.GetInt("NumOfSkin", 0);
        if(!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 25);
            PlayerPrefs.SetInt("Points", 20);
        }
        for (int i = 0; i < numOfSkinCollected; ++i)
        {
            for (int j = 0; j < Shop.instance.skinList.Length; j++)
            {
                if (Shop.instance.skinList[j].name == PlayerPrefs.GetString("Skin[" + i + "].name", ""))
                {
                    skinCollected.Add(Shop.instance.skinList[j]);
                    Shop.instance.skinList[j].GetComponent<Skin>().isBought = true;
                    break;
                }
            }
        }
        LoadTotalValue();
        secondChanceDiscount = PlayerPrefs.GetInt("secondDiscount");
        coin = PlayerPrefs.GetInt("Coin", 0);
        spinCount = PlayerPrefs.GetInt("Spin", 0);
        points = PlayerPrefs.GetInt("Points", 0);
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetFloat("HighScore", 0f);
        else
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
        }
    }

    public void LoadTotalValue()
    {
        totalDistanceTravelled = PlayerPrefs.GetFloat("TotalDistance", 0);
        totalCoinCollected = PlayerPrefs.GetInt("TotalCoin", 0);
        totalBounce = PlayerPrefs.GetInt("TotalBounce", 0);
        totalStick = PlayerPrefs.GetInt("TotalStick", 0);
        totalPlay = PlayerPrefs.GetInt("TotalPlay", 0);
        totalSpin = PlayerPrefs.GetInt("TotalSpin", 0);
        totalPoints = PlayerPrefs.GetInt("TotalPoints", 0);
    }

    public void SaveTotalValues() // for missions
    {
        totalDistanceTravelled += playerDistanceTraveled;
        PlayerPrefs.SetFloat("TotalDistance", totalDistanceTravelled);
        totalCoinCollected += coinCollectedInAGame;
        PlayerPrefs.SetInt("TotalCoin", totalCoinCollected);
        totalBounce += bounceCounterInAGame;
        PlayerPrefs.SetInt("TotalBounce", totalBounce);
        totalStick += stickCounterInAGame;
        PlayerPrefs.SetInt("TotalStick", totalStick);
        totalPlay += 1;
        PlayerPrefs.SetInt("TotalPlay", totalPlay);
        PlayerPrefs.SetInt("TotalSpin", totalSpin);
        PlayerPrefs.SetInt("TotalPoints", totalPoints);
    }

    void OnApplicationQuit()
    {
        SaveCoin();
        SaveSpin();
        SaveSkin();
        SaveTotalValues();
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetDragTimer()
    {   
        if(!UIManager.Instance.ShopMenu.activeInHierarchy)
        {
            if (ParasiteMenu.GetComponent<DragController>().timer != 0f)
                ParasiteMenu.GetComponent<DragController>().timer = 0f;
            if (PlaceMenu.GetComponent<DragController>().timer != 0f)
                PlaceMenu.GetComponent<DragController>().timer = 0f;
            if (CoinMenu.GetComponent<DragController>().timer != 0f)
                CoinMenu.GetComponent<DragController>().timer = 0f;
        }      
        if(!UIManager.Instance.ChallengesMenu.activeInHierarchy)
        {
            if (achievementMenu.GetComponent<DragController>().timer != 0f)
                achievementMenu.GetComponent<DragController>().timer = 0f;
            if (missionMenu.GetComponent<DragController>().timer != 0f)
                missionMenu.GetComponent<DragController>().timer = 0f;
        }
    }
}
