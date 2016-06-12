	using UnityEngine;
using System.Collections;

public class StarDeathScript : MonoBehaviour {
	
	private bool isHit = false;
	private Vector3 pos;
	public static bool isDead = false;
	public float pauseTime;
	public SpriteRenderer flashSprite;
	public float flashTime;
	public Camera mainCamera;
	public ScoreScript scoreScript;
	//static public bool Dead = false;
	public AudioClip starDeath;

	private int loadedLevel;



	// Use this for initialization
	void Start () {

		loadedLevel = Application.loadedLevel;
		Time.timeScale = 1;
		isDead = false;
		isHit = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isHit && !isDead) 
		{
			//Dead = true;
			Animator anim = GetComponent<Animator>();
			anim.SetBool("isHit",true);
			isDead = true;

		
		}

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Star" ) 
		{

			//stillPos = transform.position;
			if(!GetComponent<MovementScript>().isDucking)
			{
				if(loadedLevel == 2)
				{
					StartCoroutine("SleepDeath", pauseTime);
					mainCamera.GetComponent<ScreenShake>().CameraShake(0.05f, 0.05f);
					isHit = true;
					StartCoroutine("FlashWhite");
					StartCoroutine("Pause");
					GetComponent<AudioSource>().clip = starDeath;
					GetComponent<AudioSource>().Play();
				}

				if(loadedLevel == 1)
				{
					GetComponent<AudioSource>().clip = starDeath;
					GetComponent<AudioSource>().Play();
				}


			}
			if(loadedLevel == 1)
			{
				if(GetComponent<MovementScript>().isDucking)
				{
					SendMessage("TrapDodged");
				}
			}

			
		}
		if (col.tag == "Trap") 
		{
			mainCamera.GetComponent<ScreenShake>().CameraShake(0.05f, 0.05f);
			StartCoroutine("SleepDeath", pauseTime);
			isHit = true;
			StartCoroutine("FlashWhite");
			StartCoroutine("Pause");
			GetComponent<AudioSource>().clip = starDeath;
			GetComponent<AudioSource>().Play();
			//Time.timeScale = 0.2f;

		}
	}

	IEnumerator Pause()
	{
		yield return null;
		PlayerAttributes.health = 0;
	}


	void TimeResume()
	{
		Time.timeScale = 1;
	}

	IEnumerator SleepDeath(float p)
	{
		Time.timeScale = 0;
		float pauseEndTime = Time.realtimeSinceStartup + p;
		while (Time.realtimeSinceStartup < pauseEndTime) 
		{
			yield return null;
		}
		Time.timeScale = 0.2f;
	}

	IEnumerator FlashWhite(){
		
		flashSprite.enabled = true;
		float pauseEndTime = Time.realtimeSinceStartup + flashTime;
		while (Time.realtimeSinceStartup < pauseEndTime) 
		{
			yield return null;
		}
		flashSprite.enabled = false;
	}
}
