using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject LoseMenu;
    Camera cam;
    public float waterMovingSpeed;
    public GameObject MainMenu;
    public GameObject SecondChanceMenu;

    Transform myTransform;

    public Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        cam = Camera.main;
        myTransform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 14.5f, 0);

        waterMovingSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LoseMenu.activeSelf && !MainMenu.activeSelf && !SecondChanceMenu.activeSelf && Movement.deadState == 0)
        {
            LiquidMove();
        }

        WaterSpeedUpdate();
    }

    void LiquidMove()
    {
        myTransform.position += Vector3.up * waterMovingSpeed * Time.deltaTime;
        myTransform.position = new Vector3(GameManager.instance.player.transform.position.x, transform.position.y, 0f);
    }

    void WaterSpeedUpdate()
    {
        if(0 < movement.distanceCounter && movement.distanceCounter < 100)
        {
            waterMovingSpeed = 1;
        }
        else if (100 < movement.distanceCounter && movement.distanceCounter < 250)
        {
            waterMovingSpeed = 1.1f;
        }
        else if (250 < movement.distanceCounter && movement.distanceCounter < 500)
        {
            waterMovingSpeed = 1.2f;
        }
        else if (500 < movement.distanceCounter && movement.distanceCounter < 750)
        {
            waterMovingSpeed = 1.3f;
        }
        else if (750 < movement.distanceCounter && movement.distanceCounter < 1000)
        {
            waterMovingSpeed = 1.4f;
        }
        else if (1000 < movement.distanceCounter && movement.distanceCounter < 2000)
        {
            waterMovingSpeed = 1.5f;
        }
        else if (2000 < movement.distanceCounter && movement.distanceCounter < 3000)
        {
            waterMovingSpeed = 1.6f;
        }
        else if (3000 < movement.distanceCounter && movement.distanceCounter < 4000)
        {
            waterMovingSpeed = 1.7f;
        }
        else if (4000 < movement.distanceCounter && movement.distanceCounter < 5000)
        {
            waterMovingSpeed = 1.8f;
        }
        else if (5000 < movement.distanceCounter && movement.distanceCounter < 7500)
        {
            waterMovingSpeed = 1.9f;
        }
        else if (7500 < movement.distanceCounter && movement.distanceCounter < 10000)
        {
            waterMovingSpeed = 2;
        }
        else if (10000 < movement.distanceCounter)
        {
            waterMovingSpeed = 2.2f;
        }
    }
}
