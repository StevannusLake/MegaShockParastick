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
    public enum RewardType {twoFreeSpins, twentyFivePoints, fiftyPoints, oneThousandPoints, zonk, fiveHundredPoints, fivePoints, oneHundredPoints,tenPoints };
    public RewardType reward;
    public GameObject rewardWindow;
    public Text rewardText;

    public Button LuckySpinBackButton;
    public Button LuckySpinBuyButton;
    public GameObject rewardBlocker;

    private void Start()
    {
        
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
            else if(randReward > 0.23f && randReward <= 0.39f)
            {
                reward = RewardType.twoFreeSpins;
            }
            else if(randReward > 0.39f && randReward <= 0.52f)
            {
                reward = RewardType.twentyFivePoints;
            }
            else if(randReward > 0.52f && randReward <= 0.62f)
            {
                reward = RewardType.fiftyPoints;
            }
            else if(randReward > 0.62f && randReward <= 0.63f)
            {
                reward = RewardType.oneThousandPoints;
            }
            else if(randReward > 0.63f && randReward <= 0.7f)
            {
                reward = RewardType.zonk;
            }
            else if(randReward > 0.7f && randReward <= 0.72f)
            {
                reward = RewardType.fiveHundredPoints;
            }
            else if(randReward > 0.72f && randReward <= 0.97f)
            {
                reward = RewardType.fivePoints;
            }
            else if(randReward > 0.97f && randReward <= 1f)
            {
                reward = RewardType.oneHundredPoints;
            }
            #endregion 
            float randomFinalAngle = 0;

            #region //RandomAngle
            if (reward == RewardType.twoFreeSpins)
            {
                randomFinalAngle = Random.Range(124f,158f);
            }
            else if(reward == RewardType.twentyFivePoints)
            {
                randomFinalAngle = Random.Range(81f, 118f);
            }
            else if(reward == RewardType.fiftyPoints)
            {
                randomFinalAngle = Random.Range(42f, 76f);
            }
            else if(reward == RewardType.oneThousandPoints)
            {
                randomFinalAngle = Random.Range(3f, 37f);
            }
            else if(reward == RewardType.zonk)
            {
                randomFinalAngle = Random.Range(325f, 357f);
            }
            else if(reward == RewardType.fiveHundredPoints)
            {
                randomFinalAngle = Random.Range(285f, 320f);
            }
            else if(reward == RewardType.fivePoints)
            {
                randomFinalAngle = Random.Range(243f, 278f);
            }
            else if(reward == RewardType.oneHundredPoints)
            {
                randomFinalAngle = Random.Range(203f, 238f);
            }
            else if(reward == RewardType.tenPoints)
            {
                randomFinalAngle = Random.Range(164f, 197f);
            }
            #endregion
            //_sectorsAngles = new float[] { 351,32 };
            //randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];

            int fullCircles = 5; // spins number of times before stopping

            // Here we set up how many circles our wheel should rotate before stop
            _finalAngle = -(fullCircles * 360 + randomFinalAngle);
            _isStarted = true;

            GameManager.instance.DecreaseSpin(1);
            spinText.text = ""+GameManager.instance.GetSpin();
            GameManager.instance.totalSpin += 1;

            LuckySpinBackButton.interactable = false;
            LuckySpinBuyButton.interactable = false;
        }
    }

    private void GiveReward()
    {
        // Here you can set up rewards for every sector of wheel
        switch ((int)reward)
        {
            case 0:
                GameManager.instance.AddSpin(2);
                break;
            case 1:
                GameManager.instance.AddPoints(25);
                GameManager.instance.totalPoints += 25;
                break;
            case 2:
                GameManager.instance.AddPoints(50);
                GameManager.instance.totalPoints += 50;
                break;
            case 3:
                GameManager.instance.AddPoints(1000);
                GameManager.instance.totalPoints += 1000;
                break;
            case 4: // zonk
                break;
            case 5:
                GameManager.instance.AddPoints(500);
                GameManager.instance.totalPoints += 500;
                break;
            case 6:
                GameManager.instance.AddPoints(5);
                GameManager.instance.totalPoints += 5;
                break;
            case 7:
                GameManager.instance.AddPoints(100);
                GameManager.instance.totalPoints += 100;
                break;
            default:
                GameManager.instance.AddPoints(10);
                GameManager.instance.totalPoints += 10;
                break;
        }
    }

    void Update()
    {
        spinText.text = "" + GameManager.instance.GetSpin();
        coinText.text = "Coin : " + GameManager.instance.GetCoin();
        pointText.text = "Point : " + GameManager.instance.GetPoints();
        switch ((int)reward)
        {
            case 0:
                rewardText.text = "Two Free Spins";
                break;
            case 1:
                rewardText.text = "25 Platelets";
                break;
            case 2:
                rewardText.text = "50 Platelets";
                break;
            case 3:
                rewardText.text = "1000 Platelets";
                break;
            case 4: // zonk
                rewardText.text = "Zonk";
                break;
            case 5:
                rewardText.text = "500 Platelets";
                break;
            case 6:
                rewardText.text = "5 Platelets";
                break;
            case 7:
                rewardText.text = "100 Platelets";
                break;
            default:
                rewardText.text = "10 Platelets";
                break;
        }
        MissionManager.instance.CheckMissionInGame(MissionManager.instance.missions);
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
            spinText.text = ""+GameManager.instance.GetSpin();
            coinText.text = "Plasmas : " + GameManager.instance.GetCoin();
            pointText.text = "Platelets : " + GameManager.instance.GetPoints();
            rewardWindow.SetActive(true);

            rewardBlocker.SetActive(true);

            LuckySpinBackButton.interactable = true;
            LuckySpinBuyButton.interactable = true;
        }

        // Calculate current position using linear interpolation
        float t = _currentLerpRotationTime / maxLerpRotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Spinner.transform.eulerAngles = new Vector3(0, 0, -angle);
    }

    public void BuySpin()
    {
        if(GameManager.instance.GetCoin() >= TurnCost)
        {
            GameManager.instance.AddSpin(1);
            GameManager.instance.DecreaseCoin(TurnCost);
            spinText.text = ""+GameManager.instance.GetSpin();
            coinText.text = "Plasmas : " + GameManager.instance.GetCoin();
        }
    }

    public void CloseRewardWindow()
    {
        LuckySpinBackButton.interactable = true;
        LuckySpinBuyButton.interactable = true;

        rewardWindow.SetActive(false);
        rewardBlocker.SetActive(false);
    }
}
