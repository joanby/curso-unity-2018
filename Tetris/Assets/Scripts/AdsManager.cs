using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{

    //---------- ONLY NECESSARY FOR ASSET PACKAGE INTEGRATION: ----------//
    #if UNITY_IOS
        private string gameId = "2688977";
    #elif UNITY_ANDROID
        private string gameId = "2688979";
    #endif
    //-------------------------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        Advertisement.Initialize(gameId,true);
        StartCoroutine("ShowAdsWhenReady");
	}

    IEnumerator ShowAdsWhenReady(){
        while (!Advertisement.isInitialized || !Advertisement.IsReady())
        {
            Debug.Log("Aún no estoy preparado...");
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Show();
    }

    public static void ShowAds()
    {
        if (Advertisement.IsReady()) { 
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show(/*"test",*/ options);
        }
    }



    static void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");

        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}