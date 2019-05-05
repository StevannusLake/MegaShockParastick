using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfaces : MonoBehaviour
{
    public enum SurfaceTypes {Safe,Dangerous}
    public float rotationSpeed;
    Transform myTransform;
    CapsuleCollider2D myCollider;
    Rigidbody2D myRigidbody;
    static float zRotation;
    private int rotationSpeedRandom;
    public int stickCount = 0;
    string nSurfaceTag = "NSurface";

    public bool OnRotation = false;

    // Start is called before the first frame update

    private void Awake()
    {
       
    }
    void Start()
    {
        rotationSpeedRandom = Random.Range(20, 40);
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnRotation)
        {
            Rotation();
        }
        
        DropAfter();
        
    }

    // rotation
    void Rotation()
    {
        if(zRotation >= 360)
        {
            zRotation = 0;
        }

        zRotation +=   Time.deltaTime * rotationSpeedRandom *0.2f;
        Vector3 rotationVector = new Vector3(0, 0, zRotation);
        myTransform.eulerAngles = rotationVector;
    }

    void DropAfter()
    {
        if(myCollider == null)
        {
            myCollider = GetComponent<CapsuleCollider2D>();
        }
        if (myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }

        if(myCollider != null && myRigidbody != null)
        {
            if (stickCount >= 2)
            {
                myCollider.isTrigger = true;
                myRigidbody.isKinematic = false;
            }
        }
    }
}
