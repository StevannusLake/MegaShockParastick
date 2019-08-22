using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    public GameObject cutScene;
    public GameObject cutScene2;
    public GameObject cutScene3;
    public GameObject cutScene4;
    public GameObject cutScene5;
    public GameObject cutScene6;
    public bool isLast = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isLast = (PlayerPrefs.GetInt("CutScene") == 0))
        {
            cutScene.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCutScene2()
    {
        cutScene2.SetActive(true);
    }

    public void ShowCutScene3()
    {
        cutScene3.SetActive(true);
    }

    public void ShowCutScene4()
    {
        cutScene4.SetActive(true);
    }

    public void ShowCutScene5()
    {
        cutScene5.SetActive(true);
    }

    public void ShowCutScene6()
    {
        cutScene6.SetActive(true);
    }


    public void HideCutScene()
    {
        PlayerPrefs.SetInt("CutScene", (isLast ? 1 : 0));
        cutScene.SetActive(false);
        cutScene2.SetActive(false);
        cutScene3.SetActive(false);
        cutScene4.SetActive(false);
        cutScene5.SetActive(false);
        cutScene6.SetActive(false);

    }
}
