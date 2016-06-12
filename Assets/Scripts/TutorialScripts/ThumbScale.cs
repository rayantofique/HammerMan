using UnityEngine;
using System.Collections;

public class ThumbScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 scale = gameObject.transform.localScale;
		scale.x = Mathf.Abs ((Mathf.Sin (Time.time * 3f) * 1/4) + 1f);
		scale.y = Mathf.Abs ((Mathf.Sin (Time.time * 3f) * 1/4) + 1f);
		transform.localScale = scale;



	
	}
}
