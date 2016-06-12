using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashHit : MonoBehaviour {


	public Material flashMaterial;
	public Material originalMaterial;
	//public List<GameObject> objects;
	private Transform[] objects;
	private Transform[] spiderObjects;
	private Material material;

	// Use this for initialization
	
	public void Flash(float flashTime)
	{
		StartCoroutine ("Wait", flashTime);
	}
	public void SpiderFlash(float flashTime)
	{
		StartCoroutine ("SpiderWait", flashTime);
	}

	//this script has been applied to all game object needed to flash -  the respective functions are called from KillManagerScript where this script is referenced through the object

	IEnumerator Wait(float flashSeconds)
	{
		objects = GetComponentsInChildren<Transform> ();
		foreach (Transform children in objects) {

			if((children.GetComponentInChildren<Renderer>() == true && children.name != "BurrowEffect"))
			{

					children.GetComponentInChildren<Renderer>().material = flashMaterial;


			}
			
		}
		yield return new WaitForSeconds(flashSeconds);
		foreach (Transform children in objects) {
			
			if((children.GetComponentInChildren<Renderer>() == true && children.name != "BurrowEffect"))
			{
				
					children.GetComponentInChildren<Renderer>().material = originalMaterial;
				
				
			}
			
		}
		StopCoroutine ("Wait");
	}

	IEnumerator SpiderWait(float flashTime)
	{
		yield return new WaitForSeconds (flashTime);
		spiderObjects = GetComponentsInChildren<Transform> ();
		foreach (Transform objects in spiderObjects) {

			if(objects.GetComponent<Renderer>())	
			{
				objects.GetComponent<Renderer>().material = originalMaterial;
			}

		}
	}
	
	void TimeResume()
	{
		Time.timeScale = 1;
	}

}

