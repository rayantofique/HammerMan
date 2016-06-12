using UnityEngine;
using System.Collections;

public class SpiderHitScript : HitScript {


	public GameObject spiderDeathModel;

	
	// Use this for initialization
	public override void Start () {

		base.Start ();
		health = 1;
		enemyId = 2;
		scoreTrigger = true;

	
	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0) {

			PlayDeath ();
		}

	
	}

	void PlayDeath()
	{
		this.gameObject.GetComponent<SpiderBehaviour> ().isVulnerable = true;
		this.gameObject.GetComponent<BoxCollider> ().enabled = false;
		spiderDeathModel.SetActive (true);
		spiderDeathModel.GetComponent<FlashHit> ().SpiderFlash (0.08f);
		this.gameObject.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;
	
		StartCoroutine ("DestroySpider");
	}

	IEnumerator DestroySpider()	
	{

		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}





}
