using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour
{
    [HideInInspector]
    public enum RewardType {coins,skins};
    public RewardType rewardType;
    public int coinReward;
    public GameObject[] skins;
    public string mission;
    public float distanceObj;
    public float minDistance;
    public float maxDistance;
    public bool nextQuest = true;

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

    private enum MissionType { Distance,DistanceNoCoin,DistanceBetween,DistanceTotal, CoinBetween, CoinExact,Coin,CoinTotal,Bounce,BounceBetween,BounceExact,
    BounceTotal, Stick,StickBetween,StickExact,StickTotal, Play, PlayLuckySpin, Point,HighScore};
    private MissionType missionType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                break;
            case 2:
                distanceObj = 500f;
                missionType = MissionType.Distance;
                completeObj = 1;
                break;
            case 3:
                distanceObj = 1000f;
                missionType = MissionType.Distance;
                completeObj = 1;
                break;
            case 4:
                distanceObj = 150f;
                missionType = MissionType.DistanceNoCoin;
                completeObj = 1;
                break;
            case 5:
                distanceObj = 450f;
                missionType = MissionType.DistanceNoCoin;
                completeObj = 1;
                break;
            case 6:
                distanceObj = 750f;
                missionType = MissionType.DistanceNoCoin;
                completeObj = 1;
                break;
            case 7:
                minDistance = 175f;
                maxDistance = 195f;
                missionType = MissionType.DistanceBetween;
                completeObj = 2;
                break;
            case 8:
                minDistance = 250f;
                maxDistance = 265f;
                missionType = MissionType.DistanceBetween;
                completeObj = 2;
                break;
            case 9:
                minDistance = 450f;
                maxDistance = 460f;
                missionType = MissionType.DistanceBetween;
                completeObj = 2;
                break;
            case 10:
                distanceObj = 10000f;
                missionType = MissionType.DistanceTotal;
                completeObj = 1;
                break;
            #endregion
            #region Type 2
            case 11:
                minCoin = 35;
                maxCoin = 45;
                missionType = MissionType.CoinBetween;
                completeObj = 1;
                break;
            case 12:
                minCoin = 85;
                maxCoin = 95;
                missionType = MissionType.CoinBetween;
                completeObj = 1;
                break;
            case 13:
                minCoin = 150;
                maxCoin = 160;
                missionType = MissionType.CoinBetween;
                completeObj = 1;
                break;
            case 14:
                coinObj = 25;
                missionType = MissionType.CoinExact;
                completeObj = 1;
                break;
            case 15:
                coinObj = 65;
                missionType = MissionType.CoinExact;
                completeObj = 1;
                break;
            case 16:
                coinObj = 100;
                missionType = MissionType.CoinExact;
                completeObj = 1;
                break;
            case 17:
                coinObj = 50;
                missionType = MissionType.Coin;
                completeObj = 3;
                break;
            case 18:
                coinObj = 75;
                missionType = MissionType.Coin;
                completeObj = 3;
                break;
            case 19:
                coinObj = 100;
                missionType = MissionType.Coin;
                completeObj = 3;
                break;
            case 20:
                coinObj = 5000;
                missionType = MissionType.CoinTotal;
                completeObj = 1;
                break;
            #endregion
            #region Type 3
            case 31:
                bounceObj = 15;
                missionType = MissionType.Bounce;
                completeObj = 1;
                break;
            case 32:
                bounceObj = 50;
                missionType = MissionType.Bounce;
                completeObj = 1;
                break;
            case 33:
                bounceObj = 95;
                missionType = MissionType.Bounce;
                completeObj = 1;
                break;
            case 34:
                minBounce = 12;
                maxBounce = 16;
                missionType = MissionType.BounceBetween;
                completeObj = 1;
                break;
            case 35:
                minBounce = 25;
                maxBounce = 30;
                missionType = MissionType.BounceBetween;
                completeObj = 1;
                break;
            case 36:
                minBounce = 47;
                maxBounce = 53;
                missionType = MissionType.BounceBetween;
                completeObj = 1;
                break;
            case 37:
                bounceObj = 20;
                missionType = MissionType.BounceExact;
                completeObj = 4;
                break;
            case 38:
                bounceObj = 30;
                missionType = MissionType.BounceExact;
                completeObj = 4;
                break;
            case 39:
                bounceObj = 50;
                missionType = MissionType.BounceExact;
                completeObj = 4;
                break;
            case 40:
                bounceObj = 2500;
                missionType = MissionType.BounceTotal;
                completeObj = 1;
                break;
            #endregion
            #region Type 4
            case 41:
                stickObj = 20;
                missionType = MissionType.Stick;
                completeObj = 5;
                break;
            case 42:
                stickObj = 50;
                missionType = MissionType.Stick;
                completeObj = 5;
                break;
            case 43:
                stickObj = 80;
                missionType = MissionType.Stick;
                completeObj = 5;
                break;
            case 44:
                minStick = 10;
                maxStick = 14;
                missionType = MissionType.StickBetween;
                completeObj = 1;
                break;
            case 45:
                minStick = 33;
                maxStick = 36;
                missionType = MissionType.StickBetween;
                completeObj = 1;
                break;
            case 46:
                minStick = 56;
                maxStick = 58;
                missionType = MissionType.StickBetween;
                completeObj = 1;
                break;
            case 47:
                stickObj = 17;
                missionType = MissionType.StickExact;
                completeObj = 1;
                break;
            case 48:
                stickObj = 35;
                missionType = MissionType.StickExact;
                completeObj = 1;
                break;
            case 49:
                stickObj = 59;
                missionType = MissionType.StickExact;
                completeObj = 1;
                break;
            case 50:
                stickObj = 1000;
                missionType = MissionType.StickTotal;
                completeObj = 1;
                break;
            #endregion
            #region Type 5
            case 51:
                playObj = 5;
                missionType = MissionType.Play;
                completeObj = 1;
                break;
            case 52:
                playObj = 15;
                missionType = MissionType.Play;
                completeObj = 1;
                break;
            case 53:
                playObj = 30;
                missionType = MissionType.Play;
                completeObj = 1;
                break;
            case 54:
                spinObj = 3;
                missionType = MissionType.PlayLuckySpin;
                completeObj = 1;
                break;
            case 55:
                spinObj = 8;
                missionType = MissionType.PlayLuckySpin;
                completeObj = 1;
                break;
            case 56:
                spinObj = 15;
                missionType = MissionType.PlayLuckySpin;
                completeObj = 1;
                break;
            case 57:
                pointObj = 100;
                missionType = MissionType.Point;
                completeObj = 1;
                break;
            case 58:
                pointObj = 300;
                missionType = MissionType.Point;
                completeObj = 1;
                break;
            case 59:
                pointObj = 500;
                missionType = MissionType.Point;
                completeObj = 1;
                break;
            #endregion
            default:
                distanceObj = PlayerPrefs.GetFloat("HighScore");
                missionType = MissionType.HighScore;
                completeObj = 1;
                break;
        }

        if(id == 10 || id == 20 || id == 30 || id == 40 || id == 50)
        {
            nextQuest = false;
        }
    }
}
