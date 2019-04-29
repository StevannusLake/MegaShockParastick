using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdController : MonoBehaviour
{
    public static AdController Instance;

    private string store_Id = "3126637";

    private string videoAd_Id = "video";
    private string rewardedVideoAd_Id = "rewardedVideo";
    private string bannerAd_Id = "BannerAd";

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Monetization.Initialize(store_Id, true);   
    }

    // Update is called once per frame
    void Update()
    {
        //! TESTING ADS
        if(Input.GetKeyDown(KeyCode.E))
        {
            //! If video ad is ready to be played
            if(Monetization.IsReady(videoAd_Id))
            {
                ShowAdPlacementContent ad = null;
                ad = Monetization.GetPlacementContent(videoAd_Id) as ShowAdPlacementContent;

                if(ad != null)
                {
                    ad.Show();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //! If rewarded video ad is ready to be played
            if (Monetization.IsReady(rewardedVideoAd_Id))
            {
                ShowAdPlacementContent ad = null;
                ad = Monetization.GetPlacementContent(rewardedVideoAd_Id) as ShowAdPlacementContent;

                if (ad != null)
                {
                    ad.Show();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //! If banner ad is ready to be played
            if (Monetization.IsReady(bannerAd_Id))
            {
                ShowAdPlacementContent ad = null;
                ad = Monetization.GetPlacementContent(bannerAd_Id) as ShowAdPlacementContent;

                if (ad != null)
                {
                    ad.Show();
                }
            }
        }

    }
    
    public void ShowVideoAd()
    {
        //! If video ad is ready to be played
        if (Monetization.IsReady(videoAd_Id))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(videoAd_Id) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show();
            }
        }
    }

    public void ShowRewardedVideoAd()
    {
        //! If rewarded video ad is ready to be played
        if (Monetization.IsReady(rewardedVideoAd_Id))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(rewardedVideoAd_Id) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show();
            }
        }
    }

    public void ShowBannerAd()
    {
        //! If banner ad is ready to be played
        if (Monetization.IsReady(bannerAd_Id))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(bannerAd_Id) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show();
            }
        }
    }
}
