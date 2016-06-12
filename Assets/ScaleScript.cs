using UnityEngine;
using System.Collections;

public class ScaleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 scale = gameObject.transform.localScale;

		scale.x = Mathf.Abs (7 *(Mathf.Sin (Time.time * 5f)) + 39);
		scale.y = Mathf.Abs (2 *(Mathf.Sin (Time.time * 5f)) + 15);
		
		
		transform.localScale = scale;

	
	}
}
