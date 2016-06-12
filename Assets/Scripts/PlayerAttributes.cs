using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

	public Slider healthBar;
	public int startingHealth;
	static public float health;
	//private bool toggle = true;
	private PlayerFlash flash;
	public StateManager stateManager;

	public AudioClip hurtWorm;
	public AudioClip hurtSpider;
	public AudioClip dying;

	private bool projTrigger = false;

	private bool deathAudioToggle = false;

	private AudioSource audioSrc;
	// Use this for initialization
	void Start () {
		deathAudioToggle = false;
		audioSrc = GetComponent<AudioSource> ();
		flash = gameObject.GetComponent<PlayerFlash>();
		health = startingHealth;
		healthBar.maxValue = startingHealth;
		healthBar.minValue = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0) 
		{
			KillManagerScript.AddToList();
			stateManager.Hit();
			if(!audioSrc.isPlaying)
			{
				if(!deathAudioToggle)
				{
					audioSrc.clip = dying;
					audioSrc.Play();
					deathAudioToggle = true;
				}
			}

			StarDeathScript.isDead = true;
		}	
	
		if (health <= 0) {
			health = 0;
		} else if (health >= startingHealth) {
			health = startingHealth;
		}
		healthBar.value = health;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Spider") {

			SpiderBehaviour spiderScript = other.GetComponent<SpiderBehaviour>();
				
			if(spiderScript.spiderTrigger && !spiderScript.isVulnerable)
			{
				spiderScript.spiderTrigger = false;
				PlayerFlash flash = gameObject.GetComponent<PlayerFlash>();
				flash.StartCoroutine("FlashPlayer");
				if(!audioSrc.isPlaying)
				{
					audioSrc.clip = hurtSpider;
					audioSrc.Play();
				}
				health -= 1;

			}


		} 
		else if (other.tag == "Projectile") 
		{
			if(!projTrigger)
			{
				PlayerFlash flash = gameObject.GetComponent<PlayerFlash>();
				flash.StartCoroutine("FlashPlayer");
				health -= 1;	
				if(!audioSrc.isPlaying)
				{
					audioSrc.clip = hurtWorm;
					audioSrc.Play();
				}
				projTrigger = true;
			}
			
		}

	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Snail")
		{
			EnemySnailEvents snailScript= other.gameObject.GetComponent<EnemySnailEvents>();
			snailScript.snailTrigger = 1;
		}
		if (other.tag == "Projectile") {
			projTrigger = false;
		}
	}




}
