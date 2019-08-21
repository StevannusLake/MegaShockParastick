using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCollider : MonoBehaviour
{
    public ParticleSystem absorbEffect;

    // Start is called before the first frame update
    void Start()
    {
        absorbEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            absorbEffect.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            absorbEffect.Stop();
        }
    }
}
