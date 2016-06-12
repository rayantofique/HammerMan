using UnityEngine;
using System.Collections;

public class SpiderBehaviour : MonoBehaviour {

	public float speed = 3;
	public Transform spriteHit;

	[HideInInspector]
	public bool isVulnerable = false;
	private Animator anim;
	private int direction = 1;
	public bool spiderTrigger = true;
	private int levelLoaded;

	public GameObject player;

	public enum State
	{
		Idle,
		Walking,
		Vulnerable,
		Dead
	}

	private State state = State.Walking;


	// Use this for initialization
	IEnumerator Start () {

		levelLoaded = Application.loadedLevel;
		anim = GetComponent<Animator> ();

		while (true) {

			switch(state)
			{
			case State.Walking:
				Walking();
				break;
			case State.Vulnerable:
				VulnerableState();
				break;
			case State.Dead:
				//code for dead
				break;

			}

			yield return null;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Walking()
	{
	
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime * direction;
		transform.position = pos;
		Vector3 angles;

		if (transform.position.x > 10.3 || transform.position.x < -10.3)
		{
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
		else if(!isVulnerable)
		{
			gameObject.GetComponent<BoxCollider>().enabled = true;
		}

		if (transform.position.x > 14) {

			direction = -1;
			angles = new Vector3(0, 330,0);	
			transform.eulerAngles = angles;
			spriteHit.transform.localPosition = new Vector3(-0.68f, 1.81f, -0.08f);	
			spriteHit.transform.localEulerAngles = new Vector3(0, 50, 0);
		

		}
		else if(transform.position.x < -14)
		{
			direction = 1;
			angles = new Vector3(0, 210,0);	
			transform.eulerAngles = angles;
			spriteHit.transform.localPosition = new Vector3(-0.95f, 1.86f, 2.75f);
			spriteHit.transform.localEulerAngles = new Vector3(0, 150, 0);


		}



	}

	void VulnerableState()
	{
		anim.SetBool ("isVulnerable", true);
		isVulnerable = true;
	}

	void OnTriggerEnter(Collider col)
	{

		if (col.tag == "Trap" || col.tag == "Star") 
		{
			state = State.Vulnerable;
			if(levelLoaded == 1)
			{
				player.GetComponent<PlayerEventsTutorial>().SpiderHitRecieved();
			}
			StartCoroutine("WakeUp");

		}
	}

	IEnumerator WakeUp()
	{
		yield return new WaitForSeconds(2);
		anim.SetBool ("isVulnerable", false);
		if (levelLoaded != 1) {
			isVulnerable = false;
			state = State.Walking;
		}


	}

	void OnTriggerExit()
	{
		spiderTrigger = true;
	}

}
