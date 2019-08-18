using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragController : MonoBehaviour
{
    private Vector3 rawDelta = Vector3.zero;
    public Vector3 initPos;
    private bool isDragging = false;
    public Transform parasiteBottomObject;
    public Transform placeBottomObject;
    public Transform coinsBottomObject;
    private Transform bottomObject;
    public Transform challengesBottom;
    public Transform achievementBottom;
    public Transform creditsBottom;
    public float timer;
    public float duration = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.Instance.CreditsMenu.activeInHierarchy)
        {
            initPos = new Vector3(transform.position.x, -1f, transform.position.z);
        }
        //else if(UIManager.Instance.ShopMenu.activeInHierarchy)
        //{
        //    if (Shop.instance.shopState == Shop.ShopState.parasite)
        //    {
        //        if (GameManager.instance.parasiteInitPos == Vector3.zero)
        //        {
        //            GameManager.instance.parasiteInitPos = initPos;
        //        }
        //    }
        //    else if (Shop.instance.shopState == Shop.ShopState.place)
        //    {
        //        if (GameManager.instance.placeInitPos == Vector3.zero)
        //        {
        //            GameManager.instance.placeInitPos = initPos;
        //        }
        //    }
        //    else if (Shop.instance.shopState == Shop.ShopState.coins)
        //    {
        //        if (GameManager.instance.coinInitPos == Vector3.zero)
        //        {
        //            GameManager.instance.coinInitPos = initPos;
        //        }
        //    }         
        //}

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {   
        if(timer >= duration)
        {
            if (UIManager.Instance.ShopMenu.activeInHierarchy)
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
            else if (UIManager.Instance.ChallengesMenu.activeInHierarchy)
            {
                if (MissionManager.instance.challengeState == MissionManager.ChallengeState.Missions)
                    bottomObject = challengesBottom;
                else bottomObject = achievementBottom;
            }
            else if (UIManager.Instance.CreditsMenu.activeInHierarchy)
            {
                bottomObject = creditsBottom;
            }
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    rawDelta = Input.GetTouch(0).deltaPosition * 0.01f;
                    //Debug.Log(Input.GetTouch(0).deltaPosition.magnitude);
                    if (Input.GetTouch(0).deltaPosition.magnitude > 4f)
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
                        initPos = GameManager.instance.parasitePos;
                        if (transform.position.y < initPos.y)
                        {
                            this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.unscaledDeltaTime);
                        }
                        else if (bottomObject.position.y > initPos.y + 1f)
                        {
                            rawDelta.y = 0f;
                            this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 26.7f, transform.position.z), 3f * Time.unscaledDeltaTime);
                        }
                        Debug.Log("CurrentPos:" + transform.position);
                    }
                    else if (Shop.instance.shopState == Shop.ShopState.place)
                    {
                        initPos = GameManager.instance.placePos;
                        if (transform.position.y < initPos.y)
                        {
                            this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.unscaledDeltaTime);
                        }
                        else if (bottomObject.position.y > initPos.y - 1f)
                        {
                            rawDelta.y = 0f;
                            this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 10.9f, transform.position.z), 3f * Time.unscaledDeltaTime);
                        }
                        Debug.Log("CurrentPos:" + transform.position);
                    }
                    else if (Shop.instance.shopState == Shop.ShopState.coins)
                    {
                        initPos = GameManager.instance.coinPos;
                        if (transform.position.y < initPos.y)
                        {
                            this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.unscaledDeltaTime);
                        }
                        else if (bottomObject.position.y > initPos.y - 1f)
                        {
                            //rawDelta.y = 0f;
                            //this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 10.9f, transform.position.z), 3f * Time.deltaTime);
                        }
                        Debug.Log("CurrentPos:" + transform.position);
                    }
                }
                else if (UIManager.Instance.ChallengesMenu.activeInHierarchy)
                {
                    Debug.Log("CurrentPos:" + transform.position);
                    initPos = GameManager.instance.achievementPos.transform.position;
                    if (MissionManager.instance.challengeState == MissionManager.ChallengeState.Missions)
                    {
                        if (transform.position.y < initPos.y)
                        {
                            this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.unscaledDeltaTime);
                        }
                        else if (bottomObject.position.y > initPos.y - 2f)
                        {
                            rawDelta.y = 0f;
                            this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, initPos.y + 2.3f, transform.position.z), 3f * Time.unscaledDeltaTime);
                        }
                    }
                    else
                    {
                        if (transform.position.y < initPos.y)
                        {
                            this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.unscaledDeltaTime);
                        }
                        else if (bottomObject.position.y > initPos.y - 2f)
                        {
                            rawDelta.y = 0f;
                            this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, initPos.y + 17f, transform.position.z), 3f * Time.unscaledDeltaTime);
                        }
                    }
                }
                else if (UIManager.Instance.CreditsMenu.activeInHierarchy)
                {
                    if (transform.position.y < initPos.y)
                    {
                        this.transform.position = Vector3.Lerp(transform.position, initPos, 3f * Time.unscaledDeltaTime);
                        Debug.Log("too hight");
                    }
                    else if (bottomObject.position.y > initPos.y - 2f)
                    {
                        rawDelta.y = 0f;
                        this.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 5.2f, transform.position.z), 3f * Time.unscaledDeltaTime);
                        Debug.Log("too low");
                    }
                }
            }
            rawDelta = Vector3.Lerp(rawDelta, Vector3.zero, Time.unscaledDeltaTime * 1f);
            //Debug.Log("This Pos: " + transform.position + " InitPos: " + initPos + " BottomPos: " + bottomObject.position);
        }
        else
        {
            timer += Time.unscaledDeltaTime;
            ResetPosition();
        }
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
        else if(UIManager.Instance.ChallengesMenu.activeInHierarchy || UIManager.Instance.CreditsMenu.activeInHierarchy)
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

    public void ResetPosition()
    {
        if (UIManager.Instance.ShopMenu.activeInHierarchy)
        {
            if (Shop.instance.shopState == Shop.ShopState.parasite)
            {
                transform.position = GameManager.instance.parasitePos;
            }
            else if(Shop.instance.shopState == Shop.ShopState.place)
            {
                transform.position = GameManager.instance.placePos;
            }
            else if (Shop.instance.shopState == Shop.ShopState.coins)
            {
                transform.position = GameManager.instance.coinPos;
            }
        }
        else if (UIManager.Instance.ChallengesMenu.activeInHierarchy)
        {
            if (MissionManager.instance.challengeState == MissionManager.ChallengeState.Missions)
            {
                 transform.position = GameManager.instance.achievementPos.transform.position;
            }
            else
            {    
                 transform.position = GameManager.instance.achievementPos.transform.position;
            }
        }
    }
}
