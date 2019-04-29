using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfaces : MonoBehaviour
{
    public enum SurfaceTypes {Safe,Dangerous}
    public float rotationSpeed;
    Transform myTransform;
    CircleCollider2D myCollider;
    static float zRotation;

    public int stickCount = 0;
    string nSurfaceTag = "NSurface";

    public bool OnRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnRotation)
        {
            Rotation();
        }
    }

    // rotation
    void Rotation()
    {
        if(zRotation >= 360)
        {
            zRotation = 0;
        }

        zRotation += rotationSpeed * Time.deltaTime * 40;
        Vector3 rotationVector = new Vector3(0, 0, zRotation);
        myTransform.eulerAngles = rotationVector;
    }
}
