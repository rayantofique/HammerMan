using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashBar : MonoBehaviour {

	public float time = 1;
	public Image image;
	private float healthRatio;
	public int maxHealth = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//Color temp = gameObject.GetComponent<Image>().color;
		//ameObject.GetComponent<Image>().color = new Color(temp.r,temp.g,temp.b,Mathf.Abs(Mathf.Sin(Time.time * 2f)));
		Vector3 scale = gameObject.transform.localScale;

		if (PlayerAttributes.health > 0) {
			healthRatio = maxHealth / PlayerAttributes.health;
		}
		else if(PlayerAttributes.health <= 0)
		{
			healthRatio = 0;
		}
		scale.x = Mathf.Abs ((Mathf.Sin (Time.time * 3f * healthRatio) * 1/4) + .7f);
		scale.y = Mathf.Abs ((Mathf.Sin (Time.time * 3f * healthRatio) * 1/4) + .7f);


		transform.localScale = scale;
			
	}



}
