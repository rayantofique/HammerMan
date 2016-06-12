using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	private Animator anim;

	public void Shoot()
	{
		CentipedeBehaviour centiScript = GetComponentInParent<CentipedeBehaviour> ();
		centiScript.ExecuteShoot ();
	}

	public void BoolTrig()
	{
		anim = GetComponent<Animator> ();
		anim.SetBool ("isDead", false);
	}

}
