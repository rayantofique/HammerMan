using UnityEngine;
using System.Collections;

public class TurnOff : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
	
		Invoke ("Die", 0.65f);

	}
	
	// Update is called once per frame

	void Die()
	{
		gameObject.SetActive (false);
	}
}
