using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragController : MonoBehaviour
{
    private Vector3 rawDelta = Vector3.zero;
    private Vector3 initPos;
    private bool isDragging = false;
    public Transform parasiteBottomObject;
    public Transform placeBottomObject;
    public Transform coinsBottomObject;
    private Transform bottomObject;
    public Transform challengesBottom;
    public GameObject scrollBar;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if(UIManager.Instance.ShopMenu.activeInHierarchy)
        {
            if (Shop.instance.shopState == Shop.ShopState.parasite)
            {
                bottomObject = parasiteBottomObject;
            }
            else if (Shop.instance.shopState == Shop.ShopState.place)
            {
                bottomObject = placeBottomObject;
            }
            else if (Shop.instance.shopState == Shop.ShopState.coins)
            {
                bottomObject = coinsBottomObject;
            }            
        }
        else if(UIManager.Instance.ChallengesMenu.activeInHierarchy)
        {
            bottomObject = challengesBottom;
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                rawDelta = Input.GetTouch(0).deltaPosition;
                isDragging = true;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
        MoveByDrag(rawDelta);
        if(!isDragging)
        {
            if(transform.position.y < initPos.y )
            {
               this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.deltaTime);
            }
            else if(transform.position.y > bottomObject.position.y)
            {
                this.transform.position = Vector3.Lerp(transform.position, bottomObject.position, 3f * Time.deltaTime);
            }
        }
        rawDelta = Vector3.Lerp(rawDelta, Vector3.zero, Time.deltaTime * 3f);
    }

    void MoveByDrag(Vector3 rawD)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.y = rawD.y;
        this.transform.Translate(-moveDirection, Space.World);
    }
}
