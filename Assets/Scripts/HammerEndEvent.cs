using UnityEngine;
using System.Collections;

public class HammerEndEvent : MonoBehaviour {


	Animator hammerAnim;
	private SpriteRenderer hitSphere;
	private bool enemyHit;
	public TrailRenderer trail;


	// Use this for initialization
	void Start () {

		hitSphere = GetComponentInChildren<SpriteRenderer> ();
		hammerAnim = this.GetComponent<Animator> ();
	
	}
	
	public void hitStatus()
	{
		MovementScript.isHitting = false;
		hammerAnim.ResetTrigger ("isHitting");
		//hammerAnim.ResetTrigger ("isHitting");

	}

	public void ExecuteSphereShow()
	{
		if (enemyHit) {
		
			hitSphere.enabled = true;	
			enemyHit = false;
		}

	}
	public void HideSphere()
	{
		hitSphere.enabled = false;
		enemyHit = false;
	}

	void OnTriggerEnter(Collider other)
	{
		enemyHit = true;

	}

	void EnableTrail()
	{
		trail.enabled = true;
	}

	void DisableTrail()
	{
		trail.enabled = false;
	}


}
