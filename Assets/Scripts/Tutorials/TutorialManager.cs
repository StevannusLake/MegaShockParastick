using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialImage1;
    public GameObject tutorialImage2;
    public GameObject tutorialImage3;
    public GameObject tutorialImage4;
    public GameObject tutorialImage5;
    public GameObject tutorialImage6;
    private GameObject player;
    private Movement movement;
    public GameObject uiManager;
    public GameObject waterObject;
    private Water water;
    public bool isTutorial = false;
    public bool isShow = false;
    public bool isShow2 = false;
    public bool isShow3 = false;
    public bool isShow4 = false;
    public bool isShow5 = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        movement = player.GetComponent<Movement>();
        water = waterObject.GetComponent<Water>();

        if (isTutorial = (PlayerPrefs.GetInt("Tutorials") == 0))
        {
            isTutorial = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTutorial()
    {
        if (isTutorial == true)
        {
            tutorialImage1.SetActive(true);
            movement.enabled = false;
            uiManager.GetComponent<UIManager>().rippleEffect.enabled = false;
            water.enabled = false;
        }
    }

    public void ShowTutorial2()
    {
        if (isShow == false)
        {
            tutorialImage2.SetActive(true);
            movement.enabled = false;
            uiManager.GetComponent<UIManager>().rippleEffect.enabled = false;
            water.enabled = false;
            isShow = true;
        }
    }

    public void ShowTutorial3()
    {
        if (isShow2 == false)
        {
            tutorialImage3.SetActive(true);
            movement.enabled = false;
            uiManager.GetComponent<UIManager>().rippleEffect.enabled = false;
            water.enabled = false;
            isShow2 = true;
        }
    }

    public void ShowTutorial4()
    {
        if (isShow3 == false)
        {
            tutorialImage4.SetActive(true);
            movement.enabled = false;
            uiManager.GetComponent<UIManager>().rippleEffect.enabled = false;
            water.enabled = false;
            isShow3 = true;
        }
    }

    public void ShowTutorial5()
    {
        if (isShow4 == false)
        {
            tutorialImage5.SetActive(true);
            movement.enabled = false;
            uiManager.GetComponent<UIManager>().rippleEffect.enabled = false;
            water.enabled = false;
            isShow4 = true;
        }
    }

    public void ShowTutorial6()
    {
        if (isShow5 == false)
        {
            tutorialImage6.SetActive(true);
            movement.enabled = false;
            uiManager.GetComponent<UIManager>().rippleEffect.enabled = false;
            water.enabled = false;
            isShow5 = true;
        }
    }

    public void CloseTutorial()
    {
        tutorialImage1.SetActive(false);
        uiManager.GetComponent<UIManager>().rippleEffect.enabled = true;
        movement.enabled = true;
        water.enabled = true;
    }

    public void CloseTutorial2()
    {
        tutorialImage2.SetActive(false);
        uiManager.GetComponent<UIManager>().rippleEffect.enabled = true;
        movement.enabled = true;
        water.enabled = true;
    }

    public void CloseTutorial3()
    {
        tutorialImage3.SetActive(false);
        uiManager.GetComponent<UIManager>().rippleEffect.enabled = true;
        movement.enabled = true;
        water.enabled = true;
    }

    public void CloseTutorial4()
    {
        tutorialImage4.SetActive(false);
        uiManager.GetComponent<UIManager>().rippleEffect.enabled = true;
        movement.enabled = true;
        water.enabled = true;
    }

    public void CloseTutorial5()
    {
        tutorialImage5.SetActive(false);
        uiManager.GetComponent<UIManager>().rippleEffect.enabled = true;
        movement.enabled = true;
        water.enabled = true;
    }

    public void CloseTutorial6()
    {
        PlayerPrefs.SetInt("Tutorials", (isTutorial ? 1 : 0));
        tutorialImage6.SetActive(false);
        uiManager.GetComponent<UIManager>().rippleEffect.enabled = true;
        movement.enabled = true;
        water.enabled = true;
    }
}
