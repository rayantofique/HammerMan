using UnityEngine;
using System.Collections;

public class CentipedeBehaviour : MonoBehaviour {

	public float minDistance;
	private GameObject player;
	public float LimitLeft;
	public float LimitRight;
	public float runningSpeed;

	public Vector3 positionLeft;	
	public Vector3 positionRight;
	
	private Animator anim;
	public GameObject child;

	private Vector3 burrowPos;
	private string dir;



	public Transform instantiationPoint;
	public GameObject projectile;
	public float projectileSpeed;

	public Transform hitSprite;
	private Vector3 startingPosSprite;
	//public int health = 2;

	public ParticleSystem burrowEffect;

	private GameObject manager;


	[HideInInspector]
	public bool isRunning = false;

	//private bool toggle = true;
	//private CharacterController character;
	//enemies drop from above


	public enum State
	{
		Idle,
		Burrow,										
		Shooting
	}

	private State state;
	
	// Use this for initialization
	IEnumerator Start () {
		manager = GameObject.Find ("EnemyManager");
		burrowEffect.Stop ();
		dir = "right";
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = child.GetComponent<Animator> ();
		state = State.Idle;

		while (true) 
		{
			switch(state)
			{
			case State.Idle:
				Initialize();
				break;
			case State.Burrow:
				Burrow ();
				break;
			case State.Shooting:
				Shooting();
				break;
			}
			yield return null;
		}
	}
	
	// Update is called once per frame

	private void Initialize()
	{
		if (Mathf.Abs(distanceBetween ()) > minDistance) {				 //if distance between player and enemy greater than preset, switch to Shooting

			gameObject.GetComponent<BoxCollider> ().enabled = true;
			state = State.Shooting;


		} 
		else if (Mathf.Abs(distanceBetween()) < minDistance && MovementScript.isGround) 
		{
			//gameObject.GetComponent<BoxCollider>().enabled = false;
			state = State.Burrow;									//else call function to determine new position and change state to running from inside function
		}
	}
	
	private void Shooting()
	{

		if (Mathf.Abs(distanceBetween ()) < minDistance)				// if distance less than preset, switch state to Idle to determine next state
			state = State.Idle;
	} 	

	private float distanceBetween()						//return horizontal distance between player and enemy
	{
		float distance = (player.transform.position.x) - transform.position.x;				
		return Mathf.Abs(distance);
	}


	private Vector3 newPositionX(float minDistance)				//determines new positon and changes state to burrow
	{
		Vector3 newPos= new Vector3(0,0,0);
		Vector3 position = transform.position;
		if(position.x == positionLeft.x)
		{	

			newPos = positionRight;

			dir = "left";	
		}
		else if(position.x == positionRight.x)
		{

			newPos = positionLeft;
			dir = "right";
		}
		return newPos;
	}


	public void Burrow()
	{
		gameObject.GetComponent<BoxCollider> ().enabled = false;
		burrowPos = newPositionX (minDistance);
		anim.SetBool ("inRange", true);
		burrowEffect.Play ();
	}
	public void ExecuteBurrow()
	{
		//clear particle system
		burrowEffect.Clear ();


		transform.position = burrowPos;
		if (dir == "right") {
			transform.localEulerAngles = new Vector3 (0, 0, 0);
			hitSprite.eulerAngles = new Vector3(0,0,0);
			hitSprite.localPosition = new Vector3(-0.15f, 1.58f, -0.7f);
			instantiationPoint.localPosition = new Vector3(0.16f, 3.02f, 1.4f);


		} 
		else if (dir == "left") {
			transform.localEulerAngles = new Vector3 (0, 125, 0);
			hitSprite.eulerAngles = new Vector3(0,0,0);
			hitSprite.localPosition = new Vector3(0.5f, 1.58f, 0.3f);	
			instantiationPoint.localPosition = new Vector3(-1.2f, 3.02f, -0.7f);




		}

		state = State.Idle;
		anim.SetBool ("inRange", false);

	}

	public void ExecuteShoot()
	{
		GameObject _projectile = Instantiate(projectile, instantiationPoint.transform.position, Quaternion.identity) as GameObject;
		Vector3 projectileSpeed = new Vector3 ((distanceBetween () / 0.7f), 0, 0);
	
		if (dir == "right") {
			_projectile.GetComponent<Rigidbody> ().velocity = projectileSpeed;
		} else if (dir == "left") 
		{
			_projectile.GetComponent<Rigidbody> ().velocity = projectileSpeed * -1;
		}
	}

	public IEnumerator DeathTime()
	{
		isRunning = true;
		manager.GetComponent<EnemyManagerScript>().StartCoroutine ("WormInstantiation");
		yield return new WaitForSeconds(1);
		Destroy (this.gameObject);
	}


}
