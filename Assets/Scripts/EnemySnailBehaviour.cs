using UnityEngine;
using System.Collections;

public class EnemySnailBehaviour : MonoBehaviour {


	public float speedLimitLow;
	public float speedLimitHigh;
	private float speed;

	// Use this for initialization

	void Start () {



	}
	
	// Update is called once per frame
	void Update () {		


		Vector3 pos = transform.position;
		if (pos.y <= -3.2f) {

			pos.y = -3.2f;
		}
		pos.x += speed * Time.deltaTime;
		transform.position = pos;


	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Ground") {
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;


		}
	}

	public void InitializeSnail()
	{
		speed = Random.Range (speedLimitLow, speedLimitHigh);
		gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
	}





}
