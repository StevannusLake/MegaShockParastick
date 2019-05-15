using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffectController : MonoBehaviour
{
    public GameObject[] smokes = new GameObject[3];
    SpriteRenderer[] smokesSR = new SpriteRenderer[3];
    public Movement playerMovementScript;

    private void Start()
    {
        for(int i=0; i<smokesSR.Length; i++)
        {
            smokesSR[i] = smokes[i].GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        UpdateSmoke();
    }

    void UpdateSmoke()
    {
        if(!smokesSR[0].enabled)
        {
            playerMovementScript.mySmokeEffect = smokes[0].GetComponent<SmokeEffect>();
        }
        else if(!smokesSR[1].enabled)
        {
            playerMovementScript.mySmokeEffect = smokes[1].GetComponent<SmokeEffect>();
        }
        else if (!smokesSR[2].enabled)
        {
            playerMovementScript.mySmokeEffect = smokes[2].GetComponent<SmokeEffect>();
        }
        else
        {
            playerMovementScript.mySmokeEffect = smokes[0].GetComponent<SmokeEffect>();
        }
    }
}
