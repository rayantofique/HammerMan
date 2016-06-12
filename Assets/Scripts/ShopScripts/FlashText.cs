using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashText : MonoBehaviour {

	public float time = 1;
	public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Color temp = text.color;
		text.color = new Color(temp.r,temp.g,temp.b,Mathf.Abs(Mathf.Sin(Time.time * 2f)));
	
			
	}



}
