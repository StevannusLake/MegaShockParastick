using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public List<string> soundSourcesCreated;
    public int coin = 0;
    public List<GameObject> skinCollected;
    public int numOfSkinCollected = 0;
    public MixingCameraController mixingCameraController;
    public float playerDistanceTraveled;
    public int coinCollectedInAGame = 0;
    public GameObject player;
    public Movement playerMovement;
    public ColliderController playerColliderController;
    public static GameManager instance;
    public float currentActiveLevelGeneratorID = 0;
    public LevelHandler levelHandler;
    public GameObject audioSourcePlayer;
    public float highScore;
    public int spinCount;
    private int points;
    public UIManager uiManager;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        AudioManager.Initialize();
       
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
        AudioManager.PlaySound(AudioManager.Sound.InGameBGM);
        //Refrences to Player
        playerMovement = player.GetComponent<Movement>();
        playerColliderController = player.GetComponent<ColliderController>();
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
        for (int i = 0; i < numOfSkinCollected; ++i)
        {
            for (int j = 0; j < Shop.instance.skinList.Length; j++)
            {
                if (Shop.instance.skinList[j].name == PlayerPrefs.GetString("Skin[" + i + "].name", ""))
                {
                    skinCollected.Add(Shop.instance.skinList[j]);
                    break;
                }
            }
        }
        coin = PlayerPrefs.GetInt("Coin", 0);
        spinCount = PlayerPrefs.GetInt("Spin", 0);
        points = PlayerPrefs.GetInt("Points", 0);
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetFloat("HighScore", 0f);
    }

    void OnApplicationQuit()
    {
        SaveCoin();
        SaveSpin();
        SaveSkin();
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
