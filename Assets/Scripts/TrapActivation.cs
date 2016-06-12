	using UnityEngine;
using System.Collections;

public class TrapActivation : MonoBehaviour {

	private int randomNum;							//number which decides trap
	public float trapTime = 5;						//time between each trap	
	private float trapTimePerma;						


	public Transform trap1;
	public Vector3 trap1StartRot;					

	//waypoint positions for trap 2
	public float trapSpeed = 17;					//speed for trap2
	public Transform trap2;
	public Vector3 bottomPosition1;
	public Vector3 bottomPosition2;
	public Vector3 startPosition;

	private bool switchNum = false;
	private bool switchMorningStarSound = false;

	public AudioClip sawblade;
	public AudioClip morningStar;

	private bool toggle = false;

	//sprites for alerts
	public SpriteRenderer alert;
	public SpriteRenderer alert2;
	// Use this for initialization
	void Start () {

		trapTimePerma = trapTime;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		trapTime -= Time.deltaTime;						//trap time counts down
		if (trapTime <= 0)
		{
			if(!switchNum)								//get a random number once to initialize trap
			{
				randomNum = Random.Range(0, 2);		
				switchNum = true;
			}
			if(randomNum == 0)
			{
				alert2.enabled = true;					
				StartCoroutine("Trap2Start");			//start trap 2 coroutine
			}
			else 
			{
				alert.enabled = true;
				if(!toggle)
				{
					StartCoroutine("WaitTrap1");
					toggle = true;
				}//start trap 1 coroutine
			
			}
		}

	}

	IEnumerator Trap2Start ()
	{
		//yield return new WaitForSeconds (1);
		AudioSource.PlayClipAtPoint (sawblade, transform.position);
		while (trap2.transform.position != bottomPosition1) 
		{
			//move trap to position 1 
			trapTime = trapTimePerma;			//keeps time between traps constant
			trap2.transform.position = Vector3.MoveTowards(trap2.transform.position, bottomPosition1, trapSpeed *Time.deltaTime);
			yield return null;

		}

		yield return new WaitForSeconds (1);
		alert2.enabled = false;
		while (trap2.transform.position != bottomPosition2) 
		{
			//move trap to position 2
			trapTime = trapTimePerma;
			trap2.transform.position = Vector3.MoveTowards(trap2.transform.position, bottomPosition2, trapSpeed * Time.deltaTime);
			yield return null;
		}
		trap2.transform.position = startPosition;
		switchNum = false;				//switch variable to enable trap to be called again

	}
	IEnumerator WaitTrap1()
	{
		yield return new WaitForSeconds (1.5f);
		trap1.GetComponent<MorningstarScript>().enabled = true;

		StartCoroutine("Trap1");

	}
	IEnumerator Trap1()
	{
		while (trap1.transform.eulerAngles.z < 280) 
		{
			
			if(!switchMorningStarSound)
			{
				AudioSource.PlayClipAtPoint(morningStar, transform.position);
				switchMorningStarSound = true;
			}
			
			trapTime = trapTimePerma;
			yield return null;
		}

		alert.enabled = false;
		trap1.GetComponent<MorningstarScript>().enabled = false;
		switchNum = false;
		switchMorningStarSound = false;
		trap1.transform.eulerAngles = trap1StartRot;
		toggle = false;
	}


}
