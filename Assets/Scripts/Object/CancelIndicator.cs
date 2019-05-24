using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelIndicator : MonoBehaviour
{
    [HideInInspector] public Vector2 screenPos;
    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.position = (Vector2)Camera.main.ScreenToWorldPoint(screenPos);
    }
}
