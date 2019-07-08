using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengesUI : MonoBehaviour
{
    public GameObject MissionsMenu;
    public GameObject AchievementsMenu;

    // Start is called before the first frame update
    void Start()
    {
        MissionsMenu.SetActive(false);
        AchievementsMenu.SetActive(true);
        MissionManager.instance.challengeState = MissionManager.ChallengeState.Achievements;
    }

    public void ShowAchievement()
    {
        AchievementsMenu.SetActive(true);
        MissionsMenu.SetActive(false);
        MissionManager.instance.challengeState = MissionManager.ChallengeState.Achievements;
    }

    public void ShowMission()
    {
        MissionsMenu.SetActive(true);
        AchievementsMenu.SetActive(false);
        MissionManager.instance.challengeState = MissionManager.ChallengeState.Missions;
    }
}
