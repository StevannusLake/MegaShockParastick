using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    public GameObject LoseMenu;
    public GameObject MainMenu;
    public Movement playerMovement;
    public GameObject SecondChanceMenu;

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

    public void CallLoseMenu()
    {
        LoseMenu.SetActive(true);
        CloseSecondChanceMenu();
    }

    public void CloseLoseMenu()
    {
        LoseMenu.SetActive(false);
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

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CallSecondChanceMenu()
    {
        SecondChanceMenu.SetActive(true);
    }

    public void CloseSecondChanceMenu()
    {
        SecondChanceMenu.SetActive(false);
    }
}
