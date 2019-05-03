using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckySpinManager : MonoBehaviour
{
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    public Button TurnButton;
    public GameObject Spinner; 			
    public Text CoinText; 		
    public int TurnCost = 250;
    public enum RewardType {tenPoints, twoFreeSpins, twentyFivePoints, fiftyPoints, oneThousandPoints, zonk, fiveHundredPoints, fivePoints, oneHundredPoints};
    public RewardType reward;

    public void TurnWheel()
    {
        // Player has enough money to turn the wheel
        if (GameManager.instance.GetSpin() >= 1)
        {
            _currentLerpRotationTime = 0f;

            #region //RandomizePrize
            float randReward = Random.value;
            if (randReward <= 0.23f)
            {
                reward = RewardType.tenPoints;
            }
            else if(randReward > 0.23f && randReward <= 0.33f)
            {
                reward = RewardType.twoFreeSpins;
            }
            else if(randReward > 0.33f && randReward <= 0.48f)
            {
                reward = RewardType.twentyFivePoints;
            }
            else if(randReward > 0.48 && randReward <= 0.58f)
            {
                reward = RewardType.fiftyPoints;
            }
            else if(randReward > 0.58f && randReward <= 0.6f)
            {
                reward = RewardType.oneThousandPoints;
            }
            else if(randReward > 0.6f && randReward <= 0.67f)
            {
                reward = RewardType.zonk;
            }
            else if(randReward > 0.67f && randReward <= 0.7f)
            {
                reward = RewardType.fiveHundredPoints;
            }
            else if(randReward > 0.7f && randReward <= 0.95f)
            {
                reward = RewardType.fivePoints;
            }
            else if(randReward > 0.95f && randReward <= 1f)
            {
                reward = RewardType.oneHundredPoints;
            }
            #endregion 
            float randomFinalAngle = 0;

            #region //RandomAngle
            if (reward == RewardType.tenPoints)
            {
                _sectorsAngles = new float[] { 7,20,40,56,70,78 };              
            }
            else if(reward == RewardType.twoFreeSpins)
            {
                _sectorsAngles = new float[] { 97, 108 };
            }
            else if(reward == RewardType.twentyFivePoints)
            {
                _sectorsAngles = new float[] { 138, 146,159,168};
            }
            else if(reward == RewardType.fiftyPoints)
            {
                _sectorsAngles = new float[] { 181, 187,193 };
            }
            else if(reward == RewardType.oneThousandPoints)
            {
                _sectorsAngles = new float[] { 209 };
            }
            else if(reward == RewardType.zonk)
            {
                _sectorsAngles = new float[] { 221, 227 };
            }
            else if(reward == RewardType.fiveHundredPoints)
            {
                _sectorsAngles = new float[] { 242 };
            }
            else if(reward == RewardType.fivePoints)
            {
                _sectorsAngles = new float[] { 264,282,303,316,328 };
            }
            else if(reward == RewardType.oneHundredPoints)
            {
                _sectorsAngles = new float[] {351};
            }
            #endregion
            randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];

            int fullCircles = 5; // spins number of times before stopping

            // Here we set up how many circles our wheel should rotate before stop
            _finalAngle = -(fullCircles * 360 + randomFinalAngle);
            _isStarted = true;

            GameManager.instance.DecreaseSpin(1);
        }
    }

    private void GiveReward()
    {
        // Here you can set up rewards for every sector of wheel
        switch ((int)reward)
        {
            case 0:
                GameManager.instance.AddPoints(10);
                break;
            case 1:
                GameManager.instance.AddSpin(2);
                break;
            case 2:
                GameManager.instance.AddPoints(25);
                break;
            case 3:
                GameManager.instance.AddPoints(50);
                break;
            case 4:
                GameManager.instance.AddPoints(1000);
                break;
            case 5:
                break;
            case 6:
                GameManager.instance.AddPoints(500);
                break;
            case 7:
                GameManager.instance.AddPoints(5);
                break;
            default:
                GameManager.instance.AddPoints(100);
                break;
        }
    }

    void Update()
    {
        // Make turn button non interactable if user has not enough money for the turn
        if (_isStarted || GameManager.instance.GetSpin() < 1)
        {
            TurnButton.interactable = false;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            TurnButton.interactable = true;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if (!_isStarted)
            return;

        float maxLerpRotationTime = 4f;

        // increment timer once per frame
        _currentLerpRotationTime += Time.deltaTime;
        if (_currentLerpRotationTime > maxLerpRotationTime || Spinner.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;

            GiveReward();
        }

        // Calculate current position using linear interpolation
        float t = _currentLerpRotationTime / maxLerpRotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Spinner.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
