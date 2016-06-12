using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerEventsTutorial : MonoBehaviour {


	public GameObject[] startingObj;
	public GameObject[] moveLeft;
	public GameObject[] moveRight;
	public GameObject[] balanceObj;

	public GameObject[] hitHammerObj;
	public GameObject[] hitSnailObj;

	public GameObject[] jumpObj;
	public GameObject downArrow;

	public GameObject jumpLimiter;
	private int jumpCount;

	public GameObject trap;
	public SpriteRenderer alert;

	private bool toggleTrap = false;

	public Text tutText;
	public Text timerText;

	public AudioClip tutorialProgress;


	public GameObject endButton;

	private bool toggle = false;
	private bool timerComplete = false;

	public GameObject spider;

	public enum TutorialState
	{
		Initialize,
		MoveLeft,
		MoveRight,
		Balance,
		HitHammer,
		Jump,
		Duck,
		DodgeTrap,
		EnemyHit,
		EnemyHitFinal,
		SpiderHit,
		SpiderDown,
		EndTut
	}

	private bool soundBool = false;
	private bool isRunning = false;
	private TutorialState state;
	private bool duckRecieved = false;


	// Use this for initialization
	IEnumerator Start () {


		state = TutorialState.Initialize;

		while (true) {

			switch(state)
			{
			case TutorialState.Initialize:
				//
				break;
			case TutorialState.MoveLeft:
				MoveLeftStuff();
				break;
			case TutorialState.MoveRight:
				MoveRightStuff();
				break;	
			case TutorialState.Balance:
				Balance();
				break;
			case TutorialState.HitHammer:
				HitHammer();
				break;
			case TutorialState.EnemyHit:
				EnemyHit();
				break;
			case TutorialState.Jump:
				JumpTut();
				break;
			case TutorialState.Duck:
				Duck();
				break;
			case TutorialState.DodgeTrap:
				if(!toggleTrap)
				{
					StartCoroutine ("DodgeTrap");
					toggleTrap = true;
				}
				break;
			case TutorialState.SpiderHit:
				SpiderHit();
				break;
			case TutorialState.SpiderDown:
				SpiderDown();
				break;			
			case TutorialState.EndTut:
				EndTut();
				break;
			}
			yield return null;
		}
	
	}
	
	// Update is called once per frame


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Tut" && state == TutorialState.MoveLeft) {

			foreach (GameObject obj in moveLeft) {
				obj.SetActive (false);
			}
			AudioSource.PlayClipAtPoint (tutorialProgress, transform.position);
			state = TutorialState.MoveRight;

		} else if (other.tag == "Tut" && state == TutorialState.MoveRight) {
			foreach (GameObject obj in moveRight) {
				obj.SetActive (false);
			}
			AudioSource.PlayClipAtPoint (tutorialProgress, transform.position);
			state = TutorialState.Balance;

		} 

		else if (other.tag == "TimerTut" && state == TutorialState.Balance) 
		{
			if(!timerText.enabled)
			{
				timerText.enabled = true;
			}
			toggle = true;
		}

	}

	IEnumerator timer()
	{
		isRunning = true;
		
		timerText.text = "2";
		
		yield return StartCoroutine (WaitForRealSeconds(0.7f));
		
		timerText.text = "1";
		
		yield return StartCoroutine (WaitForRealSeconds(0.65f));
		
		timerText.text = "0";
		
		yield return StartCoroutine (WaitForRealSeconds(0.6f));
		
		Time.timeScale = 1;	
		timerComplete = true;
		isRunning = false;
		StopCoroutine ("timer");
	}


	private void Balance()
	{
		tutText.text = "Use the controls to stay in the box for three seconds";
		foreach(GameObject obj in balanceObj)
		{
			obj.SetActive(true);
		}
		if (toggle && !timerComplete) {
			if (!isRunning) {
				StartCoroutine ("timer");
			}
		}
		if (timerComplete) 
		{
			foreach (GameObject obj in balanceObj) 
			{
				obj.SetActive (false); 
			}
			state = TutorialState.HitHammer;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if ((other.tag == "TimerTut") && state == TutorialState.Balance) 
		{
			toggle= false;

			timerText.text = "3";
			timerText.enabled = false;
			isRunning = false;
			StopCoroutine("timer");

		}
		if (other.tag == "JumpCol" && state == TutorialState.Jump) {
			jumpCount += 1;
		}
	}



	public void Initialize()
	{
		foreach (GameObject obj in startingObj) {
			obj.SetActive(false);
				}


		state = TutorialState.MoveLeft;
	}

	
	private void MoveLeftStuff()
	{
		if (!soundBool) {
			AudioSource.PlayClipAtPoint (tutorialProgress, transform.position);
			soundBool = true;
		}
		tutText.text = "Tap in the box to run left";
		foreach(GameObject obj in moveLeft)
		{
			obj.SetActive(true);
		}
		//limiters[1].transform.position;
	}

	private void MoveRightStuff()
	{

		tutText.text = "Now tap in the box to run Right";
		foreach(GameObject obj in moveRight)
		{
			obj.SetActive(true);
		}
		//limiters[1].transform.position;
	}



	private void HitHammer()
	{
		tutText.text = "Tap in the Action Box to hit";
		foreach (GameObject obj in hitHammerObj) {
			obj.SetActive (true); 
		}
	}

	public void HitSent()
	{
		if (state == TutorialState.HitHammer) {
			state = TutorialState.EnemyHit;

		}
	}

	private void EnemyHit()
	{
		tutText.text = "Now move towards the snail and hit it";
		foreach (GameObject obj in hitSnailObj) {
			obj.SetActive (true); 
		}

	}

	public IEnumerator HitTrue()
	{
		AudioSource.PlayClipAtPoint (tutorialProgress, transform.position);

		yield return new WaitForSeconds (2);
		state = TutorialState.Jump;
	}

	private void JumpTut()
	{
		foreach (GameObject obj in jumpObj) {
			obj.SetActive (true); 
		}	
		tutText.text = "Swipe up in the box to jump.";
		jumpLimiter.SetActive (false);

		if (jumpCount >= 1 && jumpCount <=3) {
			tutText.text = "Try a few more jumps while moving";
			foreach(GameObject obj in moveLeft)
			{
				obj.SetActive(true);
			}
			foreach(GameObject obj in moveRight)
			{
				obj.SetActive(true);
			}
		}
		if (jumpCount > 3) {
			AudioSource.PlayClipAtPoint (tutorialProgress, transform.position);

			state = TutorialState.Duck;
		}

	}


	/*IEnumerator MoveFingersUp()
	{
		Vector3 newPos = moveRight [1].transform.position;
		Vector3 target = newPos;
		target.y = 3;
		while (newPos.y < 3) 
		{	

			newPos = Vector3.MoveTowards(newPos, target , 3 * Time.deltaTime);
			moveRight[1].transform.position = newPos;
			yield return null;
		}


		

	}*/

	
	/*IEnumerator MoveFingersDown()
	{

		foreach (GameObject obj in moveLeftDown) {
			obj.SetActive (true);
		}
		moveLeft [0].SetActive (true);
		
		Vector3 newPos = moveLeftDown [0].transform.position;
		Vector3 target = newPos;
		target.y = 0;
		while (newPos.y > 0) 
		{
			newPos = Vector3.MoveTowards(newPos, target , 3 * Time.deltaTime);
			moveLeftDown[0].transform.position = newPos;
			yield return null;
		}
		
		
	}*/
	private void Duck()
	{
		tutText.text = "Now swipe down in the box to roll while moving.";
		foreach (GameObject obj in jumpObj) {
			obj.SetActive (false); 
		}

		downArrow.SetActive (true);
		if (duckRecieved == true) {
			AudioSource.PlayClipAtPoint (tutorialProgress, transform.position);
			StartCoroutine("DuckEnd");
		}
	}
	public void DuckSent()
	{
		if (state == TutorialState.Duck) {
			duckRecieved = true;
		}
	}
		
	IEnumerator DuckEnd()
	{			
		yield return null;
		state = TutorialState.DodgeTrap;
	}

	IEnumerator DodgeTrap()
	{
		tutText.text = "Roll to dodge the trap";
		alert.enabled = true;
		yield return new WaitForSeconds (2);
		trap.SetActive (true);

	}

	public void TrapDodged()
	{
		if (state == TutorialState.DodgeTrap) {

			AudioSource.PlayClipAtPoint (tutorialProgress, transform.position);
			foreach (GameObject obj in hitHammerObj) {
				obj.SetActive (true); 
			}

			StartCoroutine("StartSpider");
			StartCoroutine("endTrap");
		}
	}

	IEnumerator endTrap()
	{
		yield return new WaitForSeconds (0.5f);
		if (trap.transform.eulerAngles.z >= 270) {
			trap.SetActive (false);
			alert.enabled = false;
		}
		StopCoroutine ("endTrap");
	}

	IEnumerator StartSpider()
	{
		tutText.text = "Good job!";
		yield return new WaitForSeconds (2);
		alert.enabled = true;
		trap.SetActive (true);
		state= TutorialState.SpiderHit;
	}

	private void SpiderHit()
	{
		downArrow.SetActive (false);
		foreach (GameObject obj in hitHammerObj) {
			obj.SetActive (false); 
		}
		spider.SetActive (true);
		tutText.text = "To defeat the spider, you must wait for the trap to hit it first.";	
		trap.SetActive (true);
	
	}

	public void SpiderHitRecieved()
	{
		state = TutorialState.SpiderDown;
	}

	public void SpiderDown()
	{
		tutText.text = "Hit now!";
		if (trap.transform.eulerAngles.z >= 270) {
			trap.SetActive (false);
		}

	}
	IEnumerator SpiderKilled()
	{
		AudioSource.PlayClipAtPoint (tutorialProgress, transform.position);
		foreach(GameObject obj in moveLeft)
		{
			obj.SetActive(false);
		}
		foreach(GameObject obj in moveRight)
		{
			obj.SetActive(false);
		}
		tutText.text = "Good Job! Always let the trap hit the spider first.";			
		yield return new WaitForSeconds (2);
		state = TutorialState.EndTut;
		if (trap.transform.eulerAngles.z >= 270) {
			trap.SetActive (false);
		}

	}


	public void EndTut()
	{

		tutText.text = "Success! Tap to exit tutorial.";
		StartCoroutine ("EndThis");
		alert.enabled = false;

	

	}

	public void StartNextLevel()
	{
		Application.LoadLevel (0);
	}

	IEnumerator EndThis()
	{
		yield return new WaitForSeconds (0.3f);
		endButton.SetActive (true);
	}

	/*private void EnemyHitTutorial()
	{
		tutText.text = "Run towards the enemy snail";

		moveLeft [0].SetActive (false);
		moveRight [0].SetActive (false);
		foreach (GameObject obj in moveRightDown) {
			obj.SetActive (false);
		}
		foreach (GameObject obj in moveLeftDown) {
			obj.SetActive (false);
		}
		if (snail) {
			snail.SetActive (true);
		}
	}*/

	/*private void EnemyHitFinal()
	{
		tutText.text = "Tap anywhere to hit the snail";
		state = TutorialState.EndTut;
	}
	private void EndTut()
	{
		if (!routineToggle) {
			StartCoroutine ("StartNextLevel");
			routineToggle = true;
		}
	}*/



	IEnumerator WaitForRealSeconds (float waitTime) {
		float endTime = Time.realtimeSinceStartup+waitTime;
		
		while (Time.realtimeSinceStartup < endTime) {
			yield return null;
		}
	}
}
