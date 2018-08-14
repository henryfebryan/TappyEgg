using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdScript : MonoBehaviour {
    private BannerView bannerView;
    // Use this for initialization
    public void Start () {
        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9478209802818241/8848624594";
#else
        string adUnitId = "unexpected_platform";
#endif
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    
}
