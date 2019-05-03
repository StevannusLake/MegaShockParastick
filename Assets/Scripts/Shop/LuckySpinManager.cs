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
    public Text spinText;
    public Text coinText;
    public Text pointText;
    public int TurnCost = 250;
    public enum RewardType {tenPoints, twoFreeSpins, twentyFivePoints, fiftyPoints, oneThousandPoints, zonk, fiveHundredPoints, fivePoints, oneHundredPoints};
    public RewardType reward;

    private void Start()
    {
        spinText.text = "Spin Left : " + GameManager.instance.GetSpin();
        coinText.text = "Coin : " + GameManager.instance.GetCoin();
        pointText.text = "Point : " + GameManager.instance.GetPoints();
    }

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
                randomFinalAngle = Random.Range(1f,39f);
            }
            else if(reward == RewardType.twoFreeSpins)
            {
                randomFinalAngle = Random.Range(41f, 79f);
            }
            else if(reward == RewardType.twentyFivePoints)
            {
                randomFinalAngle = Random.Range(81f, 119f);
            }
            else if(reward == RewardType.fiftyPoints)
            {
                randomFinalAngle = Random.Range(121f, 159f);
            }
            else if(reward == RewardType.oneThousandPoints)
            {
                randomFinalAngle = Random.Range(161f, 199f);
            }
            else if(reward == RewardType.zonk)
            {
                randomFinalAngle = Random.Range(201f, 239f);
            }
            else if(reward == RewardType.fiveHundredPoints)
            {
                randomFinalAngle = Random.Range(241f, 279f);
            }
            else if(reward == RewardType.fivePoints)
            {
                randomFinalAngle = Random.Range(281f, 319f);
            }
            else if(reward == RewardType.oneHundredPoints)
            {
                randomFinalAngle = Random.Range(321f, 359f);
            }
            #endregion
            //_sectorsAngles = new float[] { 351,32 };
            //randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];

            int fullCircles = 5; // spins number of times before stopping

            // Here we set up how many circles our wheel should rotate before stop
            _finalAngle = -(fullCircles * 360 + randomFinalAngle);
            _isStarted = true;

            GameManager.instance.DecreaseSpin(1);
            spinText.text = "Spin Left : " + GameManager.instance.GetSpin();
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
            spinText.text = "Spin Left : " + GameManager.instance.GetSpin();
            coinText.text = "Coin : " + GameManager.instance.GetCoin();
            pointText.text = "Point : " + GameManager.instance.GetPoints();
        }

        // Calculate current position using linear interpolation
        float t = _currentLerpRotationTime / maxLerpRotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Spinner.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void BuySpin()
    {
        if(GameManager.instance.GetCoin() >= TurnCost)
        {
            GameManager.instance.AddSpin(1);
            GameManager.instance.DecreaseCoin(TurnCost);
            spinText.text = "Spin Left : " + GameManager.instance.GetSpin();
            coinText.text = "Coin : " + GameManager.instance.GetCoin();
        }
    }
}
