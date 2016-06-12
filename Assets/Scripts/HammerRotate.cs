using UnityEngine;
using System.Collections;

public class HammerRotate : MonoBehaviour {

	public float rotationSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		HammerRotateFunc (361);
	
	}

	public void HammerRotateFunc(float angle)
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
		
	}
}
