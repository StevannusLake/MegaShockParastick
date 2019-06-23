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
                rawDelta = Input.GetTouch(0).deltaPosition * 0.01f;
                Debug.Log(Input.GetTouch(0).deltaPosition.magnitude);
                if(Input.GetTouch(0).deltaPosition.magnitude > 4f)
                {
                    GameManager.instance.isDragging = true;
                }
                isDragging = true;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                GameManager.instance.isDragging = false;
                isDragging = false;
            }
        }
        MoveByDrag(rawDelta);
        if (!isDragging)
        {
            if (UIManager.Instance.ShopMenu.activeInHierarchy)
            {
                if (Shop.instance.shopState == Shop.ShopState.parasite)
                {
                    if (transform.position.y < initPos.y)
                    {
                        this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.deltaTime);
                    }
                    else if (bottomObject.position.y > initPos.y + 1f)
                    {
                        rawDelta.y = 0f;
                        this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 26.7f, transform.position.z), 3f * Time.deltaTime);
                    }
                }
                else if(Shop.instance.shopState == Shop.ShopState.place)
                {
                    if (transform.position.y < initPos.y)
                    {
                        this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.deltaTime);
                    }
                    else if (bottomObject.position.y > initPos.y - 1f)
                    {
                        rawDelta.y = 0f;
                        this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 10.9f, transform.position.z), 3f * Time.deltaTime);
                    }
                }
                else if(Shop.instance.shopState == Shop.ShopState.coins)
                {
                    if (transform.position.y < initPos.y)
                    {
                        this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.deltaTime);
                    }
                    else if (bottomObject.position.y > initPos.y - 1f)
                    {
                        //rawDelta.y = 0f;
                        //this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 10.9f, transform.position.z), 3f * Time.deltaTime);
                    }
                }
            }
            else if (UIManager.Instance.ChallengesMenu.activeInHierarchy)
            {
                if (transform.position.y < initPos.y)
                {
                    this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.deltaTime);
                    Debug.Log("Too Low");
                }
                else if (bottomObject.position.y > initPos.y-2f)
                {
                    rawDelta.y = 0f;
                    this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -0.9f, transform.position.z), 3f * Time.deltaTime);
                    Debug.Log("Too HIGH");
                }
            }
        }
        rawDelta = Vector3.Lerp(rawDelta, Vector3.zero, Time.deltaTime * 1f);
        //Debug.Log("This Pos: " + transform.position + " InitPos: " + initPos + " BottomPos: " + bottomObject.position);
    }

    void MoveByDrag(Vector3 rawD)
    {
        if (UIManager.Instance.ShopMenu.activeInHierarchy)
        {
            if (rawD.y > 0)
            {
                if (bottomObject.position.y < initPos.y + 1f)
                {
                    Vector3 moveDirection = Vector3.zero;
                    moveDirection.y = rawD.y;
                    this.transform.Translate(moveDirection, Space.World);
                }
            }
            else
            {
                if (transform.position.y >= initPos.y)
                {
                    Vector3 moveDirection = Vector3.zero;
                    moveDirection.y = rawD.y;
                    this.transform.Translate(moveDirection, Space.World);
                }
            }
        }
        else if(UIManager.Instance.ChallengesMenu.activeInHierarchy)
        {
            if (rawD.y > 0)
            {
                if (bottomObject.position.y < initPos.y-2f)
                {
                    Vector3 moveDirection = Vector3.zero;
                    moveDirection.y = rawD.y;
                    this.transform.Translate(moveDirection, Space.World);
                }
            }
            else
            {
                if (transform.position.y >= initPos.y)
                {
                    Vector3 moveDirection = Vector3.zero;
                    moveDirection.y = rawD.y;
                    this.transform.Translate(moveDirection, Space.World);
                }
            }
        }
    }
}
