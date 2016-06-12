using UnityEngine;
using System.Collections;

public class CentipedeEvents : HitScript {

	private Animator anim;
	static int deathStateId;
	private bool toggle = false;
	public ParticleSystem burrow;



	// Use this for initialization
	public override void Start () {

		base.Start ();
		health = 1;
		enemyId = 1;
		scoreTrigger = true;
		anim = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(health <= 0)
		{
			burrow.enableEmission = false;
			anim.SetBool("isDead", true);

		}
			
	}

}


	

