using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTemp : MonoBehaviour
{
    public static GameManagerTemp instance = null;
    public List<string> soundSourcesCreated;
    public GameObject audioSourcePlayer;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
        // Start is called before the first frame update
    void Start()
    {
        AudioManagerTemp.Initialize();
        AudioManagerTemp.PlaySound(AudioManagerTemp.Sound.LogoSound);
    }
}
