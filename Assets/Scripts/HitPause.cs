using UnityEngine;
using System.Collections;

public class HitPause : MonoBehaviour {

	//public float pauseIntensity;
	public float pauseDuration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExecutePause()
	{
		//StartCoroutine ("SleepGame");
		StartCoroutine ("Sleep", pauseDuration);
	}

	IEnumerator SleepGame()
	{
		//Time.timeScale = pauseIntensity;
		yield return new WaitForSeconds(pauseDuration);
		Time.timeScale = 1;	
		StopCoroutine ("SleepGame");
	}	

	IEnumerator Sleep(float p)
	{
		Time.timeScale = 0;
		float pauseEndTime = Time.realtimeSinceStartup + p;
		while (Time.realtimeSinceStartup < pauseEndTime) 
		{
			yield return null;
		}
		Time.timeScale = 1;
	}
}
