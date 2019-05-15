using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    public int[] missionListId;
    public Missions[] missions;
    public GameObject[] MissionTabs;
    public GameObject ChallengesMenu;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {   
        if(!PlayerPrefs.HasKey("Mission1"))
        {
            missionListId[0] = 1;
            missionListId[1] = 11;
            missionListId[2] = 24;
            missionListId[3] = 37;
            missionListId[4] = 41;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                missionListId[i] = PlayerPrefs.GetInt("Mission" + (i + 1));
            }
        }

        missions = new Missions[5];

        for (int i=0;i<5;i++)
        {
            var tempItem = new Missions(missionListId[i]);
            missions[i] = tempItem;
        }
    }

    private void Update()
    {
        if(ChallengesMenu.activeInHierarchy)
        {
            ShowMissionObjective();
        }
    }

    void ShowMissionObjective()
    {
        for(int i=0;i<MissionTabs.Length;i++)
        {
            MissionTabs[i].transform.Find("Text").GetComponent<Text>().text = missions[i].description;
        }
    }

    private void OnApplicationQuit()
    {
        for(int i=0;i < 5;i++)
        {
            PlayerPrefs.SetInt("Mission" + (i + 1), missionListId[i]);
        }
    }

    public void CheckMissionEnd(Missions[] missions)
    {
        for(int i=0;i<5;i++)
        {
            if(!missions[i].isCompleted)
            {
                #region Distance
                if (missions[i].missionType == Missions.MissionType.DistanceBetween)
                {
                    if (GameManager.instance.playerDistanceTraveled >= missions[i].minDistance && GameManager.instance.playerDistanceTraveled <= missions[i].maxDistance)
                    {
                        missions[i].completeNum++;
                        if (missions[i].completeNum == missions[i].completeObj)
                        {
                            missions[i].isCompleted = true;
                        }
                    }
                }
                #endregion
                #region Coin
                else if (missions[i].missionType == Missions.MissionType.CoinBetween)
                {
                    if (GameManager.instance.coinCollectedInAGame >= missions[i].minCoin && GameManager.instance.coinCollectedInAGame <= missions[i].maxCoin)
                    {
                        missions[i].isCompleted = true;
                        Debug.Log(missions[i].description + " COMPLETED");
                    }
                }
                else if (missions[i].missionType == Missions.MissionType.CoinExact)
                {
                    if (GameManager.instance.coinCollectedInAGame == missions[i].coinObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                else if (missions[i].missionType == Missions.MissionType.Coin)
                {
                    if (GameManager.instance.coinCollectedInAGame >= missions[i].coinObj)
                    {
                        missions[i].completeNum++;
                        if (missions[i].completeNum == missions[i].completeObj)
                        {
                            missions[i].isCompleted = true;
                        }
                    }
                }
                #endregion
                #region Bounce
                else if (missions[i].missionType == Missions.MissionType.BounceBetween)
                {
                    if (GameManager.instance.bounceCounterInAGame >= missions[i].minBounce && GameManager.instance.bounceCounterInAGame <= missions[i].maxBounce)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                else if (missions[i].missionType == Missions.MissionType.BounceExact)
                {
                    if (GameManager.instance.bounceCounterInAGame == missions[i].bounceObj)
                    {
                        missions[i].completeNum++;
                        if (missions[i].completeNum == missions[i].completeObj)
                        {
                            missions[i].isCompleted = true;
                        }
                    }
                }
                #endregion
                #region Stick
                else if (missions[i].missionType == Missions.MissionType.Stick)
                {
                    if (GameManager.instance.stickCounterInAGame >= missions[i].stickObj)
                    {
                        missions[i].completeNum++;
                        if (missions[i].completeNum == missions[i].completeObj)
                        {
                            missions[i].isCompleted = true;
                        }
                    }
                }
                else if (missions[i].missionType == Missions.MissionType.StickBetween)
                {
                    if (GameManager.instance.stickCounterInAGame >= missions[i].minStick && GameManager.instance.stickCounterInAGame <= missions[i].maxStick)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                else if (missions[i].missionType == Missions.MissionType.StickExact)
                {
                    if (GameManager.instance.stickCounterInAGame == missions[i].stickObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                #endregion
                #region Other
                else if (missions[i].missionType == Missions.MissionType.Play)
                {
                    if(GameManager.instance.totalPlay >= missions[i].playObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                #endregion
            }
        }
    }

    public void CheckMissionInGame(Missions[] missions)
    {
        for(int i=0;i<5;i++)
        {   
            if(!missions[i].isCompleted)
            {
                #region Distance
                if (missions[i].missionType == Missions.MissionType.Distance)
                {
                    if(GameManager.instance.playerDistanceTraveled >= missions[i].distanceObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                else if(missions[i].missionType == Missions.MissionType.DistanceNoCoin)
                {
                    if(GameManager.instance.playerDistanceTraveled >= missions[i].distanceObj && GameManager.instance.coinCollectedInAGame == 0)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                else if(missions[i].missionType == Missions.MissionType.DistanceTotal)
                {
                    if(GameManager.instance.totalDistanceTravelled >= missions[i].distanceObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                #endregion
                #region Coin
                else if(missions[i].missionType == Missions.MissionType.CoinTotal)
                {
                    if(GameManager.instance.totalCoinCollected >= missions[i].coinObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                #endregion
                #region Bounce
                else if(missions[i].missionType == Missions.MissionType.Bounce)
                {
                    if(GameManager.instance.bounceCounterInAGame >= missions[i].bounceObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                else if(missions[i].missionType == Missions.MissionType.BounceTotal)
                {
                    if(GameManager.instance.totalBounce >= missions[i].bounceObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                #endregion
                #region Stick
                else if(missions[i].missionType == Missions.MissionType.StickTotal)
                {
                    if(GameManager.instance.totalStick >= missions[i].stickObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                #endregion
                #region Other
                else if(missions[i].missionType == Missions.MissionType.PlayLuckySpin)
                {
                    if(GameManager.instance.totalSpin >= missions[i].spinObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                else if(missions[i].missionType == Missions.MissionType.Point)
                {
                    if(GameManager.instance.totalPoints >= missions[i].pointObj)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                else if(missions[i].missionType == Missions.MissionType.HighScore)
                {
                    if(GameManager.instance.playerDistanceTraveled >= GameManager.instance.highScore)
                    {
                        missions[i].isCompleted = true;
                    }
                }
                #endregion
            }
        }
    }
}

public class Missions
{
    [HideInInspector]
    public enum RewardType { coins, skins };
    public RewardType rewardType;
    public int coinReward;
    public GameObject[] skins;
    public string mission;
    public float distanceObj;
    public float minDistance;
    public float maxDistance;
    public bool nextQuest = true;
    public string description;
    public int nextId;
    public int id;

    [HideInInspector]
    public int coinObj;
    public int minCoin;
    public int maxCoin;

    [HideInInspector]
    public int bounceObj;
    public int minBounce;
    public int maxBounce;

    [HideInInspector]
    public int stickObj;
    public int minStick;
    public int maxStick;

    [HideInInspector]
    public int playObj;
    public int spinObj;
    public int pointObj;
    public int completeObj;
    public int completeNum = 0;
    public bool isCompleted = false;

    public enum MissionType
    {
        Distance, DistanceNoCoin, DistanceBetween, DistanceTotal, CoinBetween, CoinExact, Coin, CoinTotal, Bounce, BounceBetween, BounceExact,
        BounceTotal, Stick, StickBetween, StickExact, StickTotal, Play, PlayLuckySpin, Point, HighScore
    };
    public MissionType missionType;

    public Missions(int id)
    {
        this.id = id;
        SetObjective(id);
    }

    void SetObjective(int id)
    {
        switch (id)
        {
            #region Type 1
            case 1:
                distanceObj = 100f;
                missionType = MissionType.Distance;
                completeObj = 1;
                description = "Reach 100+ mm in 1 run";
                nextId = 4;
                break;
            case 2:
                distanceObj = 500f;
                missionType = MissionType.Distance;
                completeObj = 1;
                description = "Reach 500+ mm in 1 run";
                nextId = 5;
                break;
            case 3:
                distanceObj = 1000f;
                missionType = MissionType.Distance;
                completeObj = 1;
                description = "Reach 1000+ mm in 1 run";
                nextId = 6;
                break;
            case 4:
                distanceObj = 150f;
                missionType = MissionType.DistanceNoCoin;
                completeObj = 1;
                description = "Reach 150 mm without collecting any coins";
                nextId = 7;
                break;
            case 5:
                distanceObj = 450f;
                missionType = MissionType.DistanceNoCoin;
                completeObj = 1;
                description = "Reach 450 mm without collecting any coins";
                nextId = 8;
                break;
            case 6:
                distanceObj = 750f;
                missionType = MissionType.DistanceNoCoin;
                completeObj = 1;
                description = "Reach 750 mm without collecting any coins";
                nextId = 9;
                break;
            case 7:
                minDistance = 175f;
                maxDistance = 195f;
                missionType = MissionType.DistanceBetween;
                completeObj = 2;
                description = "Reach between 175 to 195 mm in a row (" + completeNum + "/" + completeObj + ")";
                nextId = 2;
                break;
            case 8:
                minDistance = 250f;
                maxDistance = 265f;
                missionType = MissionType.DistanceBetween;
                completeObj = 2;
                description = "Reach between 250 to 265 mm in a row (" + completeNum + "/" + completeObj + ")";
                nextId = 3;
                break;
            case 9:
                minDistance = 450f;
                maxDistance = 460f;
                missionType = MissionType.DistanceBetween;
                completeObj = 2;
                description = "Reach between 450 to 460 mm in a row (" + completeNum + "/" + completeObj + ")";
                nextId = 10;
                break;
            case 10:
                distanceObj = 10000f;
                missionType = MissionType.DistanceTotal;
                completeObj = 1;
                description = "Reach 10000 distance in total";
                break;
            #endregion
            #region Type 2
            case 11:
                minCoin = 35;
                maxCoin = 45;
                missionType = MissionType.CoinBetween;
                completeObj = 1;
                description = "Collect between 35 to 45 coins";
                nextId = 14;
                break;
            case 12:
                minCoin = 85;
                maxCoin = 95;
                missionType = MissionType.CoinBetween;
                completeObj = 1;
                description = "Collect between 85 to 95 coins";
                nextId = 15;
                break;
            case 13:
                minCoin = 150;
                maxCoin = 160;
                missionType = MissionType.CoinBetween;
                completeObj = 1;
                description = "Collect between 150 to 160 coins";
                nextId = 16;
                break;
            case 14:
                coinObj = 25;
                missionType = MissionType.CoinExact;
                completeObj = 1;
                description = "Collect exactly 25 coins in 1 run";
                nextId = 17;
                break;
            case 15:
                coinObj = 65;
                missionType = MissionType.CoinExact;
                completeObj = 1;
                description = "Collect exactly 65 coins in 1 run";
                nextId = 18;
                break;
            case 16:
                coinObj = 100;
                missionType = MissionType.CoinExact;
                completeObj = 1;
                description = "Collect exactly 100 coins in 1 run";
                nextId = 19;
                break;
            case 17:
                coinObj = 50;
                missionType = MissionType.Coin;
                completeObj = 3;
                description = "Collect 50+ coins in 1 run & in a row (" + completeNum + "/" + completeObj + ")";
                nextId = 12;
                break;
            case 18:
                coinObj = 75;
                missionType = MissionType.Coin;
                completeObj = 3;
                description = "Collect 75+ coins in 1 run & in a row (" + completeNum + "/" + completeObj + ")";
                nextId = 13;
                break;
            case 19:
                coinObj = 100;
                missionType = MissionType.Coin;
                completeObj = 3;
                description = "Collect 100+ coins in 1 run & in a row (" + completeNum + "/" + completeObj + ")";
                nextId = 20;
                break;
            case 20:
                coinObj = 5000;
                missionType = MissionType.CoinTotal;
                completeObj = 1;
                description = "Collect 5000 coins in total";
                break;
            #endregion
            #region Type 3
            case 21:
                bounceObj = 15;
                missionType = MissionType.Bounce;
                completeObj = 1;
                description = "Bounce 15+ times in 1 run";
                nextId = 27;
                break;
            case 22:
                bounceObj = 50;
                missionType = MissionType.Bounce;
                completeObj = 1;
                description = "Bounce 50+ times in 1 run";
                nextId = 28;
                break;
            case 23:
                bounceObj = 95;
                missionType = MissionType.Bounce;
                completeObj = 1;
                description = "Bounce 95+ times in 1 run";
                nextId = 29;
                break;
            case 24:
                minBounce = 12;
                maxBounce = 16;
                missionType = MissionType.BounceBetween;
                completeObj = 1;
                description = "Bounce between 12 to 16 times";
                nextId = 21;
                break;
            case 25:
                minBounce = 25;
                maxBounce = 30;
                missionType = MissionType.BounceBetween;
                completeObj = 1;
                description = "Bounce between 25 to 30 times";
                nextId = 22;
                break;
            case 26:
                minBounce = 47;
                maxBounce = 53;
                missionType = MissionType.BounceBetween;
                completeObj = 1;
                description = "Bounce between 47 to 53 times";
                nextId = 23;
                break;
            case 27:
                bounceObj = 20;
                missionType = MissionType.BounceExact;
                completeObj = 4;
                description = "Bounce exactly 20 times in 1 run (" + completeNum + "/" + completeObj + ")";
                nextId = 25;
                break;
            case 28:
                bounceObj = 30;
                missionType = MissionType.BounceExact;
                completeObj = 4;
                description = "Bounce exactly 30 times in 1 run (" + completeNum + "/" + completeObj + ")";
                nextId = 26;
                break;
            case 29:
                bounceObj = 50;
                missionType = MissionType.BounceExact;
                completeObj = 4;
                description = "Bounce exactly 50 times in 1 run (" + completeNum + "/" + completeObj + ")";
                nextId = 30;
                break;
            case 30:
                bounceObj = 2500;
                missionType = MissionType.BounceTotal;
                completeObj = 1;
                description = "Bounce 2500 times in total";
                break;
            #endregion
            #region Type 4
            case 31:
                stickObj = 20;
                missionType = MissionType.Stick;
                completeObj = 5;
                description = "Stick on 20+ platforms in 1 run (" + completeNum + "/" + completeObj + ")";
                nextId = 34;
                break;
            case 32:
                stickObj = 50;
                missionType = MissionType.Stick;
                completeObj = 5;
                description = "Stick on 50+ platforms in 1 run (" + completeNum + "/" + completeObj + ")";
                nextId = 35;
                break;
            case 33:
                stickObj = 80;
                missionType = MissionType.Stick;
                completeObj = 5;
                description = "Stick on 80+ platforms in 1 run (" + completeNum + "/" + completeObj + ")";
                nextId = 36;
                break;
            case 34:
                minStick = 10;
                maxStick = 14;
                missionType = MissionType.StickBetween;
                completeObj = 1;
                description = "Stick between 10~14 platforms";
                nextId = 37;
                break;
            case 35:
                minStick = 33;
                maxStick = 36;
                missionType = MissionType.StickBetween;
                completeObj = 1;
                description = "Stick between 33~36 platforms";
                nextId = 39;
                break;
            case 36:
                minStick = 56;
                maxStick = 58;
                missionType = MissionType.StickBetween;
                completeObj = 1;
                description = "Stick between 56~58 platforms";
                nextId = 40;
                break;
            case 37:
                stickObj = 17;
                missionType = MissionType.StickExact;
                completeObj = 1;
                description = "Stick exactly on 17 platforms in 1 run";
                nextId = 31;
                break;
            case 38:
                stickObj = 35;
                missionType = MissionType.StickExact;
                completeObj = 1;
                description = "Stick exactly on 35 platforms in 1 run";
                nextId = 32;
                break;
            case 39:
                stickObj = 59;
                missionType = MissionType.StickExact;
                completeObj = 1;
                description = "Stick exactly on 59 platforms in 1 run";
                nextId = 33;
                break;
            case 40:
                stickObj = 1000;
                missionType = MissionType.StickTotal;
                completeObj = 1;
                description = "Stick on 1000 platforms in total";
                break;
            #endregion
            #region Type 5
            case 41:
                playObj = 5;
                missionType = MissionType.Play;
                completeObj = 1;
                description = "Play 5 times";
                nextId = 44;
                break;
            case 42:
                playObj = 15;
                missionType = MissionType.Play;
                completeObj = 1;
                description = "Play 15 times";
                nextId = 45;
                break;
            case 43:
                playObj = 30;
                missionType = MissionType.Play;
                completeObj = 1;
                description = "Play 30 times";
                nextId = 46;
                break;
            case 44:
                spinObj = 3;
                missionType = MissionType.PlayLuckySpin;
                completeObj = 1;
                description = "Play Lucky Spin 3 times";
                nextId = 47;
                break;
            case 45:
                spinObj = 8;
                missionType = MissionType.PlayLuckySpin;
                completeObj = 1;
                description = "Play Lucky Spin 8 times";
                nextId = 48;
                break;
            case 46:
                spinObj = 15;
                missionType = MissionType.PlayLuckySpin;
                completeObj = 1;
                description = "Play Lucky Spin 15 times";
                nextId = 49;
                break;
            case 47:
                pointObj = 100;
                missionType = MissionType.Point;
                completeObj = 1;
                description = "Collect 100 Points";
                nextId = 42;
                break;
            case 48:
                pointObj = 300;
                missionType = MissionType.Point;
                completeObj = 1;
                description = "Collect 300 Points";
                nextId = 43;
                break;
            case 49:
                pointObj = 500;
                missionType = MissionType.Point;
                completeObj = 1;
                description = "Collect 100 Points";
                nextId = 50;
                break;
            case 50:
                distanceObj = PlayerPrefs.GetFloat("HighScore");
                missionType = MissionType.HighScore;
                completeObj = 1;
                description = "Beat your own highscore";
                break;
            #endregion
            default:
                Debug.Log("Quest ID not found");
                break;
        }

        if (id == 10 || id == 20 || id == 30 || id == 40 || id == 50)
        {
            nextQuest = false;
        }
    }
}

