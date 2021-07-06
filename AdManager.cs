using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

#if UNITY_IOS

using Unity.Advertisement.IosSupport;

#endif

public class AdManager : MonoBehaviour
{
    string gameId = "0";
    bool testMode = false;
    bool adComplete = false;

    void Start()
    {
        #if UNITY_IOS
            StartCoroutine(adStart());
        #else
            Advertisement.Initialize (gameId, testMode);
        #endif
    }

    IEnumerator adStart()
    {
        #if UNITY_IOS
            if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(0.1f);
            Advertisement.Initialize (gameId, testMode);
        #else
            yield return new WaitForSeconds(1);
        #endif
    }

    public void callAd()
    {
        StartCoroutine(a());
    }

    public void callBannerAd()
    {
        StartCoroutine(b());
    }

    public bool checkIfAdComplete()
    {
        if (adComplete)
        {
            adComplete = false;
            return true;
        }
        else
            return false;
    }

    IEnumerator a()
    {
        int time = 0;
        bool i = true;
        while (i)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video");
                i = false;
            }
            else
            {
                yield return new WaitForSeconds(1);
                time++;
                if (time > 3)
                {
                    i = false;
                }
            }
        }
        adComplete = true;
    }

    IEnumerator b()
    {
        while (!Advertisement.IsReady())
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Show("bannerAd");
    }
}
