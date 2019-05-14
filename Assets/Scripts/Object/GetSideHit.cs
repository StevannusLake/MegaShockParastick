using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }
public class GetSideHit : MonoBehaviour
{


    /************************************************************
    ** Make sure to add rigidbodies to your objects.
    ** Place this script on your object not object being hit
    ** this will only work on a Cube being hit 
    ** it does not consider the direction of the Cube being hit
    ** remember to name your C# script "GetSideHit"
    ************************************************************/

    private void Start()
    {
        //! Random Start Position Of Player
        float randXPos = Random.Range(-1.72f, 2.04f);
        transform.position = new Vector3(randXPos, transform.position.y, 0f);
    }

    public  HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {

        HitDirection hitDirection = HitDirection.None;
        if ((Object.transform.position.x - ObjectHit.GetComponent<Collider2D>().transform.position.x) < 0)
        {
            hitDirection = HitDirection.Left;
        }
        else if ((Object.transform.position.x - ObjectHit.GetComponent<Collider2D>().transform.position.x) > 0)
        {
            hitDirection = HitDirection.Right;
        }
        
        return hitDirection;
        
      
    }

}
