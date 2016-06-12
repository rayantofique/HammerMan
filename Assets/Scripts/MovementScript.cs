using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public static bool isHitting = false;
	public float speed;
	private CharacterController controller;

	public float rotationSpeed;
	public float leftPoint;
	public float rightPoint;
	public int jumpSpeed;
	public float gravity;
	public bool isDucking;

	private Vector3 fingerStart;
	private Vector3 fingerEnd;
	private Vector3 movementVector;
	public Transform hammer;
	//private int taps;
	//private int clicks;

	public AudioClip hammerSwingSound;
	public AudioClip jumpSound;
	public AudioClip duckSound;

	private Animator anim;
	private Animator hammerAnim;
	private bool toggle = false;
	private float startTime;
	private bool moved = false;
	private bool isJumping;
	private bool isDead;
	static public bool isGround;

	private int levelLoaded;
	private AudioSource audioSrc;


	// Use this for initialization
	void Start () {

		audioSrc = GetComponent<AudioSource> ();
		isHitting = false;
		anim = GetComponent<Animator> ();
		hammerAnim = hammer.GetComponent<Animator> ();
		isGround = true;
		levelLoaded = Application.loadedLevel;
		 
	}
	
	// Update is called once per frame
	void Update () {


		if (PlayerAttributes.health <= 0) 
		{
			anim.SetBool("isHealthZero", true);
		}

		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;

		//
		controller = GetComponent<CharacterController> ();
		isGround = controller.isGrounded;
		Vector3 vel = controller.velocity;
		movementVector.y -= gravity * Time.deltaTime;

		if (vel.x > 0.09 || vel.x < -0.09) {
			anim.SetBool ("isMoving", true);
			hammerAnim.SetBool ("isMoving", true);



		} else if (vel.x <= 0.03 && vel.x >= -0.03) {
			anim.SetBool("isMoving", false);
			hammerAnim.SetBool ("isMoving", false);
		}

		if (isDucking) 
		{

			if(!toggle)
			{
				startTime = Time.time;
				toggle = true;
			}
			if(Time.time - startTime > 0.7f)
			{
				isDucking = false;
				toggle = false;
				anim.SetBool("isDucking", false);
			}

		}
		anim.SetFloat("VelocityX", vel.x);

	

		//start a timer when ducking is true, after a second, reset timer and transition back to running, if jump occurs during ducking, transition to jump and reset timer

		Movement(movementVector);

		//if isJumping is true, then call a Jump function from another script

		int numTouches = Input.touchCount;
		if (Application.loadedLevel == 2) {
			isDead = StarDeathScript.isDead;
		} else 
		{
			anim.SetBool("isHealthZero", false);
			isDead = false;
		}



		if (numTouches > 0) 
		{
			for (int i = 0; i < numTouches; i++)
			{
				Touch touch = Input.GetTouch(i);

				if(touch.phase == TouchPhase.Began)
				{
					Vector3 touchPosition = Camera.main.ScreenToWorldPoint((Input.GetTouch(i).position));

					fingerStart = touchPosition;
				
				}
				if(touch.phase == TouchPhase.Stationary)
				{
					Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

					if(touchPosition.x < leftPoint && isGround && !isDead)
					{
						movementVector = new Vector3(-1, 0, 0);

					}
					else if ((touchPosition.x > leftPoint) && isGround && !isDead)
					{
						if(touchPosition.x < rightPoint)
						{
							movementVector = new Vector3(1, 0, 0);
						}

					}

					if(!isHitting && touchPosition.x > rightPoint)
					{

						isHitting = true;
						if(levelLoaded == 1)
						{

							Time.timeScale = 1;
							SendMessage("HitSent");
							hammerAnim.SetTrigger("isHitting");
						}
						AudioSource.PlayClipAtPoint(hammerSwingSound, transform.position);			
						hammerAnim.SetTrigger("isHitting");
						
					}
				}

			
				if(touch.phase == TouchPhase.Moved)
				{

					moved = true;

				}
				if(touch.phase == TouchPhase.Ended)
				{
					if(moved)
					{
						Vector3 fingerEnd = Camera.main.ScreenToWorldPoint((Input.GetTouch(i).position));
						float difference = fingerStart.y - fingerEnd.y;



						if(difference < -1f && isGround && !isDead && fingerEnd.x > rightPoint) 
						{
							if(!audioSrc.isPlaying)
							{
								audioSrc.clip = jumpSound;
								audioSrc.Play();
							}
							movementVector.y = jumpSpeed;
										
						}
						if(difference > 1f && vel.magnitude > 0.09f && isGround  && !isDead  && fingerEnd.x > rightPoint)
						{
							
							isDucking = true;
							anim.SetBool("isDucking", true);
							if(!audioSrc.isPlaying)
							{
								audioSrc.clip = duckSound;
								audioSrc.Play();
							}

							if(levelLoaded == 1)
							{
								SendMessage("DuckSent");
							}	
							
						}

						moved = false;
					}

				}

			}
		}

		/*
		if (Input.GetMouseButtonDown (0)) {
			if (!isHitting) {
				isHitting = true;

				if (levelLoaded == 1) {
					Time.timeScale = 1;
					hammerAnim.SetTrigger ("isHitting");
				}
				AudioSource.PlayClipAtPoint (hammerSwingSound, transform.position);			
				hammerAnim.SetTrigger ("isHitting");
				
			}


			//print (mousePos.x); 

			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (mousePos.x < leftPoint && isGround && !isDead) {
				movementVector = new Vector3 (-1, 0, 0);


			} else if (mousePos.x > leftPoint && isGround && !isDead) {
				if (mousePos.x < rightPoint) {
					movementVector = new Vector3 (1, 0, 0);
				}

			}




		}

			if (Input.GetKeyDown ("a") && controller && !isDead) {
				movementVector.y = jumpSpeed;
				if (!audioSrc.isPlaying) {
					audioSrc.clip = jumpSound;
					audioSrc.Play ();
				}
			} else if (Input.GetKeyDown ("c") && controller && !isDead) {
				anim.SetBool ("isDucking", true);
				isDucking = true;
				if (!audioSrc.isPlaying) {
					audioSrc.clip = duckSound;
					audioSrc.Play ();
				}
				if (levelLoaded == 1) {
					SendMessage ("DuckSent");
				}


			}*/
		



	}

	public void Movement(Vector3 movementVector)
	{
		controller.Move(movementVector * speed * Time.deltaTime);
	}

	public void HammerRotate(float angle)
	{
		Vector3 angles = hammer.transform.eulerAngles;
		float newAngle = angles.x + angle;
		angles.x = Mathf.MoveTowards(angles.x, newAngle, rotationSpeed * Time.deltaTime);
		hammer.transform.eulerAngles = angles;
		if (angles.x > angle) 
		{
			//clicks = 0;
			//taps = 0;
			angles.x = 0;
			hammer.transform.eulerAngles = angles;
		}

	}

	public void ResetVector()
	{
		controller.Move(new Vector3(-1,0,0) * 0 * Time.deltaTime);
	}



}
