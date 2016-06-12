using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySnailEvents : HitScript {
	
	public float pauseTime = 0.5f;
	private Animator anim;
	private Rigidbody rigBody;

	public float forceIntensity;
	public float forceUp;
	public bool isDead = false;	

	public int snailTrigger = 1;


	// Use this for initialization
	public override void Start () {

		base.Start ();

		enemyId = 0;


		//rigBody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0)  
		{
			isDead = true;
			if(Application.loadedLevel == 1)
			{
				anim = GetComponent<Animator>();
				anim.SetBool("isDead", true);
			}
			else
			{
				anim.SetBool("isDead", true);
				isDead = true;
				OnEnableEnemy();
			}

		           
		}

		if (transform.position.x > 17) {
			OnEnableEnemy();
		}

		
	}

	public void OnEnable()
	{
		anim = GetComponent<Animator> ();
		anim.SetBool ("isDead", false);
		scoreTrigger = true;
		health = 1;
		snailTrigger = 1;
		isDead = false;
		//anim.SetTrigger("isBelow");
	}
	

	IEnumerator HitPause(float p)
	{
		Time.timeScale = 0;
		float pauseEndTime = Time.realtimeSinceStartup + p;
		while (Time.realtimeSinceStartup < pauseEndTime) 
		{
			yield return null;
		}
		Time.timeScale = 1;
	}

	IEnumerator Dead()
	{

		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);

	}

	public void OnEnableEnemy()
	{
		/*gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
		gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (-1, 1, 0), ForceMode.Impulse);*/

		Invoke ("Destroy", 2f);
	}
	
	void Destroy()
	{
		gameObject.SetActive (false);	
	}


}
