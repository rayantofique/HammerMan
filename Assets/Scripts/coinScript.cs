using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class coinScript : MonoBehaviour {

	public float rotationSpeed = 10;
	private float changeUp = 15f;
	private float changeDown = -10f;
	private GameObject scoreManager;
	private GameObject coinManager;
	public AudioClip coinPickupSound;
	private bool toggle = false;

	// Use this for initialization
	void Start () {


		scoreManager = GameObject.Find ("ScoreManager");
		coinManager = GameObject.Find ("CoinManager");
	}
	
	// Update is called once per frame
	void Update () {

		CoinRotate (361);
	
	}

	public void CoinRotate(float angle)
	{
		Vector3 angles = transform.eulerAngles;
		float newAngle = angles.y + angle;
		angles.y = Mathf.MoveTowards(angles.y, newAngle, rotationSpeed * Time.deltaTime);
		transform.eulerAngles = angles;
		if (angles.y > angle) 
		{

			angles.y = 0;
			transform.eulerAngles = angles;
		}

		if (transform.position.y <= -2.1f) {

			Vector3 pos = transform.position;
			pos.y = -2.1f;
			transform.position = pos;	
			toggle = true;

		}

		if (!toggle) {

			Vector3 position = transform.position;
			position.y -= changeDown * Time.deltaTime;
			transform.position = position;
		}

		
		
		
	}

	void OnEnable()
	{
		toggle = false;
	}


	IEnumerator MoveUp()
	{

		while (transform.position.y < 3) 
		{
			changeDown = 0;
			//move trap to position 1 
					//keeps time between traps constant
			Vector3 pos = transform.position;
			pos.y += changeUp * Time.deltaTime;
			transform.position = pos;
			yield return null;
			
		}
		changeDown = 20;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			AudioSource.PlayClipAtPoint(coinPickupSound, transform.position);
			gameObject.SetActive(false);
			coinManager.GetComponent<CoinManager>().InstantiateExplosion(transform.position);
			scoreManager.GetComponent<ScoreScript>().AddCoins();

		}
	}


}
