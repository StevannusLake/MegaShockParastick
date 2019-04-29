using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WatchVideo()
    {
        GameManager.instance.AddCoin(20);
    }

    public void MainMenu()
    {
        UIManager.Instance.CallMainMenu();
    }
}
