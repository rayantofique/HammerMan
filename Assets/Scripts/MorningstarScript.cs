using UnityEngine;
using System.Collections;

public class MorningstarScript : MonoBehaviour {

	public float rotationSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		StarRotate (361);
	
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
