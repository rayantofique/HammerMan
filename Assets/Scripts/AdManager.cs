using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

	[SerializeField] string gameID = "58045";


	public GameObject scoreScript;

	// Use this for initialization
	void Awake () {
	
		Advertisement.Initialize (gameID, false);

	}
	
	// Update is called once per frame

	public void ShowAd()
	{

		ShowOptions options = new ShowOptions ();
		options.resultCallback = AdCallbackHandler;

		if (Advertisement.IsReady ("rewardedVideoZone")) 	
		{
			Advertisement.Show("rewardedVideoZone", options);
		}
	}

	void AdCallbackHandler(ShowResult result)
	{
		switch (result) 
		{
		case ShowResult.Finished:
			scoreScript.GetComponent<ScoreScript>().GiveCoins();
			break;
		case ShowResult.Failed:
			scoreScript.GetComponent<ScoreScript>().GiveCoins();
			break;
		}
	}

	IEnumerator WaitForAd()
	{
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0;
		yield return null;

		while(Advertisement.isShowing)
		{
			yield return null;
		}

		Time.timeScale = 1;
	}
}
