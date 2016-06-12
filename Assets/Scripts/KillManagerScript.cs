	using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillManagerScript : MonoBehaviour {


	public float comboWindowTime = 3;
	public Sprite[] spriteNums;
	public SpriteRenderer comboCountX;
	public SpriteRenderer comboUnits;
	public SpriteRenderer comboTens;
	public SpriteRenderer comboHundreds;

	public Transform snailEmission;
	public Transform wormEmission;
	public Transform spiderEmission;

	public AudioClip snailHit;
	public AudioClip spiderHit;
	public AudioClip wormHit;
	

	public Camera mainCamera;

	public float flashTime;
	private float duration;
	[HideInInspector]
	static public List<Transform> killCount;
	static public List<int> comboList;
	private bool isStarted;
	private float timeStart;

	private int loadedLevel;

	public CoinManager coinManager;

	public Transform playerEvents;

	// Use this for initialization
	void Start () {
		killCount = new List<Transform> ();
		comboList = new List<int> ();
		comboList.Clear ();
		loadedLevel = Application.loadedLevel;
	}
	
	// Update is called once per frame
	void Update () {

		if (killCount.Count != 0 && !isStarted) 
		{
			isStarted = true;
			timeStart = Time.time;
		}
		if (isStarted) 
		{
			if((Time.time - timeStart) > comboWindowTime)
			{
				comboList.Add (killCount.Count);
				killCount.Clear();
				isStarted = false;
			}
		}

		int comboCount = killCount.Count;

		if (comboCount < 1) 
		{
			comboCountX.GetComponent<SpriteRenderer>().enabled = false;
			comboUnits.sprite = null;
			comboTens.sprite = null;
			comboHundreds.sprite = null;
		}
	
		if (comboCount > 1 && comboCount < 10) {


			comboCountX.GetComponent<SpriteRenderer> ().enabled = true;
			comboUnits.sprite = spriteNums [comboCount];
		} else if (comboCount >= 10 && comboCount < 100) {
			comboTens.enabled = true;
			comboTens.sprite = spriteNums [comboCount / 10];
			comboUnits.sprite = spriteNums [comboCount % 10];
		
		} else if (comboCount >= 100) 
		{
			comboHundreds.enabled = true;
			comboHundreds.sprite = spriteNums[comboCount/100];
			int rest = comboCount % 100;
			comboTens.sprite = spriteNums[rest / 10];
			comboUnits.sprite = spriteNums[rest % 10];
		}

	}

	public void hitRegistered(Transform obj)
	{
		float shakeAmount = 0;

		if (!StarDeathScript.isDead) {
			killCount.Add (obj);
			ComboScore ();
		}	

		timeStart = Time.time;

		if (obj.tag == "Snail") {
			if(snailHit)
			{
				AudioSource.PlayClipAtPoint(snailHit, obj.transform.position);
			}
			if(loadedLevel == 1)
			{
				playerEvents.GetComponent<PlayerEventsTutorial>().StartCoroutine("HitTrue");
			}
			Vector3 emissionPos = obj.transform.position;
			emissionPos.y += 1.5f;
			emissionPos.x -= 3.4f;
			Instantiate(snailEmission, emissionPos, transform.rotation);
			shakeAmount = 0.65f;
			duration = 0.14f;
			obj.GetComponent<FlashHit> ().Flash (flashTime);

		} else if (obj.tag == "Centipede") {
			if(wormHit)
			{
				AudioSource.PlayClipAtPoint(wormHit, obj.transform.position);
			}

			Vector3 emissionPos = obj.transform.position;
			emissionPos.y += 0.5f;
			//emissionPos.x -= 1f;
			Instantiate(wormEmission, emissionPos, transform.rotation);
			shakeAmount = 0.7f;	
			duration = 0.20f;
			obj.GetComponent<FlashHit> ().Flash (flashTime);
			coinManager.InstantiateCoin(obj.transform.position);

		} else if (obj.tag == "Spider") {
			if(spiderHit)
			{
				AudioSource.PlayClipAtPoint(spiderHit, obj.transform.position);
			}
			if(loadedLevel == 1)
			{
				playerEvents.GetComponent<PlayerEventsTutorial>().StartCoroutine("SpiderKilled");
			}

			Vector3 emissionPos = obj.transform.position;
			emissionPos.y += 1.5f;
			emissionPos.x -= 1f;
			Instantiate(spiderEmission, emissionPos, transform.rotation);
			shakeAmount = 0.75f;
			duration = 0.31f;
			obj.GetComponent<FlashHit>().SpiderFlash(flashTime);
			coinManager.InstantiateCoin(obj.transform.position);
		}

		mainCamera.GetComponent<ScreenShake> ().CameraShake (shakeAmount, duration);
		mainCamera.GetComponent<HitPause> ().ExecutePause ();
	}
		
	public void ComboScore()
	{
		ScoreScript.score += 2 * killCount.Count;
	}
	static public void AddToList()
	{
		comboList.Add (killCount.Count);

	}

	
}
