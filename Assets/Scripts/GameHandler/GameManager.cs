﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public List<string> soundSourcesCreated;
    private int coin = 0;
    public List<GameObject> skinCollected;
    public int numOfSkinCollected = 0;
    public MixingCameraController mixingCameraController;
    public float playerDistanceTraveled;
    public GameObject player;
    public static GameManager instance;
    public float currentActiveLevelGeneratorID = 0;
    public LevelHandler levelHandler;
    public GameObject audioSourcePlayer;
    public float highScore;

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
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetFloat("HighScore", 0f);
    }

    void OnApplicationQuit()
    {
        SaveCoin();
        SaveSkin();
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
