﻿using System.Collections;
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
    public GameObject SecondChanceMenu;
    public Text currentScore;
    public Text highScore;
    public GameObject player;
    public GameObject scoreInGameUI;
    public Text highScoreInMainMenu;
    public Text coinText;
    public Text coinText2;

    public Button SecondChanceButton;

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
        highScoreInMainMenu.text = "HighScore : " + PlayerPrefs.GetFloat("HighScore",0).ToString("F1")+" mm";
    }

    private void Update()
    {
        GameManager.instance.SaveData();
        CheckSecondChanceButton();
        ButtonManager.instance.TempScore = player.GetComponent<Movement>().playerDistance;
    }

    public void CallLoseMenu()
    {
        AudioManager.PlaySound(AudioManager.Sound.Lose);

        LoseMenu.SetActive(true);
        // Update score
        GameManager.instance.SaveData();
        scoreInGameUI.SetActive(false);
        currentScore.text = "Your Score : " + player.GetComponent<Movement>().playerDistance.ToString("F1") + " mm";
        highScore.text = "" + PlayerPrefs.GetFloat("HighScore", 0).ToString("F1") + " mm";
        coinText.text = "$ " + PlayerPrefs.GetInt("Coin", 0);
        SecondChanceMenu.SetActive(false);
    }

    public void CloseLoseMenu()
    {
        LoseMenu.SetActive(false);

        ButtonManager.instance.secondlife = false;
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("SecondLife", ButtonManager.instance.secondlife ? 1 : 0);

        //! Save Temporary Player Distance
        PlayerPrefs.SetFloat("TempScore", 0);
    }

    public void CallMainMenu()
    {
        MainMenu.SetActive(true);
        LoseMenu.SetActive(false);
        playerMovement.enabled = false;
    }

    public void CloseMainMenu()
    {
        MainMenu.SetActive(false);
        playerMovement.enabled = true;
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
        GameManager.instance.SaveData();
        GameManager.instance.LoadData();

        AudioManager.PlaySound(AudioManager.Sound.Continue);

        SecondChanceMenu.SetActive(true);
        coinText2.text = "$ " + PlayerPrefs.GetInt("Coin", 0);
    }

    public void CloseSecondChanceMenu()
    {
        if (GameManager.instance.GetCoin() >= 25)
        {
            AudioManager.PlaySound(AudioManager.Sound.Reborn);

            GameManager.instance.DecreaseCoin(25);

            ButtonManager.instance.secondlife = true;
            // Save boolean using PlayerPrefs
            PlayerPrefs.SetInt("SecondLife", ButtonManager.instance.secondlife ? 1 : 0);

            ButtonManager.instance.TempScore = player.GetComponent<Movement>().playerDistance;
            //! Save Temporary Player Distance
            PlayerPrefs.SetFloat("TempScore", ButtonManager.instance.TempScore);

            SecondChanceMenu.SetActive(false);

            ReloadScene();
        }
    }

    public void CheckSecondChanceButton()
    {
        if(GameManager.instance.GetCoin() >= 25)
        {
            SecondChanceButton.interactable = true;
        }
        else if (GameManager.instance.GetCoin() < 25)
        {
            SecondChanceButton.interactable = false;
        }
    }

    public void ButtonSound()
    {
        AudioManager.PlaySound(AudioManager.Sound.Button);
    }
}
