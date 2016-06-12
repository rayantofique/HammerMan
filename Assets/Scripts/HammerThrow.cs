using UnityEngine;
using System.Collections;

public class HammerThrow : MonoBehaviour {
	
	public float rotationSpeed;
	private float step = 0;
	private float angle = 0;
	public float throwingSpeed;



	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.position;
		pos.x += step * Time.deltaTime;
		if (step != 0) 
		{
			step +=0.12f;
		}
		transform.position = pos;



			step = -throwingSpeed;
			angle = 361;


		StarRotate (angle);

	}

	public void StarRotate(float angle)
	{
		Vector3 angles = transform.eulerAngles;
		float newAngle = angles.z + angle;
		angles.z = Mathf.MoveTowards(angles.z, newAngle, rotationSpeed * Time.deltaTime);
		transform.eulerAngles = angles;
		if (angles.z > angle) 
		{
			angles.z = 0;
			transform.eulerAngles = angles;
		}
		
	}

}
