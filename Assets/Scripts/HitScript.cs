using UnityEngine;
using System.Collections;


public class HitScript : MonoBehaviour {



	[HideInInspector]
	public bool scoreTrigger;
	[HideInInspector]
	public int health;
	[HideInInspector]
	public int enemyId;
	private KillManagerScript killManager;


	private int isVibOn;
	private SpriteRenderer flashSprite;
	public float flashTime = 0.1f;




	public virtual void Start()
	{
		isVibOn = PlayerPrefs.GetInt ("toggleVib", 1);
		flashSprite = GetComponentInChildren<SpriteRenderer> ();
		//originalMaterial = gameObject.GetComponentInChildren<Texture>();
		//print (originalMaterial.name);

			killManager = GameObject.Find ("KillManager").GetComponent<KillManagerScript>();


	}

	void OnTriggerEnter(Collider col)
	{
		if (enemyId == 0 || enemyId == 1) {

			if (col.gameObject.tag == "Hammer" && MovementScript.isHitting == true) 
			{
				ScoreCheck();
				if (enemyId == 1 && !(this.gameObject.GetComponent<CentipedeBehaviour>().isRunning)) 
				{
					this.gameObject.GetComponent<CentipedeBehaviour>().StartCoroutine("DeathTime");

				}
			}
		}

		if (enemyId == 2) {
				
			if(col.gameObject.tag == "Hammer" && MovementScript.isHitting == true)
			{

				if(this.gameObject.GetComponent<SpiderBehaviour>().isVulnerable)
				{

					ScoreCheck();
				}
			}
		}
	}

	void ScoreCheck()
	{
		if(scoreTrigger)
		{
			StartCoroutine("FlashWhite");

			if(isVibOn == 1)
			{

				Vibration.Vibrate(10);
			}
			health -= 1;
			scoreTrigger = false;
			killManager.hitRegistered (this.transform);
		}

	}
	IEnumerator FlashWhite(){

		flashSprite.enabled = true;
		yield return new WaitForSeconds(flashTime);
		flashSprite.enabled = false;
	}




}
