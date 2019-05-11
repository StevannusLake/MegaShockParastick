using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject LoseMenu;
    Camera cam;
    public int waterMovingSpeed;
    public GameObject MainMenu;
    public GameObject SecondChanceMenu;

    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        cam = Camera.main;
        myTransform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 14.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && Movement.deadState == 0)
        {
            LiquidMove();
        }
    }

    void LiquidMove()
    {
        myTransform.position += Vector3.up * waterMovingSpeed * Time.deltaTime;
        myTransform.position = new Vector3(GameManager.instance.player.transform.position.x, transform.position.y, 0f);
    }
}
